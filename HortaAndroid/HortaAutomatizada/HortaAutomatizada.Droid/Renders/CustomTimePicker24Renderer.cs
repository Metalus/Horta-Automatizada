using System;

using Android.App;
using Android.Runtime;
using Xamarin.Forms;
using HortaAutomatizada.Controls;
using HortaAutomatizada.Droid.Renders;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomTimePicker24), typeof(CustomTimePicker24Renderer))]
namespace HortaAutomatizada.Droid.Renders
{
    public class CustomTimePicker24Renderer : ViewRenderer<CustomTimePicker24, Android.Widget.EditText>, 
                                      TimePickerDialog.IOnTimeSetListener, IJavaObject, IDisposable
    {
        private TimePickerDialog dialog = null;
        string Sufix = string.Empty;
        protected override void OnElementChanged(ElementChangedEventArgs<CustomTimePicker24> e)
        {
            base.OnElementChanged(e);
            this.SetNativeControl(new Android.Widget.EditText(Forms.Context));
            this.Control.Click += Control_Click;
            this.Control.Text = e.NewElement.Text;
            this.Sufix = e.NewElement.Sufix;
            this.Control.KeyListener = null;
            this.Control.FocusChange += Control_FocusChange;
        }

        void Control_FocusChange(object sender, Android.Views.View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
                ShowTimePicker();
        }

        void Control_Click(object sender, EventArgs e)
        {
            ShowTimePicker();
        }

        private void ShowTimePicker()
        {
            if (dialog == null)
                dialog = new TimePickerDialog(Forms.Context, this, 0, 0, true);

            dialog.Show();
        }

        public void OnTimeSet(Android.Widget.TimePicker view, int hourOfDay, int minute)
        {
            var time = new TimeSpan(hourOfDay, minute, 0);
            this.Element.SetValue(TimePicker.TimeProperty, time);

            this.Control.Text = $"{time.Hours} hora(s) e {time.Minutes} minuto(s) de {Sufix}";
   
        }
    }
}
