using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_Bancaire    // Endroit pour conserver les copies des anciens codes
{
    public class Banque_ancien
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


                            bool estInt = int.TryParse(infos[0], out int id_cpt);
                            banques.Add(id_cpt);

                        }
                    }
                }
            }
            return banques;
        }

        public static bool verify_cpt_exist(List<int> banques, int id_cpt)
        {
            bool estExist = false;

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
