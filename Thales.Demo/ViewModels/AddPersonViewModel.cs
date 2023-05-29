using System.Windows.Input;
using Thales.Demo.Commands;
using Thales.Demo.Stores;

namespace Thales.Demo.ViewModels
{
    public class AddPersonViewModel : ViewModelBase
    {
        public PersonFormViewModel PersonFormViewModel { get; }

        public AddPersonViewModel(PersonsStore personsStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand submitCommand = new AddPersonCommand(this, personsStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            PersonFormViewModel = new PersonFormViewModel(submitCommand, cancelCommand);
        }
    }
}
