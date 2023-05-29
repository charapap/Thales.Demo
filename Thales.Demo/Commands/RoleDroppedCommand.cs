using Thales.Demo.ViewModels;

namespace Thales.Demo.Commands
{
    public class RoleDroppedCommand : BaseCommand
    {
        private PersonsListItemViewModel _personsListItemViewModel;

        public RoleDroppedCommand(PersonsListItemViewModel personsListItemViewModel)
        {
            _personsListItemViewModel = personsListItemViewModel;
        }

        public override void Execute(object parameter)
        {
            _personsListItemViewModel.AssignRole(_personsListItemViewModel.AssignedRole);
        }
    }
}
