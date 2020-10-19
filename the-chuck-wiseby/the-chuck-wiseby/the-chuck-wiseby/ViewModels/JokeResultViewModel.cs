using the_chuck_wiseby.Models;
using the_chuck_wiseby.Services;

namespace the_chuck_wiseby.ViewModels
{
    public class JokeResultViewModel
    {
        private IHttpService<ChuckJoke> httpService;
        public JokeResultViewModel(IHttpService<ChuckJoke> httpService)
        {
            this.httpService = httpService;
        }


    }
}
