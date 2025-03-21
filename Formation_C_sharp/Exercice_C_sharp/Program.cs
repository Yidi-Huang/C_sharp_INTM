﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            //string pmsg = Exercice12.GoodDay(h);
            //Console.WriteLine($"Il est {h} heure, {pmsg}");

            //bool isS = false; 
            //Exercice13.PyramidConstruction(x, isS);
            //Console.WriteLine("  ");

            //int res = Exercice14.Factorial(y);
            //Console.WriteLine($"Factorial est {res}.");
            //int res2 = Exercice14.FactorialRecursive(y);
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
            //int sum31 = Exercice41.LettersCount(ch);
            //Console.WriteLine($"Le nombre de lettres dans cette phrase est : {sum31}.");
            //int sum32 = Exercice41.WordsCount(ch);
            //Console.WriteLine($"Le nombre de mots dans cette phrase est : {sum32}.");
            Morse k = new Morse();
            string phrase = k.MorseTranslation(ch);
            Console.WriteLine(phrase);

            //string ch3 = "===.=.===.=....===..===..===...===.=.=...=.......===.===";
            //string p3 = Morse.EfficientMorseTranslation(ch3);
            //Console.WriteLine(p3);

            //string ch2 = "CODE M";
            //string p2 = Morse.MorseEncryption(ch2);
            //Console.WriteLine(p2);

            //string ch42 = "{[2+4]}*(3-6)";
            //bool isOk = Exercice42.BracketsControls(ch42);
            //Console.WriteLine(isOk);

            string infile = "C:\\Users\\Formation\\Downloads\\Notes.csv";
            string outfile = "C:\\Users\\Formation\\Downloads\\Moyenne.csv";
            Exercice31.SchoolMeans(infile, outfile);


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


}
