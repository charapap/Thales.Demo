using Thales.Demo.Models;
using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class EditPersonCommand : BaseCommand
    {
        private readonly EditPersonViewModel _editPersonViewModel;
        private readonly PersonsStore _personsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditPersonCommand(EditPersonViewModel editPersonViewModel, PersonsStore personsStore, ModalNavigationStore modalNavigationStore)
        {
            _editPersonViewModel = editPersonViewModel;
            _personsStore = personsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            Person person = new Person()
            {
                Id = _editPersonViewModel.PersonId,
                Name = _editPersonViewModel.PersonFormViewModel.Name,
                Notes = _editPersonViewModel.PersonFormViewModel.Notes
            };

            _personsStore.Edit(person);

            _modalNavigationStore.Close();
        }
    }
}
