using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.HR
{
    class PlusMultArray
    {
            //A is an array of integers described as {A[0], A[1], A[2], A[3],..., A[n - 1]
            //Perform the following calculations on the elements of A: 

            //(Reven = (((((A[0] × A[2]) + A[4]) × A[6]) + A[8]) × ... ) % 2 )
            //(Rodd = (((((A[1] × A[3]) + A[5]) × A[7]) + A[9]) × ... ) % 2 )
 
            //Notice that zero-indexing is used to calculate Reven and Rodd, and they are always modulo 2. 

            //You can then use Reven and Rodd to determine if A is odd, even, or neutral using the criterion below:

            //If Rodd > Reven , then A is ODD.
            //If Reven > Rodd , then A is EVEN.
            //If Rodd = Reven, then A is NEUTRAL.

            //For example, given the array A = [12,3,5,7,13,12], calculations are:

            //Reven = A[0] * A[2] = 12 * 5 = 60
            //Reven + A[4] = 60 + 13 = 73
            //Rodd = A[1] * A[3] = 3 * 7 = 21
            //Rodd + A[5] = 21 + 12 = 33 

            //The final values are Reven % 2 = 73 % 2 = 1, Rodd % 2 = 33 % 2 = 1 so the answer is NEUTRAL.

            //Function Description

            //Complete the function plusMult in the editor below.The function must return a string that denotes the relation: ODD, EVEN or NEUTRAL.

            //plusMult has the following parameter(s) :

            //A[A[0], ...A[n - 1]]:  an array of integers

            //Constraints

            //10 ≤ n< 105
            //-109 ≤ A[i] ≤ 109

        public static string PlusMult(int[] array)
        {
            int even = 0;
            int odd = 0;
            int evenIndex = 0;
            int oddIndex = 1;
            string result = "";
           while(oddIndex < array.Length && evenIndex < array.Length)
           {
                if (evenIndex + 2 < array.Length)
                    even += array[evenIndex] * array[evenIndex + 2];
                else if (evenIndex < array.Length)
                    even += array[evenIndex];

                if (oddIndex + 2 < array.Length)
                    odd += array[oddIndex] * array[oddIndex + 2];
                else if (oddIndex < array.Length)
                    odd += array[oddIndex];

                evenIndex += 4;
                oddIndex += 4;
            }

            if (even % 2 > odd % 2)
                result = "EVEN";
            else if (even % 2 < odd % 2)
                result = "ODD";
            else
                result = "NEUTRAL";

            return result;
        }

    }
}
