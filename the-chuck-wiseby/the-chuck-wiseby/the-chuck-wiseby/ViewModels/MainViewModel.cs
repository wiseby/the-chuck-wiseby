using System.Collections.ObjectModel;
using System.Windows.Input;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.Services;
using the_chuck_wiseby.Views;
using Xamarin.Forms;

namespace the_chuck_wiseby.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IHttpService<ChuckJoke> httpService;

        private string searchTerm;
        public string SearchTerm 
        { 
            get => searchTerm; 
            set
            {
                if (value != searchTerm)
                {
                    searchTerm = value;
                }
                this.OnPropertyChanged(nameof(SearchTerm));
            }
        }


        public ObservableCollection<string> Categories { get; set; }

        private string selectedCategory;

        public string SelectedCategory
        {
            get => selectedCategory;
            set 
            { 
                selectedCategory = value;
                this.OnPropertyChanged(nameof(SelectedCategory));
            }
        }


        public ICommand RandomCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand CategoryCommand { get; }


        public MainViewModel(IHttpService<ChuckJoke> httpService)
        {
            this.httpService = httpService;
            RandomCommand = new Command(OnRandomCommand);
            SearchCommand = new Command(OnSearchCommand);
            CategoryCommand = new Command(OnCategoryCommand);
            Initialize();
        }

        public async void Initialize()
        {
            try
            {
                this.Categories = new ObservableCollection<string>(await this.httpService.GetCategories());
            }
            catch (System.Exception)
            {

            }            
        }

        private void CategorySelected()
        {
            OnCategoryCommand();
        }

        private async void OnRandomCommand()
        {
            MessagingCenter.Send<MainViewModel>(this, Messages.RandomSelected.ToString());
            await App.Current.MainPage.Navigation.PushAsync(new RandomJokeView());
        }

        private async void OnSearchCommand()
        {
            MessagingCenter.Send<MainViewModel>(this, Messages.SearchSelected.ToString());
            await App.Current.MainPage.Navigation.PushAsync(new SearchView());
        }

        private void OnCategoryCommand()
        {
            MessagingCenter.Send<MainViewModel>(this, Messages.CategorySelected.ToString());
            App.Current.MainPage.Navigation.PushAsync(new JokeResultView());
        }


    }
}
