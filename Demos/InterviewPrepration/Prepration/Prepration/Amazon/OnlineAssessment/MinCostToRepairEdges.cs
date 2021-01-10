using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Prepration.Amazon.OnlineAssessment
{
    class MinCostToRepairEdges
    {
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

        private bool IsInSameEdgeToRepair(int[] edge, int[][] repairEdge)
        {
            foreach(int[] repair in repairEdge)
            {
                if (repair[0] == edge[0] && repair[1] == edge[1]) return true;
            }
            return false;
        }

        public int MinCostToRepair(int n, int[][] edges, int[][] repairEdges)
        {
            this.connectionLength = n;
            this.Parent = new int[n + 1];

            //Every node parent of its own

            for (int i = 0; i <= n; i++)
                this.Parent[i] = i;

            Array.Sort(repairEdges, (a, b) => a[2] - b[2]);

            foreach (int[] edge in edges)
            {
                if(!IsInSameEdgeToRepair(edge, repairEdges))
                    Union(edge[0], edge[1]);
            }



            int minCost = 0;

            foreach (int[] edge in repairEdges)
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
