using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration
{
    class BasicQuestions
    {
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

    }
}
