using the_chuck_wiseby.Containers;
using the_chuck_wiseby.Models;
using the_chuck_wiseby.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace the_chuck_wiseby.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JokeCategoryView : ContentPage
    {
        private JokeCategoryViewModel viewModel;
        public JokeCategoryView(ChuckMessage message)
        {
            InitializeComponent();
            BindingContext = viewModel = AppContainer.Resolve<JokeCategoryViewModel>();
            viewModel.ChuckMessage = message;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetCategoryJoke();
        }
    }
}