using HalloWinUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWinUI.ViewModels.Models
{
    public class EnfantViewModel : BaseViewModel
    {
        private readonly Enfant _enfant;
        public EnfantViewModel(Enfant enfant)
        {
            this._enfant = enfant;
        }

        public int Id { get => this._enfant.Id; }

        public string Nom
        {
            get => this._enfant.Nom;
            set
            {
                if (value != this._enfant.Nom)
                {
                    this._enfant.Nom = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
