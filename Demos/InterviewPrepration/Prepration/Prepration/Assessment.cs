using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Xml.XPath;

namespace Prepration
{
    public class Assessment
    {
        //https://leetcode.com/discuss/interview-question/344650/Amazon-Online-Assessment-Questions

        //1. Reorder the data in log files
        //2. https://leetcode.com/discuss/interview-question/344677  -- https://leetcode.com/problems/minimum-cost-to-connect-sticks (premium)
        //Input: ropes = [8, 4, 6, 12]
        //Output: 58
        //Explanation: The optimal way to connect ropes is as follows
        //1. Connect the ropes of length 4 and 6 (cost is 10). Ropes after connecting: [8, 10, 12]
        //2. Connect the ropes of length 8 and 10 (cost is 18). Ropes after connecting: [18, 12]
        //3. Connect the ropes of length 18 and 12 (cost is 30).
        //Total cost to connect the ropes is 10 + 18 + 30 = 58

        public static int MinCostConnectedRops()
        {
            var input = new List<int>() { 8,4,6,12 };
            int maxcost = 0;
            while(input.Count > 1)
            {
                var iarray = input.ToArray();
                Array.Sort(iarray);
                if(iarray.Length >=2)
                {
                    int cost = iarray[0] + iarray[1];
                    input.Remove(iarray[0]);
                    input.Remove(iarray[1]);
                    input.Add(cost);
                    maxcost += cost;
                }
                
            }
            return maxcost;
        }


        //Treasure IsLand
        //https://leetcode.com/discuss/interview-question/347457

        public static int TreasureIsLand()
        {
            char[,] land = new char[,] { {'O', 'O', 'O', 'O'},
            {'D', 'O', 'D', 'O'},
            {'O', 'O', 'O', 'O'},
            {'X', 'D', 'D', 'O'} };
           
            var queue = new Queue<Coordinate>();
            queue.Enqueue(new Coordinate(0, 0));
            land[0, 0] = 'D';
            int length = land.GetUpperBound(0);
            bool[,] visited = new bool[length + 1, length + 1];
            visited[0, 0] = true;

            return NewMethod(land, queue, length, visited);
        }

        private static int NewMethod(char[,] land, Queue<Coordinate> queue, int length, bool[,] visited)
        {
            int steps = 0;

            int[] xdirs = new int[] { 1, 0, -1, 0 };
            int[] ydirs = new int[] { 0, 1, 0, -1 };

            while (queue.Count > 0)
            {
                int size = queue.Count;

                for (int j = 0; j < size; j++)
                {
                    Coordinate coordinate = queue.Dequeue();
                    int x = coordinate.x;
                    int y = coordinate.y;

                    if (land[x, y] == 'X')
                        return steps;

                    for (int i = 0; i < xdirs.Length; i++)
                    {
                        int newX = x + xdirs[i];
                        int newY = y + ydirs[i];

                        if (newX >= 0 && newX <= length && newY >= 0 && newY <= length && land[newX, newY] != 'D' && !visited[newX, newY])
                        {
                            queue.Enqueue(new Coordinate(newX, newY));
                            //land[newX, newY] = 'D';
                            visited[newX, newY] = true;
                        }
                    }
                }
                steps++;
            }
            return 0;
        }

