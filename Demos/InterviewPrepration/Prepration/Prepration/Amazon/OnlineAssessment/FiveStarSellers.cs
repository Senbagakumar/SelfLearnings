using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class FiveStarSellers
    {
        //https://leetcode.com/discuss/interview-question/983856/Amazon-or-OA-or-Five-Star-Sellers

        public int FiveStarReviews(int[][] productRatings, int ratingsThreshold)
        {

            decimal cThreshold = 0;
            int n = productRatings.Length;
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                cThreshold += (decimal)((decimal)productRatings[i][0] / (decimal)productRatings[i][1]) / n;
            }

            while (cThreshold * 100 <= ratingsThreshold)
            {
                int index = MaxValue(productRatings);
                cThreshold -= (decimal)((decimal)productRatings[index][0] / (decimal)productRatings[index][1]) / n;
                productRatings[index][0] += 1;
                productRatings[index][1] += 1;
                cThreshold += (decimal)((decimal)productRatings[index][0] / (decimal)productRatings[index][1]) / n;
                count++;
            }

            return count;
        }

        private static int MaxValue(int[][] productRatings)
        {
            decimal max = 0;
            int index = 0;
            for (int i = 0; i < productRatings.Length; i++)
            {
                decimal prev = (decimal)((decimal)(productRatings[i][0]) / (decimal)(productRatings[i][1]));
                decimal div = (decimal)((decimal)(productRatings[i][0] + 1) / (decimal)(productRatings[i][1] + 1));

                if (max < div - prev && div != 1)
                {
                    max = div - prev;
                    index = i;
                }
            }
            return index;
        }
    }
}
