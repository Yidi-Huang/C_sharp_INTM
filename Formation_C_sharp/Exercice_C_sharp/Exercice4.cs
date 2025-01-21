using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_C_sharp
{
    class Exercice4
    {
        public static int Factorial(int n)
        {
            int res = 1;

            if (n < 0)
            {
                Console.WriteLine("Input invalide.");
            }
            else if (n == 0)
            {
                res = 1;
            }
            else
            {
                for (int i = 1; i <= n; i++)
                {
                    res *= i;
                }
            }
            return res;
        }

        public static int FactorialRecursive(int n)
        {
            if (n < 0)
            {
                return -1;
            }
            else if (n == 0)
            {
                return 1;
            }
            return n * FactorialRecursive(n - 1);
        }


    }
}
