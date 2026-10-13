using HalloWinUI.Data;
using HalloWinUI.Models;
using System.Collections.ObjectModel;

namespace HalloWinUI.ViewModels.Pages
{
    public class MainMaisonsViewModel : BaseViewModel
    {
        private readonly IHalloweenDataProvider _dataProvider;

        public ObservableCollection<MaisonViewModel> Maisons { get; } = new ObservableCollection<MaisonViewModel>();

        public MainMaisonsViewModel(IHalloweenDataProvider halloweenDataProvider)
        {
            _dataProvider = halloweenDataProvider;
        }

        public void ChargerDonnees()
        {
            Maisons.Clear();
            foreach (Maison maison in _dataProvider.GetMaisons())
            {
                Maisons.Add(new MaisonViewModel(maison));
            }

        }

    }
}
