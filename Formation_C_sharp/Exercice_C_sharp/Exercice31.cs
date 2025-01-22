using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_C_sharp
{
    class Exercice31
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
}
