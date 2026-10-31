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
    public class EnfantViewModel : ViewModelValidable
    {
        private readonly Enfant _enfant;
        public EnfantViewModel(Enfant enfant)
        {
            this._enfant = enfant;
            string[] proprietesValidables = ["Nom"];
            foreach (string proprieteValidable in proprietesValidables)
            {
                this._errors.Add(proprieteValidable, new ObservableCollection<ValidationResult>());
                this.Validate(typeof(Enfant).GetProperty(proprieteValidable)?.GetValue(this._enfant, null), proprieteValidable);
                // Voir https://stackoverflow.com/questions/138045/is-there-something-like-pythons-getattr-in-c
            }
        }

        public int Id { get => this._enfant.Id; }

        [Required(ErrorMessage = "Le nom est obligatoire!")]
        [RegularExpression("""^(\p{Lu}|\p{Ll}|[ '-])*$""", ErrorMessage = "Seulement les lettres, espaces, traits d'union et apostrophes sont autorisés!")]
        [Length(2, 50, ErrorMessage = "Le nom doit être de 2 à 50 caractères!")]
        public string Nom
        {
            get => this._enfant.Nom;
            set
            {
                if (value != this._enfant.Nom && Validate(value))
                {
                    this._enfant.Nom = value;
                    RaisePropertyChanged();
                }
            }
        }
        public new IEnumerable GetErrors(string propriete)
        {
            return base.GetErrors(propriete);
        }

    }
}
