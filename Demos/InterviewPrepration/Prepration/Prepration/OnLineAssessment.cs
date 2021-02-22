using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
    public class OnLineAssessment
    {
        //https://molchevskyi.medium.com/numbers-with-equal-digit-sum-c8a7e01ad7db -- Have to check


        //https://leetcode.com/discuss/interview-question/398023/Microsoft-Online-Assessment-Questions/
        //https://leetcode.com/discuss/interview-question/344650/Amazon-Online-Assessment-Questions

        public static int Microsoft1(string s)
        {
            string S = "100";
            S = S.Substring(S.IndexOf("1"), S.Length - S.IndexOf("1"));
            int steps = S.Length;
            S = S.Substring(1).Replace("0", "");
            steps += S.Length;
            Console.WriteLine(steps);
            return steps;
        } 

        //int[] A = {1, 2, 3, 3};  int[] B = { 2, 3, 1, 4 }; int N = 4; Answer: 4
        //A=[1,2,4,5] B=[2,3,5,6] int N=6 ; Answer: 2

        //https://leetcode.com/discuss/interview-question/364760/
        public static int ConnectedCitiesCount(int[] A, int[] B, int N)
        {
            int maxRank = 0;
            int edgesLen = A.Length;

            int[] edgesCount = new int[N + 1];

            for (int i = 0; i < edgesLen; i++)
            {
                edgesCount[A[i]] += 1;
                edgesCount[B[i]] += 1;
            }

            for (int i = 0; i < edgesLen; i++)
            {
                int localMax = edgesCount[A[i]] + edgesCount[B[i]] - 1;
                maxRank = Math.Max(maxRank, localMax);
            }

            return maxRank;
        }

        //Return minimum No of Substring count
        //s="world" ans:  1
        //s="dddd", ans: 4
        //s="cycle", ans: 2
        //s="abba", ans: 2
        //s="abacdec", ans: 3

        public static int SubstringLength(string s)
        {
            int length = 1;
            List<char> subChar = new List<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (!subChar.Contains(s[i]))
                {
                    subChar.Add(s[i]);
                }
                else
                {
                    subChar.Clear();
                    subChar.Add(s[i]);
                    length++;
                }
            }
            return length;
        }

        //you want to delete some letters from S to obtain a string without two identical letters next to each other.
        // what is the minimum total cost of deletions to achieve such a string.
        //s="abccbd" c=[0,1,2,3,4,5] ans=2
        //s="aabbcc" c=[1,2,1,2,1,2] ans=3
        //s="aaaa" c=[3,4,5,6] ans=12
        //s="ababa" c=[10,5,10,5,10] ans=0
        public static int MinCostOfDuplicationLetter(string s, int[] costs)
        {
            int cost = 0;
            int i = 0;
            while(i <s.Length-1)
            {
                if(s[i] == s[i+1])
                {
                    cost += Math.Min(costs[i], costs[i + 1]);
                }
                i++;
            }
            return cost;
        }

        //Stack Programming Questions
        public int StackProgram(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return -1;
            string[] inputs = input.Split(' ');
            var stack = new Stack<int>();
            foreach (string inp in inputs)
            {
                switch (inp)
                {
                    case "dup":
                        {
                            stack.Push(stack.Peek());
                        }
                        break;
                    case "pop":
                        {
                            stack.Pop();
                        }
                        break;
                    case "+":
                        {
                            if (stack.Count > 1)
                            {
                                stack.Push(stack.Pop() + stack.Pop());
                            }
                            else return -1;
                        }
                        break;
                    case "-":
                        {
                            if (stack.Count > 1)
                            {
                                int firstTop = stack.Pop();
                                int secondTop = stack.Pop();
                                stack.Push(secondTop - firstTop);
                            }
                            else
                                return -1;

                        }
                        break;
                    default:
                        {
                            int num = 0;
                            if(int.TryParse(inp, out num))
                            {
                                stack.Push(num);
                            }
                        }
                        break;
                }
            }
            if (stack.Count > 1)
                return stack.Peek();
            else
                return -1;
        }

        //Max Sum of numbers with same digits sum

        int SumOfDigits(int a)
        {
            int sum = 0;
            while(a > 0)
            {
                sum += a % 10;
                a = a / 10;
            }
            return sum;
        }

        int CalculateSum(int[] numbers)
        {
            int maxSum = 0;
            int sum = 0;
            var dict = new Dictionary<int, int>();

            foreach(int num in numbers)
            {
                int digitSum = SumOfDigits(num);
                if (!dict.ContainsKey(num))
                    dict.Add(digitSum, num);
                else
                {
                    maxSum = Math.Max(maxSum, dict[digitSum] + num);
                    dict[digitSum] = Math.Max(dict[digitSum], num);
                }
            }

            return maxSum > 0 ? maxSum : -1;
        }
    }
}
