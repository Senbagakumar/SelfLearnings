using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class Turnstile
    {
        //https://leetcode.com/discuss/interview-question/699973/
        //9. Turnstile.

        public static int[] getTimes(int numCustomers, int[] arrTime, int[] direction)
        {


            int i = 0;
            int j = 1;
            int time = arrTime[0];
            int previous = -1;

            int[] result = new int[numCustomers];
            while (i < numCustomers)
            {
                if (j < numCustomers && arrTime[j] - (time-1) <= 1)
                {
                    if (arrTime[i] != arrTime[j] && previous == -1)
                    {
                        result[i] = time;
                        previous = direction[i];
                        time++;
                        i = j;
                        j++;                        
                    }

                    else if (direction[i] > direction[j] && (previous == -1 || previous == 1))
                    {
                        result[i] = time;
                        previous = direction[i];
                        time++;
                        i = j;
                        j++;

                    }
                    else if (direction[i] > direction[j] && previous == 0)
                    {
                        result[j] = time;
                        previous = direction[j];
                        time++;
                        j++;
                    }
                    else if (direction[j] > direction[i] && (previous == -1 || previous == 1))
                    {
                        result[j] = time;
                        previous = direction[j];
                        time++;
                        j++;

                    }
                    else if (direction[j] > direction[i] && previous == 0)
                    {
                        result[i] = time;
                        previous = direction[i];
                        time++;
                        i = j;
                        j++;
                    }
                }
                else
                {
                    if (j >= numCustomers && arrTime[i] > time)
                        time = arrTime[i];
                    result[i] = time;
                    previous = direction[i];
                    time++;
                    i = j;
                    j++;

                }
            }
            return result;

            //https://leetcode.com/problems/minimum-difficulty-of-a-job-schedule/
            //Beta Testing.

            //https://leetcode.com/problems/largest-sum-of-averages/ 
            // Same Category as above



        }
    }
}
