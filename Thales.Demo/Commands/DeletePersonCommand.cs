using Thales.Demo.Models;
using Thales.Demo.Stores;
using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class DeletePersonCommand : BaseCommand
    {
        private readonly PersonsListItemViewModel _personsListItemViewModel;
        private readonly PersonsStore _personsStore;

        public DeletePersonCommand(PersonsListItemViewModel personsListItemViewModel, PersonsStore personsStore)
        {
            _personsListItemViewModel = personsListItemViewModel;
            _personsStore = personsStore;
        }

        public override void Execute(object parameter)
        {
            Person person = _personsListItemViewModel.Person;
            _personsStore.Delete(person.Id);
        }
    }
}
