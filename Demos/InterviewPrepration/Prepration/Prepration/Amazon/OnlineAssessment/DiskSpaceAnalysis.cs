using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class DiskSpaceAnalysis
    {
        //https://leetcode.com/problems/sliding-window-maximum/

        // Sliding window Maximum
        //Input: nums = [1,3,-1,-3,5,3,6,7], and k = 3 Output: [3,3,5,5,6,7]
        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            // A deque which holds the max elements for window size of k
            var maxWindowQueue = new LinkedList<int>();

            // Max window to be returned
            int[] maxWindow = new int[nums.Length + 1 - k];

            int left = 0, right = 0, mwCtr = 0;

            while (right < nums.Length)
            {
                int dig = nums[right];

                // Remove from the end, those elements which are smaller than dig.
                while (maxWindowQueue.Count > 0 && dig > maxWindowQueue.Last())
                {
                    maxWindowQueue.RemoveLast();
                }

                // Add the new found element.
                maxWindowQueue.AddLast(dig);

                // We have reached the window size
                if (right - left + 1 == k)
                {
                    maxWindow[mwCtr] = maxWindowQueue.First();
                    mwCtr++;

                    // Now we need to slice the left corner
                    // Doing so, If you find the number being removed is the max element we need to pop
                    // that element as well from the dequeue
                    if (nums[left] == maxWindowQueue.First())
                    {
                        maxWindowQueue.RemoveFirst();
                    }
                    // Slice the left corner
                    left++;
                }

                // Increment right as usual
                right++;
            }
            return maxWindow;
        }
    }
}
