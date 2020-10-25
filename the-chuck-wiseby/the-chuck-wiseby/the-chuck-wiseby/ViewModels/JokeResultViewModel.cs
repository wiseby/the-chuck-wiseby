using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace the_chuck_wiseby.ViewModels
{
    public class JokeResultViewModel : BaseViewModel
    {
        private IHttpService<ChuckJoke, ChuckMessage> httpService;
        private IFavouriteService<ChuckJoke> favouriteService;

        public JokeResultViewModel(
            IHttpService<ChuckJoke, ChuckMessage> httpService,
            INavigationService navigationService,
            IFavouriteService<ChuckJoke> favouriteService)
                : base(navigationService)
        {
            this.httpService = httpService;
            this.favouriteService = favouriteService;

            SearchCommand = new Command(OnSearchCommand);
            GoBackCommand = new Command(OnGoBackCommand);
            FavouriteCommand = new Command<ChuckJoke>(OnFavouriteCommand);

        }

        #region Properties
        private ChuckMessage chuckMessage;
        public ChuckMessage ChuckMessage
        {
            get => chuckMessage;

            set
            {
                if (value != null)
                {
                    chuckMessage = value;
                    OnPropertyChanged(nameof(ChuckMessage));
                }
            }
        }

        private ChuckJoke activeJoke;
        public ChuckJoke ActiveJoke
        {
            get => activeJoke;

            set
            {
                activeJoke = value;
                OnPropertyChanged(nameof(ActiveJoke));
            }
        }

        private int numberOfMatches;
        public int NumberOfMatches
        {
            get => numberOfMatches;
            set
            {
                numberOfMatches = value;
                this.OnPropertyChanged(nameof(NumberOfMatches));
            }
        }

        public ObservableCollection<ChuckJoke> Jokes { get; set; }

        public ICommand SearchCommand { get; }
        public ICommand GoBackCommand { get; } 
        public ICommand FavouriteCommand { get; }
        #endregion

        public void Initialize()
        {
            Jokes = new ObservableCollection<ChuckJoke>();
        }

        private async void GetJokes()
        {
            var result = await this.httpService.GetBySearch(ChuckMessage.SearchTerm);

            Jokes.Clear();

            result.Jokes.ForEach((joke) =>
            {
                joke.IsFavourite = favouriteService.IsFavourite(joke);
                Jokes.Add(joke);
            });
            
            NumberOfMatches = result.Total;

            OnPropertyChanged(nameof(Jokes));
        }

        private void OnSearchCommand()
        {
            ChuckMessage.Category = null;
            GetJokes();
        }

        private async void OnGoBackCommand()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private void OnFavouriteCommand(ChuckJoke joke)
        {
            favouriteService.Save(joke);
            OnPropertyChanged(nameof(Jokes));
        }
    }
}
