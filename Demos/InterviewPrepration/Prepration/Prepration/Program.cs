using Prepration.Amazon;
using Prepration.Amazon.OnlineAssessment;
using Prepration.Microsoft;
using System;
using System.Collections;
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
using System.Web.WebSockets;
using System.Xml.Linq;
using System.Xml.XPath;
using static Prepration.DesignQuestions;

namespace Prepration
{
    class Program
    {

        static List<char> wcount = new List<char>();
        static List<char> bcount = new List<char>();
        public static string gameWinner(string colors)
        {
            int i = 1;
            string userName = string.Empty;
            string firstUser = "wendy";
            string secondUser = "bob";
            while (!whoWin(colors))
            {
                if (i % 2 != 0)
                {
                    //string userName = user == 'w' ? "wendy" : "blob";
                    colors = Move(colors, 'w');
                    userName = "wendy";
                }
                else
                {
                    colors = Move(colors, 'b');
                    userName = "blob";
                }
                i++;
            }
            if (i == 1)
            {
                if (bcount.Count == 0 && wcount.Count > 0)
                    return secondUser;
                else if (wcount.Count == 0 && bcount.Count > 0)
                    return firstUser;
                else
                    return secondUser;
            }
            return userName;
        }

        public static string Move(string colors, char user)
        {

            int i = 0;
            for (i = 0; i < colors.Length; i++)
            {
                if (colors[i] == user)
                {
                    break;
                }
            }
            colors = colors.Remove(i, 1);
            return colors;

        }

        public static bool whoWin(string colors)
        {
            wcount.Clear();
            bcount.Clear();
            foreach (var c in colors)
            {
                if (c == 'w')
                    wcount.Add(c);
                else
                    bcount.Add(c);
            }
            if (wcount.Count == bcount.Count)
                return true;
            else
                return false;
        }

        static long repeatedString(string s, long n)
        {
            long count = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'a')
                    count++;
            }

            long div = n / s.Length;
            long reminder = n % s.Length;

            count = div * count;

            for (int i = 0; i < reminder; i++)
            {
                if (s[i] == 'a')
                    count++;
            }

