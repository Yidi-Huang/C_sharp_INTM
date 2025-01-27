using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Bancaire
{
    public class Banque
    {
        public static List<int> banques = new List<int>();

        public static List<int> ChargeBanque(string input)
        {

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


                            bool estInt =  int.TryParse(infos[0], out int id_cpt);
                            banques.Add(id_cpt);

                        }
                    }
                }
            }
            return banques;
        }

        static public bool verify_cpt_exist(string infile, int id_cpt)
        {
            bool estExist = false;

            List<int> banques = ChargeBanque(infile);

            if (banques.Contains(id_cpt) || id_cpt == 0)
            {
                estExist = true;
            }
            else
            {
                estExist = false;
            }

            return estExist;
        }
    }
}
