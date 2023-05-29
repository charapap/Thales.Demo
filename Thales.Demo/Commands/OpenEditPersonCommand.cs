using Thales.Demo.Models;
using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class OpenEditPersonCommand : BaseCommand
    {
        private readonly PersonsListItemViewModel _personsListItemViewModel;
        private readonly PersonsStore _personsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenEditPersonCommand(PersonsListItemViewModel personsListItemViewModel, PersonsStore personsStore, ModalNavigationStore modalNavigationStore)
        {
            _personsListItemViewModel = personsListItemViewModel;
            _personsStore = personsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            Person person = _personsListItemViewModel.Person;
            EditPersonViewModel viewModel = new EditPersonViewModel(person, _personsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = viewModel;
        }
    }
}
