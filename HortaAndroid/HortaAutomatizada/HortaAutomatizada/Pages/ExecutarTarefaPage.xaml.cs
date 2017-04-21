using HortaAutomatizada.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HortaAutomatizada.Pages
{
    public partial class ExecutarTarefaPage : ContentPage
    {
        ExecutarViewModel viewModel;
        public ExecutarTarefaPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ExecutarViewModel();
        }

    }
}
