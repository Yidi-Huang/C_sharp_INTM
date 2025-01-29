using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_banque_II
{
    public class Compte
    {
        public int id_cpt { get; set; }
        public DateTime date_creation { get; set; }
        public DateTime date_resili { get; set; }
        public decimal solde { get; set; }
        public Dictionary<int, DateTime> id_gs { get; set; }   // le gestionnaire change, enregistre les changems  id - date ou il prend effet
        public Dictionary<DateTime, decimal> his_soldes { get; set; }  // date - solde vire
        public static int nb_comptes {get;set;}

        public Compte(int id, DateTime dc, DateTime dr, decimal s, Dictionary<int, DateTime> gs, Dictionary<DateTime, decimal> hs)
        {
            id_cpt = id;
            date_creation = dc;
            date_resili = dr;
            solde = s;
            id_gs = gs;
            his_soldes = hs;
            nb_comptes++;
        }

        static Compte()
        {
            nb_comptes = 0;
        }
    }
}
