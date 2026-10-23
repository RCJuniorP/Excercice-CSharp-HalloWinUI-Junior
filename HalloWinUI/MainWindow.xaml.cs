using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System.Linq;
using System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace HalloWinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Code de la démo

        /// <summary>
        /// Gère le changement de sélection dans le menu de navigation.
        /// Navigue vers la page correspondant à l’élément sélectionné.
        /// </summary>
        /// <param name="sender">Le NavigationView source.</param>
        /// <param name="args">Les arguments contenant les informations sur la sélection.</param>
        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer is not null && args.SelectedItemContainer.Tag is not null)
            {
                Type navPageType = Type.GetType(args.SelectedItemContainer.Tag.ToString());
                NavView_Navigate(navPageType, args.RecommendedNavigationTransitionInfo);
            }
        }

        /// <summary>
        /// Effectue la navigation vers une page donnée,
        /// sauf si cette page est déjà affichée (évite une navigation en double lors d'un retour).
        /// </summary>
        /// <param name="navPageType">Le type de la page cible.</param>
        /// <param name="transitionInfo">Les informations sur la transition visuelle entre les pages.</param>
        private void NavView_Navigate(Type navPageType, NavigationTransitionInfo transitionInfo)
        {
            if (navPageType is not null)
            {
                ContentFrame.Navigate(navPageType, null, transitionInfo);
            }
        }

        /// <summary>
        /// Gère la demande de retour en arrière dans le NavigationView.
        /// Appelle <see cref="TryGoBack"/> pour tenter un retour vers la page précédente.
        /// </summary>
        /// <param name="sender">Le NavigationView déclencheur.</param>
        /// <param name="args">Les arguments de l’événement.</param>
        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            TryGoBack();
        }

        /// <summary>
        /// Tente d’effectuer un retour vers la page précédente dans la pile de navigation.
        /// </summary>
        /// <returns>
        /// true si le retour a été effectué avec succès, sinon false.
        /// </returns>
        private bool TryGoBack()
        {
            if (!ContentFrame.CanGoBack)
            {
                return false;
            }
            else
            {
                ContentFrame.GoBack();
                return true;
            }
        }

        /// <summary>
        /// Méthode appelée à chaque fois que la navigation change.
        /// Met à jour l’état du bouton "Retour", la sélection du menu et l’en-tête affiché.
        /// </summary>
        /// <param name="sender">Le Frame qui a navigué.</param>
        /// <param name="e">Les arguments de l’événement de navigation.</param>
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Activer/désactiver le bouton retour
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType is not null)
            {
                // Trouver l'item correspondant à la page actuelle
                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .FirstOrDefault(i => i.Tag.Equals(ContentFrame.SourcePageType.FullName.ToString()));

                // Afficher le titre de la page dans l'en-tête
                //  lorsque AlwaysShowHeader="True" dans la navigation de MainWindow.xaml
                NavView.Header = ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }

        /// <summary>
        /// Gère les erreurs de navigation survenant lors du chargement d’une page dans le <see cref="Frame"/> principal.
        /// Lève une exception si la page cible ne peut pas être chargée.
        /// Le nom de la méthode doit être le même que celui indiqué dans le XAML :
        /// <c>&lt;Frame NavigationFailed="ContentFrame_NavigationFailed" /&gt;</c>.
        /// </summary>
        /// <param name="sender">Le Frame dont la navigation a échoué.</param>
        /// <param name="e">Les arguments contenant le type de la page introuvable.</param>
        /// <exception cref="Exception">Toujours levée, avec le nom de la page introuvable.</exception>
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Impossible de charger la page " + e.SourcePageType.FullName);
        }
    }
}
