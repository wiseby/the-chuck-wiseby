using System.ComponentModel;
using the_chuck_wiseby.Services;

namespace the_chuck_wiseby.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly INavigationService navigationService;

        public BaseViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
