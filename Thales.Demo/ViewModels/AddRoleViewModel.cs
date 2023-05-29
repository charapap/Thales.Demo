using System;
using System.Windows.Input;
using Thales.Demo.Commands;
using Thales.Demo.Stores;

namespace Thales.Demo.ViewModels
{
    public class AddRoleViewModel : ViewModelBase
    {
        public RoleFormViewModel RoleFormViewModel { get; }
        public Guid ParentId { get; }

        public AddRoleViewModel(Guid parentId, RolesStore rolesStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand submitCommand = new AddRoleCommand(this, rolesStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            RoleFormViewModel = new RoleFormViewModel(submitCommand, cancelCommand);
            ParentId = parentId;
        }
    }
}
