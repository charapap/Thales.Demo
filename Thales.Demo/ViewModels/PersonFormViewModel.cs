using System;
using System.Windows.Input;
using Thales.Demo.Commands;
using Thales.Demo.Models;

namespace Thales.Demo.ViewModels
{
    public class PersonFormViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private string _notes;
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        private Role _role;
        public Role Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        public bool CanSubmit => !string.IsNullOrEmpty(Name);

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand RemoveRoleCommand { get; }

        public PersonFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
            RemoveRoleCommand = new RemovePersonRoleCommand(this);
        }

        public void RemoveRole()
        {
            if (Role != null)
            {
                Role = null;
                OnPropertyChanged(nameof(Role));
            }
        }
    }
}
