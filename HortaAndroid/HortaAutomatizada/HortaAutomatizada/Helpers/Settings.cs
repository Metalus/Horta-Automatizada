// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.ComponentModel;

namespace HortaAutomatizada.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings 
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Consumo Settings

        private const string WaterLimitKey = "water_limit_key";
        private static readonly int WaterLimiteDefault = 10; // Litros

        private const string EnergyLimitKey = "energy_limit_key";
        private static readonly double EnergyLimiteDefault = 1200; // Watts
        
        public static bool IsConnected { get; set; }

        #endregion

        public static int WaterLimit
        {
            get { return AppSettings.GetValueOrDefault(WaterLimitKey, WaterLimiteDefault); }
            set { AppSettings.AddOrUpdateValue(WaterLimitKey, value); }
        }

        public static double EnergyLimit
        {
            get { return AppSettings.GetValueOrDefault(EnergyLimitKey, EnergyLimiteDefault); }
            set { AppSettings.AddOrUpdateValue(EnergyLimitKey, value); }
        }
    }
}