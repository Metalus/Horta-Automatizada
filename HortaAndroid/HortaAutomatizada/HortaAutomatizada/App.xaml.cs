using FormsToolkit;
using HortaAutomatizada.Abstractions;
using HortaAutomatizada.Helpers;
using HortaAutomatizada.Modules;
using HortaAutomatizada.Pages;
using HortaAutomatizada.Pages.Android;
using HortaAutomatizada.ViewModel;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HortaAutomatizada
{
    public partial class App : Application
    {
        bool registered;
        public static SocketTcp Socket { get; set; }
        List<int> ValoresSensores = new List<int>(6);

        public App()
        {
            InitializeComponent();
            Socket = new SocketTcp();
            MainPage = new HortaAutomatizada.Pages.Android.RootPage();
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            if (!registered)
                return;

            registered = false;
            MessagingService.Current.Unsubscribe<MessagingServiceConnectModule>(MessageKeys.ConnectModule);
            MessagingService.Current.Unsubscribe(MessageKeys.NavigateConnectModule);
            MessagingService.Current.Unsubscribe<MessagingServiceDataReceived>(MessageKeys.DataReceived);
            CrossConnectivity.Current.ConnectivityChanged -= Current_ConnectivityChanged;
        }

        public static ulong GetEpochTime(DateTime time)
        {
            TimeSpan t = time.ToUniversalTime() - new DateTime(1970, 1, 1);
            return (ulong)t.TotalSeconds;
        }

        protected override void OnResume()
        {
            if (registered)
                return;
            registered = true;
            Settings.IsConnected = CrossConnectivity.Current.ConnectionTypes.Contains(ConnectionType.WiFi);
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

            MessagingService.Current.Subscribe<MessagingServiceConnectModule>(MessageKeys.ConnectModule, async (m, q) =>
            {
                await Socket.Connect(q.IP, q.Port);
                if (!Socket.Connected)
                    await MainPage.DisplayAlert("Erro de conexão", "Não foi possível se conectar ao módulo da horta.", "OK");
                else
                {
                    ulong currentTime = GetEpochTime(DateTime.Now);
                    string data = $"{PacketType.CT.ToString()}={currentTime.ToString()};"
                                + $"{PacketType.LW.ToString()}={Settings.WaterLimit};";

                    Socket.SendData.Execute(Encoding.UTF8.GetBytes(data));

                }
            });

            MessagingService.Current.Subscribe(MessageKeys.NavigateConnectModule, async m =>
            {
                ((RootPage)MainPage).IsPresented = false;
                Page page = new ConnectModulePage();

                await MainPage.Navigation.PushModalAsync(page);
            });


            MessagingService.Current.Subscribe<MessagingServiceDataReceived>(MessageKeys.DataReceived, (m, q) =>
            {
                byte[] data = q.Data;
                string rawString = Encoding.UTF8.GetString(data, 0, data.Length);

                RootPage root = MainPage as RootPage;
                PacketType type = (PacketType)Enum.Parse(typeof(PacketType), rawString.Substring(0, 2), true);
                if (root.pages[Model.NavigationPagesEnum.Andamento].BindingContext == null && type != PacketType.WU)
                    return;
                switch (type)
                {
                    case PacketType.TA: //Volume Tanque
                        {
                            AndamentoViewModel vm = root.pages[Model.NavigationPagesEnum.Andamento].BindingContext as AndamentoViewModel;
                            int Value = Convert.ToInt32(rawString.Substring(3, rawString.Length));
                            vm.CurrentTanque = Value;
                            break;
                        }
                    case PacketType.WU: //Uso de água
                        {
                            ConsumoViewModel vm = root.pages[Model.NavigationPagesEnum.Consumo].BindingContext as ConsumoViewModel;
                            vm.CurrentWater = Convert.ToInt32(rawString.Substring(3, rawString.Length));
                            break;
                        }
                    case PacketType.LU: //Luz bool
                        {
                            AndamentoViewModel vm = root.pages[Model.NavigationPagesEnum.Andamento].BindingContext as AndamentoViewModel;
                            vm.Luz = Convert.ToBoolean(Convert.ToInt32(rawString.Substring(3, rawString.Length)));
                            break;
                        }
                    case PacketType.UA:// Umidade Ar
                        {
                            AndamentoViewModel vm = root.pages[Model.NavigationPagesEnum.Andamento].BindingContext as AndamentoViewModel;
                            vm.UmidadeAr = Convert.ToInt32(rawString.Substring(3, rawString.Length));
                            break;
                        }
                    case PacketType.US: // Umidade solo
                        {
                            AndamentoViewModel vm = root.pages[Model.NavigationPagesEnum.Andamento].BindingContext as AndamentoViewModel;
                            vm.UmidadeSolo = Convert.ToInt32(rawString.Substring(3, rawString.Length));
                            break;
                        }
                    case PacketType.TP: // Temperatura
                        {
                            AndamentoViewModel vm = root.pages[Model.NavigationPagesEnum.Andamento].BindingContext as AndamentoViewModel;
                            vm.Temperatura = Convert.ToInt32(rawString.Substring(3, rawString.Length));
                            break;
                        }
                }

                //MessagingService.Current.SendMessage(MessageKeys.Sensors, new MessagingServiceSensores(ValoresSensores));
            });
        }

        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            Settings.IsConnected = CrossConnectivity.Current.ConnectionTypes.Contains(ConnectionType.WiFi);
            if (!Settings.IsConnected)
            {
                await MainPage.DisplayAlert("WiFi desconectado", "Ligue o WiFi novamente e ligue-o à rede.", "OK");
                DependencyService.Get<IWifi>().CallWifiContext();
            }

            MessagingService.Current.SendMessage(MessageKeys.NavigateConnectModule);
        }
    }
}
