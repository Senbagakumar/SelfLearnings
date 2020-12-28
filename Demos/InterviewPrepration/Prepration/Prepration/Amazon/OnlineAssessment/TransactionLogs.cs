using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class TransactionLogs
    {
        //Transaction Logs

        //Amazon Question
        //5. Reorder Data in Log Files Input: logs = ["dig1 8 1 5 1","let1 art can","dig2 3 6","let2 own kit dig","let3 art zero"]
        //Output: ["let1 art can","let3 art zero","let2 own kit dig","dig1 8 1 5 1","dig2 3 6"]

        public static string[] ReOrderTheLogFiles(string[] inputs)
        {
            List<string> letter = new List<string>();
            List<string> digit = new List<string>();

            foreach (var input in inputs)
            {
                if (IsDigit(input))
                {
                    digit.Add(input);
                }
                else
                {
                    letter.Add(input);
                }
            }

            letter.Sort(delegate (string x, string y)
            {
                int result;

                string a = x.Substring(x.IndexOf(' ') + 1);
                string b = y.Substring(y.IndexOf(' ') + 1);

                result = a.CompareTo(b);
                if (result == 0)
                    result = x.CompareTo(y);

                return result;
            });

            return letter.Concat(digit).ToArray();
        }

        private static bool IsDigit(string input)
        {
            var split = input.Split(' ');
            var secondIndex = split[1];
            return secondIndex.All(char.IsDigit);
        }
    }
}
