using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projet_banque_III
{
    public class Gestionnaire
    {
        public int id_gs;
        public string type_gs;
        public int nb_trs;
        public decimal frais_gs;
        public List<int> comptes { get; set; }


        public Gestionnaire(int id, string tp, int nb, decimal fg, List<int> cs)
        {
            id_gs = id;
            type_gs = tp;
            nb_trs = nb;
            frais_gs = fg;
            comptes = cs;

        }

        public static Dictionary<int, Gestionnaire> ChargeGestionnaire(string input)
        {
            Dictionary<int, Gestionnaire> gestionnaires = new Dictionary<int, Gestionnaire>();
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

                            if (infos.Length >= 3)
                            {
                                bool estInt1 = int.TryParse(infos[0], out int id_gs);
                                bool estInt2 = int.TryParse(infos[2], out int nb_trs);
                                decimal frais_gs = 0m;
                                List<int> comptes = new List<int>();

                                if (estInt1 && estInt2 && (infos[1] == "Particulier" || infos[1] == "Entreprise"))
                                {
                                    Gestionnaire gestionnaire_iv = new Gestionnaire(id_gs, infos[1], nb_trs, frais_gs, comptes);

                                    if (!gestionnaires.ContainsKey(id_gs))
                                    {
                                        gestionnaires[id_gs] = gestionnaire_iv;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return gestionnaires;
        }
    }
}
