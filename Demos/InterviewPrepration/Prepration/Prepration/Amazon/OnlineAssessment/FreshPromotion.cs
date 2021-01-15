using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class FreshPromotion
    {
        //https://leetcode.com/discuss/interview-question/1002811/Amazon-or-OA-20201-or-Fresh-Promotion

        //Consider the following secret code list: [[apple, apple], [banana, anything, banana]]
        //Based on the above secret code list, a customer who made either of the following purchases would win the prize:
        //orange, apple, apple, banana, orange, banana
        //apple, apple, orange, orange, banana, apple, banana, banana

        //Write an algorithm to output 1 if the customer is a winner else output 0.

        //Input
        //The input to the function/method consists of two arguments:
        //codeList, a list of lists of strings representing the order and grouping of specific fruits that must be purchased in order to win the prize for the day.
        //shoppingCart, a list of strings representing the order in which a customer purchases fruit.

        //Output
        //Return an integer 1 if the customer is a winner else return 0.

        //Note
        //'anything' in the codeList represents that any fruit can be ordered in place of 'anything' in the group. 'anything' has to be something, it cannot be "nothing."
        //'anything' must represent one and only one fruit.
        //If secret code list is empty then it is assumed that the customer is a winner.

        //Example 1:

        //Input: codeList = [[apple, apple], [banana, anything, banana]] shoppingCart = [orange, apple, apple, banana, orange, banana]
        //        Output: 1
        //Explanation:
        //codeList contains two groups - [apple, apple] and[banana, anything, banana].
        //The second group contains 'anything' so any fruit can be ordered in place of 'anything' in the shoppingCart.The customer is a winner as the customer has added fruits in the order of fruits in the groups and the order of groups in the codeList is also maintained in the shoppingCart.

        //Example 2:

        //Input: codeList = [[apple, apple], [banana, anything, banana]]
        //shoppingCart = [banana, orange, banana, apple, apple]
        //        Output: 0
        //Explanation:
        //The customer is not a winner as the customer has added the fruits in order of groups but group[banana, orange, banana] is not following the group[apple, apple] in the codeList.

        //Example 3:

        //Input: codeList = [[apple, apple], [banana, anything, banana]] shoppingCart = [apple, banana, apple, banana, orange, banana]
        //        Output: 0
        //Explanation:
        //The customer is not a winner as the customer has added the fruits in an order which is not following the order of fruit names in the first group.

        //Example 4:

        //Input: codeList = [[apple, apple], [apple, apple, banana]] shoppingCart = [apple, apple, apple, banana]
        //        Output: 0
        //Explanation:
        //The customer is not a winner as the first 2 fruits form group 1, all three fruits would form group 2, but can't because it would contain all fruits of group 1.

        public bool IsWinner(List<List<string>> secrets, List<string>shoppingCart)
        {
            int i=0, k = 0;
            while(i < secrets.Count)
            {
                List<string> secret = secrets[i];
                int j = 0;
                while(j < secret.Count)
                {
                    if (k == shoppingCart.Count) return false;
                    if(secret[j] == "anything" || secret[j] == shoppingCart[k])
                    {
                        j++;
                        k++;
                    }
                    else
                    {
                        k -= j - 1;
                        j = 0;
                    }
                }
                i++;
            }
            return true;
        }
    }
}
