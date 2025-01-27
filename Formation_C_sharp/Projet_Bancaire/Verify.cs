using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Bancaire
{
    class Verify
    {
        static public bool verify_cpt_exist(string infile, int id_cpt)
        {
            bool estExist = false;

            List<int> banques = Banque.ChargeBanque(infile);

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

        static public void trans(string infile)
        {
            



        }
    }
}
