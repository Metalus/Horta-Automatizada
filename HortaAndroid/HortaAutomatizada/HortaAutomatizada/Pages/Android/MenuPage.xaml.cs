using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HortaAutomatizada.Pages.Android
{
    public partial class MenuPage : ContentPage
    {
        RootPage root;
        public MenuPage(RootPage root)
        {
            this.root = root;
            InitializeComponent();
            NavView.NavigationViewItemSelected += (sender, e) =>
            {
                this.root.IsPresented = false;
                Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                        {
                            await this.root.NavigateAsync(e.Page);
                        });
                    return false;
                });
            };
        }
    }
}
