using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.Services;
using Xamarin.Forms;

namespace the_chuck_wiseby.ViewModels
{
    class RandomJokeViewModel : BaseViewModel
    {
        private IHttpService<ChuckJoke> httpService;

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
        public RandomJokeViewModel(IHttpService<ChuckJoke> httpService)
        {
            this.httpService = httpService;
            BackCommand = new Command(OnBackCommand);
        }

        private async void OnBackCommand()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
