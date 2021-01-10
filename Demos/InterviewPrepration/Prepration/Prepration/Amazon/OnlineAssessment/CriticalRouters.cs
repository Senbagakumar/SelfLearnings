using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class CriticalRouters
    {

        //https://leetcode.com/problems/jump-game/

        //Critical Routers -- https://www.youtube.com/watch?v=2kREIkF9UAs

        //https://leetcode.com/discuss/interview-question/436073/

        //Critical Connections in a Network
        //https://leetcode.com/problems/critical-connections-in-a-network/
        //https://leetcode.com/problems/critical-connections-in-a-network/discuss/397045/C-Trajan's-Algorithm-%2B-unit-test
        //Input: n = 4, connections = [[0,1],[1,2],[2,0],[1,3]] Output: [[1,3]] Explanation: [[3,1]] is also accepted.
        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)  // this is wrong
        {
            int[] low = new int[n];
            List<IList<int>> result = new List<IList<int>>();
            //we init the visited array to -1 for all vertices
            int[] visited = Enumerable.Repeat(-1, n).ToArray();

            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            //the graph is connected two ways
            foreach (var list in connections)
            {
                if (!dict.ContainsKey(list[0]))
                {
                    dict.Add(list[0], new List<int>());
                }
                dict[list[0]].Add(list[1]);


                if (!dict.ContainsKey(list[1]))
                {
                    dict.Add(list[1], new List<int>());
                }
                dict[list[1]].Add(list[0]);

            }

            for (int i = 0; i < n; i++)
            {
                if (visited[i] == -1)
                {
                    DFS(i, low, visited, dict, result, i);
                }
            }
            return result;
        }
        private int time = 0;
        private void DFS(int u, int[] low, int[] visited, Dictionary<int, List<int>> dict, List<IList<int>> result, int pre)
        {
            visited[u] = low[u] = ++time; // discovered u;
            for (int j = 0; j < dict[u].Count; j++) //iterate all of the nodes connected to u
            {
                int v = dict[u][j];
                if (v == pre)
                {
                    //if parent vertex ignore
                    continue;
                }

                if (visited[v] == -1) // if not visited
                {
                    DFS(v, low, visited, dict, result, u);
                    low[u] = Math.Min(low[u], low[v]);
                    if (low[v] > visited[u])
                    {
                        //u-v is critical there is no path for v to reach back to u or previous vertices of u
                        result.Add(new List<int> { u, v });
                    }
                }
                else // if v is already visited put the minimum into low for vertex u
                {
                    low[u] = Math.Min(low[u], visited[v]);
                }
            }
        }

    }
}
