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

        //Time out.
        public static int[] getTimes(int numCustomers, int[] arrTime, int[] direction)
        {


            int i = 0;
            int j = 1;
            int time = arrTime[0];
            int previous = -1;

            int[] result = new int[numCustomers];
            while (i < numCustomers)
            {
                if (j < numCustomers && arrTime[j] - (time - 1) <= 1)
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
        }


        //Another Solution
        public static int[] getTimes1(int N, int[] t, int[] dir)
        {
            Queue<(int, int)> entry = new Queue<(int, int)>(); // pair <time[index], index>
            Queue<(int, int)> exit = new Queue<(int, int)>();

            int[] res = new int[N];

            for (int i = 0; i < N; i++)
            {
                if (dir[i] == 1)
                    exit.Enqueue((t[i], i));
                else
                    entry.Enqueue((t[i], i));
            }

            int ct = 0, lc = -1; // ct is the current time & lc indicates who used
                                 // turnstile in the previous second lc = -1 if none
                                 // used the turnstile in the last second

            while (exit.Count > 0 || entry.Count > 0)
            {
                // checking for exit queue
                if (exit.Count > 0 && exit.First().Item1 <= ct &&
                    (lc == 1 || lc == -1 || entry.Count == 0 ||
                     (entry.First().Item1 > ct)))
                {
                    res[exit.First().Item2] = ct;
                    lc = 1;
                    exit.Dequeue();
                }
                // checking for entry queue
                else if (entry.Count > 0 && entry.First().Item1 <= ct)
                {
                    res[entry.First().Item2] = ct;
                    lc = 0;
                    entry.Dequeue();
                }
                else
                {
                    lc = -1;
                }

                ct++;
            }

            return res;
        }


        //https://leetcode.com/problems/largest-sum-of-averages/ 
        // Same Category as above




    }
}
