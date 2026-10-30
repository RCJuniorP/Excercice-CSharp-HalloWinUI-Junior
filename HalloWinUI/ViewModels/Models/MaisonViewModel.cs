using HalloWinUI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWinUI.ViewModels.Models
{
    public class MaisonViewModel : ViewModelValidable
    {
        private readonly Maison _maison;

        public MaisonViewModel(Maison maison)
        {
            this._maison = maison;
            string[] proprietesValidables = ["NumeroCivique", "Rue", "Ville", "CodePostal", "EstSignalee"];
            foreach (string proprieteValidable in proprietesValidables)
            {
                this._errors.Add(proprieteValidable, new ObservableCollection<ValidationResult>());
            }
        }

        public int Id { 
            get {
                return this._maison.Id; 
            }
        }

        [Required(ErrorMessage = "Le numéro civique est requis!")]
        public string NumeroCivique
        {
            get {
                return this._maison.NumeroCivique;
            }
            set {
                if (this._maison.NumeroCivique != value) {
                    this._maison.NumeroCivique = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Adresse");
                }
            }
        }

        public string Rue
        {
            get {
                return this._maison.Rue;
            }
            set { 
                if (this._maison.Rue != value)
                {
                    this._maison.Rue = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Adresse");
                }
            }
        }

        public string Adresse
        {
            get => this.NumeroCivique + " " + this.Rue;
        }

        public string Ville
        {
            get
            {
                return this._maison.Ville;
            }
            set
            {
                if (this._maison.Ville != value)
                {
                    this._maison.Ville = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Region");
                }
            }
        }

        public string CodePostal
        {
            get
            {
                return this._maison.CodePostal;
            }
            set
            {
                if (this._maison.CodePostal != value)
                {
                    this._maison.CodePostal = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Region");
                }
            }
        }

        public string Region
        {
            get
            {
                if (String.IsNullOrEmpty(this.CodePostal))
                {
                    return this.Ville;
                } else
                {
                    return this.Ville + " (" + this.CodePostal + ")";
                }
            }
        }

        public bool EstSignalee
        {
            get
            {
                return this._maison.EstSignalee;
            }
            set
            {
                if (this._maison.EstSignalee != value)
                {
                    this._maison.EstSignalee = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("StatutSecurite");
                }
            }
        }

        public string StatutSecurite
        {
            get => this.EstSignalee ? "Signalé" : "Sécuritaire";
        }

        public IEnumerable GetErrors(string propriete)
        {
            return base.GetErrors(propriete);
        }
    }
}
