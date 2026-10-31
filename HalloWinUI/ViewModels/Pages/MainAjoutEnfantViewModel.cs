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
        private EnfantViewModel _nouvelEnfant = new EnfantViewModel(new Enfant());
        public EnfantViewModel NouvelEnfant
        {
            get => this._nouvelEnfant;
            set
            {
                if (value != this._nouvelEnfant)
                {
                    this._nouvelEnfant = value;
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

        public string OperationRetour { get => this.OperationActuelle == "Modification" ? this.OperationActuelle + "-" + this.idEnfant.ToString() : this.OperationActuelle; }

        public MainAjoutEnfantViewModel()
        {

        }

        public void GererParametreNavigation(object parametre)
        {
            if (parametre is EnfantViewModel)
            {
                EnfantViewModel enfantModifie = (EnfantViewModel)parametre;
                this.OperationActuelle = "Modification";
                this.idEnfant = enfantModifie.Id;
                this.SousTitrePage = $"Modifier l'enfant #{this.idEnfant}";
                this.NouvelEnfant = enfantModifie;
            }
            else
            {
                this.OperationActuelle = "Addition";
                this.idEnfant = null;
                this.SousTitrePage = "Ajouter un enfant";
                this.NouvelEnfant = new EnfantViewModel(new Enfant());
            }
        }
    }
}
