using System;
using Thales.Demo.Models;
using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class AddRoleCommand : BaseCommand
    {
        private readonly AddRoleViewModel _addRoleViewModel;
        private readonly RolesStore _rolesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddRoleCommand(AddRoleViewModel addRoleViewModel, RolesStore rolesStore, ModalNavigationStore modalNavigationStore)
        {
            _addRoleViewModel = addRoleViewModel;
            _rolesStore = rolesStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            Role role = new Role()
            {
                Id = Guid.NewGuid(),
                Name = _addRoleViewModel.RoleFormViewModel.Name,
                Description = _addRoleViewModel.RoleFormViewModel.Description,
                ParentId = _addRoleViewModel.ParentId
            };

            _rolesStore.Add(role);
            //Add role
            _modalNavigationStore.Close();
        }
    }
}
