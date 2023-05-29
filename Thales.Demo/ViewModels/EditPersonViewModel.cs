using System;
using System.Windows.Input;
using Thales.Demo.Commands;
using Thales.Demo.Models;
using Thales.Demo.Stores;

namespace Thales.Demo.ViewModels
{
    public class EditPersonViewModel : ViewModelBase
    {
        public Guid PersonId { get; } 
        public PersonFormViewModel PersonFormViewModel { get; }

        public EditPersonViewModel(Person person, PersonsStore personsStore, ModalNavigationStore modalNavigationStore)
        {
            PersonId = person.Id;
            ICommand submitCommand = new EditPersonCommand(this, personsStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            PersonFormViewModel = new PersonFormViewModel(submitCommand, cancelCommand)
            {
                Name = person.Name,
                Notes = person.Notes,
                Role = person.Role,
            };
        }
    }
}
