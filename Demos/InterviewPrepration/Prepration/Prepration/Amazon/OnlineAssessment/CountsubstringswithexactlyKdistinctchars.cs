using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class CountsubstringswithexactlyKdistinctchars
    {
        //https://leetcode.com/discuss/interview-question/370157

        //Input: s = "pqpqs", k = 2
        //Output: 7
        //Explanation: ["pq", "pqp", "pqpq", "qp", "qpq", "pq", "qs"]

        //        Input: s = "aabab", k = 3
        //Output: 0

        //Time Limit Exceeded;
        //https://www.geeksforgeeks.org/count-number-of-substrings-with-exactly-k-distinct-characters/
        
        public int SubstringKdistinctCharacter(string input, int k)
        {
            List<char> list = new List<char>();
            int i = 0;
            int j = 0;
            int result = 0;
            while (i < input.Length)
            {
                if (!list.Contains(input[i]))
                {
                    if (list.Count < k)
                        list.Add(input[i]);
                    else
                    {
                        list.Clear();
                        i = j;
                        j++;

                    }
                }
                if (list.Count == k)
                    result++;

                if (i == input.Length - 1 && j < input.Length - 1)
                {
                    list.Clear();
                    i = j;
                    j++;
                }
                i++;
            }
            return result;
        }

        //from Discussion
        //https://leetcode.com/problems/subarrays-with-k-different-integers/discuss/459766/C-O(N)-dictionary-two-pointers
        public int SubstringKdistinctCharacter1(string A, int K)
        {

            int res = 0;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int start = 0;
            int current = 0;
            for (int i = 0; i < A.Length; i++)
            {
                if (dict.ContainsKey(A[i]))
                {
                    dict[A[i]]++;
                }
                else
                {
                    dict[A[i]] = 1;
                }
                if (dict.Keys.Count >= K)
                {
                    while (dict[A[current]] > 1)
                    {
                        dict[A[current++]]--;
                    }
                    if (dict.Keys.Count == K)
                    {
                        res += current - start + 1;
                    }
                    else
                    {
                        dict.Remove(A[current++]);
                        start = current;
                        dict[A[i]] = 0;
                        i--;
                    }
                }
            }
            return res;
        }

        //https://leetcode.com/problems/subarrays-with-k-different-integers/discuss/479088/C-Solution-(2-hashmaps)


    }
}
