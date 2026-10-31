using HalloWinUI.Data;
using HalloWinUI.Models;
using HalloWinUI.ViewModels;
using HalloWinUI.ViewModels.Models;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace HalloWinUI.ViewModels.Pages
{
    public class MainMaisonsViewModel : BaseViewModel
    {
        private readonly IHalloweenDataProvider _dataProvider;

        public ObservableCollection<MaisonViewModel> Maisons { get; } = new ObservableCollection<MaisonViewModel>();

        private MaisonViewModel _nouvelleMaison = new MaisonViewModel(new Maison());
        public MaisonViewModel NouvelleMaison
        {
            get => this._nouvelleMaison;
            set
            {
                if (this._nouvelleMaison != value)
                {
                    this._nouvelleMaison = value;
                    RaisePropertyChanged();
                }
            }
        }

        private MaisonViewModel? _maisonSelectionnee = null;
        public MaisonViewModel? MaisonSelectionnee
        {
            get => this._maisonSelectionnee;
            set
            {
                if (value != this._maisonSelectionnee)
                {
                    this._maisonSelectionnee = value;
                    RaisePropertyChanged();
                }
            }
        }

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

        public bool NouvelleMaisonExisteDeja
        {
            get => this.Maisons.FirstOrDefault((maison) =>
                        (maison.NumeroCivique == this.NouvelleMaison.NumeroCivique 
                        && maison.Rue.ToLower() == this.NouvelleMaison.Rue.ToLower()
                        && maison.Ville.ToLower() == this.NouvelleMaison.Ville.ToLower())
                
            ) is not null;
        }

        public bool AjouterMaison()
        {
            if (!this.NouvelleMaison.EstValide || this.NouvelleMaisonExisteDeja)
            {
                return false;
            }
            Maison nouvelleMaison = new Maison(this.NouvelleMaison.NumeroCivique, this.NouvelleMaison.Rue, 
                this.NouvelleMaison.Ville, this.NouvelleMaison.CodePostal);
            this.Maisons.Add(new MaisonViewModel(nouvelleMaison));
            this._dataProvider.AjouterMaison(nouvelleMaison);
            this.NouvelleMaison = new MaisonViewModel(new Maison());
            return true;
        }

        public bool SupprimerMaison()
        {
            MaisonViewModel? maisonSupprimee = this.MaisonSelectionnee;
            if (maisonSupprimee is null || !this.Maisons.Contains(maisonSupprimee))
            {
                return false;
            }
            this.Maisons.Remove(maisonSupprimee);
            this._dataProvider.SupprimerMaison(maisonSupprimee.Id);
            return true;
        }
    }
}
