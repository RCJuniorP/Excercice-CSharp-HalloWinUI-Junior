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

        private EnfantViewModel? _enfantSelectionne = null;

        public EnfantViewModel? EnfantSelectionne
        {
            get => this._enfantSelectionne;
            set
            {
                if (value != this._enfantSelectionne)
                {
                    this._enfantSelectionne = value;
                    RaisePropertyChanged();
                }
            }
        }


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

        public bool AjouterNouvelEnfantFormulaire(EnfantViewModel nouvelEnfant)
        {
            this._dataProvider.AjouterEnfant(new Enfant(nouvelEnfant.Nom));
            return true;
        }

        public bool ModifierNouvelEnfantFormulaire(EnfantViewModel nouvelEnfant, int idEnfant)
        {
            Enfant enfantModifie = new Enfant(nouvelEnfant.Nom);
            enfantModifie.Id = idEnfant;
            this._dataProvider.ModifierEnfant(enfantModifie);
            return true;
            
        }

        public bool SupprimerEnfant()
        {
            EnfantViewModel? enfantSupprime = this.EnfantSelectionne;
            if (enfantSupprime is null || !this.Enfants.Contains(enfantSupprime))
            {
                return false;
            }
            this.Enfants.Remove(enfantSupprime);
            this._dataProvider.SupprimerEnfant(enfantSupprime.Id);
            return true;
        }
    }
}
