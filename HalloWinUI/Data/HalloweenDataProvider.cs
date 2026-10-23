using HalloWinUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWinUI.Data
{
    public class HalloweenDataProvider : IHalloweenDataProvider
    {
        private readonly List<Maison> _maisons = new List<Maison>();
        private int _prochainIdMaison = 1;

        private readonly List<Enfant> _enfants = new List<Enfant>();
        private int _prochainIdEnfant = 1;

        public HalloweenDataProvider()
        {
            InitialiserDonnees();
        }

        private void InitialiserDonnees()
        {
            AjouterMaison(new Maison("123", "Rue des Fantômes", "Gatineau", estSignalee: true));
            AjouterMaison(new Maison("456", "Avenue des Citrouilles", "Gatineau", codePostal:"J81 5R2"));
            AjouterMaison(new Maison("789", "Boulevard Hanté", "Gatineau"));
            AjouterMaison(new Maison("321", "Chemin des Sorcières", "Gatineau"));

            AjouterEnfant(new Enfant("Nom extrêmement long"));
            AjouterEnfant(new Enfant("Nom très long"));
            AjouterEnfant(new Enfant("Nom très très long"));
        }

        public List<Maison> GetMaisons()
        {
            // Retourne une copie : la liste interne ne peut pas être modifiée de l'extérieur
            return new List<Maison>(_maisons);
        }

        public void AjouterMaison(Maison maison)
        {
            ArgumentNullException.ThrowIfNull(maison);

            maison.Id = _prochainIdMaison++;
            _maisons.Add(maison);
        }

        public void SupprimerMaison(int maisonId)
        {
            Maison? maison = _maisons.FirstOrDefault(m => m.Id == maisonId);
            if (maison != null)
            {
                _maisons.Remove(maison);
            }
        }

        public void ModifierMaison(Maison maison)
        {
            Maison? existante = _maisons.FirstOrDefault(m => m.Id == maison.Id);
            if (existante != null)
            {
                existante.NumeroCivique = maison.NumeroCivique;
                existante.Rue = maison.Rue;
                existante.Ville = maison.Ville;
                existante.CodePostal = maison.CodePostal;
                existante.EstSignalee = maison.EstSignalee;
            }
        }

        public List<Enfant> GetEnfants()
        {
            return new(this._enfants);
        }

        public void AjouterEnfant(Enfant enfant)
        {
            enfant.Id = this._prochainIdEnfant++;
            this._enfants.Add(enfant);
        }

        public void SupprimerEnfant(int enfantId)
        {
            Enfant? enfant = this._enfants.FirstOrDefault<Enfant>(enfant => enfant.Id == enfantId);
            if (enfant is not null)
            {
                this._enfants.Remove(enfant);
            }
        }

        public void ModifierEnfant(Enfant enfantModifie)
        {
            Enfant? enfantConcerne = this._enfants.FirstOrDefault(enfant => enfant.Id == enfantModifie.Id);
            if (enfantConcerne is not null)
            {
                enfantConcerne.Nom = enfantModifie.Nom;
            }
        }

    }
}
