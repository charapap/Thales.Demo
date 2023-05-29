using System.Windows.Input;
using Thales.Demo.Commands;
using Thales.Demo.Models;
using Thales.Demo.Stores;

namespace Thales.Demo.ViewModels
{
    public class PersonsListItemViewModel : ViewModelBase
    {
        private readonly PersonsStore _personsStore;

        public Person Person { get; private set; }

        public string Name => Person.Name;
        public string Role => Person.Role?.Name;

        private RolesTreeItemViewModel _assignedRole;
        public RolesTreeItemViewModel AssignedRole
        {
            get
            {
                return _assignedRole;
            }
            set
            {
                _assignedRole = value;
                OnPropertyChanged(nameof(AssignedRole));
            }
        }

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RoleDroppedCommand { get; }

        public PersonsListItemViewModel(Person person, PersonsStore personsStore, ModalNavigationStore modalNavigationStore)
        {
            _personsStore = personsStore;
            Person = person;
            EditCommand = new OpenEditPersonCommand(this, _personsStore, modalNavigationStore);
            DeleteCommand = new DeletePersonCommand(this, _personsStore);
            RoleDroppedCommand = new RoleDroppedCommand(this);
        }

        internal void Update(Person person)
        {
            Person = person;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Role));
        }

        internal void AssignRole(RolesTreeItemViewModel rolesTreeItemViewModel)
        {
            Person.Role = rolesTreeItemViewModel.Role;
            OnPropertyChanged(nameof(Role));
            _personsStore.Edit(Person);
        }
    }
}
