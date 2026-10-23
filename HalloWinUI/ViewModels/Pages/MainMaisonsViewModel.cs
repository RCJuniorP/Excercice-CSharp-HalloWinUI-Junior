using HalloWinUI.Data;
using HalloWinUI.Models;
using HalloWinUI.ViewModels;
using HalloWinUI.ViewModels.Models;
using System.Collections.ObjectModel;

namespace HalloWinUI.ViewModels.Pages
{
    public class MainMaisonsViewModel : BaseViewModel
    {
        private readonly IHalloweenDataProvider _dataProvider;

        public ObservableCollection<MaisonViewModel> Maisons { get; } = new ObservableCollection<MaisonViewModel>();
        
        private string _formNomRue = "";
        public string FormNomRue { 
            get => this._formNomRue; 
            set {
               if (value != this._formNomRue)
                {
                    this._formNomRue = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _formNumeroRue = "";
        public string FormNumeroRue
        {
            get => this._formNumeroRue;
            set
            {
                if (value != this._formNumeroRue)
                {
                    this._formNumeroRue = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _formVille = "";
        public string FormVille
        {
            get => this._formVille;
            set
            {
                if (value != this._formVille)
                {
                    this._formVille = value;
                    RaisePropertyChanged();
                }
            }
        }


        private string _formCodePostal = "";
        public string FormCodePostal
        {
            get => this._formCodePostal;
            set
            {
                if (value != this._formCodePostal)
                {
                    this._formCodePostal = value;
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

        public bool AjouterMaison()
        {
            if (string.IsNullOrEmpty(this.FormNumeroRue) || string.IsNullOrEmpty(this.FormNomRue) || string.IsNullOrEmpty(this.FormVille))
            {
                return false;
            }
            MaisonViewModel nouvelleMaison = new MaisonViewModel(new Maison(this.FormNumeroRue, this.FormNomRue, this.FormVille, this.FormCodePostal));
            this.Maisons.Add(nouvelleMaison);
            return true;
        }

        public bool SupprimerMaison()
        {
            if (this.MaisonSelectionnee is null || !this.Maisons.Contains(this.MaisonSelectionnee))
            {
                return false;
            }
            this.Maisons.Remove(this.MaisonSelectionnee);
            return true;
        }
    }
}
