using Thales.Demo.Models;
using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class EditRoleCommand : BaseCommand
    {
        private readonly EditRoleViewModel _editRoleViewModel;
        private readonly RolesStore _rolesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public EditRoleCommand(EditRoleViewModel editRoleViewModel, RolesStore rolesStore, ModalNavigationStore modalNavigationStore)
        {
            _editRoleViewModel = editRoleViewModel;
            _rolesStore = rolesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            Role role = new Role()
            {
                Id = _editRoleViewModel.RoleId,
                Name = _editRoleViewModel.RoleFormViewModel.Name, 
                Description = _editRoleViewModel.RoleFormViewModel.Description,
                ParentId = _editRoleViewModel.ParentId
                //Roles = _editRoleViewModel.RoleFormViewModel.
            };

            _rolesStore.Edit(role);

            _modalNavigationStore.Close();
        }
    }
}
