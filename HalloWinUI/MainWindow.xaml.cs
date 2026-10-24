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

        public void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItemContainer is not null)
            {
                Type? navPageType = Type.GetType(args.SelectedItemContainer.Tag?.ToString() ?? "");
                if (navPageType is null)
                {
                    return;
                }
                NaviguerVers(navPageType, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NaviguerVers(Type navPageType, NavigationTransitionInfo transitionInfo)
        {
            ContentFrame.Navigate(navPageType, null, transitionInfo);
        }

        public void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            EssayerReculer();
        }

        private bool EssayerReculer()
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

        public void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Activer/désactiver le bouton retour
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            if (ContentFrame.SourcePageType is not null)
            {
                // Trouver l'item correspondant à la page actuelle
                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .FirstOrDefault(i => i.Tag.Equals(ContentFrame.SourcePageType.FullName?.ToString()));

                // Afficher le titre de la page dans l'en-tête
                //  lorsque AlwaysShowHeader="True" dans la navigation de MainWindow.xaml
                if (NavView.SelectedItem is null)
                {
                    NavView.Header = "";
                    return;
                }
                NavView.Header = ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
            }
        }

        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Impossible de charger la page " + e.SourcePageType.FullName);
        }
    }
}
