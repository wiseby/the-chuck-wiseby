using System.Windows.Input;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.Services;
using Xamarin.Forms;

namespace the_chuck_wiseby.ViewModels
{
    class JokeCategoryViewModel : BaseViewModel
    {
        private readonly IHttpService<ChuckJoke, ChuckMessage> httpService;

        public JokeCategoryViewModel(IHttpService<ChuckJoke, ChuckMessage> httpService)
        {
            this.httpService = httpService;
            NextJokeCommand = new Command(GetCategoryJoke);
        }

        #region Properties
        private ChuckMessage chuckMessage;
        public ChuckMessage ChuckMessage
        {
            get => chuckMessage;

            set
            {
                chuckMessage = value;
                OnPropertyChanged(nameof(ChuckMessage));
            }
        }

        private ChuckJoke joke;
        public ChuckJoke Joke
        {
            get => joke;

            set
            {
                joke = value;
                OnPropertyChanged(nameof(Joke));
            }
        }

        public ICommand NextJokeCommand { get; } 
        #endregion

        public async void GetCategoryJoke()
        {
            Joke = await httpService.GetByCategory(ChuckMessage.Category);
        }
    }
}
