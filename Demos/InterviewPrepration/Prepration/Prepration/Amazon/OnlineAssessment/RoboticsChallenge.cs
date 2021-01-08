using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class RoboticsChallenge
    {
        //https://leetcode.com/problems/baseball-game/

        //You are keeping score for a baseball game with strange rules.The game consists of several rounds, where the scores of past rounds may affect future rounds' scores.

        //At the beginning of the game, you start with an empty record. You are given a list of strings ops, where ops[i] is the ith operation you must apply to the record and is one of the following:

        //An integer x - Record a new score of x.
        //"+" - Record a new score that is the sum of the previous two scores.It is guaranteed there will always be two previous scores.
        //"D" - Record a new score that is double the previous score.It is guaranteed there will always be a previous score.
        //"C" - Invalidate the previous score, removing it from the record.It is guaranteed there will always be a previous score.
        //Return the sum of all the scores on the record.




        //Example 1:


        //Input: ops = ["5", "2", "C", "D", "+"]
        //Output: 30
        //Explanation:
        //"5" - Add 5 to the record, record is now[5].
        //"2" - Add 2 to the record, record is now[5, 2].
        //"C" - Invalidate and remove the previous score, record is now[5].
        //"D" - Add 2 * 5 = 10 to the record, record is now[5, 10].
        //"+" - Add 5 + 10 = 15 to the record, record is now[5, 10, 15].
        //The total sum is 5 + 10 + 15 = 30.
        //Example 2:


        //Input: ops = ["5", "-2", "4", "C", "D", "9", "+", "+"]
        //Output: 27
        //Explanation:
        //"5" - Add 5 to the record, record is now[5].
        //"-2" - Add -2 to the record, record is now[5, -2].
        //"4" - Add 4 to the record, record is now[5, -2, 4].
        //"C" - Invalidate and remove the previous score, record is now[5, -2].
        //"D" - Add 2 * -2 = -4 to the record, record is now[5, -2, -4].
        //"9" - Add 9 to the record, record is now[5, -2, -4, 9].
        //"+" - Add -4 + 9 = 5 to the record, record is now[5, -2, -4, 9, 5].
        //"+" - Add 9 + 5 = 14 to the record, record is now[5, -2, -4, 9, 5, 14].
        //The total sum is 5 + -2 + -4 + 9 + 5 + 14 = 27.
        //Example 3:


        //Input: ops = ["1"]
        //Output: 1

        public static int CalPoints(string[] ops)
        {

            var st = new Stack<int>();
            string op = "CD+";
            int score = 0;
            foreach (string s in ops)
            {
                if (!op.Contains(s))
                {
                    int res = int.Parse(s);
                    st.Push(res);
                    score += res;
                }
                else
                {
                    switch (s)
                    {
                        case "C":
                            {
                                int val = st.Pop();
                                score -= val;
                            }
                            break;

                        case "D":
                            {
                                int topValue = st.Peek();
                                topValue *= 2;
                                st.Push(topValue);
                                score += topValue;
                            }
                            break;

                        case "+":
                            {
                                int first = st.Pop();
                                int second = st.Pop();
                                st.Push(second);
                                st.Push(first);
                                int sum = first + second;
                                score += sum;
                                st.Push(sum);
                            }
                            break;
                    }
                }
            }
            return score;

        }
    }
}
