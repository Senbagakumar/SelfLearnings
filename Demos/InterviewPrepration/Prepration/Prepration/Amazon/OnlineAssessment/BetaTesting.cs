using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class BetaTesting
    {
        //https://leetcode.com/problems/minimum-difficulty-of-a-job-schedule/
        //You want to schedule a list of jobs in d days. Jobs are dependent (i.e To work on the i-th job, you have to finish all the jobs j where 0 <= j < i).
        //You have to finish at least one task every day.The difficulty of a job schedule is the sum of difficulties of each day of the d days.The difficulty of a day is the maximum difficulty of a job done in that day.
        //Given an array of integers jobDifficulty and an integer d. The difficulty of the i-th job is jobDifficulty[i].
        //Return the minimum difficulty of a job schedule. If you cannot find a schedule for the jobs return -1.

        //Input: jobDifficulty = [6,5,4,3,2,1], d = 2
        //Output: 7
        //Explanation: First day you can finish the first 5 jobs, total difficulty = 6.
        //Second day you can finish the last job, total difficulty = 1.
        //The difficulty of the schedule = 6 + 1 = 7 

        public int MinDifficulty(int[] jobDifficulty, int d)
        {
            checked
            {
                long[,] dp = new long[jobDifficulty.Length + 1, d + 1];

                for (int i = 1; i <= jobDifficulty.Length; i++)
                {
                    dp[i, 0] = int.MaxValue;
                }

                for (int i = 1; i <= d; i++)
                {
                    dp[0, i] = int.MaxValue;
                }

                for (int i = 1; i <= jobDifficulty.Length; i++)
                {
                    for (int j = 1; j <= d; j++)
                    {
                        dp[i, j] = int.MaxValue;

                        if (j > i)
                        {
                            continue;
                        }

                        int max = int.MinValue;
                        for (int k = i; k >= 1; k--)
                        {
                            max = Math.Max(max, jobDifficulty[k - 1]);
                            dp[i, j] = Math.Min(dp[i, j], dp[k - 1, j - 1] + max);
                        }

                    }
                }

                var res = dp[jobDifficulty.Length, d];
                return res >= int.MaxValue ? -1 : (int)res;
            }
        }
    }
}
