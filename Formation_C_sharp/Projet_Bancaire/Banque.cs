using System;
using System.Collections.Generic;
using System.IO;

namespace Projet_Bancaire
{
    public class Banque
    {
        public int id_cpt;
        public List<Compte> compte;

        // Constructor to initialize a Banque instance
        public Banque(int ID_C, List<Compte> LS)
        {
            id_cpt = ID_C;
            compte = LS;
        }

        // Static method to load a dictionary of Banque from a file
        public static Dictionary<int, List<Compte>> ChargeBanque(string input)
        {
            // Dictionary to store id_cpt as the key and List<Compte> as the value
            Dictionary<int, List<Compte>> banques = new Dictionary<int, List<Compte>>();

            // Check if the file exists
            if (File.Exists(input))
            {
                using (FileStream file = File.Open(input, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        while (!sr.EndOfStream)
                        {
                            string ligne = sr.ReadLine();
                            string[] infos = ligne.Split(';'); // Splitting by semicolon

                            if (infos[1] != "")
                            {
                                // Parsing the account ID and balance
                                bool estInt1 = int.TryParse(infos[0], out int id_cpt);
                                bool estDec = decimal.TryParse(infos[1], out decimal solde);
                                List<decimal> his_soldes = new List<decimal>();

                                if (estInt1 && estDec)
                                {
                                    // Creating a Compte and adding it to the dictionary
                                    Compte compte = new Compte(id_cpt, solde, his_soldes);

                                    // If the dictionary already has this id_cpt, add the compte to its list
                                    if (banques.ContainsKey(id_cpt))
                                    {
                                        banques[id_cpt].Add(compte);
                                    }
                                    else
                                    {
                                        // Otherwise, create a new list and add the compte to it
                                        banques[id_cpt] = new List<Compte> { compte };
                                    }
                                }
                            }
                            else
                            {
                                // Handle case where there might not be a balance
                                bool estInt1 = int.TryParse(infos[0], out int id_cpt);
                                decimal solde = 0m; // Default balance is 0
                                List<decimal> his_soldes = new List<decimal>();

                                if (estInt1)
                                {
                                    // Creating a Compte with default balance and adding to the dictionary
                                    Compte compte = new Compte(id_cpt, solde, his_soldes);

                                    // If the dictionary already has this id_cpt, add the compte to its list
                                    if (banques.ContainsKey(id_cpt))
                                    {
                                        banques[id_cpt].Add(compte);
                                    }
                                    else
                                    {
                                        // Otherwise, create a new list and add the compte to it
                                        banques[id_cpt] = new List<Compte> { compte };
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return banques;
        }

        public static bool verify_cpt_exist(Dictionary<int,List<Compte>> banques, int id_cpt)
        {
            if (banques.ContainsKey(id_cpt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
