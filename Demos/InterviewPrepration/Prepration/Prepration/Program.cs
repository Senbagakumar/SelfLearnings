using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Prepration
{
    class Program
    {

        public static bool isPrime(int number)
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

        public static int CalcLCM(int firstNo, int secondNo)
        {
            return (firstNo * secondNo) / CalcGCD(firstNo, secondNo);
        }

        public static int ReverseNo(int inputNo)
        {
            int reverseno = 0;
            for(int i=inputNo; i > 0; i=i/10)
            {
                int remainder = i % 10;
                reverseno = reverseno * 10 + remainder;
            }
            return reverseno;
        }

        public static bool ArmstrongNo(int inputNo)
        {
            int armstrongNo = 0;
            for(int i=inputNo; i > 0; i=i/10)
            {
                int remainder = i % 10;
                armstrongNo += (remainder * remainder * remainder);
            }
            if (inputNo == armstrongNo)
                return true;
            else
                return false;
        }

        public static int Fibonacci(int no)
        {
            if (no == 0) return 0;
            if (no == 1) return 1;
            return Fibonacci(no-1) + Fibonacci(no - 2);
        }

        public static void PrintFibonacci(int no)
        {
            int i = 0;
            while(i < no)
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

       

        public static void FindTheDuplicates(string inputString)
        {
            var wordCount = new Dictionary<char, int>();
            string s = string.Empty;
            foreach(var c in inputString)
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

        public static void Palindrome(string inputString)
        {
            int i = 0; int j = inputString.Length-1;
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
            if(isPalindrome)
                Console.WriteLine($"{inputString} is a palindrome");
        }

        public static void Anagram(string firstString, string secondString)
        {
            firstString = firstString.ToLower();
            secondString = secondString.ToLower();
            var first = firstString.ToCharArray();
            var second = secondString.ToCharArray();
            Array.Sort(first);
            Array.Sort(second);
            if(new string(first) == new string(second))
            {
                Console.WriteLine($"{firstString} and {secondString} is Anagram");
            }
            else
            {
                Console.WriteLine($"{firstString} and {secondString} is not an Anagram");
            }
        }

        public static void SubStringPattern(string input, string substring)
        {
            //HiHowHowareyou
            //Howa
            int i = 0, j = 0, k = 0;
            while(i < input.Length)
            {
                while(j < substring.Length)
                {
                    if(input[i] == substring[j])
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
            }
        }

        static void Main(string[] args)
        {
            //stringprograms.reverseWords("the sky is blue".ToCharArray());
            stringprograms.CalculateValues();
            //DesignQuestions.ExecuteTicTacToe();
            //LinkedListPrograms.OddEvenLinkedList();

            ////var definitionFile = @"C:\Users\v-sesiga\Desktop\New folder\PackageConfig.xml";
            ////var runnerElement = XDocument.Load(definitionFile).Descendants("Runner").Where(q=> q.Attribute("enable").Value == "true").ToList(); // count of the Runners
            ////Console.WriteLine($"Runner List Count {runnerElement.Count}");
            ////for(int i=0; i<runnerElement.Count; i++)
            ////{
            ////    //Console.WriteLine($"{i+1}-->{runnerElement[i].Attribute("name").Value}");

            ////    Console.WriteLine($"{runnerElement[i].Attribute("name").Value}");
            ////}
            ////Console.Read();

            ////var output = ArrayPrograms.SortArrayByParity(new int[] { 3, 1, 2, 4 });
            //var res = ArrayPrograms.SortedSquar(new int[] { 4, -1, 0, 3, 10  });

            ////for Vijay
            ////new JsonConversion().JsonTestCases();


            ////GCD
            //Console.WriteLine(CalcGCD(25, 20));

            ////LCM
            //Console.WriteLine(CalcLCM(25, 20));

            ////Reverse
            //Console.WriteLine(ReverseNo(121));

            ////Armstrong
            //Console.WriteLine(ArmstrongNo(171)); //371

            ////Fibonacci
            //PrintFibonacci(10);

            ////Factorial
            //Console.WriteLine(Factorial(5));

            ////Binary Search
            //int[] inputs = new int[] { 5, 7, 9, 10, 11, 13 };
            //var element = ArrayPrograms.BinarySearch(inputs, 9, 0, 4);
            //Console.WriteLine(element);

            ////Find the duplicates from the given string
            //FindTheDuplicates("Hi How Are You I am fine How about you");

            ////Palindrome
            //Palindrome("abcaba");

            ////Anagram
            //Anagram("pot", "topa");

            ////Sorting
            //ArrayPrograms.Sorting(new int[] { 12, 5, 2, 4, 8, 1, 10, 9 });

            ////Array Rotation
            //ArrayPrograms.ArrayRotation(new int[] { 1, 2, 3, 4, 5 }, 2);

            ////Array Rotation Search
            //ArrayPrograms.RotationSearch(new int[] { 4, 5, 1, 2, 3 }, 3, 0, 5);


            ////Binary Tree
            //var node= BinaryTree.ConstructBinaryTree(new int[] { 9, 3, 15, 20, 7 }, new int[] { 9, 15, 7, 20, 3 });
            //BinaryTree.SeriaizeDeserializeBinaryTree(node);
            ////Print Binary tree
            //// BinaryTree.BFSTPrintByLevel(node);
            //BinaryTree.BFSTPrintByZigZagLevel(node);

            //ArrayPrograms.SetZeros(); 1,3,7,8,9, 11 ,15,18,19,21,25,, input: x:  { 1, 3, 8, 9, 15 } input: y:  { 7, 11, 19, 21, 18, 25 }

            //int res = ArrayPrograms.FindMedianOfTwoArrays(x, y);

            //int[] result = ArrayPrograms.MergeTwoSortedArrays(new int[] { 7, 11, 19, 21, 25 }, new int[] { 1,15 });

            //LinkedListPrograms.SumOfTwoLinkedList();


            Console.ReadKey();
        }

    }
}
