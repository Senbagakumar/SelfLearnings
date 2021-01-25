using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Prepration.Microsoft.OnlineAssessment
{
    class PatternRecognition
    {
        //https://leetcode.com/discuss/interview-question/398023/Microsoft-Online-Assessment-Questions

        //https://leetcode.com/discuss/interview-question/928806/

        //Programming challenge description:
        //Given a pattern as the first argument and a string of blobs split by | show the number of times the pattern is present in each blob and the total number of matches.

        //Input:
        //The input consists of the pattern (“bc” in the example) which is separated by a semicolon followed by a list of blobs(“bcdefbcbebc|abcdebcfgsdf|cbdbesfbcy|1bcdef23423bc32” in the example). Example input: bc;bcdefbcbebc|abcdebcfgsdf|cbdbesfbcy|1bcdef23423bc32

        //Output:
        //The output should consist of the number of occurrences of the pattern per blob(separated by |). Additionally, the final entry should be the summation of all the occurrences(also separated by |).

        //Example output: 3|2|1|2|8 where ‘bc’ was repeated 3 times, 2 times, 1 time, 2 times in the 4 blobs passed in. And 8 is the summation of all the occurrences(3+2+1+2 = 8)

        //Test 1
        //Test Input
        //Download Test 1 Input
        //bc; bcdefbcbebc|abcdebcfgsdf|cbdbesfbcy|1bcdef23423bc32
        // Expected Output
        // Download Test 1 Input
        //3|2|1|2|8

        public static string GetCount(string input)
        {
            string pattern = input.Split(';')[0];
            string remainInput = input.Replace(pattern + ";", "");
            string[] inputs = remainInput.Split('|');

            int totalSum = 0;
            string result = string.Empty;
            List<int> res = new List<int>();

            if (pattern.Length == 0)
            {
                foreach (var st in inputs)
                    result += totalSum + "|";

                result += totalSum;
            }
            else
            {
                foreach (var st in inputs)
                {
                    int count = GetMatchesCount(st, pattern);//Regex.Matches(st, pattern).Count;
                    result += count + "|";
                    totalSum += count;
                }
                result += totalSum;
            }
            return result;
        }

        static int GetMatchesCount(string input, string pattern)
        {
            int count = 0;
            int j = 0;
            int k = pattern.Length;
            while(k <= input.Length)
            {
                string substring = input.Substring(j, k-j);
                if (pattern == substring)
                {
                    count++;
                }
                k++;
                j++;
            }
            return count;
        }
    }
}
