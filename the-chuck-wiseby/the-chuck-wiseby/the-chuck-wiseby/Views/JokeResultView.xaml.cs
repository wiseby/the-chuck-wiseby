using the_chuck_wiseby.Containers;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace the_chuck_wiseby.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JokeResultView : ContentPage
    {
        private readonly JokeResultViewModel viewModel;
        public JokeResultView(ChuckMessage message)
        {
            InitializeComponent();
            BindingContext = viewModel = AppContainer.Resolve<JokeResultViewModel>();
            // Set message to viewmodel directly
            this.viewModel.ChuckMessage = message;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.Initialize();
            if (viewModel.ChuckMessage.SearchTerm == null) { viewModel.ChuckMessage.SearchTerm = string.Empty; }
            GetBySearch();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            GetBySearch();
        }

        private void GetBySearch()
        {
            if (viewModel.ChuckMessage.SearchTerm == string.Empty ||
                viewModel.ChuckMessage.SearchTerm.Length < 3 ||
                viewModel.ChuckMessage.SearchTerm.Length > 120)
            {
                DisplayAlert(
                    "SearchTerm not valid",
                    "The search parameter must be between 3 - 120 characters",
                    "Ok");
            }
            else
            {
                viewModel.SearchCommand.Execute(this);
            }
        }
    }
}