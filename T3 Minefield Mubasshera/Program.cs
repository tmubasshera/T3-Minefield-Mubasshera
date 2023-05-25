using System;
using System.Collections.Generic;

namespace T3_Minefield_Mubasshera
{
    class Node //a class to hold all the information about a Node 
    {
        //the coordinates
        public int X { set; get; }
        public int Y { set; get; }
        public Node(int x, int y)//constructor
        {
            X = x;
            Y = y;
        }
        public bool IsVisited { set; get; } 
        public bool IsSafe { set; get; }

        public bool isNeighbour(Node check)
        {
            int dx = Math.Abs(this.X - check.X);
            int dy = Math.Abs(this.Y - check.Y);

            if (dx == 0 && dy == 1 || dx == 1 && dy == 0 || dx == 1 && dy == 1)
            {
                return true;
            }
            else 
                return false;
        }
    }

    class Traverser
    {
        Traverser(Node startPos)
        {
            CurrentPos = startPos;
            PrevPos = null;
            TravelledPath = new List<Node>();
        }
        //to keep track of where dog/girl is currently
        public Node CurrentPos { set; get; }
        //to check the previous Node so it can later help Ally
        public Node PrevPos { set; get; }

        //need to keep track of the path already travelled
        public List<Node> TravelledPath { set; get; }
        //function to change to new position
        public void MoveTo(Node newNode)
        {
            PrevPos = CurrentPos;
            CurrentPos = newNode;
        }

        //Getting neighbors (assuming traversing diagonally is possible) 
        public List<Node> GetNeighbours()
        {
            List<Node> neighbors = new List<Node>();
            for (int i= (this.CurrentPos.X-1); i<= (this.CurrentPos.X+1); i++)
            {
                for  (int j =(this.CurrentPos.Y-1); j <=(this.CurrentPos.Y+1); j++)
                {
                    if (i==CurrentPos.X && j==CurrentPos.Y)
                    {
                        continue;
                    }
                    else
                    neighbors.Add(new Node(i,j));
                }
            }
            return neighbors;
        }

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

        static void DisplayNodesList(List<Node> nodes)
        {
            foreach (Node n in nodes)
            {
                Console.WriteLine(" ( " + n.X + " , " + n.Y + " ) ");
            }
        }

        static void Main(string[] args)
        {
            Node startNode = new Node(0, 1);
            List<Node> safePath = new List<Node>();

            for (int i=0; i<5; i++)
            {
                for (int j=0; j<5; j++)
                {
                    if (minefield[i, j] == 'S')
                    {
                        safePath.Add(new Node(i, j));
                    }

                }
            }
        }

    }
}
