using HalloWinUI.Models;
using HalloWinUI.ViewModels.Models;
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

        public string OperationActuelle { get; private set; } = "Addition";

        private string _sousTitrePage = "Ajouter un enfant";
        public string SousTitrePage
        {
            get => this._sousTitrePage;
            set
            {
                if (value != this._sousTitrePage)
                {
                    this._sousTitrePage = value;
                    RaisePropertyChanged();
                }
            }
        }

        private int? idEnfant = null;

        public MainAjoutEnfantViewModel()
        {

        }

        public Enfant? CreerEnfant()
        {
            if (String.IsNullOrEmpty(this._nomEnfant))
            {
                return null;
            }
            Enfant nouvelEnfant = new Enfant(this.NomEnfant);
            if (this.idEnfant is not null)
            {
                nouvelEnfant.Id = (int)this.idEnfant;
            }
            return nouvelEnfant;
        }

        public void GererParametreNavigation(object parametre)
        {
            if (parametre is EnfantViewModel)
            {
                EnfantViewModel enfantModifie = (EnfantViewModel)parametre;
                this.OperationActuelle = "Modification";
                this.idEnfant = enfantModifie.Id;
                this.SousTitrePage = $"Modifier l'enfant #{this.idEnfant}";
                this.NomEnfant = enfantModifie.Nom;
            }
            else
            {
                this.OperationActuelle = "Addition";
                this.idEnfant = null;
                this.SousTitrePage = "Ajouter un enfant";
                this.NomEnfant = "";
            }
        }
    }
}
