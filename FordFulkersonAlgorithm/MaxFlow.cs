using System;
using System.Collections.Generic;
using System.Text;

namespace FordFulkersonAlgorithm
{
    public class MaxFlow
    {
        private int maxFlow = 0;
        public int getMaxFlow(int [,] graph, int endPoint) 
        {
            FindPath findPath = new FindPath(graph, endPoint);

            while (true)
            {
                int[] path = findPath.findPath();
                if (path[path.Length - 1] != endPoint) 
                {
                    break;
                }
                int bottleNeckVal = findPath.getBottleNeckValue(path);
                drawPath(path);
                Console.WriteLine("Bottle Neck val: " + bottleNeckVal);
                maxFlow += bottleNeckVal;
                findPath.fixFlows(path, bottleNeckVal);

                findPath.clearTraces();
            }

            Console.WriteLine();
            return maxFlow;
        }

        private void drawPath(int [] path) 
        {
            for (int ctr = 0; ctr < path.Length; ctr++) 
            {
                Console.Write(path[ctr] + "->");
            }
            Console.WriteLine();
        }
    }
}
