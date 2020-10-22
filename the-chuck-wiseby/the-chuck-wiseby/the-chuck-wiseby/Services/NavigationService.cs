using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace the_chuck_wiseby.Services
{
    public class NavigationService : INavigationService
    {
        private INavigation navigation;
        public NavigationService()
        {
            this.navigation = App.Current.MainPage.Navigation;
        }
        public async Task GoBack()
        {
            await navigation.PopAsync();
        }

        public Task NavigateToCategory()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToMain()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToRandom()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToSearch()
        {
            throw new NotImplementedException();
        }
    }
}
