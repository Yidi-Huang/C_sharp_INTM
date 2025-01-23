using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_C_sharp
{
    class Exercice42
    {
        public static bool BracketsControls(string sentence)
        {
            bool isCorrect = true;
            var s = new Stack<char>();
            string po = "([{";
            string pf = ")]}";
            foreach (char cara in sentence)
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
