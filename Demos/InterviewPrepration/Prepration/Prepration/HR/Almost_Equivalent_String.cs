using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.HR
{
    class Almost_Equivalent_String
    {

        //Two strings are considered “almost equivalent” if they have the same length AND for each lowercase letter x, the number of occurrences of x in the two strings 
        //differs by no more than 3. There are two string arrays, s and i, that each contains n strings.Strings s[i] and t[i] are the ith pair of strings.
        //They are of equal length and consist of lowercase English letters.For each pair of strings, determine if they are almost equivalent.

        //Example

        //s = ['aabaab', 'aaaaabb']

        //t = ['bbabbc', 'abb']

        //Occurrences of 'a' in s[0] = 4 and in t[0] = 1, difference is 3 

        //Occurrences of 'b' in s[0] = 2 and in t[0] = 4, difference is 2

        //Occurrences of 'c' in s[0] = 0 and in t[0] = 1, difference is 1

        //The number of occurrences of 'a', 'b', and 'c' in the two strings never differs by more than 3. This pair is almost equivalent so the return value for this case is 'YES'.

        //Occurrences of 'a' in s[1] = 5 and in t[1] = 1, difference is 4

        //Occurrences of 'b' in s[1] = 2 and in t[1] = 2, difference is 0

        //The difference in the number of occurrences of 'a' is greater than 3 so the return value for this case is 'NO'.

        //The return array is ['YES', 'NO'].

        //Function Description

        //Complete the function areAlmostEquivalent in the editor below.

        //areAlmostEquivalent has the following parameters:

        //string s[n]:  an array of strings

        //string t[n]:  an array of strings

        //Returns:

        //string[n]: an array of strings, either 'YES' or 'NO' in answer to each test case

        //Constraints

        //1 ≤ n  ≤ 5
        //1 ≤ length of any string in the input ≤ 105

        public static string[] AreAlmostEquivalent(string[] s, string[] t)
        {
            string[] result = new string[s.Length];

            int i = 0;
            while(i < s.Length)
            {
                int[] sf = new int[26];
                int[] tf = new int[26];

                foreach (char c in s[i])
                    sf[c - 'a']++;
                foreach (char c in t[i])
                    tf[c - 'a']++;

                int j = 0;
                bool res = true;
                while( j < sf.Length)
                {
                    if(Math.Abs(sf[j] - tf[j]) > 3)
                    {
                        res = false;
                        break;
                    }
                    j++;
                }
                result[i] = res == true ? "YES" : "NO";
                i++;
            }
            return result;
        }

    }
}
