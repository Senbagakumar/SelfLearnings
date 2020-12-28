using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class SubstringsOfSizeKwithK_1DistinctChars
    {
        //Substrings of Size K with K-1 Distinct Chars	
        //https://leetcode.com/discuss/interview-question/370112/
        //Given a string s and an int k, return all unique substrings of s of size k with k distinct characters.

        //Example 1:

        //Input: s = "abcabc", k = 3
        //Output: ["abc", "bca", "cab"]
        //        Example 2:

        //Input: s = "abacab", k = 3
        //Output: ["bac", "cab"]
        //        Example 3:

        //Input: s = "awaglknagawunagwkwagl", k = 4
        //Output: ["wagl", "aglk", "glkn", "lkna", "knag", "gawu", "awun", "wuna", "unag", "nagw", "agwk", "kwag"]
        //        Explanation: 
        //Substrings in order are: "wagl", "aglk", "glkn", "lkna", "knag", "gawu", "awun", "wuna", "unag", "nagw", "agwk", "kwag", "wagl" 
        //"wagl" is repeated twice, but is included in the output once.


        public static List<string> GetMaxSubstring(string input, int size)
        {
            var result = new List<string>();
            int[] ar = new int[128];
            int j = 0;
            for (int i = 0; i < input.Length; i++)
            {
                j = Math.Max(j, ar[input[i]]);
                if (i - j + 1 == size)
                {
                    var temp = input.Substring(j, size);
                    if (!result.Contains(temp))
                    {
                        result.Add(temp);
                        j++;
                    }
                }
                ar[input[i]] = (i + 1);
            }
            return result;
        }
    }
}
