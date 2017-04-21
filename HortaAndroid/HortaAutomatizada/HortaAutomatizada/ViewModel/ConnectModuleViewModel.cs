using FormsToolkit;
using MvvmHelpers;
using Sockets.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HortaAutomatizada.ViewModel
{
    public class ConnectModuleViewModel : BaseViewModel
    {
        TcpSocketClient socket;
        string ip = "192.168.0.2";
        public string IP
        {
            get { return ip; }
            set { SetProperty(ref ip, value); }
        }
        const int Port = 8090;
        bool searchingModules = false;
        public bool SearchingModules
        {
            get { return searchingModules; }
            set { SetProperty(ref searchingModules, value); }
        }

        Command connectModule;

        public Command ConnectModule =>
            connectModule ?? (connectModule = new Command(async () => await ConnectModuleAsync()));
        public ConnectModuleViewModel()
        {
            socket = new TcpSocketClient();
        }



        private async Task ConnectModuleAsync()
        {
            SearchingModules = true;
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    await socket.ConnectAsync(IP, Port);
                    if (socket.WriteStream.CanWrite)
                        break;
                }
                catch { }

               // await Task.Delay(2000);
            }
            if (socket.WriteStream.CanWrite)
            {
                App.Socket.socket = socket;
                MessagingService.Current.SendMessage(MessageKeys.ConnectModule, new MessagingServiceConnectModule(IP, Port));
                MessagingService.Current.SendMessage(MessageKeys.CloseConnectModule);
            }
            else
                await App.Current.MainPage.DisplayAlert("Erro ao conectar", "Erro ao se conectar ao módulo WiFi\nVerifique o IP.", "OK");
            SearchingModules = false;
        }
    }
}
