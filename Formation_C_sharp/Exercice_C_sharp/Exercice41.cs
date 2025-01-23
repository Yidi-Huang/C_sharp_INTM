using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_C_sharp
{
    class Exercice41
    {

        
        public static int LettersCount(string code)
        {
            int sum = 0;

            int index = code.IndexOf("...");
            while (index != -1 && index < code.Length -3)
            {
                if (code[index+3] != '.' || index + 3 == code.Length)
                {
                    sum++;
                }
                
                index += 3;
                index = code.IndexOf("...", index);
            }
            return sum +1;
        }


        public static int WordsCount(string code)
        {
            int sum = 0;

            int index = code.IndexOf(".....");
            while (index  != -1 && index<code.Length -5)
            {
                sum++;
                index += 5;
                index = code.IndexOf(".....", index);
            }
            return sum+1;
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

        public string EfficientMorseTranslation(string code)
        {
            StringBuilder trans = new StringBuilder();

            code = code.Trim('.');
            code = code.Replace("=..=", "=.=");
            code = code.Replace("=....=", "=...=");

            // +5
            code = ReduceLength(code);

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

        public string MorseEncryption(string sentence)
        {
            StringBuilder trans = new StringBuilder();
            string[] mots = sentence.Split(' ');
            foreach (string mot in mots)
            {
                foreach (char cara in mot)
                {
                    foreach(var pair in _alphabet)
                    {
                        if (pair.Value == cara)
                        {
                            trans.Append(pair.Key);
                            break;
                        }
                    }
                    trans.Append("...");
                }
                trans.Append(".....");
            }
            return ReduceLength(trans.ToString());
        }

        public string ReduceLength(string ch)
        {
            StringBuilder nch = new StringBuilder();
            int c = 0;
            for (int i = 0; i < ch.Length; i++)
            {
                if (ch[i] == '.')
                {
                    c++;
                    if (c <= 5)
                    {
                        nch.Append(ch[i]);
                    }
                }
                else
                {
                    c = 0;
                    nch.Append(ch[i]);
                }
            }

            return nch.ToString();
        }




    }
}
