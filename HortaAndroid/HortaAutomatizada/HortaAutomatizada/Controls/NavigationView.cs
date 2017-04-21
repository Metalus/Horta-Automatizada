using HortaAutomatizada.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HortaAutomatizada.Controls
{
    public class CustomNavigationView : ContentView
    {
        public void OnNavigationViewItemSelected(NavigationViewItemSelectedEventArgs e)
            => NavigationViewItemSelected?.Invoke(this, e);

        public event NavigationViewItemSelectedEventHandler NavigationViewItemSelected;
    }

    public class NavigationViewItemSelectedEventArgs : EventArgs
    {
        public NavigationPagesEnum Page { get; set; }
    }

    public delegate void NavigationViewItemSelectedEventHandler(object sender, NavigationViewItemSelectedEventArgs e);
}
