using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloWinUI.Models
{
    public class Visite
    {
        public int Id { get; set; }
        public int EnfantId { get; set; }
        public int MaisonId { get; set; }
        public DateTime DateVisite { get; set; }

        public Visite(int enfantId, int maisonId, DateTime dateVisite)
        {
            EnfantId = enfantId;
            MaisonId = maisonId;
            DateVisite = dateVisite;
        }

    }
}
