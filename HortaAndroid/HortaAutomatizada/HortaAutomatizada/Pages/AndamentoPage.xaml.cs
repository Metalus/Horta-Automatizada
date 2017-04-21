using HortaAutomatizada.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HortaAutomatizada.Pages
{
    public partial class AndamentoPage : ContentPage
    {
        AndamentoViewModel vm;
        public AndamentoPage()
        {
            InitializeComponent();
            BindingContext = vm = new AndamentoViewModel();
        }
    }
}
