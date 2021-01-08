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




        
    }
}
    