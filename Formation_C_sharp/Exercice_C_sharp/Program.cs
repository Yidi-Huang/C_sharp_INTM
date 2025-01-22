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
            //int x = 9;
            //int y = 4;
            //int z = 0;
            //int c = -2;
            //char op = '/';
            //char op2 = 'L';
            //BasicOperation(x, y, op);
            //BasicOperation(x, y, op2);

            //IntegerDivision(x, y);
            //IntegerDivision(x, z);
            //IntegerDivision(x, c);

            //Pow(x, y);
            //Pow(x, c);

            //int h = 13;
            //string pmsg = Exercice2.GoodDay(h);
            //Console.WriteLine($"Il est {h} heure, {pmsg}");

            //bool isS = false; 
            //Exercice3.PyramidConstruction(x, isS);
            //Console.WriteLine("  ");

            //int res = Exercice4.Factorial(y);
            //Console.WriteLine($"Factorial est {res}.");
            //int res2 = Exercice4.FactorialRecursive(y);
            //Console.WriteLine($"Factorial est {res2}.");
            //Console.WriteLine("  ");


            //int[] tab = new int [] { -4, -5, 10, -3, 0, 4, 1, -7 };
            //int n21 = 1;
            //int indice = Exercice21.LinearSearch(tab, n21);
            //Console.WriteLine($"L'indice de {n21} est {indice}.");

            //Array.Sort(tab);
            //int indice2 = Exercice21.BinarySearch(tab, n21);
            //Console.WriteLine($"L'indice de {n21} est {indice2}.");
            //Console.WriteLine("  ");

            //int[] tb1 = { 1, 2, 3 };
            //int[] tb2 = { -1, -4, 0 };
            //int[] tb3 = { 1, 1, 1 };
            //int[,] matrice1 = Exercice22.BuildingMatrix(tb1, tb2);
            //int[,] matrice2 = Exercice22.BuildingMatrix(tb1, tb3);

            //Console.WriteLine("  ");
            //int[,] matrice_a = Exercice22.Addition(matrice1, matrice2);
            //int[,] matrice_s = Exercice22.Substraction(matrice1, matrice2);

            //Exercice22.DisplayMatrix(matrice_a);
            //Exercice22.DisplayMatrix(matrice_s);

            //int[,] matrice_m = Exercice22.Multiplication(matrice_a, matrice_s);
            //Exercice22.DisplayMatrix(matrice_m);

            string ch = "===.=.===.=...===.===.===...===.=.=...=.....===.===";
            int sum31 = Exercice31.LettersCount(ch);
            Console.WriteLine($"Le nombre de lettres dans cette phrase est : {sum31}.");
            int sum32 = Exercice31.WordsCount(ch);
            Console.WriteLine($"Le nombre de mots dans cette phrase est : {sum32}.");
            Morse Morse = new Morse();
            string phrase = Morse.MorseTranslation(ch);
            Console.WriteLine(phrase);

            string ch3 = "===.=.===.=....===..===..===...===.=.=...=.....";
            string p2 = Morse.EfficientMorseTranslation(ch3);
            Console.WriteLine(p2); ;


            Console.ReadKey();
        }

        public static void BasicOperation(int a, int b, char operation)
        {
            if (operation == '+')
            {
                Console.WriteLine($"{a} + {b} = {a + b}");
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

    public class Morse
    {
        private const string Taah = "===";
        private const string Ti = "=";
        private const string Point = ".";
        private const string PointLetter = "...";
        private const string PointWord = ".....";

        private readonly Dictionary<string, char> _alphabet;

        public Morse()
        {
            _alphabet = new Dictionary<string, char>()
            {
                {$"{Ti}.{Taah}", 'A'},
                {$"{Taah}.{Ti}.{Ti}.{Ti}", 'B'},
                {$"{Taah}.{Ti}.{Taah}.{Ti}", 'C'},
                {$"{Taah}.{Ti}.{Ti}", 'D'},
                {$"{Ti}", 'E'},
                {$"{Ti}.{Ti}.{Taah}.{Ti}", 'F'},
                {$"{Taah}.{Taah}.{Ti}", 'G'},
                {$"{Ti}.{Ti}.{Ti}.{Ti}", 'H'},
                {$"{Ti}.{Ti}", 'I'},
                {$"{Ti}.{Taah}.{Taah}.{Taah}", 'J'},
                {$"{Taah}.{Ti}.{Taah}", 'K'},
                {$"{Ti}.{Taah}.{Ti}.{Ti}", 'L'},
                {$"{Taah}.{Taah}", 'M'},
                {$"{Taah}.{Ti}", 'N'},
                {$"{Taah}.{Taah}.{Taah}", 'O'},
                {$"{Ti}.{Taah}.{Taah}.{Ti}", 'P'},
                {$"{Taah}.{Taah}.{Ti}.{Taah}", 'Q'},
                {$"{Ti}.{Taah}.{Ti}", 'R'},
                {$"{Ti}.{Ti}.{Ti}", 'S'},
                {$"{Taah}", 'T'},
                {$"{Ti}.{Ti}.{Taah}", 'U'},
                {$"{Ti}.{Ti}.{Ti}.{Taah}", 'V'},
                {$"{Ti}.{Taah}.{Taah}", 'W'},
                {$"{Taah}.{Ti}.{Ti}.{Taah}", 'X'},
                {$"{Taah}.{Ti}.{Taah}.{Taah}", 'Y'},
                {$"{Taah}.{Taah}.{Ti}.{Ti}", 'Z'},
            };
        }

        public string MorseTranslation(string code)
        {
            StringBuilder trans = new StringBuilder();

            string[] mots = code.Split(new string[] { "....." },StringSplitOptions.RemoveEmptyEntries);
            foreach (string mot in mots)
            {
                string[] caras = mot.Split(new string[] {"..."}, StringSplitOptions.RemoveEmptyEntries);
                foreach (string cara in caras)
                {
                    bool isFind = _alphabet.TryGetValue(cara, out char lettre);
                    if (isFind)
                    {
                        trans.Append(lettre);
                    }
                    else
                    {
                        trans.Append('?');
                    }  
                }
                trans.Append(' ');
            }
            return trans.ToString();
        }

        public string EfficientMorseTranslation(string code)
        {
            StringBuilder trans = new StringBuilder();

            code = code.Trim();
            code = code.Replace("=..=", "=.=");
            code = code.Replace("=....=", "=...=");
            // +5 .

            string[] mots = code.Split(new string[] { "....." }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string mot in mots)
            {
                string[] caras = mot.Split(new string[] { "..." }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string cara in caras)
                {
                    bool isFind = _alphabet.TryGetValue(cara, out char lettre);
                    if (isFind)
                    {
                        trans.Append(lettre);
                    }
                    else
                    {
                        trans.Append('?');
                    }
                }
                trans.Append(' ');
            }
            return trans.ToString();
        }




    }
}
