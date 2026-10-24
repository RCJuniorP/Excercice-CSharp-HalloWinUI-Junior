using HalloWinUI.Data;
using HalloWinUI.Models;
using HalloWinUI.ViewModels.Models;
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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HalloWinUI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EnfantsPage : Page
    {
        MainEnfantsViewModel ViewModel { get; }
        public EnfantsPage()
        {
            this.ViewModel = new MainEnfantsViewModel(new HalloweenDataProvider());
            InitializeComponent();

            this.Loaded += this.ApresChargement;
        }

        public async void ApresChargement(object sender, RoutedEventArgs e) 
        {
            this.ViewModel.ChargerDonnees();
        }

        public void btnAjouterEnfant_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AjoutEnfantPage));
        }

        public async void btnSupprimerEnfant_Click(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel.EnfantSelectionne is null)
            {
                ContentDialog dialogueErreur = new ContentDialog()
                {
                    Title = "Erreur",
                    Content = "Vous devez sélectionner un enfant avant de le supprimer!",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialogueErreur.ShowAsync();
                return;
            }

            ContentDialog dialogueAvertissement = new ContentDialog()
            {
                Title = "Confirmer",
                Content = $"Êtes vous sûr de vouloir supprimer l'enfant '{this.ViewModel.EnfantSelectionne.Nom}' ({this.ViewModel.EnfantSelectionne.Id}) ?",
                PrimaryButtonText = "Continuer",
                CloseButtonText = "Annuler",
                XamlRoot = this.XamlRoot
            };

            ContentDialogResult resultatConfirmation = await dialogueAvertissement.ShowAsync();

            if (resultatConfirmation != ContentDialogResult.Primary)
            {
                return;
            }

            bool resultatSupression = this.ViewModel.SupprimerEnfant();

            if (!resultatSupression)
            {
                ContentDialog dialogueErreur = new ContentDialog()
                {
                    Title = "Erreur",
                    Content = "Il y a eu une erreur lors de la supression!",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialogueErreur.ShowAsync();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is Enfant)
            {
                this.ViewModel.AjouterEnfantFormulaire((Enfant)e.Parameter);
            }
        }
    }
}
