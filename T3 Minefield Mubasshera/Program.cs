using System;
using System.Collections.Generic;

namespace T3_Minefield_Mubasshera
{
    class Node
    {
        public int X { set; get; }
        public int Y { set; get; }

        public Node(int x, int y)
        {
            x = X;
            y = Y;
        }
        public bool IsVisited { set; get; } 
        public bool isSafe { set; get; }
    }

    

    class Program
    {
        public int n = 5, m = 5;
        static char[,] minefield = new char[,]
        {
            {'0', 'S', 'B', 'B', '0'},
            {'B', 'S', 'S', 'S', '0'},
            {'0', 'B', 'B', 'S', 'B'},
            {'B', '0', 'B', 'S', 'B'},
            {'0', 'B', 'S', 'S', 'B'}
        };

        static void Main(string[] args)
        {
            Node startNode = new Node(0, 1);
            List<Node> safePath = new List<Node>();

            for (int i=0; i<5; i++)
            {
                for (int j=0; j<5; j++)
                {
                    if (minefield[i, j] == 'S')
                        safePath.Add(new Node(i, j));

                }
            }
        }
    }
}
