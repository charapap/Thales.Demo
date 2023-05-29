using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class OpenAddChildRoleCommand : BaseCommand
    {
        private readonly RolesStore _rolesStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly RolesTreeItemViewModel _rolesTreeItemViewModel;

        public OpenAddChildRoleCommand(RolesTreeItemViewModel rolesTreeItemViewModel, RolesStore rolesStore, ModalNavigationStore modalNavigationStore)
        {
            _rolesStore = rolesStore;
            _modalNavigationStore = modalNavigationStore;
            _rolesTreeItemViewModel = rolesTreeItemViewModel;
        }

        public override void Execute(object parameter)
        {
            AddRoleViewModel viewModel = new AddRoleViewModel(_rolesTreeItemViewModel.Role.Id, _rolesStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = viewModel;
        }
    }
}
