using HalloWinUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWinUI.ViewModels.Pages // TODO : Déterminer si c'est la page AjoutEnfant ou la page Enfants qui ajoute les enfants
{
    public class MainAjoutEnfantViewModel : BaseViewModel
    {
        private string _nomEnfant = "";
        public string NomEnfant
        {
            get => this._nomEnfant;
            set
            {
                if (value != this._nomEnfant)
                {
                    this._nomEnfant = value;
                    RaisePropertyChanged();
                }
            }
        }

        public MainAjoutEnfantViewModel()
        {

        }

        public Enfant? CreerEnfant()
        {
            if (String.IsNullOrEmpty(this._nomEnfant))
            {
                return null;
            }
            return new Enfant(this._nomEnfant);
        }
    }
}
