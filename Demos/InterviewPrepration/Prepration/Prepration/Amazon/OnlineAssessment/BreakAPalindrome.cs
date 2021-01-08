using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class BreakAPalindrome
    {
        //https://leetcode.com/problems/break-a-palindrome/
        //1328. Break a Palindrome  . Input: palindrome = "abccba" Output: "aaccba" Input: palindrome = "a"  Output: ""
        public string BreakPalindrome(string palindrome)
        {
            if (palindrome.Length == 1)
            {
                return "";
            }

            StringBuilder res = new StringBuilder(palindrome);

            for (int i = 0; i < res.Length; i++)
            {
                if (res[i] != 'a')
                {
                    if (!(res.Length % 2 == 1 && i == res.Length / 2))
                    {
                        res[i] = 'a';
                        return res.ToString();
                    }
                }

            }

            res[res.Length - 1] = 'b';
            return res.ToString();
        }
    }
}
