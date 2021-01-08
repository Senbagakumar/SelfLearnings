using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Prepration.Assessment;

namespace Prepration.Amazon.OnlineAssessment
{
    class ZombieInMatrix
    {
        //https://leetcode.com/discuss/interview-question/411357/

        //Amazon Assessment -- 2 --  Zombie in Matrix -- https://leetcode.com/discuss/interview-question/411357/
        public static int MinDays()
        {
            int[,] grid = new int[,] { { 0, 1, 1, 0, 1 }, { 0, 1, 0, 1, 0 }, { 0, 0, 0, 0, 1 }, { 0, 1, 0, 0, 0 } };
            Queue<Coordinate> q = new Queue<Coordinate>();
            int m = grid.GetLength(0);
            int n = grid.GetLength(1);
            int target = m * n;
            int cnt = 0, res = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i, j] == 1)
                    {
                        q.Enqueue(new Coordinate(i, j));
                        cnt++;
                    }
                }
            }
            int[] xdirs = new int[] { 1, 0, -1, 0 };
            int[] ydirs = new int[] { 0, 1, 0, -1 };

            while (q.Count > 0)
            {
                int size = q.Count();
                if (cnt == target)
                    return res;
                for (int i = 0; i < size; i++)
                {
                    Coordinate cur = q.Dequeue();
                    for (int k = 0; k < xdirs.Length; k++)
                    {
                        int ni = cur.x + xdirs[k];
                        int nj = cur.y + ydirs[k];
                        if (ni >= 0 && ni < m && nj >= 0 && nj < n && grid[ni, nj] == 0)
                        {
                            cnt++;
                            q.Enqueue(new Coordinate(ni, nj));
                            grid[ni, nj] = 1;
                        }
                    }
                }
                res++;
            }
            return -1;
        }


    }
}
