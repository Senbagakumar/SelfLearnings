using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Microsoft
{
    class MicrosoftInterviewQuestions
    {

        //Amazon Questions
        //1. Two Sums
        //https://leetcode.com/explore/interview/card/microsoft/30/array-and-strings/173/
        //https://leetcode.com/problems/two-sum/
        //https://leetcode.com/discuss/interview-question/372434 -- for duplication
        //Given nums = [2, 7, 11, 15], target = 9,  Because nums[0] + nums[1] = 2 + 7 = 9, return [0, 1].
        public static void TwoSum(int[] nums, int target)
        {
            var dict = new Dictionary<int, int>();
            HashSet<int> seen = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (target < nums[i]) continue;
                var remainder = target - nums[i];
                int result;
                int count=0;
                if (dict.TryGetValue(nums[i], out result) && !seen.Contains(nums[i]))
                {
                    seen.Add(nums[i]);
                    Console.WriteLine(i + "," + result);
                    count++;
                }
                else if(!dict.ContainsKey(remainder))
                {
                    dict.Add(remainder, i);
                }
            }
        }

        //2. Valid Palindrome
        //https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
        //Input: "A man, a plan, a canal: Panama" Output: true
        //Input: "race a car" Output: false
        public static bool IsWordPalindrome(string input)
        {
            StringBuilder sb = new StringBuilder();
            int len = input.Length;
            input = input.ToLower();
            for (int i = 0; i < len; i++)
            {
                char cv = input[i];
                if (!(cv >= 'a' && cv <= 'z') || (cv >= '0' && cv <= '9'))
                    continue;
                else
                    sb.Append(cv);
            }
            string first = sb.ToString();
            string second = new string(first.Reverse().ToArray());
            bool result = first.Equals(second);
            return result;
        }

        //Amazon Question
        //3.. String to Int ( atoi )
        //Ans: https://leetcode.com/problems/string-to-integer-atoi/discuss/522015/Concise-C-Solution
        //Quest: https://leetcode.com/problems/string-to-integer-atoi/
        //Input: "42" Output: 42, Input: "   -42" Output: -42, Input: "4193 with words"  Output: 4193
        public static int StringToIntConversion(string str)
        {
            int res = 0, b = 0;
            if (string.IsNullOrWhiteSpace(str)) return res;
            while (b < str.Length && str[b] == ' ') b++;
            bool isNeg = str[b] == '-';
            if (str[b] == '+' || isNeg) b++;
            for (int i = b; i < str.Length; i++)
            {
                var d = str[i] - '0';
                if (d < 0 || d > 9) break;
                try
                {
                    res = checked((res * 10) + d);
                }
                catch { return isNeg ? int.MinValue : int.MaxValue; }
            }
            return isNeg ? -res : res;
        }

        //4. Reverse String
        //Input: ["h","e","l","l","o"]     Output: ["o","l","l","e","h"]
        //Input: ["H","a","n","n","a","h"]  Output: ["h","a","n","n","a","H"]
        public void ReverseString(char[] s)
        {
            int len = s.Length;
            int i = 0;
            int j = len - 1;
            while (i < len / 2)
            {
                var temp = s[i];
                s[i] = s[j];
                s[j] = temp;
                i++;
                j--;
            }
        }

        //5. Reverse Words in a String
        //https://leetcode.com/problems/reverse-words-in-a-string/
        //Given s = "the sky is blue", return "blue is sky the".
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

        //Amazon Question
        //6. Valid Parantheses
        //https://leetcode.com/problems/valid-parentheses/
        //Input: "()" Output: true, Input: "([)]" Output: false
        public static bool ValidParantheses()
        {
            bool isValid = true;
            string input = "({})";//"(]";//"()[]{}";

            var stack = new Stack<char>();
            char temp;

            foreach (var i in input)
            {
                switch (i)
                {
                    case ')':
                        temp = stack.Pop();
                        if (temp != '(')
                            isValid = false;
                        break;
                    case ']':
                        temp = stack.Pop();
                        if (temp != '[')
                            isValid = false;
                        break;
                    case '}':
                        temp = stack.Pop();
                        if (temp != '{')
                            isValid = false;
                        break;
                    default:
                        stack.Push(i);
                        break;
                }
                if (!isValid)
                    break;

            }
            return isValid;
        }

        //Amazon Question
        //7. Longest Palindromic Substring
        //Input: "babad" Output: "bab" Note: "aba" is also a valid answer.
        //https://leetcode.com/problems/longest-palindromic-substring/
        //https://www.geeksforgeeks.org/longest-palindromic-substring-set-2/
        public static int LongestPalindrome(string str)
        {
            //string str = "babad";
            //str = "forgeeksskeegfor";
            int low, high,start=0;
            int len = str.Length;
            int maxLength = 1;
            for(int i=1; i< len; i++)
            {
                // Find the longest even length  5
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
            return maxLength; // Length of substring
            //return substring; // return substring.
        }

        //Amazon Qustion
        //8. Group Anagrams
        //https://leetcode.com/problems/group-anagrams/
        //https://leetcode.com/problems/group-anagrams/discuss/518006/Method-with-HashMap-is-much-faster
        //Input: ["eat", "tea", "tan", "ate", "nat", "bat"], Output: [   ["ate","eat","tea"],   ["nat","tan"],   ["bat"] ]
        public static List<List<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
            for (int i = 0; i < strs.Length; i++)
            {
                char[] chars = strs[i].ToCharArray();
                Array.Sort(chars);
                string curKey = new string(chars);
                if (map.ContainsKey(curKey))
                {
                    map[curKey].Add(strs[i]);
                }
                else
                {
                    List<string> newSub = new List<string>();
                    newSub.Add(strs[i]);
                    map.Add(curKey, newSub);
                }
            }
            List<List<string>> val = map.Values.ToList();
            return val;
        }

        //Amazon Question
        //9. Trapping train water.
        //https://leetcode.com/problems/trapping-rain-water/discuss/362788/Java-or-Time-O(N)-Space-O(N)
        //https://leetcode.com/problems/trapping-rain-water/

        public int Trap(int[] height)
        {

            if (height.Length == 0)
                return 0;

            int[] A = new int[height.Length];
            int[] B = new int[height.Length];

            int maxA = height[0];
            int maxB = height[height.Length - 1];

            for (int i = 0; i < height.Length; i++)
            {
                maxA = Math.Max(height[i], maxA);
                A[i] = maxA;
            }
            for (int i = height.Length - 1; i >= 0; i--)
            {
                maxB = Math.Max(height[i], maxB);
                B[i] = maxB;
            }

            int sum = 0;

            for (int i = 0; i < height.Length; i++)
            {
                sum = sum + Math.Abs(height[i] - Math.Min(A[i], B[i]));
            }
            return sum;
        }

        //10 Set Matrix Zeroes
        //Given a m x n matrix, if an element is 0, set its entire row and column to 0
        //https://leetcode.com/explore/interview/card/microsoft/30/array-and-strings/203/

        public static void SetZeros()
        {
            int[,] input = new int[,] { { 0, 5, 6, 0 }, { 2, 3, 6, 9 }, { 5, 7, 8, 1 } };

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

        public static int[,] SetZeroForRowsAndColumns(int[,] input, int rowIndex, int colIndex)
        {
            int rowIndexUpperBound = input.GetLength(0);
            int colIndexUpperBound = input.GetLength(1);

            //Set Zero for Rows
            for (int i = 0; i < colIndexUpperBound; i++)
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

        //Amazon Question
        //11  Rotate Image
        //https://leetcode.com/problems/rotate-image/
        // Input: [[1,2],[3,4]] Outpu: [[3,1],[4,2]]
        //https://leetcode.com/explore/interview/card/microsoft/30/array-and-strings/202/
        public static void RotateImage(int[,] matrix)
        {
            int n = matrix.GetLength(0);

            // transpose
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    int t = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = t;
                }
            }

            // reverse / swap only half
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n / 2; j++)
                {
                    int t = matrix[i, j];
                    matrix[i, j] = matrix[i, n - j - 1];
                    matrix[i, n - j - 1] = t;
                }
            }
        }

        //12. Spiral Matrix
        // https://leetcode.com/problems/spiral-matrix/
        //https://www.geeksforgeeks.org/print-a-given-matrix-in-spiral-form/
        public static void SpiralMatrix()
        {
            int[,] a = new int[,] { { 1, 2, 3, 10 },
                                    { 4, 5, 6, 11 }, 
                                    { 7, 8, 9, 12 },
                                    { 13, 14, 15, 16 }};
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

         //Amazon Question
        //13. Letter Combination
        List<string> result = new List<string>();
        Dictionary<char, string> ld = new Dictionary<char, string>();
        public IList<string> LetterCombinations(string digits)
        {

            if (string.IsNullOrWhiteSpace(digits))
                return result;

            ld.Add('1', "");
            ld.Add('2', "abc");
            ld.Add('3', "def");
            char[] idigits = digits.ToCharArray();
            dfs(idigits, 0, new StringBuilder());
            return result;

        }
        void dfs(char[] idigits, int index, StringBuilder sb)
        {
            if (index == idigits.Length)
            {
                result.Add(sb.ToString());
                return;
            }

            foreach (char c in ld[idigits[index]])
            {
                dfs(idigits, index + 1, sb.Append(c));
                sb.Length--;
            }
        }

        //14. Remove duplicates from sorted array
        //https://leetcode.com/problems/remove-duplicates-from-sorted-array/
        // https://leetcode.com/problems/remove-duplicates-from-sorted-array/discuss/521364/Straightforward-and-optimised-Go-solution-(8ms-46MB)
        public static int RemoveDuplicates(int[] array)
        {
            int j = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != array[j])
                    j++;
                array[j] = array[i];
            }
            return j + 1;
        }

        //15. Merge Sorted Arrays
        //nums1 = [1,2,3,0,0,0], m = 3
        //nums2 = [2,5,6],       n = 3
        //Output: [1,2,2,3,5,6]

        public static int[] MergeTwoSortedArrays(int[] first, int[] second)
        {
            int i = 0, j = 0, k = 0;
            int[] result = new int[first.Length + second.Length];
            while (i < first.Length && j < second.Length)
            {
                if (first[i] < second[j])
                {
                    result[k++] = first[i++];
                }
                else
                {
                    result[k++] = second[j++];
                }
            }
            while (i < first.Length)
            {
                result[k++] = first[i++];
            }
            while (j < second.Length)
            {
                result[k++] = second[j++];
            }
            return result;
        }

        //16. Sort Colors
        //https://leetcode.com/problems/sort-colors/  //Input: [2,0,2,1,1,0]  Output: [0,0,1,1,2,2] -> Red 0, White 1, Blue 2
        //https://leetcode.com/problems/sort-colors/discuss/517277/Fast-1-pass-JAVA-solution
        public static void SortColors(int[] nums)
        {
            int left = 0;
            int right = nums.Length - 1;
            int curr = left;

            while (curr <= right)
            {
                int num = nums[curr];
                if (num == 0)
                {
                    nums[curr] = nums[left];
                    nums[left] = 0;
                    left++;
                    curr = left;
                }
                else if (num == 2)
                {
                    nums[curr] = nums[right];
                    nums[right] = 2;
                    right--;
                }
                else
                {
                    curr++;
                }
            }

        }

        //17 Find a minimum in Rotational Array
        //Input: [3,4,5,1,2] Output: 1
        //Input: [1,3,5] Output: 1
        //Input: [2,2,2,0,1] Output: 0
        public int FindMin(int[] nums)
        {
            int pivot = FindPivot(nums, 0, nums.Length - 1);
            if (pivot == -1)
                return nums[0];
            else
                return nums[pivot + 1];
        }
        public static int FindPivot(int[] input, int min, int max)
        {
            if (input == null) return -1;
            if (min > max) return -1;
            int mid = min + max / 2;
            if (mid < max && input[mid] > input[mid + 1])
                return mid;
            if (mid > min && input[mid] < input[mid - 1])
                return mid - 1;
            if (input[min] >= input[mid])
                return FindPivot(input, min, mid - 1);
            else
                return FindPivot(input, mid + 1, max);
        }

        //Amazon Question
        //18. Search in Rotational Sorted Array
        //Input: nums = [4,5,6,7,0,1,2], target = 0  Output: 4
        //https://leetcode.com/explore/interview/card/microsoft/47/sorting-and-searching/191/

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

        public static int BinarySearch(int[] input, int element, int low, int high)
        {
            if (input == null || input.Length <= 0) return -1;
            if (low > high) return -1;
            int mid = (low + high) / 2;
            if (input[mid] == element) return mid;
            if (input[mid] < element) return BinarySearch(input, element, (mid + 1), high);
            return BinarySearch(input, element, low, (mid - 1));
        }

        //19. Search in 2D Matrix
        //https://leetcode.com/explore/interview/card/microsoft/47/sorting-and-searching/154/
        //Input:
        //matrix = [
        //  [1,   3,  5,  7],
        //  [10, 11, 16, 20],
        //  [23, 30, 34, 50]
        //]
        //target = 3
        //Output: true
        public static bool SearchMatrix(int[,] matrix, int target)
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);

            int m = matrix.GetLength(0);
            if (m == 0) return false;
            int n = matrix.GetLength(1);

            // binary search
            int left = 0, right = m * n - 1;
            int pivotIdx, pivotElement;
            while (left <= right)
            {
                pivotIdx = (left + right) / 2;
                pivotElement = matrix[pivotIdx / n, pivotIdx % n];
                if (target == pivotElement) return true;
                else
                {
                    if (target < pivotElement) right = pivotIdx - 1;
                    else left = pivotIdx + 1;
                }
            }
            return false;
        }

        //20. Search a 2D Matrix II
        //[1,   4,  7, 11, 15],
        //[2,   5,  8, 12, 19],
        //[3,   6,  9, 16, 22],
        //[10, 13, 14, 17, 24],
        //[18, 21, 23, 26, 30]
        //Given target = 5, return true.
        //Given target = 20, return false.
        //https://leetcode.com/problems/search-a-2d-matrix-ii/
        // https://leetcode.com/problems/search-a-2d-matrix-ii/discuss/519303/C-don't-know-what-approach-this-is-90
        public static bool SearchMatrixII(int[,] matrix, int target)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            if (rows == 0 || cols == 0) return false;

            var row = 0;
            var col = cols - 1;

            while (row < rows && col >= 0)
            {
                if (matrix[row, col] == target) return true;
                if (matrix[row, col] < target) row++;
                else col--;
            }
            return false;
        }

        //Amazon Question
        //21. Median of Two Sorted Arrays
        //nums1 = [1, 3] nums2 = [2] The median is 2.0
        //nums1 = [1, 2] nums2 = [3, 4] The median is (2 + 3)/2 = 2.5
        public static int FindMedianOfTwoArrays(int[] first, int[] second)
        {
            int flen = first.Length;
            int slen = second.Length;

            if (flen > slen)
                return FindMedianOfTwoArrays(second, first);

            int min = 0;
            int max = flen;

            while (min <= max)
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

        //Amazon Question
        //22. Best Time to Buy and Sell Stock
        //Input: [7,1,5,3,6,4]
        //Output: 5
        //Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6-1 = 5.
        //Not 7-1 = 6, as selling price needs to be larger than buying price.
        //https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
        //https://leetcode.com/problems/best-time-to-buy-and-sell-stock/discuss/522783/A-Python-Solution-with-O(n)-time-complexity-and-O(1)-space-complexity
        public static int BestBuyAndSellStock(int[] stocks)
        {
            int minprice = int.MaxValue;
            int maxprofit = 0;
            foreach (var stock in stocks)
            {
                if (stock < minprice)
                    minprice = stock;
                else if (maxprofit < stock)
                    maxprofit = stock - minprice;
            }
            return maxprofit;
        }

        //23. SingleNumber
        //Given a non-empty array of integers, every element appears twice except for one. Find that single one.
        //https://leetcode.com/explore/interview/card/microsoft/48/others/208/
        //Input: [2,2,1] Output: 1
        //Input: [4,1,2,1,2] Output: 4
        public int SingleNumber(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;
            int res = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                res = res ^ nums[i];
            }
            return res;
        }

        //Amazon Question
        //24. Integer to English Words
        //https://www.geeksforgeeks.org/the-celebrity-problem/
        //Input: 123 Output: "One Hundred Twenty Three"
        public static string NumberToWords(int n)
        {
            string word = string.Empty;
            if (n != 0) return null;
            if (n < 0)
            {
                word += "Minus";
                NumberToWords(Math.Abs(n));
            }
            if (n > 1000000)
            {
                word += $"{n / 1000000} millions";
                n = n % 1000000;
            }
            if (n > 1000)
            {
                word += $"{n / 1000} thousands";
                n = n % 1000;
            }
            if (n > 100)
            {
                word += $"{n / 100} hundreds";
                n = n % 100;
            }
            if (n > 0)
            {
                var twoMap = new string[] { "Zero", "Ten", "Twenty", "Thirty", "", "", "", "Ninty" };
                var oneMap = new string[] { "Zero", "one", "Two", "", "", "", "Ten", "eleven", "Tweleve", "", "", "Ninteen" };
                if (n < 20)
                {
                    word += oneMap[n];
                }
                else
                {
                    word += twoMap[n / 10];
                    n = n % 10;
                    if (n > 0)
                        word += oneMap[n];
                }
            }
            return word;
        }

        //Amazon Question
        //25. Find Islands
        public static void FindIsLand()
        {
            int[][] ab = new int[4][];
            

            int[,] a = new int[,] { 
                                   { 1, 1, 0, 0, 0 }, 
                                   { 1, 1, 0, 0, 0 }, 
                                   { 0, 0, 1, 0, 0 },
                                   { 0, 0, 0, 1, 1 } 
                                  };
            int iland = 0;
            for (int i = 0; i <= a.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= a.GetUpperBound(1); j++)
                {
                    if (a[i, j] == 1)
                    {
                        iland++;
                        Iland(a, i, j);
                    }
                }
            }
        }

        private static void Iland(int[,] ab, int i, int j)
        {
            if (i < 0 || j < 0 || i >= ab.GetLength(0) || j >= ab.GetLength(1) || ab[i, j] == 0)
                return;

            ab[i, j] = 0;


            Iland(ab, i + 1, j);
            Iland(ab, i, j + 1);
            //Iland(ab, i + 1, j + 1);
            Iland(ab, i - 1, j);
            Iland(ab, i, j - 1);
            //Iland(ab, i - 1, j - 1);
            //Iland(ab, i - 1, j + 1);
            //Iland(ab, i + 1, j - 1);

        }

        //https://leetcode.com/problems/number-of-closed-islands/

        public int ClosedIsland(int[][] grid)
        {
            int count = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == 0 && DFS(grid, i, j))
                        count++;
                }
            }
            return count;
        }

        private bool DFS(int[][] grid, int i, int j)
        {
            if (i < 0 || i == grid.Length ||
                j < 0 || j == grid[0].Length ||
                grid[i][j] == 1)
                return true;

            if (i == 0 || i == grid.Length - 1 ||
                j == 0 || j == grid[0].Length - 1)
                return false;

            grid[i][j] = 1;

            return DFS(grid, i + 1, j) &
                   DFS(grid, i, j - 1) &
                   DFS(grid, i - 1, j) &
                   DFS(grid, i, j + 1);
        }
    }
}
