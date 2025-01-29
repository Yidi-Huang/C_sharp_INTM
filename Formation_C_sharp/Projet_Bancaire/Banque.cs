using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Projet_Bancaire
{
    public class Banque
    {
        public int id_cpt { get; }
        public Compte compte { get; set; }

        public Banque(int ID_C, Compte LS)
        {
            id_cpt = ID_C;
            compte = LS;
        }

        public static Dictionary<int, Compte> ChargeBanque(string input)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            Dictionary<int, Compte> banques = new Dictionary<int, Compte>();

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

                                    if (! banques.ContainsKey(id_cpt))
                                    {
                                        banques[id_cpt] = compte;
                                    }
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

                                    if (! banques.ContainsKey(id_cpt))
                                    {
                                        banques[id_cpt] = compte ;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return banques;
        }

        public static bool verify_cpt_exist(Dictionary<int,Compte> banques, int id_cpt)
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
