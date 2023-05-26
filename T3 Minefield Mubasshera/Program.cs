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
        public Node CurrentPos { set; get; }
        //to check the previous Node so it can later help Ally
        public Node PrevPos { set; get; }

        public Traverser(Node startPos)
        {
            CurrentPos = startPos;
            PrevPos = new Node (-1, -1);
        }
        
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

        public List<Node> GetSafeNeighbors(char[,] minefield)
        {
            List<Node> neighbours = this.GetNeighbours();
            List<Node> safeNeighbors = new List<Node>();
            foreach (Node n in neighbours)
            {
                if (n.X >= 0 && n.X < minefield.GetLength(0) && n.Y >= 0 && n.Y < minefield.GetLength(1) && minefield[n.X, n.Y] == 'S')
                {
                    safeNeighbors.Add(n);
                }
            }
            return safeNeighbors;
        }
    }
    

    class Program
    {
        static char[,] minefield = new char[,]
            {
                {'0', 'S', 'B', 'B', '0'},
                {'B', 'S', 'B', 'B', '0'},
                {'0', 'B', 'S', 'B', 'B'},
                {'B', '0', 'S', 'B', 'B'},
                {'0', 'B', 'S', 'S', 'B'}
            };
        static void Display(List<Node> totoshkaPath, List<Node> allyPath)
        {
            Console.WriteLine("Path as traversed by Totoshka and Ally: ");
            Console.Write("TotoshKa: \t Ally: \n");
            for (int i=0; i<totoshkaPath.Count; i++)
            {
                
                Console.Write(" (" + totoshkaPath[i].X + "," + totoshkaPath[i].Y + ")");
                
                Console.WriteLine("\t\t(" + allyPath[i].X + "," + allyPath[i].Y + ")");
            }
        }

        static bool checkIfInList(List<Node> travelled, Node n)
        {
            foreach (Node t in travelled)
            {
                if (t.X == n.X && t.Y == n.Y)
                    return true;
            }
            return false;
        }

        static void FindSafePath(Traverser totoshka, Traverser ally, Node startNode, Node endNode, List<Node> totoshkaPath, List<Node> allyPath)
        {
            totoshka.MoveTo(startNode);
            ally.MoveTo(totoshka.PrevPos);
            totoshkaPath.Add(totoshka.CurrentPos);
            allyPath.Add(ally.CurrentPos);
            if (totoshka.CurrentPos.isNeighbour(endNode))
            {
                totoshka.MoveTo(endNode);
                ally.MoveTo(totoshka.PrevPos);

                totoshkaPath.Add(totoshka.CurrentPos);
                allyPath.Add(ally.CurrentPos);
                return;
            }

            List <Node> totoshkasNeighbors = totoshka.GetSafeNeighbors(minefield);
            Random random = new Random();
            Node nextNode = null;
            while (totoshkasNeighbors.Count > 0)
            {
                int randomIndex = random.Next(totoshkasNeighbors.Count);
                nextNode = totoshkasNeighbors[randomIndex];
                
                if (!checkIfInList(totoshkaPath, nextNode))
                {
                    break;
                }
                totoshkasNeighbors.RemoveAt(randomIndex);
            }
            FindSafePath(totoshka, ally, nextNode, endNode, totoshkaPath,allyPath);
        }

        static void Main(string[] args)
        {
            Node startNode = new Node(0, 1);
            Node endNode = new Node(4,3);
            List<Node> totoshkaPath = new List<Node>();
            List<Node> allyPath = new List<Node>();
            List<Node> safePath = new List<Node>();

            Traverser totoshka = new Traverser(new Node(-1,-1));
            Traverser ally = new Traverser(new Node(-1, -1));

            FindSafePath(totoshka, ally, startNode, endNode, totoshkaPath, allyPath);
            
            
            Display(totoshkaPath, allyPath);

           

        }

    }
}
