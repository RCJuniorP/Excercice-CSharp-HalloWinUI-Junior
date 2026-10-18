using HalloWinUI.Data;
using HalloWinUI.ViewModels.Pages;
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
    public sealed partial class MaisonsPage : Page
    {
        public MainMaisonsViewModel ViewModel { get; }
        public MaisonsPage()
        {
            ViewModel = new MainMaisonsViewModel(new HalloweenDataProvider());
            InitializeComponent();

            this.Loaded += this.MaisonPage_Loaded;
        }

        public void MaisonPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.ChargerDonnees();
        }

        private async void btnAjouterMaison_Click(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.AjouterMaison())
            {
                ContentDialog dialog = new ContentDialog()
                {
                    Title = "Erreur",
                    Content = "Il manque des information(s) obligatoires!",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                await dialog.ShowAsync();
            }
        }

        private async void btnSupprimerMaison_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.MaisonSelectionnee is null)
            {
                ContentDialog dialogueErreur = new ContentDialog()
                {
                    Title = "Erreur",
                    Content = "Vous devez sélectionner une maison à supprimer!",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                await dialogueErreur.ShowAsync();
                return;
            }

            ContentDialog dialogueConfirmation = new ContentDialog()
            {
                Title = "Erreur",
                Content = $"Êtes vous sûr de vouloir supprimer la maison {ViewModel.MaisonSelectionnee.Adresse} à {ViewModel.MaisonSelectionnee.Region} ?",
                PrimaryButtonText = "Supprimer",
                CloseButtonText = "Annuler",
                XamlRoot = this.Content.XamlRoot
            };
            ContentDialogResult resultat = await dialogueConfirmation.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                bool resultatSuppression = ViewModel.SupprimerMaison();
                if (!resultatSuppression)
                {
                    ContentDialog dialogueErreur = new ContentDialog()
                    {
                        Title = "Erreur",
                        Content = "Il y a eu une erreur lors de la supression! Elle n'a pas été effectuée.",
                        CloseButtonText = "OK",
                        XamlRoot = this.Content.XamlRoot
                    };
                    await dialogueErreur.ShowAsync();
                    return;
                }
            }
            return;

        }
    }
}
