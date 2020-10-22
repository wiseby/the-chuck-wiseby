using System.Collections.ObjectModel;
using System.Windows.Input;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.Services;
using the_chuck_wiseby.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace the_chuck_wiseby.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IHttpService<ChuckJoke, ChuckMessage> httpService;

        public MainViewModel(IHttpService<ChuckJoke, ChuckMessage> httpService)
        {
            this.httpService = httpService;
            RandomCommand = new Command(OnRandomCommand);
            SearchCommand = new Command(OnSearchCommand);
            CategoryCommand = new Command(OnCategoryCommand);
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
            await App.Current.MainPage.Navigation.PushAsync(new RandomJokeView());
        }

        private async void OnSearchCommand()
        {
            var message = new ChuckMessage() { Category = null, SearchTerm = searchTerm };
            await App.Current.MainPage.Navigation.PushAsync(new JokeResultView(message));
        }

        private async void OnCategoryCommand()
        {
            var message = new ChuckMessage() { Category = SelectedCategory, SearchTerm = null };
            await App.Current.MainPage.Navigation.PushAsync(new JokeCategoryView(message));
        }


    }
}
