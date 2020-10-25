using System.Windows.Input;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.Services;
using the_chuck_wiseby.Views;
using Xamarin.Forms;

namespace the_chuck_wiseby.ViewModels
{
    public class RandomJokeViewModel : BaseViewModel
    {
        private IHttpService<ChuckJoke, ChuckMessage> httpService;
        private IFavouriteService<ChuckJoke> favouriteService;

        public RandomJokeViewModel(
            IHttpService<ChuckJoke, ChuckMessage> httpService, 
            INavigationService navigationService,
            IFavouriteService<ChuckJoke> favouriteService)
                : base(navigationService)
        {
            this.httpService = httpService;
            this.favouriteService = favouriteService;
            BackCommand = new Command(OnBackCommand);
            NextJokeCommand = new Command(NextJoke);
            FavouriteCommand = new Command<ChuckJoke>(OnFavouriteCommand);
            Initialize();
        }

        #region Properties
        private ChuckJoke joke;
        public ChuckJoke Joke
        {
            get => joke;
            set
            {
                joke = value;
                this.OnPropertyChanged(nameof(Joke));
            }
        } 

        public ICommand BackCommand { get; }
        public ICommand NextJokeCommand { get; }
        public ICommand FavouriteCommand { get; }
        #endregion

        public void Initialize()
        {
            MessagingCenter.Subscribe<RandomJokeView>(
                this,
                Messages.InitializeRandomView.ToString(),
                (sender) => NextJoke()
            );
            NextJoke();
        }

        public bool IsFavourite => favouriteService.IsFavourite(Joke);

        private async void NextJoke()
        {
            Joke = await httpService.GetRandom();
        }

        private void OnFavouriteCommand(ChuckJoke joke)
        {
            if (favouriteService.IsFavourite(joke))
            {
                favouriteService.Delete(joke);
            }
            else
            {
                favouriteService.Save(joke);
            }
        }

        private async void OnBackCommand()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