            return count;
        }

        public static int numberNeeded(string first, string second)
        {
            int[] lettercounts = new int[26];
            foreach (char c in first)
            {
                lettercounts[c - 'a']++;
            }
            foreach (char c in second)
            {
                lettercounts[c - 'a']--;
            }
            int result = 0;
            foreach (int i in lettercounts)
            {
                result += Math.Abs(i);
            }
            return result;
        }


        public static string decryptPassword(string s)
        {
            int len = s.Length;
            char[] result = new char[len * 2];
            for (int i = 0; i < len; i++)
            {
                if (i < len - 1 && char.IsLower(s[i]) && char.IsUpper(s[i + 1]))
                {
                    char temp = s[i + 1];
                    result[i + 1] = s[i];
                    result[i] = temp;

                    result[i + 2] = '*';
                }
                else if (char.IsDigit(s[i]))
                {
                    result[i] = '0';
                    result[0] = s[i];
                }
                else
                    result[i] = s[i];
            }
            string res = new string(result).Trim();
            return res;
        }

        private static int solve(string s1, string s2)
        {
            if (s1.Length % s2.Length != 0)
                return -1;
            int l2 = s2.Length;
            for (int i = 0; i < s1.Length; i++)
            {
                int s2p = i % l2;
                if (s1[i] != s2[s2p])
                    return -1;
            }
            for (int i = 0; i < s2.Length; i++)
            {
                int j = 0;
                for (; j < s2.Length; j++)
                {
                    int s2pos = j % (i + 1);
                    if (s2[j] != s2[s2pos])
                        break;
                }
                if (j == s2.Length)
                {
                    return i + 1;
                }
            }
            return -1;
        }

        

        static void Main(string[] args)
        {
            var newEdges = new int[3][];
            newEdges[0] = new int[] { 1, 2, 12 };
            newEdges[1] = new int[] { 3, 4, 30 };
            newEdges[2] = new int[] { 1, 5, 8 };


            var edges = new int[5][];
            edges[0] = new int[] { 1, 2 };
            edges[1] = new int[] { 2, 3 };
            edges[2] = new int[] { 3, 4 };
            edges[3] = new int[] { 4, 5 };
            edges[4] = new int[] { 1, 5 };


            int v = new MinCostToRepairEdges().MinCostToRepair(5, edges, newEdges);

            MicrosoftInterviewQuestions.LongestPalindrome("babad");

            var usersongs = new Dictionary<string, List<string>>();
            usersongs.Add("David", new List<string>() { "song1", "song2", "song3", "song4", "song8" });
            usersongs.Add("Emma", new List<string>() { "song5", "song6", "song7" });

            var songgenres = new Dictionary<string, List<string>>();
            songgenres.Add("Rock", new List<string>() { "song1", "song3" });
            songgenres.Add("Dubstep", new List<string>() { "song7" });
            songgenres.Add("Techno", new List<string>() { "song2", "song4" });
            songgenres.Add("Pop", new List<string>() { "song5", "song6" });
            songgenres.Add("Jazz", new List<string>() { "song8", "song9" });

            FavoriteGenres.FavoriteGenre(usersongs, songgenres);

            PartitionLabels.StringPartion("ababcbacadefegdehijhklij");
          

            var list = new List<IList<int>>();
            list.Add(new List<int>() { 0, 1 });
            list.Add(new List<int>() { 0, 2 });
            list.Add(new List<int>() { 1, 3 });
            list.Add(new List<int>() { 2, 3 });
            list.Add(new List<int>() { 3, 4 });
            list.Add(new List<int>() { 2, 5 });
            list.Add(new List<int>() { 5, 6 });

            var products = new int[3][];
            products[0] = new int[2] { 4, 4};
            products[1] = new int[] { 1,2 };
            products[2] = new int[] { 3,6 };

            //new Program().FiveStarReviews(products, 77);

            KeywordSuggestions.SuggestedProducts2(new string[] { "mobile", "mouse", "moneypot", "monitor", "mousepad" }, "mouse");

            var res = SlowestKeyPress.LongestKeyPress(new int[] { 9, 29, 49, 50 }, "cbcd");

            MultiProcessorSystems.ProcessorSystem(5, new int[] { 3, 1, 7, 2, 4 }, 15);
            MicrosoftInterviewQuestions.TwoSum(new int[] { 2, 7, 11, 15 }, 9);
            //Turnstile.getTimes(4, new int[] { 0, 0, 1, 5 }, new int[] { 0, 1, 1, 0 });

            //Turnstile.getTimes(5, new int[] { 1, 2,2,4,4 }, new int[] { 0,1,0,0,1 });

            Turnstile.getTimes(4, new int[] { 1, 1, 2, 6 }, new int[] { 0, 1, 1, 0 });


            string gs = "aaaaaa";
            string t = "a";

            int v1 = solve(gs, t);

            string rs = gs.Replace(t, "");
            string substring = string.Empty;

            if (string.IsNullOrEmpty(rs) || rs.Length == t.Length)
            {
                int[] abc = new int[26];
                int j = 0;
                int ans = 0;

                for (int i = 0; i < t.Length; i++)
                {
                    j = Math.Max(j, abc[t[i] - 'a']);

                    if (i - j + 1 > ans)
                    {
                        ans = i - j + 1;
                        substring = t.Substring(j, ans);
                    }
                    abc[t[i] - 'a'] = i + 1;
                }
            }



            //Validate given string and given substring




            Console.Read();


            //var v = new int[3][];
            //v[0] = new int[] { 1, 1, 0 };
            //v[1] = new int[] { 1, 1, 0 };
            //v[2] = new int[] { 0, 0, 1 };
            //Amazon.OnlineAssessment.GiftingGroups.FindCircleNum(v);
            Amazon.OnlineAssessment.RoboticsChallenge.CalPoints(new string[] { "5", "2", "C", "D", "+" });
            Amazon.OnlineAssessment.RoverControl.Rover_Control(new string[] { "R", "D", "L", "L", "D" }, 4);

            Amazon.OnlineAssessment.SubstringsOfSizeKwithK_1DistinctChars.GetMaxSubstring("awaglknagawunagwkwagl", 4);

            Amazon.OnlineAssessment.AmazonMusicPairs.NumPairsDivisibleBy60(new int[] { 30, 20, 150, 100, 40 });

            decryptPassword("51Pa*0Lp*0e");

            string[] ovalue = "1 4".Split(' ');

            Node head1 = new Node(1);
            head1.Next = new Node(2);
            head1.Next.Next = new Node(3);

            Node head2 = new Node(2);
            head2.Next = new Node(3);
            head2.Next.Next = new Node(4);

            AmazonInterviewQuestions amazonInterviewQuestions = new AmazonInterviewQuestions();
            var mg = amazonInterviewQuestions.MergeTwoLists(head1, head2);


            int[][] costs = new int[3][];
            costs[0] = new int[] { 17, 2, 17 };
            costs[1] = new int[] { 16, 16, 5 };
            costs[2] = new int[] { 14, 3, 19 };

            new DesignQuestions().MinCost(costs);

            ArrayPrograms.Calculate("3+2*2");
            new stringprograms().RestoreIpAddresses("25525511135");
            new ArrayPrograms().FindLeastNumOfUniqueInts(new int[] { 4, 3, 1, 1, 3, 3, 2 }, 3);
            MicrosoftInterviewQuestions.FindIsLand();

            var mi = new MicrosoftInterviewQuestions();
            int[][] rc = new int[3][];
            rc[0] = new int[] { 0, 0, 1, 0, 0 };
            rc[1] = new int[] { 0, 1, 0, 1, 0 };
            rc[2] = new int[] { 0, 1, 1, 1, 0 };

            int re = mi.ClosedIsland(rc);


            string decode = stringprograms.decodeString("3[a]2[bc]");
            string reverse = stringprograms.reverseInParentheses("(u(love)i)");
            var listlist = new List<IList<int>>();
            listlist.Add(new List<int>() { 1, 2, 2, 1 });
            listlist.Add(new List<int>() { 3, 1, 2 });
            listlist.Add(new List<int>() { 1, 3, 2 });
            listlist.Add(new List<int>() { 2, 4 });
            listlist.Add(new List<int>() { 3, 1, 2 });
            listlist.Add(new List<int>() { 1, 3, 1, 1 });

            int count = ArrayPrograms.LeastBricks(listlist);
            //int length = stringprograms.StringCompression(new string[] { "a", "b", "b", "b", "b", "b", "b", "b", "b", "b", "b", "b", "b" });
            // var res = stringprograms.LengthEncoding("a");
            new stringprograms().DailyTemperatures(new int[] { 73, 74, 75, 71, 69, 72, 76, 73 });

            char[][] mins = new char[4][];
            mins[0] = new char[5] { 'E', 'E', 'E', 'E', 'E' };
            mins[1] = new char[5] { 'E', 'E', 'M', 'E', 'E' };
            mins[2] = new char[5] { 'E', 'E', 'E', 'E', 'E' };
            mins[3] = new char[5] { 'E', 'E', 'E', 'E', 'E' };

            new DesignQuestions().updateBoard(mins, new int[] { 0, 0 });

            // new stringprograms().MinimumWindowSubstring("abdzfgabz", "az");
            //OnLineAssessment.MinCostOfDuplicationLetter("aaaa", new int[] { 3,4,5,6 });
            //string str = "str1\nstr2\nstr3\nstr4\nstr5\nstr6\nstr7\nstr8\nstr9\nstr10\nstr11\nstr12\nstr13\nstr14\nstr15\nstr16\nstr17\nstr18\nstr19\nstr20\nstr21\nstr22\nstr23\nstr24\nstr25";
            //str = "str1\nstr2\nstr3\nstr4\nstr5\nstr6\nstr7";
            //new ArrayPrograms().PrintLast10LineOfGivenFile(str,'\n');
            //new ArrayPrograms().FindRepeatingCharacter(8, 3);
            //new ArrayPrograms().UniquePaths(3, 2);
            //List<IList<int>> input = new List<IList<int>>();
            //input.Add(new List<int> { 0, 1 });
            //input.Add(new List<int> { 1, 2 });
            //input.Add(new List<int> { 2, 0 });
            //input.Add(new List<int> { 1, 3 });
            //new ArrayPrograms().CriticalConnections(4, input);
            //[1,2],[3,5],[6,7],[8,10],[12,16]

            //int[][] interval = new int[2][];
            //interval[0] = new int[2] { 1,3};
            //interval[1] = new int[2] { 6,9};

            //int[] insert = new int[] { 2,5 };

            //int[][] interval = new int[5][];
            //interval[0] = new int[2] { 1, 2 };
            //interval[1] = new int[2] { 3, 5 };
            //interval[2] = new int[2] { 6, 7 };
            //interval[3] = new int[2] { 8, 10 };
            //interval[4] = new int[2] { 12, 16 };

            //int[] insert = new int[] { 4,8 };


            //var result = new ArrayPrograms().Insert(interval, insert);
            //var result = new ArrayPrograms().ClimbStairs(10);
            //var result = new ArrayPrograms().MinCostClimbingStairs(new int[] { 1, 100, 1, 1, 1, 100, 1, 1, 100, 1 });
            //new ArrayPrograms().NumDecodings("12");
            //new ArrayPrograms().LongestConsecutive(new int[] { 100, 4, 200, 1, 3, 2 });
            //new AmazonInterviewQuestions().MaxProductArray(new int[] { -2, 0, -1 }); 
            //new ArrayPrograms().HammingWeight(11); //100 //001
            //var leng = AmazonInterviewQuestions.GetUniqueSubstring("abcabcbb");
            //var v = new MedianFinder();
            //v.AddNum(1);
            //v.AddNum(2);
            //v.AddNum(4);
            //v.AddNum(3);
            //v.AddNum(6);
            //v.AddNum(5);
            //var result = v.FindMedian();
            //new stringprograms().Subsets(new int[] { 1,2,2 });
            //new stringprograms().permute(new int[] { 1,2,3,4 });
            //new stringprograms().combinationSum(new int[] { 2, 3, 6, 7 }, 7);
            //new stringprograms().partition("aab");
            //new stringprograms().MaxSlidingWindow(new int[] { 1, 3, -1, -3, 5, 3, 6, 7 }, 3);
            //new stringprograms().SearchRange(new int[] { 5, 7, 7, 8, 8, 9 }, 8);
            //stringprograms.IsSubsequence("axc", "ahbgdc");
            //stringprograms.RepeatedSubstringPattern("ababab");
            //MinStack minStack = new MinStack();
            //minStack.Push(-2);
            //minStack.Push(0);
            //minStack.Push(-3);
            //minStack.getMin(); //--> Returns - 3.
            //minStack.Pop();
            //minStack.Top(); //--> Returns 0.
            //minStack.getMin(); //--> Returns - 2.

            //FreqStack fk = new FreqStack();
            //fk.push(5); //5,7,5,7,4,5
            //fk.push(7);
            //fk.push(5);
            //fk.push(7);
            //fk.push(4);
            //fk.push(5);
            //int val = fk.pop(); //5
            //val = fk.pop(); //7
            //val = fk.pop(); //5
            //val = fk.pop(); //4
            //AmazonInterviewQuestions aiq = new AmazonInterviewQuestions();

            //var llist = new LinkedList();
            //llist.AddNode(1);
            //llist.AddNode(2);
            //llist.AddNode(3);
            //llist.AddNode(4);
            //llist.AddNode(5);

            // new LinkedListPrograms().ReorderList(llist.Head);
            //var node = BinaryTree.ConstructBinaryTree(new int[] { 3, 2, 4, 1, 5, 6 }, new int[] { 3, 4, 2, 6, 5, 1 });
            //BinaryTree.Flatten(node);

            //var node = BinaryTree.ConstructBinaryTree(new int[] { 2, 5, 20, 10, 8 }, new int[] { 2, 20, 5, 8, 10 });
            ////var reslut = new LinkedListPrograms().TreeToDoublyList(node);
            //var b = new BinaryTree();
            //b.PrintInOrder(node);
            //b.CorrectBST(node);
            //b.PrintInOrder(node);

            // aiq.reverseKGroup(llist.Head, 2);


            //int[,] interval = new int[3,2];
            //interval[0, 0] = 0;
            //interval[0, 1] = 30;

            //interval[1, 0] = 5;
            //interval[1, 1] = 10;

            //interval[2, 0] = 15;
            //interval[2, 1] = 20;

            //int rooms = aiq.MinMeetingRooms(interval);

            //aiq.MaxArea(new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 });

            //aiq.ThreeSum(new int[] { -1, 0, 3, -2, -1, -4 }, 1);

            //aiq.SubStringPattern("aaaa", "bba");
            //var result=aiq.ProductExceptSelf(new int[] { 1, 2, 3, 4 });
            //var result = aiq.FirstUniqueChar("loveleetcode");
            //aiq.CanFinish(2, new int[,] { { 1, 0 },{ 0,1} });
            //aiq.CutOffTree(new int[,] { {1,2,3},{ 0,0,4},{7,6,5} }); //new int[,] { {1,2,3},{ 0,0,4},{7,6,5} }
            //var result = aiq.FloodFill(new int[,] { { 1,1,1 }, { 1,1,0 }, { 1,0,1 } },1,1,2);
            //aiq.MergeInterval(new int[,] { { 1, 3 }, { 2, 6 }, { 8, 10 }, { 15, 18 } }); //{ 1,3 },{ 2,6},{ 8,10 },{15,18 }  { 1,4},{ 4,5}
            //aiq.MaxSubArray(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 });
            //var result=aiq.WordBreak("leetcode", new List<string>() { "leet", "code"});
            //var result = aiq.CoinChange(new int[] { 1, 2, 5 }, 11);
            //Console.Read();
            //int days=Assessment.MinDays();
            //Assessment.TestMaximumToys();
            //stringprograms.GetUniqueSubstring();
            // Trie trie = new Trie();

            // trie.Insert("apple");
            //bool res= trie.Search("apple");   // returns true
            //bool res1= trie.Search("app");     // returns false
            //bool res2= trie.StartsWith("app"); // returns true
            // trie.Insert("app");
            //bool res3= trie.Search("app");     // returns true

            //ArrayPrograms.BestBuyAndSellStock(new int[] { 7, 1, 5, 3, 6, 4 });
            //ArrayPrograms.SearchMatrix(new int[,] { { 1, 3, 5, 7 },{ 10, 11, 16, 20 },{ 23, 30, 34, 50 } },13);
            //ArrayPrograms.SortColors(new int[] { 2, 0, 2, 1, 1, 0});
            // ArrayPrograms.RemoveDuplicates(new int[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 });
            //stringprograms.RemoveDuplicates("wwwwaaadexxxxxxywww");
            // new stringprograms().LetterCombinations("23");
            //ArrayPrograms.RotateImage(new int[,] { {1,2 }, {3,4} });
            //stringprograms.GroupAnagrams(new string[] { "eat", "tea", "tan", "ate", "nat", "bat" });
            //stringprograms.IsWordPalindrome("race a car");
            //stringprograms.StringToIntConversion("4193 with words");
            //stringprograms.GetUniqueSubstring();
            //ArrayPrograms.FindMedianOfTwoArrays(new int[] {1,2,3,4,5 }, new int[] { 6,7,8,9,10,11 });
            //DesignQuestions.LRUCache.Test();
            //CarDesign.BuildCar();

            //var prog = new Program();
            //var no = prog.RomanToInt("IV");

            // numberNeeded("cde", "abc");
            // repeatedString1("aba", 10);
            //Assessment.MinCostConnectedRops();
            //int steps= Assessment.TreaureIsLand2();

            // LinkedList lv = new LinkedList(1);
            //lv.AddNode(2);
            //lv.AddNode(3);
            //lv.AddNode(4);

            //LinkedListPrograms.SwapPairs(lv.Head);

            // Program.gameWinner("ww");
            //DesignQuestions.MyHashMap cv = new DesignQuestions.MyHashMap();
            //cv.put(5, 10);
            //cv.get(5);
            //cv.put(6,11);
            //cv.put(7, 12);
            //cv.get(6);
            //cv.get(8);
            //cv.put(2074, 20);
            //cv.put(2073, 25);


            //DesignQuestions.LRUCache.Test();
            //stringprograms.IsIsomorphic("egg","add");
            //Microsoft.Microsoft1.KthSmallest();
            //stringprograms.ValidParantheses();
            //var result=ArrayPrograms.SortedArrayToBinarySearchTree(new int[] { -10, -3, 0, 5, 9, 10 });
            //ArrayPrograms.AsteroidCollision();
            //ArrayPrograms.MinCostPath();
            //stringprograms.search();//"HiHoHowareyou", "How" --> "AABAACAADAABAAABAA", "AABA"

            //Console.Read();




























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

            //int result= stringprograms.Factorial(5);

            // ArrayPrograms.FindUniqueNo(new int[] { 12, 5, 12, 4, 12, 1, 1, 2, 3, 3,2,5 });
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

            // ArrayPrograms.SortedSquar(new int[] { -4, -1, 0, 3, 10 });

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

            //var llist = new LinkedList();
            //llist.AddNode(1);
            //llist.AddNode(2);
            //llist.AddNode(3);
            //llist.AddNode(2);
            //llist.AddNode(1);

            //LinkedListPrograms.RecursivePalindrome(llist.Head, llist.Head, new LinkedListPrograms.Result());
            //Console.ReadKey();
        }

    }
}
