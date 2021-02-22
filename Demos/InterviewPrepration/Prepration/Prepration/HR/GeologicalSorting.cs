using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Prepration
{
    class GeologicalSorting
    {
        //Hackerrank  Geological Sorting

        //A team of geologists attempting to measure the differences in carbon-dated volcanic materials and non-volcanic materials.You need to compare the volcanic material 
        //with non-volcanic material of the same carbon-dated age in order to establish a base for testing.
        //You are given 2 arrays, volcanic and nonVolcanic.Find a list of materials with identical dates between both arrays.Return this list in descending order
        //(i.e, the highest age followed by second highest, with the lowest age at the end).
        //For example, you are given the following two arrays:

        //volcanic = [7000, 13400, 7000, 14000] ,and
        //nonVolcanic = [7000, 13400, 150000, 7000]

        //Your return array should show: result = [13400, 7000, 7000]

        //The date 7000 is present twice in both input arrays.Therefore, there are two matches and both should be returned in the output array.Likewise, 
        //13400 is present in both arrays and should be returned in the output array.However, there is no matching number for 150000 in volcanic or 14000 in nonVolcanic, 
        //so these two numbers should not be returned in result.

        //Function Description

        //Complete the function sortIntersect in the editor below.The function must return an array(in descending order) of the dates of matching pairs 
        //that can be created between volcanic and nonVolcanic.

        //sortIntersect has the following parameter(s):

        //volcanic[volcanic[0], ...volcanic[n - 1]]:  an array of integers

        // nonVolcanic[nonVolcanic[0], ...nonVolcanic[o - 1]]:  an array of integers

        //Constraints

        //5 ≤ n, o ≤ 1,000

        //6,600 ≤ volcanic[i], nonVolcanic[j] ≤ 18,300

        //The first line contains an integer, n, indicating the size of the array volcanic.
        //Each of the next n lines contains an integer, volcanic[i].
        //The next line contains an integer, o, indicating the size of the array nonVolcanic.
        //Each of the next o lines contains an integer, nonVolcanic[j].

        //Sample Input 0
        //5
        //7000
        //7000
        //12000
        //13000
        //6900
        //7
        //6910
        //7010
        //7000
        //12000
        //18000
        //15000
        //10450
        //Sample Output 0

        //12000
        //7000
        //Explanation

        //volcanic = { 7000, 7000, 12000, 13000, 6900 }

        //nonVolcanic = {6910, 7010, 7000, 12000, 18000, 15000, 10450}

        //sortIntersect(volcanic, nonVolcanic) = {12000, 7000} 

        //The integers 12000 and 7000 appear in both arrays.Although there is a second piece of volcanic material with the same 7000-year age,
        //it has no equivalent in non-volcanic material, so it cannot be duplicated in the output array.

        //One Way
        public static int[] SortIntersect(int[]volcanic, int[]nonVolcanic)
        {
            Array.Sort(volcanic);
            Array.Sort(nonVolcanic);

            List<int> ans = new List<int>();

            int i = volcanic.Length-1;
            int j = nonVolcanic.Length-1;

            while( i >=0 && j >= 0)
            {
                if (volcanic[i] == nonVolcanic[j])
                {
                    ans.Add(volcanic[i]);
                    i -= 1;
                    j -= 1;
                }
                else if (volcanic[i] > nonVolcanic[j])
                {
                    i -= 1;
                }
                else
                {
                    j -= 1;
                }
            }
            return ans.ToArray();

        }

        public static int[] SortIntersect1(int[] volcanic, int[] nonVolcanic)
        {
            const int LOW = 6600;
            const int HIGH = 18300;

            int[] vcount = new int[HIGH - LOW + 1];
            int[] nvcount = new int[HIGH - LOW + 1];

            List<int> ans = new List<int>();

            foreach (int v in vcount)
                vcount[v - LOW] += 1;

            foreach (int nv in nvcount)
                nvcount[nv - LOW] += 1;

            //# compare the frequencies from right to left
            //for i in range(HIGH - LOW, -1, -1):

            //# add zero or more instances of the current index to the answer array
            //# don't forget to adjust the index back up to the appropriate year
            //ans.extend([i + LOW] * min(vCounts[i], nvCounts[i]))

            return ans.ToArray();
        }
    }
}
