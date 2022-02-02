using System;
using System.Collections.Generic;
using System.Text;

namespace FordFulkersonAlgorithm
{
    class FindPath
    {
        private List<int> currentPath = new List<int>();

        private bool isEnd = false;

        private int[,] graph;
        private int endPoint;

        private int[] flows;

        private List<int> banList = new List<int>();

        public FindPath(int [,] graph, int endPoint) 
        {
            this.endPoint = endPoint;
            this.graph = graph;
            this.flows = new int[graph.GetLength(0)];
        }

        public int[] findPath() 
        {
            currentPath.Add(graph[0,0]);

            while (true) 
            {
                if (isEnd) 
                {
                    break;
                }

                currentPath.Add(findNextPoint());
                if (currentPath[currentPath.Count - 1] == -1)
                    currentPath.RemoveAt(currentPath.Count - 1);

                if (currentPath.Contains(endPoint)) 
                {
                    break;
                }
            }

            return currentPath.ToArray();    
        }

        private int findNextPoint() 
        {
            int nextPoint = -1;

            nextPoint = findNeighbourWithBiggestCapacity();

            if (nextPoint == -1) 
            {
                nextPoint = findBackwardsNeighbourWithBiggestCapacity();
                if (nextPoint == -1) 
                {
                    if (currentPath[currentPath.Count - 1] == graph[0, 0])
                    {
                        isEnd = true;
                        return -1;
                    }
                    backtrackOnOnePoint();
                }
            }

            return nextPoint;
        }

        private int findNeighbourWithBiggestCapacity()
        {
            int nextPoint = -1;
            int maxFlow = 0;

            for (int ctr = 0; ctr < graph.GetLength(0); ++ctr) 
            {
                if (!banList.Contains(ctr) && graph[ctr, 0] == currentPath[currentPath.Count - 1])
                {
                    if (graph[ctr, 2] > maxFlow)
                    {
                        maxFlow = graph[ctr, 2];
                        nextPoint = graph[ctr, 1];
                    }
                }
            }

            return nextPoint;
        }

        private int findBackwardsNeighbourWithBiggestCapacity() 
        {
            int nextPoint = -1;
            int maxFlow = 0;

            for (int ctr = 0; ctr < graph.GetLength(0); ++ctr) 
            {
                if (!banList.Contains(ctr) && graph[ctr, 1] == currentPath[currentPath.Count - 1] && !currentPath.Contains(graph[ctr, 0]) && flows[ctr] > maxFlow) 
                {
                    maxFlow = flows[ctr];
                    nextPoint = graph[ctr, 0];
                }
            }

            return nextPoint;
        }

        private void backtrackOnOnePoint() 
        {
            banList.Add(getBannedCtr(currentPath[currentPath.Count - 2], currentPath[currentPath.Count - 1]));
            currentPath.RemoveAt(currentPath.Count - 1);
        }

        private int getBannedCtr(int pt1, int pt2) 
        {
            for (int ctr = 0; ctr < graph.GetLength(0); ++ctr) 
            {
                if (graph[ctr, 0] == pt1 && graph[ctr, 1] == pt2 || graph[ctr, 1] == pt1 && graph[ctr, 0] == pt2) 
                {
                    return ctr;
                }
            }

            return 0;
        }

        public void clearTraces() 
        {
            banList.Clear();
            currentPath.Clear();
        }

        //need to optimize for backwards edges
        public int getBottleNeckValue(int [] path) 
        {
            int bottleNeckVal = int.MaxValue;
            for (int ctr1 = 0; ctr1 < path.Length - 1; ++ctr1) 
            {
                for (int ctr = 0; ctr < graph.GetLength(0); ++ctr) 
                {
                    if (graph[ctr, 0] == path[ctr1] && graph[ctr, 1] == path[ctr1 + 1])
                    {
                        if (graph[ctr, 2] < bottleNeckVal)
                        {
                            bottleNeckVal = graph[ctr, 2];
                            break;
                        }
                    }
                    //getting backwards value
                    if (ctr == graph.GetLength(0) - 1) 
                    {
                        for (int ctr2 = 0; ctr2 < graph.GetLength(0); ++ctr2) 
                        {
                            if (graph[ctr2, 1] == path[ctr1] && graph[ctr2, 0] == path[ctr1 + 1]) 
                            {
                                if (flows[ctr2] < bottleNeckVal) 
                                {
                                    bottleNeckVal = flows[ctr2];
                                    break;
                                }
                            }
                        }
                    }

                }
            }

            return bottleNeckVal;
        }

        public void fixFlows(int [] path, int bottleNeckVal) 
        {
            for (int ctr = 0; ctr < path.Length - 1; ++ctr) 
            {
                for (int ctr1 = 0; ctr1 < graph.GetLength(0); ++ctr1) 
                {
                    if (graph[ctr1, 0] == path[ctr] && graph[ctr1, 1] == path[ctr + 1]) 
                    {
                        graph[ctr1, 2] -= bottleNeckVal;
                        flows[ctr1] += bottleNeckVal;
                    }
                }
            }
        }
    }
}
