using System;
using System.Collections.Generic;
using System.IO;

namespace Projet_Bancaire
{
    public class Compte
    {
        public int id_cpt;
        public decimal solde;
        public List<decimal> his_soldes;

        public Compte(int ID_C, decimal SOLDE, List<decimal> LS)
        {
            id_cpt = ID_C;
            solde = SOLDE;
            his_soldes = LS;
        }
        public static List<Compte> ChargeCompte(string input)
        {
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

                                if (estInt1 && estDec)
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
            else
            {
                Console.WriteLine($"File not found: {input}");
            }

            return comptes;
        }

        public string AjoutSolde(decimal somme)
        {
            string status;
            if (somme<0)
            {
                status = "KO";
            }
            else
            {
                solde += somme;
                status = "OK";
            }
            return status;
        }

        public bool evaluateMax(List<decimal> ls, decimal somme)
        {
            decimal sum = 0m;
            bool estInfer = false;
            if (ls.Count >9)
            {
                ls.RemoveAt(0);
            }
            foreach (decimal s in ls)
            {
                sum += s;
            }
            sum += somme;

            if (sum < 1000)
            {
                estInfer = true;
            }
            else
            {
                estInfer = false;
            }
            return estInfer;
        }

        public string RetraitSolde(decimal somme)
        {
            string status;
            if (solde <= somme)                 // 10 derniers virems > 1000
            {
                status = "KO";
            }
            else
            {
                solde -= somme;
                status = "OK";
                his_soldes.Add(somme);
            }
            return status;
        }

    }
}
