using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace Prepration
{
    class WordNode
    {
        public int numSteps;
        public string word;
        public WordNode(string word, int numSteps)
        {
            this.word = word;
            this.numSteps = numSteps;
        }
    }

    class stringprograms
    {
        

        //https://www.programcreek.com/2012/12/leetcode-evaluate-reverse-polish-notation/
        //2. ["2", "1", "+", "3", "*"] -> ((2 + 1) * 3) -> 9

        public static int CalculateValues()
        {
            string[] inputs = new string[] { "2", "1", "+", "3", "*" };
            string operators = "+/*-";
            int result = 0;
            var stack = new Stack<string>();
            foreach (var inp in inputs)
            {
                if (!operators.Contains(inp))
                    stack.Push(inp);
                else
                {
                    int first = int.Parse(stack.Pop());
                    int second = int.Parse(stack.Pop());

                    switch (inp)
                    {
                        case "+":
                            stack.Push((first + second).ToString());
                            break;
                        case "-":
                            stack.Push(Math.Abs(first - second).ToString());
                            break;
                        case "*":
                            stack.Push((first * second).ToString());
                            break;
                        case "/":
                            stack.Push((first / second).ToString());
                            break;
                    }
                }
            }
            result = Convert.ToInt16(stack.Pop());
            return result;

        }

        //3. MiniParser  Given s = "[123,[456,[789]]]"
        //https://www.programcreek.com/2014/08/leetcode-mini-parser-java/


        //4. Isomorphic Strings "egg" and "add" are isomorphic, "foo" and "bar" are not
        public static bool IsIsomorphic(string word1, string word2)
        {
            if (word1.Length != word2.Length) return false;
            var dict1 = new Dictionary<char, char>();
            var dict2 = new Dictionary<char, char>();

            for (int i = 0; i < word1.Length; i++)
            {
                char c1 = word1[i];
                char c2 = word2[i];
                if (dict1.Keys.Contains(c1))
                {
                   
                    if (c1 != dict2[c2])
                        return false;
                }
                else
                {
                    dict1.Add(c1, c2); 
                    if (dict2.Keys.Contains(c2))
                    {
                        return false;
                    }
                    else
                    {
                        dict2.Add(c2, c1);
                    }
                }
            }
            return true;

        }

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

        //Amazon Question
        //6. WordLadder 
        // start = "hit" end = "cog" dict = ["hot","dot","dog","lot","log"] output =  "hit" -> "hot" -> "dot" -> "dog" -> "cog"
        public static int NoOfStepsForWordLadder(string beginWord, string endWord, List<string> noOfWord)
        {
            Queue<WordNode> words = new Queue<WordNode>();
            words.Enqueue(new WordNode(beginWord, 1));
            noOfWord.Add(endWord);

            while (words.Count > 0)
            {
                WordNode word = words.Dequeue();

                var currentWord = word.word;

                if (currentWord.Equals(endWord))
                    return word.numSteps;

                char[] chars = currentWord.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    for (char cv = 'a'; cv <= 'z'; cv++)
                    {
                        var temp = chars[i];
                        if (chars[i] != cv)
                            chars[i] = cv;

                        string newWord = new string(chars);
                        if (noOfWord.Contains(newWord))
                        {
                            words.Enqueue(new WordNode(newWord, word.numSteps + 1));
                            noOfWord.Remove(newWord);
                        }
                        chars[i] = temp;

                    }
                }
            }
            return 0;

        }

        //7. Find the kth largest element in an unsorted array
        //given [3,2,1,5,6,4] and k = 2, return 5
        public static int FindKthLargestElement(int[] array, int k)
        {
            //Solution 1:
            //Array.Sort(array);
            //int ret = array[array.Length - k];
            //return ret;

            //Solution 2:
            //ManualSort
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        var temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }

                }
            }
            return array[array.Length - k];
        }

        //8. MakeTheNumbersMatch
        public static void MakeTheNumbersMatch(int a, int b, int x, int y)
        {
            while (a != x && b != y)
            {
                if (a > x)
                {
                    a--;
                }
                else
                {
                    a++;
                }

                if (b > y)
                {
                    b--;
                }
                else
                {
                    b++;
                }
            }
        }

        //9. Find the duplicates from Given String
        //1. Use dictionary.
        public static void FindDuplicatesUseDictionary(string inputString)
        {
            var wordCount = new Dictionary<char, int>();
            string s = string.Empty;
            foreach (var c in inputString)
            {
                if (c == ' ') continue;
                if (wordCount.Keys.Contains(c))
                {
                    wordCount[c]++;
                }
                else
                {
                    s += c;
                    wordCount.Add(c, 1);
                }
            }
            //var v= wordCount.OrderBy(q => q.Value);
            Console.WriteLine($"The Unique InputString is => {s}");
            // Console.WriteLine($"The maximum duplicate word is {wordCount.Last().Key}");
        }

        //2. Use string 
        public static void FindDuplicatesUseIndex(string inputString)
        {
            string s = string.Empty;
            foreach (char inp in inputString)
            {
                if (s.IndexOf(inp) < 0)
                {
                    s += inp;
                }
            }
        }

        //Amazon Qustion
        //10. SubstringPattern
        public static bool SubStringPattern(string input, string substring)
        {
            //HiHowHowareyou
            //Howa
            bool isMatched = false;
            int i = 0, j = 0, k = 0;
            while (i < input.Length)
            {
                while (j < substring.Length)
                {
                    if (i == input.Length)
                        break;

                    if (input[i] == substring[j])
                    {
                        i++; j++;
                    }
                    else
                    {
                        j = 0;
                        k++;
                        i = k;
                    }
                }

                if (j == substring.Length)
                {
                    isMatched = true;
                    break;
                }
            }
            return isMatched;

        }

        //https://www.geeksforgeeks.org/naive-algorithm-for-pattern-searching/
        public static void SubStringMatch()
        {
            string input = "abxabcabcaby", pattern = "abcaby";
            //string input = "HiHowHowareyouT", pattern = "Howa";
            //string input= "THIS IS A TEST TEXT",pattern= "TEXT";
            int ip = input.Length;
            int pl = pattern.Length;

            for(int i=0; i<=ip-pl; i++)
            {
                int j;
                for(j=0; j<pl; j++)
                {
                    if (input[i + j] != pattern[j])
                        break;
                }

                if(j==pl)
                {
                    Console.WriteLine("found the pattern=" +i);
                }
            }
        }

        //11. Anagram
        public static void Anagram(string firstString, string secondString)
        {
            firstString = firstString.ToLower();
            secondString = secondString.ToLower();
            var first = firstString.ToCharArray();
            var second = secondString.ToCharArray();
            Array.Sort(first);
            Array.Sort(second);
            if (new string(first) == new string(second))
            {
                Console.WriteLine($"{firstString} and {secondString} is Anagram");
            }
            else
            {
                Console.WriteLine($"{firstString} and {secondString} is not an Anagram");
            }
        }

        //12. Palindrom
        public static void Palindrome(string inputString)
        {
            int i = 0; int j = inputString.Length - 1;
            bool isPalindrome = true;
            while ((j + 1) / 2 > 0)
            {
                if (inputString[i] != inputString[j])
                {
                    isPalindrome = false;
                    Console.WriteLine($"{inputString} is not a palindrome");
                    break;
                }
                i++; j--;
            }
            if (isPalindrome)
                Console.WriteLine($"{inputString} is a palindrome");
        }

        //13. Fibonacci
        public static void PrintFibonacci(int no)
        {
            int i = 0;
            while (i < no)
            {
                Console.WriteLine(Fibonacci(i));
                i++;
            }
        }
        public static int Factorial(int no)
        {
            if (no == 0 || no == 1) return no;
            return no * Factorial(no - 1);
        }

        //14. Fibonacci
        private static int Fibonacci(int no)
        {
            if (no == 0) return 0;
            if (no == 1) return 1;
            return Fibonacci(no - 1) + Fibonacci(no - 2);
        }
        //15. Prime or NthPrime
        private static bool isPrime(int number)
        {
            int counter = 0;
            for (int j = 2; j < number; j++)
            {
                if (number % j == 0)
                {
                    counter = 1;
                    break;
                }
            }
            if (counter == 0)
            {
                Console.WriteLine(number);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void NthPrime()
        {
            int num = 0;
            int count = 1;
            Console.Write("Number : ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            while (true)
            {
                num++;
                if (isPrime(num))
                {
                    count++;
                }
                if (count == n)
                {
                    Console.WriteLine(n + "th prime number is " + num);
                    break;
                }
            }
        }

        //16. GCD
        public static int CalcGCD(int firstNo, int secondNo)
        {
            //GCD
            while (firstNo != secondNo)
            {
                if (firstNo > secondNo)
                    firstNo -= secondNo;
                else
                    secondNo -= firstNo;
            }
            return firstNo;
        }

        //17. LCM
        public static int CalcLCM(int firstNo, int secondNo)
        {
            return (firstNo * secondNo) / CalcGCD(firstNo, secondNo);
        }

        //Amazon Question
        //18. ReverseNo
        public static int ReverseNo(int inputNo)
        {
            int reverseno = 0;
            for (int i = inputNo; i > 0; i = i / 10)
            {
                int remainder = i % 10;
                reverseno = reverseno * 10 + remainder;
            }
            return reverseno;
        }

        //19. ArmstrongNo, 371
        public static bool ArmstrongNo(int inputNo)
        {
            int armstrongNo = 0;
            for (int i = inputNo; i > 0; i = i / 10)
            {
                int remainder = i % 10;
                armstrongNo += (remainder * remainder * remainder);
            }
            if (inputNo == armstrongNo)
                return true;
            else
                return false;
        }

        // 20. Length Encoding InputString="wwwwaaadexxxxxxywww" output="w4a3d1e1x6y1w3"
        public static string LengthEncoding(string input)
        {
            int length = input.Length;
            string result = string.Empty;
            for (int i = 0; i < length; i++)
            {
                int count = 1;
                while (i < length - 1 && input[i] == input[i + 1])
                {
                    i++;
                    count++;
                }
                result += input[i] + count.ToString();
            }
            return result;
        }
        //https://www.geeksforgeeks.org/anagram-substring-search-search-permutations/

        // 21. Remove duplicates
        //https://www.geeksforgeeks.org/remove-duplicates-from-a-given-string/
        public static string RemoveDuplicates(string input)
        {
            //int index = 0;
            //char[] ins = input.ToCharArray();
            ////wwwwaaadexxxxxxywww --> wadexy
            //for (int i = 0; i < ins.Length; i++)
            //{
            //    int j;
            //    for (j = 0; j < i; j++)
            //    {
            //        if (ins[i] == ins[j])
            //        {
            //            break;
            //        }
            //    }
            //    if (i == j)
            //    {
            //        ins[index++] = ins[i];
            //    }
            //}
            //var res = new char[index];
            //Array.Copy(ins, res, index);
            //var result = new string(res);
            //return result;
            char[] array = input.ToCharArray();
            int j = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i] != array[j])
                    j++;
                array[j] = array[i];
            }
            var res = new char[j];
            Array.Copy(array, res, j);
            var result = new string(res);
            return result;
        }

        //https://www.geeksforgeeks.org/anagram-substring-search-search-permutations/

       
       

       



      

       
    }
}
