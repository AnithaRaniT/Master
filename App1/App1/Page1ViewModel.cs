using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1
{
    public class Page1ViewModel :NavigationService, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnpropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private INavigationService navigationServices;
        private ICommand LoginCommand { get; set; }
        public Page1ViewModel(INavigationService navigationService)
        {
            navigationServices = navigationService;
        }
        public override Task InitializeAsync(object data)
        {
            string text = data.ToString();
            return base.InitializeAsync(data);
        }
    }
}
