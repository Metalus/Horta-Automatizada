using FormsToolkit;
using HortaAutomatizada.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HortaAutomatizada.Pages
{
    public partial class ConnectModulePage : ContentPage
    {
        ConnectModuleViewModel vm;
        public ConnectModulePage()
        {
            MessagingService.Current.Subscribe(MessageKeys.CloseConnectModule, async m =>
            {
                await this.Navigation.PopModalAsync();
            });
            InitializeComponent();
            BindingContext = vm = new ConnectModuleViewModel();
            //vm.ConnectModule.Execute(null);
        }

        protected override bool OnBackButtonPressed()
        {
            DisplayAlert("Horta Automatizada", "O aplicativo não pode prosseguir sem se conectar ao módulo\nSomente pode ser fechado", "OK");
            return true;
        }
    }
}
