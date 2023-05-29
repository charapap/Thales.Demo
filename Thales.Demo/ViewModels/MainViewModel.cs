using Thales.Demo.Stores;

namespace Thales.Demo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public PersonsViewModel PersonsViewModel { get; }
        public RolesViewModel RolesViewModel { get; }

        public MainViewModel(ModalNavigationStore modalNavigationStore, PersonsViewModel personsViewModel, RolesViewModel rolesViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            PersonsViewModel = personsViewModel;
            RolesViewModel = rolesViewModel;
            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
        }

        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;
            base.Dispose();
        }

        private void ModalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}
