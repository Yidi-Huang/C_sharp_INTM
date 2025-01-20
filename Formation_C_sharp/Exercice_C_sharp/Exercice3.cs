using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_C_sharp
{
    class Exercice3
    {
        public static void PyramidConstruction(int n, bool isSmooth)
        {
            int sum1=0;
            for (int i=1;i<=n;i++)
            {
                sum1 += 2*i-1;
            }
            Console.WriteLine($"Le nombre de bloc est : {sum1}.");

            Console.WriteLine($"Le nombre total au niveau {n} est : {2*n-1}.");

            
        }
    }
}
