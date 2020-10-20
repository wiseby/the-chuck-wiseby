using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace the_chuck_wiseby.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RandomJokeView : ContentPage
    {
        public RandomJokeView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Send<RandomJokeView>(this, Messages.InitializeRandomView.ToString());
            base.OnAppearing();
        }
    }
}