namespace Thales.Demo.Models
{
    public enum DataType
    {
        Person,
        Role
    }

    public enum ActionType
    {
        Add,
        Edit,
        Delete
    }

    public class DataPacket
    {
        public object Data {  get; set; }
        public DataType DataType { get; set; }
        public ActionType ActionType { get; set;}
    }
}
