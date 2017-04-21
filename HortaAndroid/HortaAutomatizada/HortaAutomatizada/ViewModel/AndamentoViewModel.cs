using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortaAutomatizada.ViewModel
{
    public class AndamentoViewModel : BaseViewModel
    {
        const double tanqueLimit = 2; // 2L

        double currentTanque = 0;
        public double CurrentTanque
        {
            get { return currentTanque; }
            set
            {
                SetProperty(ref currentTanque, value);
                TanqueRate = value / 100;
            }
        }

        double tanqueRate = 0;

        public double TanqueRate
        {
            get
            {
                return tanqueRate;
            }
            set
            {
                SetProperty(ref tanqueRate, value);
                TanqueInfo = $"{CurrentTanque}%";

            }
        }

        string tanqueInfo;
        public string TanqueInfo
        {
            get { return tanqueInfo ?? (tanqueInfo = $"{CurrentTanque}%"); }
            set { SetProperty(ref tanqueInfo, value); }
        }

        bool luz = false;
        public bool Luz
        {
            get { return luz; }
            set { SetProperty(ref luz, value); }
        }

        const double TempMax = 39;
        double temperatura = 27;
        public double Temperatura
        {
            get { return temperatura; }
            set
            {
                SetProperty(ref temperatura, value);
                TemperaturaRate = value / 100;
            }
        }

        double temperaturaRate = 0;

        public double TemperaturaRate
        {
            get
            { return temperaturaRate; }
            set
            { SetProperty(ref temperaturaRate, value); }
        }



        int umidadeAr = 40;
        public int UmidadeAr
        {
            get { return umidadeAr; }
            set
            {
                SetProperty(ref umidadeAr, value);
                UmidadeArRate = value / 100;
            }
        }

        double umidadeArRate = 40 / 100;

        public double UmidadeArRate
        {
            get
            { return umidadeArRate; }
            set
            { SetProperty(ref umidadeArRate, value); }
        }

        int umidadeSolo = 50;
        public int UmidadeSolo
        {
            get { return umidadeSolo; }
            set
            {
                SetProperty(ref umidadeSolo, value);
                UmidadeSoloRate = value / 100;
            }
        }

        double umidadeSoloRate = 50 / 100;

        public double UmidadeSoloRate
        {
            get
            { return umidadeSoloRate; }
            set
            { SetProperty(ref umidadeSoloRate, value); }
        }

    }
}
