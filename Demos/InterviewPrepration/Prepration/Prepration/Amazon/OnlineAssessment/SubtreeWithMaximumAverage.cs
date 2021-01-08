using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class SubtreeWithMaximumAverage
    {

        //        Input:
        //		 20
        //	   /   \
        //	 12     18
        //  /  |  \   / \
        //11   2   3 15  8

        //Output: 18
        //Explanation:
        //There are 3 nodes which have children in this tree:
        //12 => (11 + 2 + 3 + 12) / 4 = 7
        //18 => (18 + 15 + 8) / 3 = 13.67
        //20 => (12 + 11 + 2 + 3 + 18 + 15 + 8 + 20) / 8 = 11.125

        //18 has the maximum average so output 18.

        double max = int.MinValue;
        Node maxNode = null;

        public Node maximumAverageSubtree(Node root)
        {
            if (root == null) return null;
            helper(root);
            return maxNode;
        }

        private double[] helper(Node root)
        {
            if (root == null) return new double[] { 0, 0 };

            double curTotal = root.Value;
            double count = 1;
            foreach(Node child in root.children)
            {
                double[] cur = helper(child);
                curTotal += cur[0];
                count += cur[1];
            }
            double avg = curTotal / count;
            if (count > 1 && avg > max)
            { //taking "at least 1 child" into account
                max = avg;
                maxNode = root;
            }
            return new double[] { curTotal, count };
        }

    }
}
