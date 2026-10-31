using HalloWinUI.ViewModels.Pages;
using HalloWinUI.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HalloWinUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjoutEnfantPage : Page
    {
        public MainAjoutEnfantViewModel ViewModel;
        public AjoutEnfantPage()
        {
            this.ViewModel = new MainAjoutEnfantViewModel();
            InitializeComponent();
        }

        public async void btnConfirmerEnfant_Click(object sender, RoutedEventArgs e)
        {
            if (!this.ViewModel.NouvelEnfant.EstValide)
            {
                ContentDialog dialogueErreur = new ContentDialog()
                {
                    Title = "Erreur",
                    Content = "Il y a un champ invalide!",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialogueErreur.ShowAsync();
                return;
            }
            Frame.Navigate(typeof(EnfantsPage), (this.ViewModel.OperationRetour, this.ViewModel.NouvelEnfant));
        }

        public void btnAnnulerAjout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EnfantsPage), null);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.ViewModel.GererParametreNavigation(e.Parameter);
        }
    }
}
