using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Android.Support.Design.Widget;
using Xamarin.Forms;
using System.ComponentModel;
using HortaAutomatizada.Controls;
using HortaAutomatizada.Model;
using HortaAutomatizada.Droid.Renders;

[assembly: ExportRenderer(typeof(CustomNavigationView), typeof(NavigationViewRenderer))]
namespace HortaAutomatizada.Droid.Renders
{
    public class NavigationViewRenderer : ViewRenderer<CustomNavigationView, NavigationView>
    {
        NavigationView navView;
        TextView profileName;
        ImageView profileImage;
        IMenuItem previousItem;
        protected override void OnElementChanged(ElementChangedEventArgs<CustomNavigationView> e)
        {
            base.OnElementChanged(e);
            if ( Element == null)
                return;

            var view = Inflate(Forms.Context, Resource.Layout.nav_view, null);
            navView = view.JavaCast<NavigationView>();

            navView.NavigationItemSelected += NavView_NavigationItemSelected;
            SetNativeControl(navView);

            var header = navView.GetHeaderView(0);
            profileName = header.FindViewById<TextView>(Resource.Id.profile_name);
            profileImage = header.FindViewById<ImageView>(Resource.Id.profile_image);

            profileName.Click += (sender, e2) => NavigateLogin();
            profileImage.Click += (sender, e2) => NavigateLogin();

            UpdateImage();
            navView.SetCheckedItem(Resource.Id.nav_consumo);
        }

        private void UpdateImage()
        {
            Koush.UrlImageViewHelper.SetUrlDrawable(profileImage, "profile_icon.png", Resource.Drawable.profile_icon);
        }
        public override void OnViewRemoved(Android.Views.View child)
        {
            base.OnViewRemoved(child);
            navView.NavigationItemSelected -= NavView_NavigationItemSelected;
        }

        private void NavigateLogin()
        {
            // Soon
        }

        private void NavView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            if (previousItem != null)
                previousItem.SetChecked(false);

            navView.SetCheckedItem(e.MenuItem.ItemId);

            previousItem = e.MenuItem;

            NavigationPagesEnum page = NavigationPagesEnum.Consumo;
            switch (previousItem.ItemId)
            {
                case Resource.Id.nav_agendarTarefa:
                    page = NavigationPagesEnum.AgendarTarefa;
                    break;
                case Resource.Id.nav_andamento:
                    page = NavigationPagesEnum.Andamento;
                    break;
                case Resource.Id.nav_consumo:
                    page = NavigationPagesEnum.Consumo;
                    break;
                case Resource.Id.nav_executarTarefa:
                    page = NavigationPagesEnum.ExecutarTarefa;
                    break;
                case Resource.Id.nav_sobreArduino:
                    page = NavigationPagesEnum.SobreArduino;
                    break;
                case Resource.Id.nav_sobreDisciplina:
                    page = NavigationPagesEnum.SobreDisciplina;
                    break;
                case Resource.Id.nav_sobreGrupo:
                    page = NavigationPagesEnum.SobreGrupo;
                    break;
                case Resource.Id.nav_sobreSoftAndroid:
                    page = NavigationPagesEnum.SobreSoftAndroid;
                    break;

            }

            this.Element.OnNavigationViewItemSelected(new NavigationViewItemSelectedEventArgs
            {
                Page = page
            });
        }
    }
}