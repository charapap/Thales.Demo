using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class OpenAddRoleCommand : BaseCommand
    {
        private readonly RolesStore _rolesStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly RolesViewModel _rolesViewModel;

        public OpenAddRoleCommand(RolesViewModel rolesViewModel, RolesStore rolesStore, ModalNavigationStore modalNavigationStore)
        {
            _rolesStore = rolesStore;
            _modalNavigationStore = modalNavigationStore;
            _rolesViewModel = rolesViewModel;
        }

        public override void Execute(object parameter)
        {
            AddRoleViewModel viewModel = new AddRoleViewModel(_rolesViewModel.SelectedRole.Id, _rolesStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = viewModel;
        }
    }
}
