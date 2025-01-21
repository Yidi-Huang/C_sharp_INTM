using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_C_sharp
{
    class Exercice21
    {
        public static int LinearSearch(int[] tableau, int v)
        {
            bool isFind = false;
            int indice = 0;
            for (int i=0; i < tableau.Length;i++)
            {
                if (tableau[i] == v)
                {
                    indice=i;
                    isFind = true;
                    break;
                }
            }

            if (isFind)
            {
                return indice;
            }
            else
            {
                return -1;
            }

        }

        public static int BinarySearch(int[] ntable, int v)
        {
            int gauche = 0 ;
            int droite = ntable.Length - 1;
            int m = (gauche + droite) / 2;


            if (ntable[m]==v)
            {
                return m;
            }
            else if (ntable[m]<v)
            {
                gauche = m + 1;
            }
            else
            {
                droite = m + 1;
            }
            return -1;

        }
    }
}
