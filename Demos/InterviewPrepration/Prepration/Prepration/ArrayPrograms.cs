using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
    public class ArrayPrograms
    {
        //1. Sort By Parity
        public static int[] SortArrayByParity(int[] input) //Input: [3,1,2,4] , Output: [2,4,3,1]
        {
            int j = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] % 2 == 0)
                {
                    var temp = input[i];
                    input[i] = input[j];
                    input[j] = temp;
                    j++;
                }
            }
            return input;
        }

        // 2. Rotation Array
        public static int[] ArrayRotation(int[] input, int rotationNo)
        {
            //first way
            int i = 0; int k = input.Length;
            int[] arr = new int[k];
            while (i < k)
            {
                int temp = (i + rotationNo) % k;
                arr[i] = input[temp];
                i++;
            }
            return arr;
        }

        public static void ArrayRotationSecondWay(int[] input, int rotationNo)
        {
            int i = 0;
            while (i < rotationNo)
            {
                RotationSecondWay(input);
                i++;
            }
        }

        public static void RotationSecondWay(int[] input)
        {
            int temp = input[0]; int j = 0;
            for (j = 0; j < input.Length - 1; j++)
            {
                input[j] = input[j + 1];
            }
            input[j] = temp;
        }

       

        //5. Sorting
        public static void Sorting(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (input[i] > input[j])
                    {
                        int temp = input[i];
                        input[i] = input[j];
                        input[j] = temp;
                    }
                }
            }
        }

        //6. Sorted Square.
        //https://leetcode.com/problems/squares-of-a-sorted-array/discuss/382764/Java-solution-recall-the-y-x2
        //Input: [-4,-1,0,3,10] Output: [0,1,9,16,100]
        public static int[] SortedSquar(int[] input)
        {
            int left = 0;
            int right, index;
            right = index = input.Length - 1; //int index = input.Length - 1;
            int[] res = new int[input.Length];

            while (index >= 0)
            {
                if (Math.Abs(input[left]) >= Math.Abs(input[right]))
                {
                    res[index] = (input[left] * input[left]);
                    left++;
                }
                else
                {
                    res[index] = (input[right] * input[right]);
                    right--;
                }
                index--;
            }
            return res;
        }

        //https://leetcode.com/problems/minimum-absolute-difference/discuss/390088/C-One-Pass-O(nlogn)
        //7. Minimum Absolute Difference
        //Input: arr = [4,2,1,3]  Output: [[1,2],[2,3],[3,4]]
        public static List<IList<int>> FindMinDiffernce(int[] input)
        {
            Array.Sort(input);
            var list = new List<IList<int>>();
            int mindifference = int.MaxValue;
            for (int i = 0; i < input.Length - 1; i++)
            {
                int difference = Math.Abs(input[i] - input[i + 1]);
                if (mindifference > difference)
                {
                    mindifference = difference;
                    list.Clear();
                    list.Add(new List<int>() { input[i], input[i + 1] });
                }
                else if (mindifference == difference)
                {
                    list.Add(new List<int>() { input[i], input[i + 1] });
                }
            }
            return list;
        }

        //8. Peak Index in a Mountain Array
        public static int PeakIndexInMountainArray(int[] A)
        {
            int low = 0;
            int high = A.Length - 1;

            while (low < high)
            {
                int mid = (low + high) / 2;
                if (A[mid] < A[mid + 1])
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid;
                }
            }
            return low;
        }

        //9. Intersection of Two Arrays
        //https://leetcode.com/problems/intersection-of-two-arrays/discuss/385410/JAVA-solution-using-HashMap-(2ms-beats-98)

        public static int[] IntersectionOfTwoArrays(int[] a1, int[] a2)
        {
            var dict = new Dictionary<int, int>();
            var result = new List<int>();
            for (int i = 0; i < a1.Length; i++)
            {
                if (dict.ContainsKey(a1[i]))
                {
                    //var value = dict[a1[i]];
                    //dict.Remove(a1[i]);
                    //dict.Add(a1[i], value + 1);
                    dict[a1[i]]++;
                }
                else
                {
                    dict.Add(a1[i], 1);
                }
            }

            for (int j = 0; j < a2.Length; j++)
            {
                if (dict[a2[j]] != 0)
                {
                    dict[a2[j]] = 0;
                    result.Add(a2[j]);
                    //dict.Remove(a2[j]);
                    //dict.Add(a2[j],0);
                }
            }
            return result.ToArray();

        }

        //13. Find the missing elelement and repeating element
        //1. use dictionary 2. using array
        // {1,3,3} => missing =2 , repeat = 3
        //{4, 3, 6, 2, 1, 1} => missing =5, repeat = 1
        public static void GetTwoElements(int[] array)
        {
            int n = array.Length;
            Array.Sort(array);
            int missingNo = 0;
            int repeatNo = 0;
            for (int i = 0; i < n - 1; i++)
            {
                int diff = array[i + 1] - array[i];
                if (diff > 1)
                    missingNo = array[i] + 1;
                if (diff == 0)
                    repeatNo = array[i];
            }

        }

        //Amazon Qustion
        //Missing number
        //Input: [9,6,4,2,3,5,7,0,1] Output: 8
        //https://leetcode.com/explore/interview/card/amazon/76/array-and-strings/2971/
        public static void PrintTwoElements(int[] arr)
        {
            int i;
            int size = arr.Length;
            Console.Write("The repeating element is ");

            for (i = 0; i < size; i++)
            {
                int abs_val = Math.Abs(arr[i]);
                if (arr[abs_val - 1] > 0)
                    arr[abs_val - 1] = -arr[abs_val - 1];
                else
                    Console.WriteLine(abs_val);
            }

            Console.Write("And the missing element is ");
            for (i = 0; i < size; i++)
            {
                if (arr[i] > 0)
                    Console.WriteLine(i + 1);
            }
        }

        //14. Find the Unique No
        // https://www.geeksforgeeks.org/fix-two-swapped-nodes-of-bst/
        //https://www.geeksforgeeks.org/find-the-element-that-appears-once/ soln : https://www.careercup.com/question?id=7902674
        public static int FindUniqueNo(int[] input)
        {
            int element = 0;
            bool isfound;

            for (int i = 0; i < input.Length; i++)
            {
                isfound = false;
                for (int j = 0; j < input.Length; j++)
                {
                    if (j != i && input[i] == input[j])
                    {
                        isfound = true;
                        break;
                    }
                }
                if (!isfound)
                {
                    element = input[i];
                }
            }
            return element;
        }


        

        //16. 
        //https://www.geeksforgeeks.org/find-maximum-value-of-sum-iarri-with-only-rotations-on-given-array-allowed/
        public static int maxSum()
        {
            int[] arr = new int[] { 10, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            // Find array sum and i*arr[i] 
            // with no rotation 
            int arrSum = 0; // Stores sum of arr[i] 
            int currVal = 0; // Stores sum of i*arr[i] 
            for (int i = 0; i < arr.Length; i++)
            {
                arrSum = arrSum + arr[i];
                currVal = currVal + (i * arr[i]);
            }

            // Initialize result as 0 rotation sum 
            int maxVal = currVal;

            // Try all rotations one by one and find 
            // the maximum rotation sum. 
            for (int j = 1; j < arr.Length; j++)
            {
                currVal = currVal + arrSum - arr.Length *
                                    arr[arr.Length - j];
                if (currVal > maxVal)
                    maxVal = currVal;
            }

            // Return result 
            return maxVal;
        }

        //17. PushZero to End
        //https://www.geeksforgeeks.org/move-zeroes-end-array/
        public static void pushZerosToEnd(bool isEnd)
        {
            int[] arr = new int[] { 1, 9, 8, 4, 0, 0, 2, 7, 0, 6, 0, 9 };

            int count = 0;
            int n = arr.Length;

            if (isEnd)
            {
                for (int i = 0; i < n; i++)
                    if (arr[i] != 0)
                        arr[count++] = arr[i];

                while (count < n)
                    arr[count++] = 0;
            }
        }

        //18.Find the element that appears once in an array where every other element appears twice
        //https://www.geeksforgeeks.org/find-element-appears-array-every-element-appears-twice/

        public static void FindElementAppearsOnce()
        {
            int[] array = { 2, 3, 5, 4, 5, 3, 4 };
            int result = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                result = result ^ array[i];
            }
        }

        //https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/discuss/441225/Share-my-simple-solution
        //19. Find the sum of target
        public static void FindTheSumOfTargetIndices()
        {
            int[] arrays = new int[] { 4, 6, 8, 2, 3, 7, 1 };
            int target = 5;
            for (int i = 0; i < arrays.Length; i++)
            {
                for (int j = i + 1; j < arrays.Length; j++)
                {
                    if (arrays[i] + arrays[j] == target)
                    {
                        Console.WriteLine(i + "," + j);
                    }
                }
            }

            //var dict = new Dictionary<int, int>();
            //for (int i = 0; i < arrays.Length; i++)
            //{
            //    if (target < arrays[i]) continue;
            //    var remainder = target - arrays[i];
            //    int result;
            //    if (dict.TryGetValue(arrays[i], out result))
            //    {
            //        Console.WriteLine(i + "," + result);
            //    }
            //    else
            //    {
            //        dict.Add(remainder, i);
            //    }
            //}


        }

        //20. Array Multiplication
        public static void ArrayMultiplication()
        {
            //int[,] a = new int[, ] { { 2,4 }, {1,4 } };
            //int[,] b = new int[,] { { 1, 4 }, { 1,3 } };
            //int[,] c = new int[2,2];

            //int[,] a = new int[,] { { 2, 1 ,1}, { 3, 4 ,4}, { 5, 6 ,6} };
            //int[,] b = new int[,] { { 1, 3, 6 }, { 2, 4, 5 }, { 3, 5,5 } };
            //int[,] c = new int[a.GetUpperBound(0) + 1, b.GetUpperBound(1) + 1];

            int[,] a = new int[,] { { 2, 1 }, { 3, 4 }, { 5, 6 } };
            int[,] b = new int[,] { { 1, 3, 6 }, { 2, 4, 5 } };
            int[,] c = new int[a.GetUpperBound(0) + 1, b.GetUpperBound(1) + 1];

            //Array Multiplication ( 2,*2)
            for (int i = 0; i <= a.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= b.GetUpperBound(1); j++)
                {
                    c[i, j] = 0;
                    for (int k = 0; k <= a.GetUpperBound(1); k++)
                    {
                        c[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            //Array Multiplication (3*3)

        }

        

        //22. Calculate Cost Path
        public static void MinCostPath()
        {
            int[,] a = new int[,] { { 4, 1, 8, 6 }, { 3, 5, 2, 1 }, { 7, 3, 6, 5 }, { 9, 5, 7, 10 } };
            int[,] cost = new int[a.GetUpperBound(0) + 1, a.GetUpperBound(1) + 1];

            //calculate the cost
            for (int i = 0; i <= a.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= a.GetUpperBound(1); j++)
                {
                    if (i == 0 && j == 0)
                        cost[i, j] = a[i, j];
                    else if (i == 0 && j > 0)
                        cost[i, j] = a[i, j] + cost[i, j - 1];
                    else if (i > 0 && j == 0)
                        cost[i, j] = a[i, j] + cost[i - 1, j];
                    else
                        cost[i, j] = a[i, j] + Math.Min(cost[i - 1, j], cost[i, j - 1]);
                }
            }

            string path = string.Empty;
            //Get the path
            int k = 0; int l = 0; int one = 0;
            while (k < a.GetUpperBound(0) || l < a.GetUpperBound(1))
            {
                if (k == 0 && l == 0)
                {
                    path += $"{a[k, l]}-->";
                    k = 1; l = 1;
                }
                else
                {
                    if (one == 0)
                    {
                        k = 0; l = 0;
                        one++;
                    }
                    if (k < a.GetUpperBound(0) && l < a.GetUpperBound(1))
                    {
                        if (a[k + 1, l] < a[k, l + 1])
                        {
                            path += $"{a[k + 1, l]}-->";
                            k = k + 1;
                        }
                        else
                        {
                            path += $"{a[k, l + 1]}-->";
                            if (l < a.GetUpperBound(0))
                                l = l + 1;
                        }
                    }
                    else
                    {
                        if (k < a.GetUpperBound(0))
                        {
                            k = k + 1;
                            path += $"{a[k, l]}-->";
                        }
                        if (l < a.GetUpperBound(0))
                        {
                            l = l + 1;
                            path += $"{a[k, l]}-->";
                        }
                    }
                }
            }
        }

        //Amazon Question
        //Microsoft Question
        //23. word search
        //https://www.geeksforgeeks.org/search-a-word-in-a-2d-grid-of-characters/
        public static void WordSearch()
        {
            char[,] wd = new char[,] { { 'C', 'O', 'R' }, { 'O', 'A', 'R' }, { 'A', 'O', 'R' } };
            string stext = "CAB";

            for (int i = 0; i < wd.GetUpperBound(0); i++)
            {
                for (int j = 0; j < wd.GetUpperBound(1); j++)
                {
                    if (wd[i, j] == stext[0])
                    {
                        if (IsWord(wd, i, j, stext))
                        {
                            Console.WriteLine("Yes word is matched");
                            break;
                        }
                    }
                }
            }
        }

        private static bool IsWord(char[,] wd, int i, int j, string text)
        {
            int[] xd = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] yd = new int[] { -1, 1, 0, 1, -1, 1, 0, -1 };

            if (wd[i, j] != text[0])
                return false;

            for (int dir = 0; dir < 8; dir++)
            {
                int rd = i + xd[dir];
                int cd = j + yd[dir];
                int l;
                for (l = 1; l < text.Length; l++)
                {
                    if (rd < 0 || cd < 0 || rd > wd.GetUpperBound(0) || cd > wd.GetUpperBound(1))
                        break;

                    if (wd[rd, cd] != text[l])
                        break;

                    rd += xd[dir];
                    cd += yd[dir];
                }
                if (l == text.Length)
                    return true;
            }
            return false;
        }

        //24. Converted sorted Array to Binary Search Tree
        //https://leetcode.com/problems/convert-sorted-array-to-binary-search-tree/
        //https://leetcode.com/problems/convert-sorted-array-to-binary-search-tree/discuss/440037/C-5-Lines-Code
        // int[] array = new int[] { -10, -3, 0, 5, 9 };
        public static Node SortedArrayToBinarySearchTree(int[] array)
        {
            if (array.Length == 0) return null;
            if (array.Length == 1) return new Node(array[0]);

            int mid = array.Length / 2;
            return new Node(array[mid])
            {
                Left = SortedArrayToBinarySearchTree(array.Take(mid).ToArray()),
                Right = SortedArrayToBinarySearchTree(array.Skip(mid + 1).Take(array.Length).ToArray())
            };
        }

        //25. https://leetcode.com/problems/asteroid-collision/
        // Input: asteroids = [5, 10, -5] Output: [5, 10]      

        //Amazon Question
        //14. Kth largest element in Array
        //https://leetcode.com/problems/kth-largest-element-in-an-array/
        // https://leetcode.com/problems/kth-largest-element-in-an-array/discuss/440593/Java-QuickSelect-(1ms)
        //https://www.geeksforgeeks.org/kth-smallestlargest-element-unsorted-array/
        //Input: [3,2,1,5,6,4] and k = 2 Output: 5 ,, Input: [3,2,3,1,2,4,5,5,6] and k = 4 Output: 4
        public static void KthSmallest()
        {
            int[] input = new int[] { 12, 3, 5, 7, 4, 19, 26 };
            //var no = KthSmallest(input, 0, input.Length - 1, 3);
            int k = 3;
            CQuickSort(input, 0, input.Length - 1);
            int tlno = input[input.Length - k];
            int tsno = input[k - 1];
        }
        //5. Quick Sort
        public static void QuickSort()
        {
            int[] input = new int[] { 12, 3, 5, 7, 4, 19, 26 };
            CQuickSort(input, 0, input.Length - 1);
        }

        private static void CQuickSort(int[] inp, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(inp, left, right);
                if (pivot > 1)
                    CQuickSort(inp, left, pivot - 1);
                if (pivot + 1 < right)
                    CQuickSort(inp, pivot + 1, right);
            }
        }

        private static int Partition(int[] inp, int left, int right)
        {
            int pivot = inp[left];
            while (true)
            {
                while (inp[left] < pivot)
                    left++;
                while (inp[right] > pivot)
                    right--;
                if (left < right)
                {
                    var temp = inp[right];
                    inp[right] = inp[left];
                    inp[left] = temp;
                }
                else
                    return right;
            }
        }

        //15. Find the minimum in sorted array
        //https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/
        //Input: [3,4,5,1,2] Output: 1 ,, Input: [4,5,6,7,0,1,2] Output: 0

        public static int FindMinimumInSortedArray()
        {
            var inp = new int[] { 4, 5, 6, 7, 0, 1, 2 };
            var result = inp[0];
            for (int i = 0; i < inp.Length - 1; i++)
            {
                if (inp[i + 1] < result)
                {
                    result = inp[i + 1];
                    //break;
                }
            }
            return result;
        }

        //https://leetcode.com/problems/insert-interval/
        //Given a set of non-overlapping intervals, insert a new interval into the intervals (merge if necessary).
        //You may assume that the intervals were initially sorted according to their start times.

        //Input: intervals = [[1,3],[6,9]], newInterval = [2,5] Output: [[1,5],[6,9]]
        //Input: intervals = [[1,2],[3,5],[6,7],[8,10],[12,16]], newInterval = [4,8] Output: [[1,2],[3,10],[12,16]]

        public int[][] Insert(int[][] intervals, int[] insert)
        {
            List<int[]> list = new List<int[]>();
            int i = 0;
            int[] temp = new int[2];
            if (intervals.Length == 0 || intervals == null)
            {
                list.Add(insert);
                return list.ToArray();
            }
            while (i < intervals.Length)
            {
                int[] current = intervals[i];
                if (current[1] < insert[0])
                {
                    list.Add(current);
                    temp = insert;
                    i++;
                }

                else if (current[0] > insert[1])
                {
                    list.Add(insert);
                    temp = current;
                    break;
                }

                else
                {
                    insert[0] = Math.Min(insert[0], current[0]);
                    insert[1] = Math.Max(insert[1], current[1]);
                    temp = insert;
                    i++;

                }
            }

            if (i == intervals.Length - 1 || i == intervals.Length)
            {
                list.Add(temp);
            }

            //if (i < intervals.Length && i != intervals.Length - 1)
            //{
            //    for (int j = i; j < intervals.Length; j++)
            //    {
            //        list.Add(intervals[j]);
            //    }
            //}

            return list.ToArray();

        }

        //https://leetcode.com/problems/climbing-stairs/
        //Climbing Stairs
        //You are climbing a stair case. It takes n steps to reach to the top.
        //Each time you can either climb 1 or 2 steps.In how many distinct ways can you climb to the top?
        //Input: 2 Output: 2 Explanation: There are two ways to climb to the top. 1. 1 step + 1 step 2. 2 steps
        //Input: 3 Output: 3 Explanation: There are three ways to climb to the top. 1. 1 step + 1 step + 1 step 2. 1 step + 2 steps 3. 2 steps + 1 step
        public int ClimbStairs(int n)
        {
            var f=new int[n + 1];
            f[0] = 1;
            f[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                f[i] = f[i - 1] + f[i - 2];
            }
            var result =f[n];
            return result;
        }

        //Min Cost Climbing Stairs
        //https://leetcode.com/problems/min-cost-climbing-stairs/
        //Once you pay the cost, you can either climb one or two steps. You need to find minimum cost to reach the top of the floor, 
        //and you can either start from the step with index 0, or the step with index 1.

        //Input: cost = [10, 15, 20] Output: 15 Explanation: Cheapest is start on cost[1], pay that cost and go to the top.
        //Input: cost = [1, 100, 1, 1, 1, 100, 1, 1, 100, 1] Output: 6 Explanation: Cheapest is start on cost[0], and only step on 1s, skipping cost[3].
        public int MinCostClimbingStairs(int[] cost)
        {
            int n = cost.Length;
            int[] dp = new int[n+1];
            dp[0] = cost[0];
            dp[1] = cost[1];

            for (int i = 2; i <= n; i++)
            {
                dp[i] = Math.Min(dp[i - 1], dp[i - 2]) + (i == n ? 0 : cost[i]);
            }
            var result = dp[n];
            return result;
        }

        //DecodeWays
        //https://leetcode.com/problems/decode-ways/
        //A message containing letters from A-Z is being encoded to numbers using the following mapping:
        //'A' -> 1 'B' -> 2 'Z' -> 26
        //Input: "12" Output: 2 Explanation: It could be decoded as "AB" (1 2) or "L" (12).
        //Input: "226" Output: 3 Explanation: It could be decoded as "BZ" (2 26), "VF" (22 6), or "BBF" (2 2 6).
        public int NumDecodings(string s)
        {
            //Corner case - 
            if (s[0] == '0') return 0;

            //General case - 
            int len = s.Length;
            int[] dp = new int[len + 1];
            //dp[i] = number of ways to decode the array till i
            dp[0] = 1;
            dp[1] = 1;

            for (int i = 2; i <= len; i++)
            {
                int one = Convert.ToInt16(s.Substring(i - 1, 1));
                int two = Convert.ToInt16(s.Substring(i - 2, 2));

                if (one >= 1) 
                    dp[i] = dp[i] + dp[i - 1];
                if (two >= 10 && two <= 26) 
                    dp[i] = dp[i] + dp[i - 2];
            }

            var result=dp[len];
            return result;
        }

        //Longest Consecutive Sequence
        //Given an unsorted array of integers, find the length of the longest consecutive elements sequence.
        //Your algorithm should run in O(n) complexity.
        //https://leetcode.com/problems/longest-consecutive-sequence/
        //Input: [100, 4, 200, 1, 3, 2] Output: 4 Explanation: The longest consecutive elements sequence is [1, 2, 3, 4]. Therefore its length is 4.
        public int LongestConsecutive(int[] arr)
        {
            var hashset = new HashSet<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                hashset.Add(arr[i]);
            }

            int max = 0;
            while (hashset.Count > 0)
            {
                var e = hashset.First();
                hashset.Remove(e);
                var u = GetConsecativeSeqUp(hashset, e + 1);
                var d = GetConsecativeSeqDown(hashset, e - 1);
                if (u + d + 1 > max) max = u + d + 1;
            }
            return max;
        }

        private int GetConsecativeSeqUp(HashSet<int> hashset, int e)
        {
            if (hashset.Contains(e))
            {
                hashset.Remove(e);
                return 1 + GetConsecativeSeqUp(hashset, e + 1);
            }
            return 0;
        }
        private int GetConsecativeSeqDown(HashSet<int> hashset, int e)
        {
            if (hashset.Contains(e))
            {
                hashset.Remove(e);
                return 1 + GetConsecativeSeqDown(hashset, e - 1);
            }
            return 0;
        }


        //https://leetcode.com/problems/reverse-bits/
        // Reverse Bits
        //Reverse bits of a given 32 bits unsigned integer.
        //Input: 00000010100101000001111010011100   Output: 00111001011110000010100101000000
        //Explanation: The input binary string 00000010100101000001111010011100 represents the unsigned integer 43261596, so return 964176192 which its binary representation is 00111001011110000010100101000000.
        public uint ReverseBits(uint n)
        {
            uint result = 0;

            for (int i = 0; i < 32; i++) //32
            {
                result <<= 1;
                result += (n & 1);
                n >>= 1;
            }

            return result;
        }
        //https://leetcode.com/problems/number-of-1-bits/
        //Write a function that takes an unsigned integer and return the number of '1' bits it has (also known as the Hamming weight).
        //Input: 00000000000000000000000000001011 Output: 3
        //Explanation: The input binary string 00000000000000000000000000001011 has a total of three '1' bits.
        // soln : https://leetcode.com/problems/number-of-1-bits/discuss/502030/C-Mask
        public int HammingWeight(uint n)
        {
            int ans = 0;
            while (n > 0)
            {
                ans++;
                n = n & (n - 1);
            }
            return ans;
        }



        //https://leetcode.com/problems/unique-paths/
        //A robot is located at the top-left corner of a m x n grid (marked 'Start' in the diagram below).
        //The robot can only move either down or right at any point in time.The robot is trying to reach the bottom-right corner of the grid(marked 'Finish' in the diagram below).
        //How many possible unique paths are there?
        public int UniquePaths(int m, int n)
        {
            //DP reasoning:
            //d[i][j] = d[i][j-1] + d[i-1][j]
            //d[0][j] = 1
            //d[i][0] = 1

            int[,] dp = new int[m,n];
            dp[0,0] = 1;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == 0 && i > 0)
                    {
                        dp[i,0] = dp[i - 1,0];
                        continue;
                    }
                    if (i == 0 && j > 0)
                    {
                        dp[0,j] = dp[0,j - 1];
                        continue;
                    }
                    if (i > 0 && j > 0)
                    {
                        dp[i,j] = dp[i,j - 1] + dp[i - 1,j];
                    }
                }
            }
            int res = dp[m - 1,n - 1];
            return res;
        }

        //https://www.geeksforgeeks.org/print-last-10-lines-of-a-given-file/
        public void PrintLast10LineOfGivenFile(string text, char delm)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            string[] texts = text.Split(delm);
            int i = 0;
            if (texts.Length > 10)
                i = texts.Length - 10;
            while(i < texts.Length)
            {
                Console.WriteLine(texts[i]);
                i++;
            }
        }
        //https://www.geeksforgeeks.org/find-recurring-sequence-fraction/
        public void FindRepeatingCharacter(int num, int denr)
        {
            string res = string.Empty;
            if (num == 0) return;
            int rem = num%denr;
            Dictionary<int, int> mp = new Dictionary<int, int>();
            while(rem!=0 && !mp.ContainsKey(rem))
            {
                // Store this remainder 
                mp[rem] = res.Length;

                // Multiply remainder with 10 
                rem = rem * 10;

                // Append rem / denr to result 
                int res_part = rem / denr;
                res += res_part.ToString();

                // Update remainder 
                rem = rem % denr;
            }
            string v= (rem == 0) ? "" : res.Substring(mp[rem]);
        }

        //https://leetcode.com/problems/brick-wall/
        //554. Brick Wall
        public static int LeastBricks(IList<IList<int>> wall)
        {
            var list = new List<int>();
            foreach (var row in wall)
            {
                var sum = 0;
                for (var j = 0; j < row.Count - 1; j++)
                {
                    sum += row[j];
                    list.Add(sum);
                }
            }

            var groups = list.GroupBy(x => x).ToList();
            int max = groups.Any() ? groups.Select(g => g.Count()).Max() : 0;
            return wall.Count - max;
        }

        //https://leetcode.com/problems/rectangle-overlap/
        //836. Rectangle Overlap
        //Example 1: Input: rec1 = [0,0,2,2], rec2 = [1,1,3,3] Output: true
        //Example 2: Input: rec1 = [0,0,1,1], rec2 = [1,0,2,1] Output: false
        //Example 3: Input: rec1 = [0,0,1,1], rec2 = [2,2,3,3] Output: false
        public bool IsRectangleOverlap(int[] rec1, int[] rec2)
        {
            return rec1[0] < rec2[2] && rec1[1] < rec2[3] && rec1[2] > rec2[0] && rec1[3] > rec2[1];
        }

        //https://leetcode.com/problems/least-number-of-unique-integers-after-k-removals/
        //1481. Least Number of Unique Integers after K Removals
        // Input: arr = [5,5,4], k = 1      Output: 1 Explanation: Remove the single 4, only 5 is left.
        //Input: arr = [4,3,1,1,3,3,2], k = 3  Output: 2 Explanation: Remove 4, 2 and either one of the two 1s or three 3s. 1 and 3 will be left.
        public int FindLeastNumOfUniqueInts(int[] arr, int k)
        {
            var dict = new Dictionary<int, int>();
            foreach(int i in arr)
            {
                if (dict.ContainsKey(i))
                    dict[i]++;
                else
                    dict[i] = 1;
            }

            var keys = dict.OrderBy(q => q.Key);

            int currentk = 0;
            int count=0;
            foreach(var i in keys)
            {
                if (currentk + dict[i.Key] <= k)
                    currentk += dict[i.Key];
                else
                    count++;
            }
            return count;

        }  

        //https://leetcode.com/problems/powx-n/
        //50. Pow(x, n)
        public double MyPow(double x, int n)
        {
            return 0.0;
        }

        //https://leetcode.com/problems/basic-calculator-ii/
        //227. Basic Calculator II
        //Example 1: Input: "3+2*2" Output: 7 Example 2: Input: " 3/2 " Output: 1 Example 3: Input: " 3+5 / 2 " Output: 5
        public static int Calculate(string s)
        {
            var st = new Stack<int>();
            char sign = '+';
            int cur = 0;
            for (int i = 0; i < s.Length; i++)
            {
                var ch = s[i];
                if (ch == ' ') continue;

                if (char.IsDigit(ch))
                    cur = cur * 10 + (ch - '0');
                if (!char.IsDigit(ch) || i == s.Length - 1)
                {
                    if (sign == '+')
                    {
                        st.Push(cur);
                    }
                    if (sign == '*')
                    {
                        var last = st.Pop();
                        st.Push(last * cur);
                    }
                    if (sign == '-')
                    {
                        st.Push(-1 * cur);
                    }
                    if (sign == '/')
                    {
                        var last = st.Pop();
                        st.Push(last / cur);
                    }
                    cur = 0;
                    sign = ch;
                }
            }
            int ret = 0;
            while (st.Any())
                ret += st.Pop();
            return ret;


        }

        //1480. Running Sum of 1d Array
        //https://leetcode.com/problems/running-sum-of-1d-array/
        public int[] RunningSum(int[] nums)
        {
            int[] result = new int[nums.Length];
            if (nums.Length == 0)
                return result;
            int sum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                result[i] = sum;
            }
            return result;
        }

        //https://leetcode.com/problems/subarray-sum-equals-k/

        public static int SubArraySumEquals(int[] nums, int k)
        {
            var dic = new Dictionary<int, int>();
            int count = 0;
            int sum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (sum == k) count++;

                if (dic.ContainsKey(sum - k))
                    count += dic[sum - k];

                if (!dic.ContainsKey(sum))
                {
                    dic.Add(sum, 1);
                }
                else
                {
                    dic[sum] = dic[sum] + 1;
                }
            }

            return count;

        }
    }
}
