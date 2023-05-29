using Thales.Demo.Models;
using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class OpenEditRoleCommand : BaseCommand
    {
        private readonly RolesTreeItemViewModel _rolesTreeItemViewModel;
        private readonly RolesStore _rolesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenEditRoleCommand(RolesTreeItemViewModel rolesTreeItemViewModel, RolesStore rolesStore, ModalNavigationStore modalNavigationStore)
        {
            _rolesTreeItemViewModel = rolesTreeItemViewModel;
            _rolesStore = rolesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            Role role = _rolesTreeItemViewModel.Role;
            EditRoleViewModel viewModel = new EditRoleViewModel(role, _rolesStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = viewModel;
        }
    }
}
