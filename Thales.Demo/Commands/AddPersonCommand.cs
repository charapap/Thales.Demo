using System;
using Thales.Demo.Models;
using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class AddPersonCommand : BaseCommand
    {
        private readonly AddPersonViewModel _addPersonViewModel;
        private readonly PersonsStore _personsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddPersonCommand(AddPersonViewModel addPersonViewModel, PersonsStore personsStore, ModalNavigationStore modalNavigationStore)
        {
            _addPersonViewModel = addPersonViewModel;
            _personsStore = personsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                Name = _addPersonViewModel.PersonFormViewModel.Name,
                Notes = _addPersonViewModel.PersonFormViewModel.Notes,
            };

            _personsStore.Add(person);
            //Add person
            _modalNavigationStore.Close();
        }
    }
}
