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

        // 3. Rotation Search Elements
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

        //10. Set Matrix Zeroes
        //Given a m x n matrix, if an element is 0, set its entire row and column to 0

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

        // 11. Find the Median 
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

        //12. Merge two Sorted Array
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
                if(!isfound)
                {
                    element = input[i];
                }
            }
            return element;
        }


        //15. https://www.geeksforgeeks.org/the-celebrity-problem/

        public static string NumberToWords(int n)
        {
            string word = string.Empty;
            if (n != 0) return null;
            if(n < 0)
            {
                word += "Minus";
                NumberToWords(Math.Abs(n));
            }
            if(n > 1000000)
            {
                word += $"{n/1000000} millions";
                n = n % 1000000;
            }
            if(n > 1000)
            {
                word += $"{n / 1000} thousands";
                n = n % 1000;
            }
            if(n > 100)
            {
                word += $"{n / 100} hundreds";
                n = n % 100;
            }
            if(n > 0)
            {
                var twoMap = new string[] { "Zero","Ten","Twenty","Thirty","","","","Ninty"};
                var oneMap = new string[] { "Zero", "one", "Two", "", "", "", "Ten", "eleven", "Tweleve", "", "", "Ninteen" };
                if(n < 20)
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

        //16. 
        //https://www.geeksforgeeks.org/find-maximum-value-of-sum-iarri-with-only-rotations-on-given-array-allowed/
        public static int maxSum()
        {
            int[] arr = new int[]{10, 1, 2, 3, 4, 5, 6, 7, 8, 9};
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
            for (int i=1; i<array.Length;i++)
            {
                result = result ^ array[i];
            }
        }

        //19. Find the sum of target
        public static void FindTheSumOfTargetIndices()
        {
            int[] arrays = new int[] { 4, 6, 8, 2, 3, 7, 1 };
            int target = 5;
            for (int i = 0; i < arrays.Length; i++)
            {
                for (int j = i; j < arrays.Length; j++)
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
            for (int i=0; i<=a.GetUpperBound(0); i++)
            {
                for(int j=0; j<=b.GetUpperBound(1); j++)
                {
                    c[i, j] = 0;
                    for(int k=0; k<=a.GetUpperBound(1); k++)
                    {
                        c[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            //Array Multiplication (3*3)

        }

        //21. Find Islands
        public static void FindIsLand()
        {
            int[,] a = new int[,] { { 0,1,0,0 }, { 0,0,0,1}, { 0,1,0,0 }, {1,1,0,0 } };
            int iland = 0;
            for(int i=0; i<=a.GetUpperBound(0); i++)
            {
                for(int j=0; j<=a.GetUpperBound(1); j++)
                {
                    if(a[i,j] == 1)
                    {
                        iland++;
                        Iland(a, i, j);
                    }
                }
            }
        }

        private static void Iland(int[,] ab, int i,int j)
        {
            if(ab[i,j] == 1)
            {
                ab[i, j] = 0;

                try
                {
                    Iland(ab, i + 1, j);
                    Iland(ab, i, j + 1);
                    Iland(ab, i + 1, j + 1);
                    Iland(ab, i - 1, j);
                    Iland(ab, i, j - 1);
                    Iland(ab, i - 1, j - 1);
                    Iland(ab, i - 1, j + 1);
                    Iland(ab, i + 1, j - 1);
                }
                catch (Exception)
                {
                }
            }
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

            string path=string.Empty;
            //Get the path
            int k=0; int l = 0; int one = 0;
            while (k < a.GetUpperBound(0) || l < a.GetUpperBound(1))
            {
                if (k == 0 && l == 0)
                {
                    path += $"{a[k, l]}-->";
                    k = 1; l = 1;
                }
                else
                {
                    if(one == 0)
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
                        if(k<a.GetUpperBound(0))
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

        //23. word search
        //https://www.geeksforgeeks.org/search-a-word-in-a-2d-grid-of-characters/
        public static void WordSearch()
        {
            char[,] wd = new char[,] { { 'C','O','R' }, { 'O','A','R' }, { 'A','O','R' } };
            string stext = "CAB";

            for(int i=0; i<wd.GetUpperBound(0); i++)
            {
                for(int j=0; j<wd.GetUpperBound(1); j++)
                {
                    if(wd[i,j] == stext[0])
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

            for(int dir=0; dir<8; dir++)
            {
                int rd = i + xd[dir];
                int cd = j + yd[dir];
                int l;
                for(l=1; l<text.Length;l++)
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

        //24.
        //https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/discuss/441225/Share-my-simple-solution

        //25. Converted sorted Array to Binary Search Tree
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

        //26. https://leetcode.com/problems/asteroid-collision/
        // Input: asteroids = [5, 10, -5] Output: [5, 10]
        public static void AsteroidCollision()
        {
            var res = new List<int>();
            int[] ast = new int[] { 5, 10, -5 };// {8,-8} {-2, -1, 1, 2 } {  5, 10, -5 };
            var stack = new Stack<int>();
            foreach(var i in ast)
            {
                stack.Push(i);
            }

            while(stack.Count > 0)
            {
                if (ast.Length == 1)
                    res.Add(stack.Pop());
                else if (ast.Length == 2)
                {
                    var e1 = stack.Pop();
                    var e2 = stack.Pop();

                    if ((e1 < 0 && e2 < 0) || (e1 > 0 && e2 > 0))
                    {
                        res.Add(e1);
                        res.Add(e2);
                    }
                }
                else if(stack.Count > 1)
                {
                    var e1 = stack.Pop();
                    var e2 = stack.Pop();

                    if ((e1 < 0 && e2 < 0) || (e1 > 0 && e2 > 0))
                    {
                        res.Add(e1);
                        res.Add(e2);
                    }
                    else
                    {
                        if (Math.Abs(e1) > Math.Abs(e2))
                            stack.Push(e1);
                        else
                            stack.Push(e2);

                    }
                }
                else
                {
                    while (stack.Count > 0)
                        res.Add(stack.Pop());
                }
            }            
        }

        //27. Trapping train water.
        //https://leetcode.com/problems/trapping-rain-water/discuss/362788/Java-or-Time-O(N)-Space-O(N)
        //https://leetcode.com/problems/trapping-rain-water/


    }
}
