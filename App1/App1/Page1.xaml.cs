using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private object navigationService;

        public Page1(INavigationService navigationService)
        {
            InitializeComponent();
            BindingContext = new Page1ViewModel(navigationService);
        }
    }
}