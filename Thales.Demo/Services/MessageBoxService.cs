using System.Windows;

namespace Thales.Demo.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public void ShowMessageBox(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
        }
    }
}
