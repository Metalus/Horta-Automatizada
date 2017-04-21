using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HortaAutomatizada.Controls
{
    public class CustomTimePicker24 : TimePicker
    {
        public static BindableProperty SufixProperty = BindableProperty.Create(nameof(Sufix), typeof(string), typeof(CustomTimePicker24), string.Empty);
        public static BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomTimePicker24), string.Empty);
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Sufix
        {
            get { return (string)GetValue(SufixProperty); }
            set { SetValue(SufixProperty, value); }
        }

       /* protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == TimeProperty.PropertyName)
              Post
        }*/
    }
}
