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

            Console.WriteLine($"La position du sommet est : {n}, {n}.");

            int j=1;
            
            for (;j<=n;j++)
            { 
                string s = new String(' ',n-j);
                string b;
                if (isSmooth)    
                {
                    b = new String('*',2*j-1);
                    Console.WriteLine(s+b+s);
                }
                else
                {
                    if (j%2==0)
                    {
                        b = new String('-',2*j-1);
                    }
                    else
                    {
                        b = new String('*',2*j-1);
                    }
                    Console.WriteLine(s+b+s);
                }
            }
        }


    }
}
