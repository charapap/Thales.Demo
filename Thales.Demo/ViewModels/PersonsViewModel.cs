using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Thales.Demo.Commands;
using Thales.Demo.Models;
using Thales.Demo.Services;
using Thales.Demo.Stores;

namespace Thales.Demo.ViewModels
{
    public class PersonsViewModel : ViewModelBase
    {
        private readonly PersonsStore _personsStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly ConnectionService _connectionService;
        private readonly DataService _dataService;
        private readonly ObservableCollection<PersonsListItemViewModel> _personsListItemViewModels;
        public IEnumerable<PersonsListItemViewModel> PersonsListItemViewModels => _personsListItemViewModels;
        public ICommand AddPersonCommand { get; }

        public PersonsViewModel(PersonsStore personsStore, ModalNavigationStore modalNavigationStore, ConnectionService connectionService, DataService dataService) 
        { 
            _personsStore = personsStore;
            _modalNavigationStore = modalNavigationStore;
            _connectionService = connectionService;
            _dataService = dataService;
            _personsListItemViewModels = new ObservableCollection<PersonsListItemViewModel>();

            _personsStore.PersonAdded += _personsStore_PersonAdded;
            _personsStore.PersonEdited += _personsStore_PersonEdited;
            _personsStore.PersonDeleted += _personsStore_PersonDeleted;
            _connectionService.DataReceived += ConnectionService_DataReceived;

            foreach(Person person in _dataService.GetPersons())
            {
                AddPerson(person, false);
            }

            AddPersonCommand = new OpenAddPersonCommand(personsStore, modalNavigationStore);
        }

        private void ConnectionService_DataReceived(DataPacket obj)
        {
            Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                if (obj.DataType == DataType.Person)
                {
                    switch (obj.ActionType)
                    {
                        case ActionType.Add:
                            AddPerson((Person)obj.Data);
                            break;
                        case ActionType.Edit:
                            EditPerson((Person)obj.Data);
                            break;
                        case ActionType.Delete:
                            DeletePerson(((Person)obj.Data).Id);
                            break;
                    }
                }
            }));
        }

        private void _personsStore_PersonDeleted(Guid id)
        {
            DeletePerson(id);
            _connectionService.SendData(new DataPacket() { ActionType = ActionType.Delete, Data = new Person() { Id = id }, DataType = DataType.Person });
        }

        private void _personsStore_PersonEdited(Person obj)
        {
            EditPerson(obj);
            _connectionService.SendData(new DataPacket() { ActionType = ActionType.Edit, Data = obj, DataType = DataType.Person });
        }

        protected override void Dispose()
        {
            _personsStore.PersonAdded -= _personsStore_PersonAdded;
            _personsStore.PersonEdited -= _personsStore_PersonEdited;
            _personsStore.PersonDeleted -= _personsStore_PersonDeleted;
            _connectionService.DataReceived -= ConnectionService_DataReceived;
            base.Dispose();
        }

        private void _personsStore_PersonAdded(Person obj)
        {
            AddPerson(obj);
            _connectionService.SendData(new DataPacket() { ActionType = ActionType.Add, Data = obj, DataType = DataType.Person });
        }

        private void AddPerson(Person person, bool saveData = true)
        {
            PersonsListItemViewModel listItemViewModel = new PersonsListItemViewModel(person, _personsStore, _modalNavigationStore);
            _personsListItemViewModels.Add(listItemViewModel);
            if (saveData)
            {
                _dataService.SavePersons(_personsListItemViewModels.Select(x => x.Person).ToList());
            }
        }

        private void EditPerson(Person person)
        {
            PersonsListItemViewModel item = _personsListItemViewModels.FirstOrDefault(x => x.Person.Id == person.Id);
            if (item != null)
            {
                item.Update(person);
                _dataService.SavePersons(_personsListItemViewModels.Select(x => x.Person).ToList());
            }
        }

        private void DeletePerson(Guid id)
        {
            PersonsListItemViewModel item = _personsListItemViewModels.FirstOrDefault(x => x.Person.Id == id);
            if (item != null)
            {
                _personsListItemViewModels.Remove(item);
                _dataService.SavePersons(_personsListItemViewModels.Select(x => x.Person).ToList());
            }
        }
    }
}
