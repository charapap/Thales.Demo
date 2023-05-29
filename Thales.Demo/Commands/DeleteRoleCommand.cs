using Thales.Demo.Models;
using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class DeleteRoleCommand : BaseCommand
    {
        private readonly RolesTreeItemViewModel _rolesTreeItemViewModel;
        private readonly RolesStore _rolesStore;

        public DeleteRoleCommand(RolesTreeItemViewModel rolesTreeItemViewModel, RolesStore rolesStore)
        {
            _rolesTreeItemViewModel = rolesTreeItemViewModel;
            _rolesStore = rolesStore;
        }

        public override void Execute(object parameter)
        {
            Role role = _rolesTreeItemViewModel.Role;
            _rolesStore.Delete(role);
        }

        public override bool CanExecute(object parameter)
        {
            if (_rolesTreeItemViewModel.Role.ParentId == null || _rolesTreeItemViewModel.Role.ParentId == System.Guid.Empty)
            {
                return false;
            }
            return true;
        }
    }
}
