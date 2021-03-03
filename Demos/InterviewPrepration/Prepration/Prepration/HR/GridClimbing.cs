using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.HR
{
    class GridClimbing
    {

        //A grid with m rows and n columns is used to form a cluster of nodes.If a point in the grid has a value of 1, then it represents a node.
        //Each node in the cluster has a level associated with it. A node located in the ith row of the grid is a level i node.
        //Here are the rules for creating a cluster:
        //Every node at level i connects to all the nodes at level k where k > i and k is the first level after level i that contains at least 1 node.
        //When i reaches the last level in the grid, no more connections are possible.
        //Given such a grid, find the number of connections present in the cluster.  
        //Example:
        //gridOfNodes = [[1, 1, 1], [0, 1, 0], [0, 0, 0], [1, 1, 0]]  

        //Row 1 to row 2:
        //Each of the three nodes in the first row connects to the single node in the second row: 3 connections.
        //Row 2 to row 4:
        //There is no node in row 3 so it is skipped.
        //The single node in the second row connects to each of the two nodes in the last row: 2 connections.
        //There are a total of 3 + 2 = 5 connections.
        //Function Description
        //Complete the numberOfConnections function in the editor below.The function must return an integer denoting the number of connections.
        //numberOfConnections has the following parameter(s) :
        //int gridOfNodes[n][m]: the nodes grid
        //Returns
        //int: the total number of connections
        //Constraints
        //1 ≤ n ≤ 500
        //1 ≤ m ≤ 500
        //Each gridOfNodes[i][j] is in the set {0, 1}.

       public static int NumberOfConnections(int[][] grid)
        {
            int sum = 0;
            var nonZeroNodes = new List<int>();
            for(int i=0; i< grid.Length; i++)
            {
                sum = NonZeroByLevels(grid, i);
                if (sum > 0)
                    nonZeroNodes.Add(sum);
            }
            sum = 0;
            for(int j=0; j<nonZeroNodes.Count-1; j++)
            {
                sum += nonZeroNodes[j] * nonZeroNodes[j + 1];
            }
            return sum;

        }

        private static int NonZeroByLevels(int[][] grid, int row)
        {
            int sum = 0;
            for(int i=0; i< grid[row].Length; i++)
            {
                sum += grid[row][i];
            }
            return sum;
        }

    }
}
