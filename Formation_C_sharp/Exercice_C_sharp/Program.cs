using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_C_sharp
{
    class Program
    {
        static void Main()
        {
            //Console.WriteLine("hello");
            int x = 9;
            int y = 4;
            int z = 0;
            int c = -2;
            char op = '/';
            char op2 = 'L';
            BasicOperation(x, y, op);
            BasicOperation(x, y, op2);

            IntegerDivision(x, y);
            IntegerDivision(x, z);
            IntegerDivision(x, c);

            Pow(x, y);
            Pow(x, c);

            int h = 13;
            string pmsg = Exercice2.GoodDay(h);
            Console.WriteLine($"Il est {h} heure, {pmsg}");
            Console.ReadKey();
        }

        public static void BasicOperation(int a, int b, char operation)
        {
            if (operation == '+')
            {
                Console.WriteLine($"{a} + {b} = {a+b}");
            }
            else if (operation == '-')
            {
                Console.WriteLine($"{a} - {b} = {a - b}");
            }
            else if (operation == '*')
            {
                Console.WriteLine($"{a} * {b} = {a * b}");
            }
            else if (operation == '/')
            {
                if (b != 0)

                {
                    int q = a / b;
                    Console.WriteLine($"{a} / {b} = {q} ");
                }
                else
                {
                    Console.WriteLine($"{a} / {b} = Opération invalide. ");
                }
                
            }
            else
            {
                Console.WriteLine($"{a} {operation} {b} = Opération invalide. ");
            }
        }

        public static void IntegerDivision(int a, int b)
        {
            if (b != 0)
            {
                int q = a / b;
                int r = a % b;
                if (r == 0)
                {
                    Console.WriteLine($"{a} / {b} = {q} ");
                }
                else
                {
                    Console.WriteLine($"{a} / {b} = {q} * {b} + {r} ");
                }

            }
            else
            {
                Console.WriteLine($"{a} / {b} = Opération invalide. ");
            }
        }

        public static void Pow(int a, int b)
        {
            if (b < 0)
            {
                Console.WriteLine($"Opération invalide");
            }
            else
            {
                double p = Math.Pow(a, b);
                Console.WriteLine($"{a} ^ {b} = {p} ");
            }
        }

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
