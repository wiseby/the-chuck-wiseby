using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace the_chuck_wiseby.ViewModels
{
    public class FavouriteViewModel : BaseViewModel
    {
        private IFavouriteService<ChuckJoke> favouriteService;

        public FavouriteViewModel(
            IFavouriteService<ChuckJoke> favouriteService,
            INavigationService navigationService)
                : base(navigationService)
        {
            this.favouriteService = favouriteService;
            RemoveCommand = new Command<ChuckJoke>(Remove);
        }

        public bool HasData()
        {
            if (Favourites.Count<ChuckJoke>() > 0)
            {
                OnPropertyChanged(nameof(Favourites));
                return true;
            }
            else { return false; }
        }

        public ObservableCollection<ChuckJoke> Favourites
        {
            get => new ObservableCollection<ChuckJoke>(favouriteService.Get());
        }

        public string CategoriesAsString 
        { 
            get 
            {
                var categories = "";
                Favourites.ForEach((category) => categories += $"{category}, ");
                return categories;
            } 
        }
        public ICommand RemoveCommand { get; }

        private void Remove(ChuckJoke joke)
        {
            favouriteService.Delete(joke);
            OnPropertyChanged(nameof(Favourites));
        }
    }
}
