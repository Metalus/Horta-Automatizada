using FormsToolkit;
using HortaAutomatizada.Controls;
using HortaAutomatizada.Model;
using HortaAutomatizada.Pages.Sobre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HortaAutomatizada.Pages.Android
{
    public class RootPage : MasterDetailPage
    {
        public Dictionary<NavigationPagesEnum, CustomNavigationPage> pages;
        public RootPage()
        {
            pages = new Dictionary<NavigationPagesEnum, CustomNavigationPage>();
            Master = new MenuPage(this);
            pages.Add(NavigationPagesEnum.Consumo, new CustomNavigationPage(new ConsumoPage()));
            Detail = pages[NavigationPagesEnum.Consumo];
        }

        public async Task NavigateAsync(NavigationPagesEnum Page)
        {
            CustomNavigationPage newPage = null;

            if (!pages.ContainsKey(Page))
            {
                switch (Page)
                {
                    case NavigationPagesEnum.AgendarTarefa:
                        newPage = new CustomNavigationPage(new AgendarTarefaPage());
                        break;

                    case NavigationPagesEnum.Consumo:
                        newPage = new CustomNavigationPage(new ConsumoPage());
                        break;
                    case NavigationPagesEnum.Andamento:
                        newPage = new CustomNavigationPage(new AndamentoPage());
                        break;
                    case NavigationPagesEnum.ExecutarTarefa:
                        newPage = new CustomNavigationPage(new ExecutarTarefaPage());
                        break;
                    case NavigationPagesEnum.SobreArduino:
                        newPage = new CustomNavigationPage(new SobreArduinoPage());
                        break;
                    case NavigationPagesEnum.SobreDisciplina:
                        newPage = new CustomNavigationPage(new SobreDisciplinaPage());
                        break;
                    case NavigationPagesEnum.SobreGrupo:
                        newPage = new CustomNavigationPage(new SobreGrupoPage());
                        break;
                    case NavigationPagesEnum.SobreSoftAndroid:
                        newPage = new CustomNavigationPage(new SobreSoftAndroidPage());
                        break;
                }
            }

            if (newPage == null) // Page already in cache
                newPage = pages[Page];

            if (Detail == newPage) // refresh current page
                await newPage.Navigation.PopToRootAsync();

            Detail = newPage;
        }
        bool firstRun = true;
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (firstRun)
                MessagingService.Current.SendMessage(MessageKeys.NavigateConnectModule);

            firstRun = false;
        }
    }
}