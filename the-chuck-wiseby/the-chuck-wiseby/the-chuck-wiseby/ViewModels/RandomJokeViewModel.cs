﻿using System.Windows.Input;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.Services;
using the_chuck_wiseby.Views;
using Xamarin.Forms;

namespace the_chuck_wiseby.ViewModels
{
    public class RandomJokeViewModel : BaseViewModel
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
        public ICommand NextJokeCommand { get; }
        public RandomJokeViewModel(IHttpService<ChuckJoke> httpService)
        {
            this.httpService = httpService;
            BackCommand = new Command(OnBackCommand);
            NextJokeCommand = new Command(NextJoke);
            InitializeMessageCenter();
        }

        public void InitializeMessageCenter()
        {
            MessagingCenter.Subscribe<RandomJokeView>(
                this,
                Messages.InitializeRandomView.ToString(),
                (sender) => NextJoke()
            );
        }

        private async void NextJoke()
        {
            Joke = await httpService.GetRandom();
        }

        private async void OnBackCommand()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
