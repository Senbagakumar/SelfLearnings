using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class RoverControl
    {
        //Rover Control
        //https://leetcode.com/discuss/interview-question/985703/Amazon-OA-2020-Rover-Control

        //A Mars rover is directed to move within a square matrix.It accepts a sequence of commands to move in any of the four directions from each cell: [UP, DOWN, LEFT or RIGHT]. 
        //The rover starts from cell 0. and may not move diagonally or outside of the boundary.

        //Example
        //n = 4
        //commands = [RIGHT, UP, DOWN, LEFT, DOWN, DOWN]

        //The rover path is shown below.

        //0 1 2 3
        //4 5 6 7
        //8 9 10 11
        //12 13 14 15

        //RIGHT: Rover moves to position 1
        //UP: Position unchanged, as the move would take the rover out of the boundary.
        //DOWN: Rover moves to Position 5.
        //LEFT: Rover moves to position 4
        //DOWN: Rover moves to position 8
        //DOWN: The rover ends up in position 12.


        //The function returns 12.

        //{ "R", "U", "D", "L", "D", "D" }, 4); Ans: 12
        //{ "R", "D", "L", "L", "D" }, 4); Ans: 8

        public static int Rover_Control(string[] commands, int n)
        {
            int positionX = 0;
            int positionY = 0;

            int[][] path = new int[4][];
            path[0] = new int[4] { 0, 1, 2, 3 };
            path[1] = new int[4] { 4,5,6,7 };
            path[2] = new int[4] { 8,9,10,11};
            path[3] = new int[4] { 12,13,14,15};

            int result = path[positionX][positionY];

            foreach(string cmd in commands)
            {
                if(cmd == "L" && positionY > 0)
                {
                    positionY--;                    
                }

                else if(cmd == "R" && positionY < n)
                {
                    positionY++;
                }

                else if(cmd == "D" && positionX < n)
                {
                    positionX++;
                }
                else if( cmd == "U" && positionX > 0)
                {
                    positionX--;
                }

                result = path[positionX][positionY];
            }

            return result;
        }
    }
}
