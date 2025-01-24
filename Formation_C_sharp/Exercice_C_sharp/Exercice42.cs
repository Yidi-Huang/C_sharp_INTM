using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_C_sharp
{
    class Exercice42    // vérifier le syntaxe des parenthèses
    {
        public static bool BracketsControls(string sentence)
        {
            bool isCorrect = true;
            Stack<char> s = new Stack<char>();
            string po = "([{";
            string pf = ")]}";

            StringBuilder s2 = new StringBuilder();
            foreach (char ele in sentence)
            {
                if (po.Contains(ele) || pf.Contains(ele))
                {
                    s2.Append(ele);
                }
            }

            string sentence2 = s2.ToString();
            foreach (char cara in sentence2)
            {
                if (po.Contains(cara))
                {
                    s.Push(cara);
                }
                else if (pf.Contains(cara))
                {
                    if (s.Count==0)
                    {
                        return false;
                    }
                    char last_o = s.Peek();
                    if (last_o == '(' && cara == ')')
                    {
                        s.Pop();
                    }
                    else if (last_o == '[' && cara == ']')
                    {
                        s.Pop();
                    }
                    else if (last_o == '{' && cara == '}')
                    {
                        s.Pop();
                    }
                    else
                    {
                        return false;
                    }
                    
                }
            }
            if (s.Count!=0)
            {
                isCorrect = false;
            }
            return isCorrect;
        }
    }
}
