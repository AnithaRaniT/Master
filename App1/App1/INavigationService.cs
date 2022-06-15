using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public interface INavigationService
    {
        Task PushTaskAsync(Page page, object parameter = null);
    }
}
