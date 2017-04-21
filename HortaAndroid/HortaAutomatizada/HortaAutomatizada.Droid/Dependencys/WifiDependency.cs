using Android.Content;
using Xamarin.Forms;
using HortaAutomatizada.Droid.Dependencys;
using HortaAutomatizada.Abstractions;
using System.Net.NetworkInformation;
using System;
using System.Threading.Tasks;
using System.Net;

[assembly: Dependency(typeof(WifiDependency))]
namespace HortaAutomatizada.Droid.Dependencys
{
    public class WifiDependency : IWifi
    {
        public void CallWifiContext()
        {
            Forms.Context.StartActivity(new Intent(Android.Provider.Settings.ActionWifiSettings));

        }
    }
}