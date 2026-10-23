using HalloWinUI.Data;
using HalloWinUI.ViewModels.Models;
using HalloWinUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWinUI.ViewModels.Pages
{
    public class MainEnfantsViewModel : BaseViewModel
    {
        private readonly IHalloweenDataProvider _dataProvider;
        public ObservableCollection<EnfantViewModel> Enfants { get; } = new();
        public MainEnfantsViewModel(IHalloweenDataProvider halloweenDataProvider)
        {
            this._dataProvider = halloweenDataProvider;
        }

        public void ChargerDonnees()
        {
            this.Enfants.Clear();
            foreach (Enfant enfant in this._dataProvider.GetEnfants())
            {
                this.Enfants.Add(new EnfantViewModel(enfant));
            }
        }
    }
}