        public  class Coordinate
        {
            public int x; public int y;
            public Coordinate(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        //Treasure IsLand 11

        public static int TreaureIsLand2()
        {
            char[,] land = new char[,]{{'S', 'O', 'O', 'S', 'S'},
                                      {'D', 'O', 'D', 'O', 'D'},
                                      {'O', 'O', 'O', 'O', 'X'},
                                      {'X', 'D', 'D', 'O', 'O'},
                                      {'X', 'D', 'D', 'D', 'O'}};

            Queue<Coordinate> queue = null;
            int length = land.GetUpperBound(0);
            

            int result = int.MaxValue;

            for (int k = 0; k <= length; k++)
            {
                for (int l = 0; l <= length; l++)
                {
                    if (land[k,l] == 'S')
                    {
                        bool[,] visited = new bool[length + 1, length + 1];
                        queue = new Queue<Coordinate>();
                        queue.Enqueue(new Coordinate(k, l));
                        visited[k, l] = true;
                        int steps = NewMethod(land, queue, length, visited);
                        if (result > steps)
                            result = steps;
                    }
                }
            }
            return result;
        }


        //Amazon Assessment -- 1 -- Top K Frequently Mentioned Keywords
        public static void TestMaximumToys()
        {
            int numToys = 6;
            int topToys = 2;
            string[] toys = new string[] { "elmo","elsa", "legos", "tablet", "warcraft"};
            int numQuotes = 6;
            string[] quotes= new string[] {"Elmo is the hottest toy of the season! Elmo will be on every kid's wishlist!", 
                    "The new Elmo dolls are super high quality.",
                    "Expect the Elsa dolls to be very popular this year",
                    "Elsa and Elmo are the toys i'll be buying for my kids",
                    "For parents of older kids, look into buying them a drone.",
                    "Warcraft is slowly rising in popularity ahead of the holiday season."};
            //var result = maximum_toys(numToys, topToys, toys, numQuotes, quotes.ToList());
            var result = GetTopKeywords(numToys, topToys, toys, numQuotes, quotes);
            //expected output: elmo, elsa

            toys = new string[] { "anacell", "cetracular", "betacellular" };
            quotes = new string[] {
                  "Anacell provides the best services in the city",
                  "betacellular has awesome services",
                  "Best services provided by anacell, everyone should use anacell",
            };

            //Output:
            //["anacell", "betacellular"]
             result = GetTopKeywords(numToys, topToys, toys, numQuotes, quotes);

            toys = new string[] { "anacell", "betacellular", "cetracular", "deltacellular", "eurocell" };
            quotes = new string[] {
                  "I love anacell Best services; Best services provided by anacell",
                  "betacellular has great services",
                  "deltacellular provides much better services than betacellular",
                  "cetracular is worse than anacell",
                  "Betacellular is better than deltacellular.",
                };
            //Output:
            //["betacellular", "anacell"]
            result = GetTopKeywords(numToys, topToys, toys, numQuotes, quotes);

            Console.Read();
        }

        private static List<string> GetTopKeywords(int k, int topToys, string[] keywords, int numQuotes, string[] reviews)
        {
            var keywordsDictionary = new Dictionary<string, int>();
            foreach (var key in keywords)
            {
                int count = reviews.Count(x => x.ToLower().Contains(key.ToLower()));
                keywordsDictionary.Add(key, count);
            }
            return keywordsDictionary.OrderByDescending(x => x.Value).Select(x => x.Key).Take(topToys).ToList();
        }

        //Amazon Assessment -- 2 --  Zombie in Matrix -- https://leetcode.com/discuss/interview-question/411357/
        public static int MinDays()
        {
            int[,] grid = new int[,] { { 0, 1, 1, 0, 1 }, { 0, 1, 0, 1, 0 }, { 0, 0, 0, 0, 1 }, { 0, 1, 0, 0, 0} };
            Queue<Coordinate> q = new Queue<Coordinate>();
            int m = grid.GetLength(0);
            int n = grid.GetLength(1);
            int target =  m*n;
            int cnt = 0, res = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i,j] == 1)
                    {
                        q.Enqueue(new Coordinate(i,j));
                        cnt++;
                    }
                }
            }
            int[] xdirs = new int[] { 1, 0, -1, 0 };
            int[] ydirs = new int[] { 0, 1, 0, -1 };

            while (q.Count > 0)
            {
                int size = q.Count();
                if (cnt == target)
                    return res;
                for(int i = 0; i < size; i++)
                {
                    Coordinate cur = q.Dequeue();
                    for(int k=0; k<xdirs.Length; k++)
                    {
                        int ni = cur.x + xdirs[k];
                        int nj = cur.y + ydirs[k];
                        if (ni >= 0 && ni < m && nj >= 0 && nj < n && grid[ni,nj] == 0)
                        {
                            cnt++;
                            q.Enqueue(new Coordinate(ni, nj));
                            grid[ni,nj] = 1;
                        }
                    }
                }
                res++;
            }
            return -1;
        }

    }
}
    