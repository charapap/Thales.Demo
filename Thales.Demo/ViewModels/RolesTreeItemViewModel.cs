using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Thales.Demo.Commands;
using Thales.Demo.Models;
using Thales.Demo.Stores;

namespace Thales.Demo.ViewModels
{
    public class RolesTreeItemViewModel : ViewModelBase
    {
        private readonly RolesStore _rolesStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public Role Role { get; private set; }

        public string Name => Role.Name;
        public bool IsExpanded { get; set; } = true;
        public ObservableCollection<RolesTreeItemViewModel> RolesTreeItemViewModels { get; private set; }

        public ICommand AddRoleCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public RolesTreeItemViewModel(Role role, RolesStore rolesStore, ModalNavigationStore modalNavigationStore)
        {
            _rolesStore = rolesStore;
            _modalNavigationStore = modalNavigationStore;
            Role = role;
            RolesTreeItemViewModels = new ObservableCollection<RolesTreeItemViewModel>(Role.Roles?.Select(x => new RolesTreeItemViewModel(x, _rolesStore, _modalNavigationStore)));
            AddRoleCommand = new OpenAddChildRoleCommand(this, rolesStore, modalNavigationStore);
            EditCommand = new OpenEditRoleCommand(this, rolesStore, modalNavigationStore);
            DeleteCommand = new DeleteRoleCommand(this, rolesStore);
        }

        internal void Update(Role role)
        {
            Role.Name = role.Name;
            Role.Description = role.Description;
            OnPropertyChanged(nameof(Name));
        }

        internal void UpdateChildren()
        {
            OnPropertyChanged(nameof(RolesTreeItemViewModels));
            OnPropertyChanged(nameof(IsExpanded));
        }
    }
}
