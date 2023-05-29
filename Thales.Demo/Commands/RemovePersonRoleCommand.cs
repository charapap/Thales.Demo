using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class RemovePersonRoleCommand : BaseCommand
    {
        private readonly PersonFormViewModel _personFormViewModel;

        public RemovePersonRoleCommand(PersonFormViewModel personsListItemViewModel)
        {
            _personFormViewModel = personsListItemViewModel;
        }

        public override void Execute(object parameter)
        {
            _personFormViewModel.RemoveRole();
        }
    }
}
