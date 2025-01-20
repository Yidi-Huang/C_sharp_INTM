using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_C_sharp
{
    class Exercice2
    {


        public static string GoodDay(int heure)
        {
            string msg;
            if (heure > 0 && heure < 6)
            {
                msg = "Merveilleuse nuit !";
            }
            else if (heure >= 6 && heure < 12)
            {
                msg = "Bonne matinée !";
            }
            else if (heure == 12)
            {
                msg = "Bon appétit !";
            }
            else if (heure > 12 && heure <= 18)
            {
                msg = "Profitez de votre après-midi !";
            }
            else if (heure > 18 && heure <= 23)
            {
                msg = "Passez une bonne soirée !";
            }
            else
            {
                msg = "Heure invalide.";
            }
            return msg;
        }
    }
}
