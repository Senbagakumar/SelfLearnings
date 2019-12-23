using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Microsoft
{
    class Microsoft1
    {
        //String Questions
        //https://leetcode.com/problems/reverse-words-in-a-string/
        //1. Given s = "the sky is blue", return "blue is sky the".
        public static void reverseWords()
        {
            char[] s = "the sky is blue".ToCharArray();
            int i = 0;
            for (int j = 0; j < s.Length; j++)
            {
                if (s[j] == ' ')
                {
                    reverse(s, i, j - 1);
                    i = j + 1;
                }
            }

            reverse(s, i, s.Length - 1);
            string sv = new string(s); // Answers--https://leetcode.com/problems/reverse-words-in-a-string-iii/
            reverse(s, 0, s.Length - 1);
        }

        private static void reverse(char[] s, int i, int j)
        {
            while (i < j)
            {
                char temp = s[i];
                s[i] = s[j];
                s[j] = temp;
                i++;
                j--;
            }
        }

        //2. Longest Palindromic Substring
        //https://www.geeksforgeeks.org/longest-palindromic-substring-set-2/
        public static int LongestPalindrome()
        {
            string str = "babad";
            str = "forgeeksskeegfor";
            int low, high,start=0;
            int len = str.Length;
            int maxLength = 1;
            for(int i=1; i< len; i++)
            {
                // Find the longest even length  
                // palindrome with center points  
                // as i-1 and i.  
                low = i - 1;
                high = i;
                while (low >= 0 && high < len &&
                       str[low] == str[high])
                {
                    if (high - low + 1 > maxLength)
                    {
                        start = low;
                        maxLength = high - low + 1;
                    }
                    --low;
                    ++high;
                }

                // Find the longest odd length  
                // palindrome with center point as i  
                low = i - 1;
                high = i + 1;
                while (low >= 0 && high < len &&
                       str[low] == str[high])
                {
                    if (high - low + 1 > maxLength)
                    {
                        start = low;
                        maxLength = high - low + 1;
                    }
                    --low;
                    ++high;
                }
            }
            var substring = str.Substring(start, maxLength);
            return maxLength;
        }

        //3. Spiral Matrix
        // https://leetcode.com/problems/spiral-matrix/
        //https://www.geeksforgeeks.org/print-a-given-matrix-in-spiral-form/
        public static void SpiralMatrix()
        {
            int[,] a = new int[,] { { 1, 2, 3, 10 },
                                    { 4, 5, 6, 11 }, 
                                    { 7, 8, 9, 12 },
                                    { 13, 14, 15, 16 }
                                  };
            int m = 4, n = 4;
            int i, k = 0, l = 0;
            /* k - starting row index 
            m - ending row index 
            l - starting column index 
            n - ending column index 
            i - iterator 
            */

            while (k < m && l < n)
            {
                // Print the first row  
                // from the remaining rows 
                for (i = l; i < n; i++)
                {
                    Console.Write(a[k, i] + " ");
                }
                k++;

                // Print the last column from the 
                // remaining columns 
                for (i = k; i < m; i++)
                {
                    Console.Write(a[i, n - 1] + " ");
                }
                n--;

                // Print the last row from  
                // the remaining rows  
                if (k < m)
                {
                    for (i = n - 1; i >= l; i--)
                    {
                        Console.Write(a[m - 1, i] + " ");
                    }
                    m--;
                }

                // Print the first column from  
                // the remaining columns 
                if (l < n)
                {
                    for (i = m - 1; i >= k; i--)
                    {
                        Console.Write(a[i, l] + " ");
                    }
                    l++;
                }
            }
        }

        //4. Kth largest element in Array
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
            if(left < right)
            {
                int pivot = Partition(inp, left, right);
                if (pivot > 1)
                    CQuickSort(inp, left, pivot - 1);
                if (pivot+1 < right)
                    CQuickSort(inp, pivot + 1, right);
            }
        }

        private static int Partition(int[] inp, int left, int right)
        {
            int pivot = inp[left];
            while(true)
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

        //5. Find the minimum in sorted array
        //https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/
        //Input: [3,4,5,1,2] Output: 1 ,, Input: [4,5,6,7,0,1,2] Output: 0

        public static int FindMinimumInSortedArray()
        {
            var inp = new int[] { 4, 5, 6, 7, 0, 1, 2 };
            var result = inp[0];
            for(int i=0; i<inp.Length-1; i++)
            {
                if (inp[i + 1] < result)
                {
                    result = inp[i + 1];
                    break;
                }
            }            
            return result;
        }

        //
    }
}
