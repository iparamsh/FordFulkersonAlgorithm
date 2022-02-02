using System;
using FordFulkersonAlgorithm;

namespace MaxFlow
{
    class main
    {
        //{point A, point B, capacity}
        private static int[,] routs = new int[,]
        {
            /*{1,2, 2},
            {1,3, 1},
            {2,4, 1},
            {2,3, 3},
            {3,4, 2}*/ //max flow: 3



            /*{1,2,10},
            {1,3,10},
            {2,4,2},
            {2,5,3},
            {2,3,2},
            {3,5,10},
            {4,6,10},
            {5,4,6},
            {5,6,10}*/ // max flow: 15


            /*{1,2,10},
            {1,3,10},
            {2,4,4},
            {2,5,8},
            {2,3,2},
            {3,5,9},
            {4,6,10},
            {5,4,6},
            {5,6,10} */// max flow: 19

            {1, 2, 104},
            {1, 3, 1},
            {1, 4, 1},
            {2, 5, 1},
            {2, 6, 1},
            {2, 7, 1},
            {2, 3, 100},
            {3, 7, 101},
            {3, 8, 1},
            {4, 3, 1},
            {4, 8, 1},
            {5, 6, 1},
            {5, 9, 1},
            {6,9,1 }, //
            {7, 9, 1},
            {7, 8, 100},
            {8, 9, 100}

        };

        private static FordFulkersonAlgorithm.MaxFlow maxFlow = new FordFulkersonAlgorithm.MaxFlow();
        static void Main(string[] args)
        {
            Console.WriteLine("Max flow: " + maxFlow.getMaxFlow(routs, 9));
        }
    }
}
