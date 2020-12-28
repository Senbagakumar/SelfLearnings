using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon
{
    //https://leetcode.com/discuss/interview-question/344650/Amazon-Online-Assessment-Questions

    class AmazonFulfillmentBuilder
    {
        //https://leetcode.com/discuss/interview-question/935558/Amazon-SDE1-or-OA-2020-or-Amazon-Fulfillment-Builder

        //2. https://leetcode.com/discuss/interview-question/344677  -- https://leetcode.com/problems/minimum-cost-to-connect-sticks (premium)
        //Input: ropes = [8, 4, 6, 12]
        //Output: 58
        //Explanation: The optimal way to connect ropes is as follows
        //1. Connect the ropes of length 4 and 6 (cost is 10). Ropes after connecting: [8, 10, 12]
        //2. Connect the ropes of length 8 and 10 (cost is 18). Ropes after connecting: [18, 12]
        //3. Connect the ropes of length 18 and 12 (cost is 30).
        //Total cost to connect the ropes is 10 + 18 + 30 = 58

        //Similar Problem : https://leetcode.com/problems/last-stone-weight/ 

        public static int MinCostConnectedRops() // or FulfillmentBuilder
        {
            var input = new List<int>() { 8, 4, 6, 12 };
            int maxcost = 0;
            while (input.Count > 1)
            {
                var iarray = input.ToArray();
                Array.Sort(iarray);
                if (iarray.Length >= 2)
                {
                    int cost = iarray[0] + iarray[1];
                    input.Remove(iarray[0]);
                    input.Remove(iarray[1]);
                    input.Add(cost);
                    maxcost += cost;
                }

            }
            return maxcost;
        }
    }
}
