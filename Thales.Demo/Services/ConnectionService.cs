using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thales.Demo.Models;

namespace Thales.Demo.Services
{
    public class ConnectionService
    {
        public event Action<DataPacket> DataReceived;

        public ConnectionService() 
        {
            Task.Run(() => Connect());
        }

        public void Connect()
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, Convert.ToInt32(ConfigurationManager.AppSettings["CurrentPort"]));
                listener.Start();

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();

                    Thread clientThread = new Thread(HandleClientConnection);
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void HandleClientConnection(object obj)
        {
            TcpClient client = (TcpClient)obj;
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] bytes = new byte[client.ReceiveBufferSize];
                int bytesRead = stream.Read(bytes, 0, client.ReceiveBufferSize);
                string receivedData = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                DataPacket dataPacket = JsonConvert.DeserializeObject<DataPacket>(receivedData);
                if (dataPacket.DataType == DataType.Person)
                {
                    dataPacket.Data = JsonConvert.DeserializeObject<Person>(dataPacket.Data.ToString());
                }
                else
                {
                    dataPacket.Data = JsonConvert.DeserializeObject<Role>(dataPacket.Data.ToString());
                }
                DataReceived?.Invoke(dataPacket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async void SendData(DataPacket dataPacket)
        {
            try
            {
                string ips = ConfigurationManager.AppSettings["ConnectionIps"];
                foreach (string ip in ips.Split(','))
                {
                    try
                    {
                        TcpClient client = new TcpClient();
                        await client.ConnectAsync(IPAddress.Parse(ip.Split(':')[0]), Convert.ToInt32(ip.Split(':')[1]));

                        byte[] bytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket));
                        NetworkStream stream = client.GetStream();
                        stream.Write(bytes, 0, bytes.Length);
                        client.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
