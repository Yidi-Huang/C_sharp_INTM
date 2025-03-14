﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Projet_Bancaire
{
    public class Compte
    {
        public int id_cpt { get; }
        public decimal solde { get; set; }
        public List<decimal> his_soldes { get; set; }

        public Compte(int ID_C, decimal SOLDE, List<decimal> LS)
        {
            id_cpt = ID_C;
            solde = SOLDE;
            his_soldes = LS;
        }
        public static List<Compte> ChargeCompte(string input)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            List<Compte> comptes = new List<Compte>();

            if (File.Exists(input))
            {
                using (FileStream file = File.Open(input, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        while (!sr.EndOfStream)
                        {
                            string ligne = sr.ReadLine();
                            string[] infos = ligne.Split(';');

                            if (infos[1] != "")
                            {
                                bool estInt1 = int.TryParse(infos[0], out int id_cpt);
                                bool estDec = decimal.TryParse(infos[1], out decimal solde);
                                List<decimal> his_soldes = new List<decimal>();

                                if (estInt1 && estDec && solde >=0)
                                {
                                    Compte compte = new Compte(id_cpt, solde, his_soldes);
                                    comptes.Add(compte);
                                }
                            }
                            else
                            {
                                bool estInt1 = int.TryParse(infos[0], out int id_cpt);
                                decimal solde = 0m;
                                List<decimal> his_soldes = new List<decimal>();

                                if (estInt1)
                                {
                                    Compte compte = new Compte(id_cpt, solde, his_soldes);
                                    comptes.Add(compte);
                                }
                            }
                        }
                    }
                }
            }
            return comptes;
        }

        public void AjoutSolde(decimal somme)
        {
            solde += somme;
        }

        public void RetraitSolde(decimal somme)
        {
            solde -= somme;
            his_soldes.Add(somme);
        }

    }
}
