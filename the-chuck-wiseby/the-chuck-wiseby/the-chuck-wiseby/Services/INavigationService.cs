using System.Threading.Tasks;

namespace the_chuck_wiseby.Services
{
    public interface INavigationService
    {
        Task NavigateToSearch(object state);
        Task NavigateToMain();
        Task NavigateToCategory(object state);
        Task NavigateToRandom();
        Task NavigateToFavourites();
        Task GoBack();
    }
}
