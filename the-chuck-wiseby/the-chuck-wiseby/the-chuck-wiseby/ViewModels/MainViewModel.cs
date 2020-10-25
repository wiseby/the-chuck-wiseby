using System.Collections.ObjectModel;
using System.Windows.Input;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace the_chuck_wiseby.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IHttpService<ChuckJoke, ChuckMessage> httpService;

        public MainViewModel(
            IHttpService<ChuckJoke, ChuckMessage> httpService,
            INavigationService navigationService)
                : base(navigationService)
        {
            this.httpService = httpService;
            RandomCommand = new Command(OnRandomCommand);
            SearchCommand = new Command(OnSearchCommand);
            CategoryCommand = new Command(OnCategoryCommand);
            FavouriteCommand = new Command(OnFavouriteCommand);
        }

        #region Properties
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
                if (value != selectedCategory)
                {
                    selectedCategory = value;
                    this.OnPropertyChanged(nameof(SelectedCategory));
                }
            }
        }

        public ICommand RandomCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand CategoryCommand { get; }
        public ICommand FavouriteCommand { get; }

        #endregion

        public async void Initialize()
        {
            Categories = new ObservableCollection<string>();
            selectedCategory = "";
            // FIXME: Maybe better to Create new instance and raise 
            // ObservableCollection.CollectionChanged?
            var result = await this.httpService.GetCategories();
            result.ForEach((item) => 
            { 
                Categories.Add(item);
            });
            OnPropertyChanged(nameof(Categories));
        }

        private async void OnRandomCommand()
        {
            MessagingCenter.Send<MainViewModel>(this, Messages.RandomSelected.ToString());
            await this.navigationService.NavigateToRandom();
        }

        private async void OnSearchCommand()
        {
            var message = new ChuckMessage() { Category = null, SearchTerm = searchTerm };
            await this.navigationService.NavigateToSearch(message);
        }

        private async void OnCategoryCommand()
        {
            var message = new ChuckMessage() { Category = SelectedCategory, SearchTerm = null };
            await this.navigationService.NavigateToSearch(message);
        }

        private async void OnFavouriteCommand()
        {
            await navigationService.NavigateToFavourites();
        }
    }
}
