using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace HortaAutomatizada.Controls
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Page root) : base (root)
        {
            BarBackgroundColor = (Color)Application.Current.Resources["Primary"];
            //BarTextColor = (Color)Application.Current.Resources["NavigationText"];
            
            Title = root.Title;
            Icon = root.Icon;
        }
    }
}