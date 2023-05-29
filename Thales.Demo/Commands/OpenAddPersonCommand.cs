using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class OpenAddPersonCommand : BaseCommand
    {
        private readonly PersonsStore _personsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddPersonCommand(PersonsStore personsStore, ModalNavigationStore modalNavigationStore)
        {
            _personsStore = personsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddPersonViewModel viewModel = new AddPersonViewModel(_personsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = viewModel;
        }
    }
}
