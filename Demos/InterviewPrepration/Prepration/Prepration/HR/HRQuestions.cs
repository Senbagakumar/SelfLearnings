using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.HR
{
    class HRQuestions
    {
        //https://allhackerranksolutionsbykaira.blogspot.com/search/label/HackerRank --> another link for preprations
        //https://github.com/alessandrobardini/HackerRank-Solutions/tree/master/All%20Tracks/Core%20CS/Algorithms/Implementation --> another link for preprations
        //https://github.com/tsyogesh40/HackerRank-solutions
        //https://www.youtube.com/playlist?list=PLSIpQf0NbcCltzNFrOJkQ4J4AAjW3TSmA

        //1. Balanced Array
        //arr=[1,2,3,4,6]
        //the sum of the first three elements, 1+2+3=6. The value of the last element is 6. 
        //Using zero based indexing, arr[3] = 4 is the pivot between the two subarrays.
        //The index of the pivot is 3.
        public static int balancedSum(List<int> arr)
        {
            int sum = 0;
            foreach (int val in arr)
            {
                sum += val;
            }

            int left = arr[0];
            for (int i = 1; i < arr.Count - 1; i++)
            {
                int l = left;
                int r = sum - arr[i] - left;
                if (l == r)
                {
                    return i;
                }
                left += arr[i];
            }

            return -1;
        }

        //2. Minimum Difference Sum
        //n = 5
        //arr = [1, 3, 3, 2, 4]
        //If the list is rearranged as arr' = [1, 2, 3, 3, 4], the absolute differences are |1 - 2| = 1, |2 - 3| = 1, |3 - 3| = 0, |3 - 4| = 1.  The sum of those differences is 1 + 1 + 0 + 1 = 3.
        public static int minDifference(int[] arr)
        {
            Array.Sort(arr);
            int sum = 0;
            for(int i=0; i<arr.Length-1; i++)
            {
                sum += (arr[i + 1] - arr[i]);
            }
            return sum;
        }

        //3. Product of the Maximum and Minimum in a Dataset
        //The integers in elements need to be ordered in such a way that after performing each operation, the product of the maximum and minimum values in the set can be easily calculated.
        //operations[] size n = 4
        //operations = [push, push, push, pop]
        //push 1 → elements = {1}, so the minimum = 1 and the maximum = 1. Then store the product as products0 = 1 × 1 = 1.
        //push 2 → elements = {1, 2}, so the minimum = 1 and the maximum = 2. Then store the product as products1 = 1 × 2 = 2.
        //push 3 → elements = {1, 2, 3}, so the minimum = 1 and the maximum = 3. Then store the product as products2 = 1 × 3 = 3.
        //pop 1 → elements = {2, 3}, so the minimum = 2 and the maximum = 3. Then store the product as products3 = 2 × 3 = 6.
        //Return products = [1, 2, 3, 6]

        //Another solutions: https://github.com/KalvaNaveen/Product-of-the-maximum-and-minimum-in-a-dataset/blob/master/script.js
        public static int[] maxMin(int[] arr, string[] operations)
        {
            var oplength = operations.Length;
            var list = new List<int>();
            var result = new List<int>();

            for(int i=0; i<oplength; i++)
            {
                if(operations[i].Equals("push"))
                {
                    list.Add(arr[i]);
                }
                else
                {
                    list.RemoveAt(0);
                }
                list.Sort();
                result.Add(list[0] * list[list.Count - 1]);
            }
            return result.ToArray();
        }

        //4. Binary Number in a Linked List
        //A binary number is represented as a series of 0's and 1's.In this challenge, the series will be in the form of a singly-linked list. Each node instance,
        //a LinkedListNode, has a value, data, and a pointer to the next node, next.
        //Given a reference to the head of a singly-linked list, convert the binary number represented to a decimal number.
        //0->1->0->0->1->1 --> 19
        //0, 0, 1, 1, 0, 1, 0 --> 26
        public static int GetNumber(int[] array)
        {
            int ans = 0;
            for(int i=0; i<array.Length; i++)
            {
                ans = ans * 2 + array[i];
            }
            return ans;
        }
        //5. Picking Tickets

        //Consider an array of n ticket prices, tickets.A number, m, is defined as the size of some subsequence, s, of tickets where each element 
        //covers an unbroken range of integers.That is to say, if you were to sort the elements in s, the absolute difference between any elements j and j + 1 
        //would be either 0 or 1. Determine the maximum length of a subsequence chosen from the tickets array.
        //Example
        //tickets = [8, 5, 4, 8, 4]
        //Valid subsequences, sorted, are { 4, 4, 5}
        //and {8, 8}. These subsequences have m values of 3 and 2, respectively.Return 3.

        public static int maxTickets(int[] tickets)
        {
            Array.Sort(tickets);
            int ans = 0;
            int i = 0;
            int j = 0;
            while(i < tickets.Length)
            {
                j = i + 1;
                while(j < tickets.Length && tickets[j]-tickets[j-1]-1 <= 1)
                {
                    j++;
                }
                ans = Math.Max(ans, j - i);
                i = j;
            }
            return ans;
        }

        //6. Array Game
        //Given an array of integers, determine the number of moves to make all elements equal.Each move consists of choosing all but 1 element and incrementing their values by 1.
        //Example

        //numbers = [3, 4, 6, 6, 3]
        //Choose 4 of the 5 elements during each move and increment each of their values by one. Indexing begins at 1. It takes 7 moves as follows:
        //                            Unchanged
        //Iteration            Array element's index
        //0		[ 3,  4,  6,  6,  3]
        //1		[ 4,  5,  7,  6,  4]        3
        //2		[ 5,  6,  7,  7,  5]        2
        //3		[ 6,  7,  8,  7,  6]        3
        //4 	[ 7,  8,  8,  8,  7]        2
        //5		[ 8,  9,  9,  8,  8]        3
        //6		[ 9,  9, 10,  9,  9]        1
        //7		[10, 10, 10, 10, 10]        2

        public static int countMoves(int[] inp)
        {
            int ans = 0;
            int minNumber = inp.Min();

            for(int i=0; i<inp.Length; i++)
            {
                ans += inp[i] - minNumber;
            }
            return ans;
        }

        //7. Bucket Fill
        //Digital graphics tools often make available a "bucket fill" tool that will only paint adjacent cells.In one fill, a modified bucket tool recolors adjacent 
        //cells(connected horizontally or vertically but not diagonally) that have the same color.Given a picture represented as a 2-dimensional array of letters representing colors, 
        //find the minimum number of fills to completely repaint the picture.
        //Example
        //picture= ["aabba", "aabba", "aaacb"] Output: (No. ofStorkes): 5
        //Each string represents a row of the picture and each letter represents a cell's color. The diagram below shows the 5 fills needed to repaint the picture. 
        //It takes two fills each for a and b, and one for c. The array picture is shown below.

        public static int strokesRequired(char[][] picture)
        {
            int rl = picture.Length;
            int cl = picture[0].Length;
            int result = 0;
            for(int i=0; i<rl; i++)
            {
                for(int j=0; j<cl; j++)
                {
                    if (picture[i][j] != '0')
                    {
                        result++;
                        dfs(picture, i, j, picture[i][j], '0');
                    }
                }
            }
            return result;
        }

        private static void dfs(char[][] image, int r, int c, char color, char newColor)
        {
            if (image[r][c] == color)
            {
                image[r][c] = newColor;
                if (r >= 1) dfs(image, r - 1, c, color, newColor);
                if (c >= 1) dfs(image, r, c - 1, color, newColor);
                if (r + 1 < image.Length) dfs(image, r + 1, c, color, newColor);
                if (c + 1 < image[r].Length) dfs(image, r, c + 1, color, newColor);
            }
        }

        //https://www.hackerrank.com/work/library/hackerrank?copyscrape=true&difficulties=Easy&page=2&question=397611&tags=Algorithms%2CProblem+Solving&view=question

    }
}
