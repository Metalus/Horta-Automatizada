using HortaAutomatizada.Helpers;
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
    public class ConsumoViewModel : BaseViewModel
    {
        #region Water
        bool changeLimitWater = false;
        public bool ChangeLimitWater
        {
            get { return changeLimitWater; }
            set { SetProperty(ref changeLimitWater, value); }
        }

        int waterLimit = Settings.WaterLimit;
        public int WaterLimit
        {
            get { return waterLimit; }
            set
            {
                SetProperty(ref waterLimit, value);
                WaterRate = CurrentWater / waterLimit;
            }
        }

        double currentWater = 4;
        public double CurrentWater
        {
            get { return currentWater; }
            set
            {
                SetProperty(ref currentWater, value);
                WaterRate = currentWater / waterLimit;
            }
        }

        double waterRate = 0;
        public double WaterRate
        {
            get
            {
                return waterRate == 0 ? (CurrentWater / WaterLimit)
                                      : (waterRate > 1 ? 1 : waterRate);
            }
            set
            {
                SetProperty(ref waterRate, value);
                WaterInfo = $"{currentWater}L / {waterLimit}L";
            }
        }


        double tanqueLimit = 2; // 2L

        double currentTanque;
        public double CurrentTanque
        {
            get { return currentTanque; }
            set { SetProperty(ref currentTanque, value); }
        }

        double tanqueRate = 0;

        public double TanqueRate
        {
            get
            {
                return tanqueRate == 0 ? (CurrentTanque / tanqueLimit)
                                       : (tanqueRate > 1 ? 1 : tanqueRate);
            }
            set
            {
                SetProperty(ref tanqueRate, value);
                TanqueInfo = $"{currentTanque}L / {tanqueLimit}L";

            }
        }

        string tanqueInfo;
        public string TanqueInfo
        {
            get { return tanqueInfo ?? (tanqueInfo = $"{CurrentTanque}L / {tanqueLimit}L"); }
            set { SetProperty(ref tanqueInfo, value); }
        }
        

        private string waterInfo;

        public string WaterInfo
        {
            get { return waterInfo ?? (waterInfo = $"{CurrentWater}L / {WaterLimit}L"); }
            set { SetProperty(ref waterInfo, value); }
        }


        #endregion

        #region Energy
        bool changeLimitEnergy = false;
        public bool ChangeLimitEnergy
        {
            get { return changeLimitEnergy; }
            set { SetProperty(ref changeLimitEnergy, value); }
        }

        double energyLimit = Settings.EnergyLimit;
        public double EnergyLimit
        {
            get { return energyLimit; }
            set
            {
                SetProperty(ref energyLimit, value);
                EnergyRate = CurrentEnergy / energyLimit;
            }
        }

        double currentEnergy = 450;
        public double CurrentEnergy
        {
            get { return currentEnergy; }
            set
            {
                SetProperty(ref currentEnergy, value);
                EnergyRate = currentEnergy / energyLimit;
            }
        }

        double energyRate = 0;
        public double EnergyRate
        {
            get
            {
                return energyRate == 0 ? (CurrentEnergy / EnergyLimit)
                                      : (energyRate > 1 ? 1 : energyRate);
            }
            set
            {
                SetProperty(ref energyRate, value);
                EnergyInfo = $"{currentEnergy}W / {energyLimit}W";
            }
        }

        private string energyInfo;

        public string EnergyInfo
        {
            get { return energyInfo ?? (energyInfo = $"{CurrentEnergy}W / {EnergyLimit}W"); }
            set { SetProperty(ref energyInfo, value); }
        }
        #endregion

        #region Commands

        private Command changeLimitWaterCommand;

        public Command ChangeLimitWaterCommand =>
            changeLimitWaterCommand ?? (changeLimitWaterCommand = new Command(async () => await ChangeLimitWaterAsync()));

        private Command applyLimitWaterCommand;

        public Command ApplyLimitWaterCommand =>
            applyLimitWaterCommand ?? (applyLimitWaterCommand = new Command(async () => await ApplyLimitWaterAsync()));

        private Command changeLimitEnergyCommand;

        public Command ChangeLimitEnergyCommand =>
            changeLimitEnergyCommand ?? (changeLimitEnergyCommand = new Command(async () => await ChangeLimitEnergyAsync()));

        private Command applyLimitEnergyCommand;

        public Command ApplyLimitEnergyCommand =>
            applyLimitEnergyCommand ?? (applyLimitEnergyCommand = new Command(async () => await ApplyLimitEnergyAsync()));
        #endregion


        async Task ChangeLimitWaterAsync() => await Task.Run(() => ChangeLimitWater = !ChangeLimitWater);
        async Task ApplyLimitWaterAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            Settings.WaterLimit = WaterLimit;
            //App.Socket.SendData.Execute(Encoding.UTF8.GetBytes("Limite de água alterado."));
            await ChangeLimitWaterAsync();
            IsBusy = false;
        }


        async Task ChangeLimitEnergyAsync() => await Task.Run(() => ChangeLimitEnergy = !ChangeLimitEnergy);
        async Task ApplyLimitEnergyAsync()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            Settings.EnergyLimit = EnergyLimit;

            await ChangeLimitEnergyAsync();
            IsBusy = false;
        }
    }
}

