using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class KClosestPointsToOrigin
    {
        //  K Closest Points to Origin
        // https://leetcode.com/problems/k-closest-points-to-origin/


        //We have a list of points on the plane.Find the K closest points to the origin(0, 0).

        //(Here, the distance between two points on a plane is the Euclidean distance.)

        //You may return the answer in any order.The answer is guaranteed to be unique (except for the order that it is in.)




        //Example 1:

        //Input: points = [[1,3],[-2,2]], K = 1
        //Output: [[-2,2]]
        //Explanation: 
        //The distance between(1, 3) and the origin is sqrt(10).
        //The distance between(-2, 2) and the origin is sqrt(8).
        //Since sqrt(8) < sqrt(10), (-2, 2) is closer to the origin.
        //We only want the closest K = 1 points from the origin, so the answer is just[[-2, 2]].
        //Example 2:

        //Input: points = [[3,3],[5,-1],[-2,4]], K = 2
        //Output: [[3,3],[-2,4]]
        //(The answer [[-2, 4],[3,3]] would also be accepted.)


        //public int[][] KClosest(int[][] points, int K)
        //{
        //   return points.OrderBy(x => x[0] * x[0] + x[1] * x[1]).Take(K).ToArray();
        //}

        public int[][] KClosest(int[][] points, int K)
        {

            var v = new Dictionary<int, List<int[]>>();

            foreach (int[] point in points)
            {
                int key = (point[0] * point[0]) + (point[1] * point[1]);

                if (!v.ContainsKey(key))
                    v[key] = new List<int[]>();
                v[key].Add(point);
            }

            int[] keys = new int[v.Count];
            v.Keys.CopyTo(keys, 0);
            Array.Sort(keys);

            var result = new List<int[]>(K);

            for (int i = 0; i < keys.Length; i++)
            {
                foreach (var k in v[keys[i]])
                    result.Add(k);
                if (K == result.Count)
                    break;
            }
            return result.ToArray();

        }

    }
}
