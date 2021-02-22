using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.HR
{
    class SortAnArray
    {

        //Given an array of integers, any array element can be moved to the end in one move.Determine the minimum number of moves required to sort the array, ascending.

        //Example:

        //arr = [5, 1, 3, 2]

        //Move the value arr[2] = 3 to the end to get arr = [5, 1, 2, 3].
        //Move arr[0] = 5 to the end to achieve the sorted array, arr = [1, 2, 3, 5].
        //The minimum number of moves required to sort the array is 2.

        //Function Description

        //Complete the function getMinimumMoves in the editor below.

        //getMinimumMoves has the following parameter:

        //int arr[n]:  an array of integers

        //Returns:

        //int: the minimum number of moves needed to sort the array in ascending order

        //Constraints

        //1 ≤ n ≤ 105
        //0 ≤ arr[i] ≤ 106
        //array elements are distinct

        public static int GetMinimumMoves(int[] arr)
        {
            int n = arr.Length;
            int[] arr1 = new int[n];
            Array.Copy(arr, arr1, n);
            Array.Sort(arr1);

            int i = 0;
            int j = 0;

            while( i < n)
            {
                if (arr[i] == arr1[j])
                    j = j + 1;
                i++;
            }
            int res = n - j;
            return res;

        }

    }
}
