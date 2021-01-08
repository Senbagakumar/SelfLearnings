using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class DivisiblityOfString
    {
        //https://leetcode.com/discuss/general-discussion/680341/Divisibility-of-Strings

        //String s1 = "bcdbcdbcd", s2 = "bcdbcd"; ans: -1
        //String s3 = "bcdbcdbcdbcd", s4 = "bcdbcd"; ans: 3
        //String s5 = "aaaaaa", s6 = "a"; ans= 1

        private static int Solve(string s1, string s2)
        {
            if (s1.Length % s2.Length != 0)
                return -1;
            int l2 = s2.Length;
            for (int i = 0; i < s1.Length; i++)
            {
                int s2p = i % l2;
                if (s1[i] != s2[s2p])
                    return -1;
            }
            for (int i = 0; i < s2.Length; i++)
            {
                int j = 0;
                for (; j < s2.Length; j++)
                {
                    int s2pos = j % (i + 1);
                    if (s2[j] != s2[s2pos])
                        break;
                }
                if (j == s2.Length)
                {
                    return i + 1;
                }
            }
            return -1;
        }
    }
}
