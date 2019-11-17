using System;
using System.Collections.Generic;
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
            for(int i=0; i<input.Length; i++)
            {
                if(input[i]%2 == 0)
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
            int[] arr = new int[input.Length];
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

        // 3. Rotation Search Elements
        public static int FindPivot(int[] input, int min, int max)
        {
            if (input == null) return -1;
            if (min > max) return -1;
            int mid = min + max / 2;
            if (mid < max && input[mid] > input[mid + 1])
                return mid;
            if (mid > min && input[mid] < input[mid + 1])
                return mid - 1;
            if (input[min] > input[mid])
                return FindPivot(input, min, mid - 1);
            else
                return FindPivot(input, mid + 1, max);
        }

        public static int RotationSearch(int[] input, int element, int min, int max)
        {
            int find = 0;
            int value = FindPivot(input, min, max);
            if (value == -1)
                find = BinarySearch(input, element, min, max);
            else
            {
                if (input[value] == element)
                    return value;
                find = BinarySearch(input, element, min, value - 1);
                if (find == -1)
                    find = BinarySearch(input, element, value + 1, max);
            }
            return find;

        }

        //4. Binary Search
        public static int BinarySearch(int[] input, int element, int low, int high)
        {
            if (input == null || input.Length <= 0) return -1;
            if (low > high) return -1;
            int mid = (low + high) / 2;
            if (input[mid] == element) return mid;
            if (input[mid] < element) return BinarySearch(input, element, (mid + 1), high);
            return BinarySearch(input, element, low, (mid - 1));
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
            int right = input.Length - 1; int index = input.Length - 1;
            int[] res = new int[input.Length];

            while(index >=0)
            {
                if(Math.Abs(input[left]) >= Math.Abs(input[right]))
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
            for(int i=0; i<input.Length-1;i++)
            {
                int difference = Math.Abs(input[i] - input[i + 1]);
                if(mindifference > difference)
                {
                    mindifference = difference;
                    list.Clear();
                    list.Add(new List<int>() { input[i], input[i + 1] });
                }
                else if(mindifference == difference)
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

            for(int j=0; j<a2.Length; j++)
            {
                if(dict[a2[j]]!=0)
                {
                    dict[a2[j]] = 0;
                    result.Add(a2[j]);
                    //dict.Remove(a2[j]);
                    //dict.Add(a2[j],0);
                }
            }
            return result.ToArray();

        }

        //10. Set Matrix Zeroes
        //Given a m x n matrix, if an element is 0, set its entire row and column to 0

        public static void SetZeros()
        {
            int[,] input = new int[,] { { 0, 5, 6,0 }, { 2, 3, 6,9 },{ 5,7,8,1 } };
            

            int rowIndexUpperBound = input.GetLength(0);
            int colIndexUpperBound = input.GetLength(1);

            int[,] result = new int[rowIndexUpperBound, colIndexUpperBound];

            for (int i = 0; i < rowIndexUpperBound; i++)
                for (int j = 0; j < colIndexUpperBound; j++)
                {
                    if (input[i, j] == 0)
                    {
                       SetZeroForRowsAndColumns(input, i, j);
                    }
                }

            for (int i = 0; i < rowIndexUpperBound; i++)
                for (int j = 0; j < colIndexUpperBound; j++)
                {
                    if (input[i, j] == -1)
                        input[i, j] = 0;
                }
        }

        public static int[,] SetZeroForRowsAndColumns(int[,] input,int rowIndex, int colIndex)
        {
            int rowIndexUpperBound = input.GetLength(0);
            int colIndexUpperBound = input.GetLength(1);

            //Set Zero for Rows
            for(int i=0; i<colIndexUpperBound; i++)
            {
                input[rowIndex, i] = -1;
            }

            //Set Zero for Columns
            for (int i = 0; i < rowIndexUpperBound; i++)
            {
                input[i, colIndex] = -1;
            }

            return input;
        }

        // 11. Find the Median 
        public static int FindMedianOfTwoArrays(int[] first, int[] second)
        {
            int flen = first.Length;
            int slen = second.Length;

            if (flen > slen)
                return FindMedianOfTwoArrays(second, first);

            int min = 0;
            int max = flen;

            while(min <= max)
            {
                int x = (min + max) / 2;
                int y = ((flen + slen + 1) / 2) - x;

                int maxX = x == 0 ? int.MinValue : first[x - 1];
                int minX = x == flen ? int.MaxValue : first[x];

                int maxY = y == 0 ? int.MinValue : second[y - 1];
                int minY = y == slen ? int.MaxValue : second[y];

                if (maxX <= minY && maxY <= minX)
                {
                    if (flen + slen % 2 == 0)
                    {
                        int result = Math.Max(maxX, maxY) + Math.Min(minX, minY);
                        return result / 2;
                    }
                    else
                    {
                        int result = Math.Max(maxX, maxY);
                        return result;
                    }
                }
                else if (maxX > minY)
                {
                    max = max - 1;
                }
                else
                    min = min + 1;
            }
            return 0;
        }

        //12. Merge two Sorted Array
        public static int[] MergeTwoSortedArrays(int[] first, int[] second)
        {
            int i = 0, j=0, k=0;
            int[] result =new int[first.Length + second.Length];
            while(i < first.Length && j < second.Length)
            {
                if(first[i] < second[j])
                {
                    result[k++] = first[i++];
                }
                else
                {
                    result[k++] = second[j++];
                }
            }
            while(i < first.Length)
            {
                result[k++] = first[i++];
            }
            while(j < second.Length)
            {
                result[k++] = second[j++];
            }
            return result;
        }
    }
}
