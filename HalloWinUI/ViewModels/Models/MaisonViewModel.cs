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
                this.Validate(typeof(Maison).GetProperty(proprieteValidable)?.GetValue(this._maison, null), proprieteValidable);
                // Voir https://stackoverflow.com/questions/138045/is-there-something-like-pythons-getattr-in-c
            }
        }

        public int Id { 
            get {
                return this._maison.Id; 
            }
        }

        [Required(ErrorMessage = "Le numéro civique est obligatoire!")]
        [RegularExpression("""^\d{1,6}$""", ErrorMessage = "Le numéro civique doit contenir 1 à 6 chiffres et seulement des chiffres!")]
        public string NumeroCivique
        {
            get {
                return this._maison.NumeroCivique;
            }
            set {
                if (this._maison.NumeroCivique != value && Validate(value)) {
                    this._maison.NumeroCivique = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Adresse");
                }
            }
        }

        [Required(ErrorMessage = "La rue est obligatoire!")]
        [Length(3, 100, ErrorMessage ="La rue doit être de 3 à 100 caractères!")]
        public string Rue
        {
            get {
                return this._maison.Rue;
            }
            set { 
                if (this._maison.Rue != value && Validate(value))
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

        [Required(ErrorMessage = "La ville est obligatoire!")]
        [RegularExpression("""^(\p{Lu}|\p{Ll}|[ '-])*$""", ErrorMessage = "Seulement les lettres, espaces, traits d'union et apostrophes sont autorisés!")]
        [Length(2, 50, ErrorMessage = "La ville doit être de 2 à 50 caractères!")]
        public string Ville
        {
            get
            {
                return this._maison.Ville;
            }
            set
            {
                if (this._maison.Ville != value && Validate(value))
                {
                    this._maison.Ville = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged("Region");
                }
            }
        }

        [Required(ErrorMessage = "Le code postal est obligatoire!")]
        [RegularExpression("""[a-zA-Z]\d[a-zA-Z]( )?\d[a-zA-Z]\d""", ErrorMessage = "Le code postal n'a pas été entré avec un format valide! (ex : A9A 9A9)")]
        public string CodePostal
        {
            get
            {
                return this._maison.CodePostal;
            }
            set
            {
                if (this._maison.CodePostal != value && Validate(value))
                {
                    if (value.Length == 6)
                    {
                        value = value.Substring(0, 3) + " " + value.Substring(3, 3);
                    }
                    this._maison.CodePostal = value.ToUpper();
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
                if (this._maison.EstSignalee != value && Validate(value))
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

        public new IEnumerable GetErrors(string propriete)
        {
            return base.GetErrors(propriete);
        }

    }
}
