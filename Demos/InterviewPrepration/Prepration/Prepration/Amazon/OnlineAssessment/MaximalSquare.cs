using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class MaximalSquare
    {
        //Given an m x n binary matrix filled with 0's and 1's, find the largest square containing only 1's and return its area.
        //https://leetcode.com/problems/maximal-square/
        //https://leetcode.com/problems/maximal-square/discuss/600519/C-DP-readable-solution
        public int Maximal_Square(char[][] matrix)
        {
            if (matrix == null || matrix.Length < 1 || matrix[0] == null || matrix[0].Length < 1) return 0;

            int rows = matrix.Length, cols = matrix[0].Length;
            var dp = new int[rows + 1, cols + 1];
            var maxSize = 0;

            for (var row = 1; row <= rows; row++)
                for (var col = 1; col <= cols; col++)
                    if (matrix[row - 1][col - 1] == '1')
                    {
                        dp[row, col] = 1 + Min(
                            dp[row, col - 1],
                            dp[row - 1, col],
                            dp[row - 1, col - 1]
                        );
                        if (maxSize < dp[row, col]) maxSize = dp[row, col];
                    }
            return maxSize * maxSize;
        }

        private static int Min(params int[] args) => args.Min();

    }
}
