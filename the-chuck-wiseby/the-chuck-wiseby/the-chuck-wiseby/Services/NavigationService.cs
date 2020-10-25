using System.Threading.Tasks;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.Views;
using the_chuck_wiseby.Utility;
using Xamarin.Forms;

namespace the_chuck_wiseby.Services
{
    public class NavigationService : INavigationService
    {
        private Page mainPage { get { return Application.Current.MainPage; } }

        public async Task GoBack()
        {
            await mainPage.Navigation.PopAsync();
        }

        public async Task NavigateToCategory(object state)
        {
            var message = state.CastToChuckMessage();
            await mainPage.Navigation.PushAsync(new JokeCategoryView(message));
        }

        public async Task NavigateToFavourites()
        {
            await mainPage.Navigation.PushAsync(new FavouriteView());
        }

        public async Task NavigateToMain()
        {
            await mainPage.Navigation.PushAsync(new MainView());
        }

        public async Task NavigateToRandom()
        {
            await mainPage.Navigation.PushAsync(new RandomJokeView());
        }

        public async Task NavigateToSearch(object state)
        {
            var message = state.CastToChuckMessage();
            await mainPage.Navigation.PushAsync(new JokeResultView(message));
        }
    }
}
