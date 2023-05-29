using System;
using System.Windows.Input;
using Thales.Demo.Commands;
using Thales.Demo.Models;
using Thales.Demo.Stores;

namespace Thales.Demo.ViewModels
{
    public class EditRoleViewModel : ViewModelBase
    {
        public RoleFormViewModel RoleFormViewModel { get; }
        public Guid RoleId { get; }
        public Guid ParentId { get; }

        public EditRoleViewModel(Role role, RolesStore rolesStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand submitCommand = new EditRoleCommand(this, rolesStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            RoleFormViewModel = new RoleFormViewModel(submitCommand, cancelCommand);
            RoleId = role.Id;
            ParentId = role.ParentId;
            RoleFormViewModel = new RoleFormViewModel(submitCommand, cancelCommand)
            {
                Name = role.Name,
                Description = role.Description
            };
        }
    }
}
