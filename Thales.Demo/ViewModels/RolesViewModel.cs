using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Thales.Demo.Commands;
using Thales.Demo.Models;
using Thales.Demo.Services;
using Thales.Demo.Stores;

namespace Thales.Demo.ViewModels
{
    public class RolesViewModel : ViewModelBase
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly RolesStore _rolesStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ConnectionService _connectionService;
        private readonly DataService _dataService;
        private readonly ObservableCollection<RolesTreeItemViewModel> _rolesTreeItemViewModels;
        public IEnumerable<RolesTreeItemViewModel> RolesTreeItemViewModels => _rolesTreeItemViewModels;

        private Role _selectedRole = null;
        public Role SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                if (_selectedRole != value)
                {
                    _selectedRole = value;
                    OnPropertyChanged(nameof(AddRoleEnabled));
                }
            }
        }
        public bool AddRoleEnabled => SelectedRole != null;

        public ICommand AddRoleCommand { get; }

        public RolesViewModel(RolesStore rolesStore, ModalNavigationStore modalNavigationStore, ConnectionService connectionService, DataService dataService, IMessageBoxService messageBoxService) 
        {
            _rolesStore = rolesStore;
            _modalNavigationStore = modalNavigationStore;
            _connectionService = connectionService;
            _dataService = dataService;
            _messageBoxService = messageBoxService;
            _rolesTreeItemViewModels = new ObservableCollection<RolesTreeItemViewModel>();

            _rolesStore.RoleAdded += _rolesStore_RoleAdded;
            _rolesStore.RoleEdited += _rolesStore_RoleEdited;
            _rolesStore.RoleDeleted += _rolesStore_RoleDeleted;
            _connectionService.DataReceived += ConnectionService_DataReceived;

            foreach (Role role in _dataService.GetRoles())
            {
                AddRole(role, false);
            }

            AddRoleCommand = new OpenAddRoleCommand(this, rolesStore, modalNavigationStore);
        }

        private void _rolesStore_RoleDeleted(Role obj)
        {
            bool result = DeleteRole(obj);
            if (result == true)
            {
                _connectionService.SendData(new DataPacket() { ActionType = ActionType.Delete, Data = obj, DataType = DataType.Role });
            }
        }

        private void FlattenRoles(Role role, List<Role> flattenedList)
        {
            flattenedList.Add(role);

            if (role.Roles != null)
            {
                foreach (Role subrole in role.Roles)
                {
                    FlattenRoles(subrole, flattenedList);
                }
            }
        }

        private void _rolesStore_RoleEdited(Role obj)
        {
            EditRole(obj);
            _connectionService.SendData(new DataPacket() { ActionType = ActionType.Edit, Data = obj, DataType = DataType.Role });
        }

        private void _rolesStore_RoleAdded(Role obj)
        {
            AddRole(obj);
            _connectionService.SendData(new DataPacket() { ActionType = ActionType.Add, Data = obj, DataType = DataType.Role });
        }

        private void ConnectionService_DataReceived(DataPacket obj)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                if (obj.DataType == DataType.Role)
                {
                    switch (obj.ActionType)
                    {
                        case ActionType.Add:
                            AddRole((Role)obj.Data);
                            break;
                        case ActionType.Edit:
                            AddRole((Role)obj.Data);
                            break;
                        case ActionType.Delete:
                            DeleteRole((Role)obj.Data);
                            break;
                    }
                }
            }));
        }

        protected override void Dispose()
        {
            _rolesStore.RoleAdded -= _rolesStore_RoleAdded;
            _rolesStore.RoleEdited -= _rolesStore_RoleEdited;
            _rolesStore.RoleDeleted -= _rolesStore_RoleDeleted;
            _connectionService.DataReceived -= ConnectionService_DataReceived;
            base.Dispose();
        }

        private void AddRole(Role role, bool saveData = true)
        {
            if (role.ParentId == Guid.Empty)
            {
                RolesTreeItemViewModel listItemViewModel = new RolesTreeItemViewModel(role, _rolesStore, _modalNavigationStore);
                _rolesTreeItemViewModels.Add(listItemViewModel);
            }
            else
            {
                RolesTreeItemViewModel listItemViewModel = new RolesTreeItemViewModel(role, _rolesStore, _modalNavigationStore);
                RolesTreeItemViewModel parentRoleVM = GetRoleVM(role.ParentId, _rolesTreeItemViewModels.ToList());
                var parentRole = GetRole(role.ParentId, _rolesTreeItemViewModels.Select(x => x.Role).ToList());
                parentRole.Roles.Add(role);
                parentRoleVM.RolesTreeItemViewModels.Add(listItemViewModel);
                parentRoleVM.IsExpanded = true;
                parentRoleVM.UpdateChildren();
                OnPropertyChanged(nameof(RolesTreeItemViewModels));
            }
            if (saveData)
            {
                _dataService.SaveRoles(_rolesTreeItemViewModels.Select(x => x.Role).ToList());
            }
        }

        private void EditRole(Role role)
        {
            RolesTreeItemViewModel roleVM = GetRoleVM(role.Id, _rolesTreeItemViewModels.ToList());
            if (roleVM != null)
            {
                RolesTreeItemViewModel parentRoleVM = GetRoleVM(role.ParentId, _rolesTreeItemViewModels.ToList());
                if (parentRoleVM != null)
                {
                    parentRoleVM.Role.Roles[parentRoleVM.Role.Roles.FindIndex(x => x.Id == role.Id)] = role;
                }
                roleVM.Update(role);
                _dataService.SaveRoles(_rolesTreeItemViewModels.Select(x => x.Role).ToList());
            }
        }

        private bool DeleteRole(Role role)
        {
            List<Person> persons = _dataService.GetPersons();
            List<Role> flattenedList = new List<Role>();
            FlattenRoles(role, flattenedList);
            foreach (Role r in flattenedList)
            {
                if (persons.Exists(x => x.Role?.Id == r.Id))
                {
                    _messageBoxService.ShowMessageBox("Action not allowed. The role or a child role is assigned to a user.", "Warning");
                    return false;
                }
            }
            RolesTreeItemViewModel roleVM = GetRoleVM(role.Id, _rolesTreeItemViewModels.ToList());
            RolesTreeItemViewModel parentRoleVM = GetRoleVM(role.ParentId, _rolesTreeItemViewModels.ToList());
            var parentRole = GetRole(role.ParentId, _rolesTreeItemViewModels.Select(x => x.Role).ToList());
            if (roleVM != null && parentRoleVM != null && parentRole != null)
            {
                parentRole.Roles.Remove(role);
                parentRoleVM.RolesTreeItemViewModels.Remove(roleVM);
                _dataService.SaveRoles(_rolesTreeItemViewModels.Select(x => x.Role).ToList());
            }
            return true;
        }

        private RolesTreeItemViewModel GetRoleVM(Guid id, List<RolesTreeItemViewModel> items)
        {
            foreach (RolesTreeItemViewModel r in items)
            {
                if (r.Role.Id == id)
                {
                    return r;
                }

                if (r.RolesTreeItemViewModels.Count > 0)
                {
                    var innerR = GetRoleVM(id, r.RolesTreeItemViewModels.ToList());
                    if (innerR != null)
                    {
                        return GetRoleVM(id, r.RolesTreeItemViewModels.ToList());
                    }
                }
            }
            return null;
        }

        private Role GetRole(Guid id, List<Role> items)
        {
            foreach (Role r in items)
            {
                if (r.Id == id)
                {
                    return r;
                }

                if (r.Roles.Count > 0)
                {
                    var innerR = GetRole(id, r.Roles);
                    if (innerR != null)
                    {
                        return GetRole(id, r.Roles);
                    }
                }
            }
            return null;
        }
    }
}
