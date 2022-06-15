using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class App : Application
    {
        INavigationService navigationService;
        public App()
        {
            InitializeComponent();
            navigationService = new NavigationService();
            MainPage = new NavigationPage( new MoviesPage());
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
