using System.Threading.Tasks;

namespace the_chuck_wiseby.Services
{
    public interface INavigationService
    {
        Task NavigateToSearch();
        Task NavigateToMain();
        Task NavigateToCategory();
        Task NavigateToRandom();
        Task GoBack();
    }
}
