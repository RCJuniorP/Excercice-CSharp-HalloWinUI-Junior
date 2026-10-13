using HalloWinUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWinUI.ViewModels.Pages
{
    public class MaisonViewModel : BaseViewModel
    {
        private readonly Maison _maison;

        public MaisonViewModel(Maison maison)
        {
            this._maison = maison;
        }

        public int Id { 
            get {
                return this._maison.Id; 
            }
        }

        public string NumeroCivique
        {
            get {
                return this._maison.NumeroCivique;
            }
            set {
                if (this._maison.NumeroCivique != value) {
                    this._maison.NumeroCivique = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
