using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_banque_III
{
    public class Compte
    {
        public int id_cpt { get; set; }
        public string type { get; set; }
        public DateTime date_creation { get; set; }
        public DateTime date_resili { get; set; }
        public decimal solde { get; set; }
        public List<Tuple<DateTime, int>> id_gs { get; set; }   // le gestionnaire change, enregistre : Date - id gestionnaire
        public List<Tuple<DateTime, decimal>> his_soldes { get; set; }  // date - solde vire
        public static int nb_comptes { get; set; }
        //public int age { get; internal set; }

        public Compte(int id, string tp,DateTime dc, DateTime dr, decimal s, List<Tuple<DateTime, int>> gs, List<Tuple<DateTime, decimal>> hs)
        {
            id_cpt = id;
            type = tp;
            date_creation = dc;
            date_resili = dr;
            solde = s;
            id_gs = gs;
            his_soldes = hs;
            nb_comptes++;
        }

    }

    public class CompteJeune : Compte
    {
        public int age;
        public CompteJeune(int id, string tp, DateTime dc, DateTime dr, decimal s, List<Tuple<DateTime, int>> gs, List<Tuple<DateTime, decimal>> hs, int ag) 
            : base(id, tp, dc, dr, s, gs, hs)
        {
            age = ag;
        }
    }

    public class CompteLivret : Compte
    {
        public decimal interet;
        public CompteLivret(int id, string tp, DateTime dc, DateTime dr, decimal s, List<Tuple<DateTime, int>> gs, List<Tuple<DateTime, decimal>> hs, decimal i)
            : base(id, tp, dc, dr, s, gs, hs)
        { interet = i; }
    }

    public class CompteATerme : Compte
    {
        public decimal interet;
        public DateTime date_fin;
        public CompteATerme(int id, string tp, DateTime dc, DateTime dr, decimal s, List<Tuple<DateTime, int>> gs, List<Tuple<DateTime, decimal>> hs, decimal i, DateTime df)
            : base(id, tp, dc, dr, s, gs, hs)
        { 
            interet = i;
            date_fin = df;
        }
    }
}
