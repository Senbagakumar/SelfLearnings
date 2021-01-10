using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Prepration.Amazon.OnlineAssessment
{
    class MinCostToConnectAllNodes
    {
        //https://leetcode.com/discuss/interview-question/356981
        //https://github.com/mission-peace/interview/blob/master/src/com/interview/graph/DisjointSet.java -- Reference

        int[] Parent;
        int connectionLength;


        private void Union(int v1, int v2)
        {
            int v1Parent = this.Parent[v1];
            int v2Parent = this.Parent[v2];

            if (v1Parent == v2Parent)
                return;

            connectionLength--;
            this.Parent[v2] = v1Parent;
        }

        private int FindSet(int v)
        {
            int vParent = this.Parent[v];
            if (vParent == v)
                return vParent;

            this.Parent[v] = FindSet(vParent); //Path-Comprssion
            return this.Parent[v];
        }

        public int MinCostToConnectNodes(int n, int[][] edges, int[][] newEdges)
        {
            this.connectionLength = n;
            this.Parent = new int[n + 1];

            //Every node parent of its own

            for (int i = 0; i <= n; i++)
                this.Parent[i] = i;

            Array.Sort(newEdges, (a, b) => a[2] - b[2]);

            foreach (int[] edge in edges)
            {
               Union(edge[0], edge[1]);
            }

            

            int minCost = 0;

            foreach(int[] edge in newEdges)
            {
                if (FindSet(edge[0]) == FindSet(edge[1]))
                    continue;

                Union(edge[0], edge[1]);
                minCost += edge[2];
                if (connectionLength == 1)
                    return minCost;
            }

            return -1;
        }



    }
}
