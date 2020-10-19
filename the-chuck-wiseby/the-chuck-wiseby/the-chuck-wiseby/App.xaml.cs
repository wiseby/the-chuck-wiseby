using System;
using the_chuck_wiseby.Containers;
using the_chuck_wiseby.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace the_chuck_wiseby
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitializeApp();

            MainPage = new NavigationPage(new MainView());
        }

        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
