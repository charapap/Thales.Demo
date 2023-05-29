using System.Windows;
using Thales.Demo.Services;
using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly PersonsStore _personsStore;
        private readonly RolesStore _rolesStore;
        private readonly DataService _dataService;
        private readonly ConnectionService _connectionService;
        private readonly IMessageBoxService _messageBoxService;

        public App()
        {
            _modalNavigationStore = new ModalNavigationStore();
            _personsStore = new PersonsStore();
            _rolesStore = new RolesStore();
            _dataService = new DataService();
            _connectionService = new ConnectionService();
            _messageBoxService = new MessageBoxService();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_modalNavigationStore, 
                new PersonsViewModel(_personsStore, _modalNavigationStore, _connectionService, _dataService), 
                new RolesViewModel(_rolesStore, _modalNavigationStore, _connectionService, _dataService, _messageBoxService))
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
