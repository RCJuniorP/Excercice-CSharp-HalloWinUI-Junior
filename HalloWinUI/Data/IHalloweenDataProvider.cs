using HalloWinUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWinUI.Data
{
    public interface IHalloweenDataProvider
    {
        List<Maison> GetMaisons();
        List<Enfant> GetEnfants();
        void AjouterMaison(Maison maison);
        void SupprimerMaison(int maisonId);
        void ModifierMaison(Maison maison);
        void AjouterEnfant(Enfant enfant);
        void SupprimerEnfant(int enfantId);
        void ModifierEnfant(Enfant enfant);
    }
}
