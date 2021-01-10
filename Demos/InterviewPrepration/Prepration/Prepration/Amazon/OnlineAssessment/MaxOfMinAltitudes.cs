using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class MaxOfMinAltitudes
    {
        //https://leetcode.com/discuss/interview-question/383669/

        //Given a matrix with r rows and c columns, find the maximum score of a path starting at [0, 0] and ending at [r-1, c-1]. 
        //The score of a path is the minimum value in that path. For example, the score of the path 8 → 4 → 5 → 9 is 4.

        //Don't include the first or final entry. You can only move either down or right at any point in time.

        //Input:
        //[[5, 1],
        //[4, 5]]

        //Output: 4
        //Explanation:
        //Possible paths:
        //5 → 1 → 5 => min value is 1
        //5 → 4 → 5 => min value is 4
        //Return the max value among minimum values => max(4, 1) = 4.


        public static int maxScore2D(int[,] grid)
        {
            int[,] land = new int[,] { { 1, 2, 3 }, { 4, 5, 1 } };

            // Assume there is at least one element
            int r = grid.GetLength(0), c = grid.GetLength(1);

            int[,] dp = new int[r, c];
            // Init
            dp[0, 0] = int.MaxValue; // first entry is not considered

            for (int i = 1; i < r; ++i)
                dp[i, 0] = Math.Min(dp[i - 1, 0], grid[i, 0]);

            for (int j = 1; j < c; ++j)
                dp[0, j] = Math.Min(dp[0, j - 1], grid[0, j]);

            // DP
            for (int i = 1; i < r; ++i)
            { // row by row
                for (int j = 1; j < c; ++j)
                {
                    if (i == r - 1 && j == c - 1)
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                    else
                    {
                        int score1 = Math.Min(dp[i, j - 1], grid[i, j]); // left
                        int score2 = Math.Min(dp[i - 1, j], grid[i, j]); // up
                        dp[i, j] = Math.Max(score1, score2);
                    }
                }
            }
            return dp[r - 1, c - 1];
        }
    }
}
