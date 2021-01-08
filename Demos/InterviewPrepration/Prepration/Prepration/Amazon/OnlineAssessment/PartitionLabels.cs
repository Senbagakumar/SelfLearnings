using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class PartitionLabels
    {
        //https://leetcode.com/problems/partition-labels/

        //Input: S = "ababcbacadefegdehijhklij"
        //Output: [9,7,8]
        //Explanation:
        //The partition is "ababcbaca", "defegde", "hijhklij".
        //This is a partition so that each letter appears in at most one part.
        //A partition like "ababcbacadefegde", "hijhklij" is incorrect, because it splits S into less parts.


        public static List<int> StringPartion(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            var result = new List<int>();
            int[] lastIndices = new int[26];

            for(int i=0; i<s.Length; i++)
            {
                lastIndices[s[i] - 'a'] = i;
            }

            int start = 0, end = 0;

            for(int i=0; i<s.Length; i++)
            {
                end = Math.Max(end, lastIndices[s[i] - 'a']);

                if(i == end)
                {
                    result.Add(end - start + 1);
                    start = end + 1;
                }
            }
            return result;
        }
    }
}
