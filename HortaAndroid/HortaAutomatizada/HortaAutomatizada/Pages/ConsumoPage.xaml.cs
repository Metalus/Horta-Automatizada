using HortaAutomatizada.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace HortaAutomatizada.Pages
{
    public partial class ConsumoPage : ContentPage
    {
        ConsumoViewModel viewModel;
        public ConsumoPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ConsumoViewModel();

        }

        private void WaterLimitChanged(object sender, ValueChangedEventArgs e)
        {
            int NewValue = Convert.ToInt32(e.NewValue);
            if (viewModel.WaterLimit != NewValue)
                viewModel.WaterLimit = NewValue;
        }

        private void ApplyLimit(object sender, EventArgs e)
        {
            double value = Convert.ToDouble(LimitEnergy.Text);
            viewModel.EnergyLimit = value;
            viewModel.ApplyLimitEnergyCommand.Execute(null);
        }
    }
}
