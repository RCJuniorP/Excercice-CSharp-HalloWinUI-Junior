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
                }
            }
        }
    }
}
