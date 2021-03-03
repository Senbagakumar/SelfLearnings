using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.HR
{
    class OfficeDesign
    {

        //A company is repainting its office and would like to choose colors that work well together.They are presented with several various-priced color options presented 
        //in a specific order so that each color complements the surrounding colors.The company has a limited budget and would like to select the greatest number of 
        //consecutive colors that fit within this budget.Given the prices of the colors and the company's budget, what is the maximum number of colors that can be 
        //purchased for this repainting project? 

        //Example

        //prices = [2, 3, 5, 1, 1, 2, 1]

        //money = 7 

        //All subarrays that sum to less than or equal to 7:

        //Length 1 subarrays are[2], [3], [5], [1], [1], [2], [1]

        //Length 2 - [2, 3], [5, 1], [1, 1], [1, 2], [2, 1]

        //Length 3 - [5, 1, 1], [1, 1, 2], [1, 2, 1]

        //Length 4 - [1, 1, 2, 1]

        //The longest of these, or the maximum number of colors that can be purchased, is 4.

        //Function Description

        //Complete the function getMaxColors in the editor below.

        //getMaxColors has the following parameters:

        //int prices[n]:  the prices of the various paint colors

        //int money:  the amount of money the company can spend on paints

        //Returns:

        //int: the maximum number of colors the company can purchase

        //Constraints

        //1 ≤ n ≤ 105

        //1 ≤ prices[i] ≤ 100

        //1 ≤ money ≤ 106

        //Sample Input For Custom Testing

        //STDIN     Function 
        //-----     --------
        //3     →   prices[] size n = 3
        //10    →   prices = [ 10, 10, 10 ] 
        //10
        //10
        //5     →   money = 5 
        //Sample Output

        //0

        //    STDIN Function
        //-----     --------
        //3     →   prices[] size n = 3
        //5     →   prices = [ 5, 10, 10 ]
        //10
        //10
        //5     →   money = 5 
        //Sample Output

        //1

        public static int GetMaxColors(int[] prices, int money)
        {
            int sum = 0;
            int cnt = 0;
            int max_len = 0;
            for(int i=0; i<prices.Length; i++)
            {
                if(sum + prices[i] <= money)
                {
                    sum = sum + prices[i];
                    cnt = cnt + 1;
                }
                else if(sum!=0)
                {
                    sum = sum - prices[i - cnt] + prices[i];
                }
                max_len = Math.Max(max_len, cnt);
            }
            return max_len;
        }
    }
}
