using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
   
    class stringprograms
    {
        //1. Given s = "the sky is blue", return "blue is sky the".
        public static void reverseWords(char[] s)
        {
            int i = 0;
            for (int j = 0; j < s.Length; j++)
            {
                if (s[j] == ' ')
                {
                    reverse(s, i, j - 1);
                    i = j + 1;
                }
            }

            reverse(s, i, s.Length - 1);

            reverse(s, 0, s.Length - 1);
        }

        private static void reverse(char[] s, int i, int j)
        {
            while (i < j)
            {
                char temp = s[i];
                s[i] = s[j];
                s[j] = temp;
                i++;
                j--;
            }
        }

        //https://www.programcreek.com/2012/12/leetcode-evaluate-reverse-polish-notation/
        //2. ["2", "1", "+", "3", "*"] -> ((2 + 1) * 3) -> 9

        public static int CalculateValues()
        {
            string[] inputs = new string[] { "2","1","+","3","*" };
            string operators = "+/*-";
            int result = 0;
            var stack = new Stack<string>();
            foreach(var inp in inputs)
            {
                if (!operators.Contains(inp))
                    stack.Push(inp);
                else
                {
                    int first = int.Parse(stack.Pop());
                    int second = int.Parse(stack.Pop());

                    switch(inp)
                    {
                        case "+":
                           stack.Push((first + second).ToString());
                            break;
                        case "-":
                            stack.Push(Math.Abs(first - second).ToString());
                            break;
                        case "*":
                            stack.Push((first * second).ToString());
                            break;
                        case "/":
                            stack.Push((first / second).ToString());
                            break;
                    }
                }
            }
            result= Convert.ToInt16(stack.Pop());
            return result;

        }

        //3. MiniParser  Given s = "[123,[456,[789]]]"
        //https://www.programcreek.com/2014/08/leetcode-mini-parser-java/
      


    }
}
