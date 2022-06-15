using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public class NavigationService : INavigationService
    {
        public async Task PushTaskAsync(Page page, object parameters)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
            await InitializeAsync(parameters);
        }
        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }
    }
}
