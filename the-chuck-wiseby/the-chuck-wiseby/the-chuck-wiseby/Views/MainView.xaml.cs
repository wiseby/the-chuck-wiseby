using the_chuck_wiseby.Containers;
using the_chuck_wiseby.ViewModels;
using Xamarin.Forms;

namespace the_chuck_wiseby.Views
{
    public partial class MainView : ContentPage
    {
        private MainViewModel viewModel;
        public MainView()
        {
            InitializeComponent();
            BindingContext = viewModel = AppContainer.Resolve<MainViewModel>();
        }

        protected override void OnAppearing()
        {
            viewModel.Initialize();
            base.OnAppearing();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            viewModel.CategoryCommand.Execute(this);
        }
    }
}
