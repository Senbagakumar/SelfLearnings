using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Prepration
{   
    class Program
    {

       
        static void Main(string[] args)
        {
            DesignQuestions.MyHashMap cv = new DesignQuestions.MyHashMap();
            cv.put(5, 10);
            cv.get(5);
            cv.put(6,11);
            cv.put(7, 12);
            cv.get(6);
            cv.get(8);
            cv.put(2074, 20);
            cv.put(2073, 25);


            //DesignQuestions.LRUCache.Test();
            //stringprograms.ValidParantheses();
            //Microsoft.Microsoft1.FindMinimumInSortedArray();
            //stringprograms.GetUniqueSubstring();
            //var result=ArrayPrograms.SortedArrayToBinarySearchTree(new int[] { -10, -3, 0, 5, 9, 10 });
            ArrayPrograms.AsteroidCollision();
            //ArrayPrograms.MinCostPath();
            //stringprograms.search();//"HiHoHowareyou", "How" --> "AABAACAADAABAAABAA", "AABA"

            Console.Read();




























            //int[] B = new int[] { 1, 1, 1, 3, 3, 3, 20, 4, 4, 4 };
            //int ones = 0;
            //int twos = 0;
            //int not_threes;
            //int x;

            //for (int i = 0; i < 10; i++)
            //{
            //    x = B[i];
            //    twos |= ones & x;
            //    ones ^= x;
            //    not_threes = ~(ones & twos);
            //    ones &= not_threes;
            //    twos &= not_threes;
            //}

            //ArrayPrograms.FindUniqueNo(new int[] { 12, 5, 12, 4, 12, 1, 1, 2, 3, 3,2,5 });
            // stringprograms.RemoveDuplicates("wwwwaaadexxxxxxywww");
            //stringprograms.reverseWords("the sky is blue".ToCharArray());
            //stringprograms.CalculateValues();
            //bool result=stringprograms.IsIsomorphic("far", "boo");

            //string[] logs = new string[] { "dig1 8 1 5 1", "let1 art can", "dig2 3 6", "let2 own kit dig", "let3 art zero" };
            //var result = stringprograms.ReOrderTheLogFiles(logs);

            //beginWord = hit; endWord=cog; dict = ["hot","dot","dog","lot","log"]
            //int steps = stringprograms.NoOfStepsForWordLadder("hit", "cog", new List<string>() { "hot", "dot", "dog", "lot","log" });

            //given [3,2,1,5,6,4] and k = 2, return 5
            //int result = stringprograms.FindKthLargestElement(new int[] { 3, 2, 1, 5, 6, 4 }, 2);
            //stringprograms.FindDuplicatesUseIndex("abcdefabc");
            //stringprograms.SubStringPattern("HiHowHowareyou", "iam");
            //stringprograms.LengthEncoding("wwwwaaadexxxxxxywww");
            //ArrayPrograms.PrintTwoElements(new int[] { 7, 3, 4, 5, 5, 6, 2 });
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


            ////Binary Tree //new int[] { 9, 3, 15, 20, 7 }, new int[] { 9, 15, 7, 20, 3 }
            /////new int[] { 3,5,8,10,12,14,16 }, new int[] { 3,8,5,12,16,14,10} -- In, Post
            /////new int[] { 3,5,8,10,12,14,16 }, new int[] { 10,5,3,8,14,12,16}
            //var node = BinaryTree.ConstructBinaryTree(new int[] { 3,5,8,10,12,14,16 }, new int[] { 10, 5, 3, 8, 14, 12, 16 },true);
            //BinaryTree.SeriaizeDeserializeBinaryTree(node);
            ////Print Binary tree
            //// BinaryTree.BFSTPrintByLevel(node);
            //BinaryTree.BFSTPrintByZigZagLevel(node);

            //ArrayPrograms.SetZeros(); 1,3,7,8,9, 11 ,15,18,19,21,25,, input: x:  { 1, 3, 8, 9, 15 } input: y:  { 7, 11, 19, 21, 18, 25 }

            //int res = ArrayPrograms.FindMedianOfTwoArrays(x, y);

            //int[] result = ArrayPrograms.MergeTwoSortedArrays(new int[] { 7, 11, 19, 21, 25 }, new int[] { 1,15 });

            //LinkedListPrograms.SumOfTwoLinkedList();

            var llist = new LinkedList();
            llist.AddNode(1);
            llist.AddNode(2);
            llist.AddNode(3);
            llist.AddNode(4);
            llist.AddNode(5);

            LinkedListPrograms.ReverseLinkedList(llist.Head);
            Console.ReadKey();
        }

    }
}
