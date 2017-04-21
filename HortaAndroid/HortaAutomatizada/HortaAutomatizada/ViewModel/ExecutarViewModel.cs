using HortaAutomatizada.Modules;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HortaAutomatizada.ViewModel
{
    public class ExecutarViewModel : BaseViewModel
    {
        string temperaturaMax;
        public string TemperaturaMax
        {
            get { return temperaturaMax; }
            set { SetProperty(ref temperaturaMax, value); }
        }

        TimeSpan endTime;
        public TimeSpan EndTime
        {
            get { return endTime; }
            set { SetProperty(ref endTime, value); }
        }

        string umidadeSoloMax;
        public string UmidadeSoloMax
        {
            get { return umidadeSoloMax; }
            set { SetProperty(ref umidadeSoloMax, value); }
        }

        string umidadeSoloMin;
        public string UmidadeSoloMin
        {
            get { return umidadeSoloMin; }
            set { SetProperty(ref umidadeSoloMin, value); }
        }

        string umidadeArMax;
        public string UmidadeArMax
        {
            get { return umidadeArMax; }
            set { SetProperty(ref umidadeArMax, value); }
        }

        string umidadeArMin;
        public string UmidadeArMin
        {
            get { return umidadeArMin; }
            set { SetProperty(ref umidadeArMin, value); }
        }

        private Command sendDataCommand;

        public Command SendDataCommand =>
            sendDataCommand ?? (sendDataCommand = new Command(async() =>  await SendDataAsync()));

        async Task SendDataAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            ulong epochEnd = App.GetEpochTime(DateTime.Now + EndTime);

            string data = $"{PacketType.ET}={epochEnd};"+
                          $"{PacketType.TM}={TemperaturaMax};"+
                          $"{PacketType.UM}={UmidadeArMax};"+
                          $"{PacketType.UI}={UmidadeArMin};"+
                          $"{PacketType.SM}={UmidadeSoloMax};"+
                          $"{PacketType.SI}={UmidadeSoloMin};";

            App.Socket.SendData.Execute(Encoding.UTF8.GetBytes(data));
            await Task.Delay(3000);
            IsBusy = false;
        }
    }
}
