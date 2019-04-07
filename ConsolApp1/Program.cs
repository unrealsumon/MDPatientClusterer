using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsolApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[5, 10];
            Random random = new Random();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(0, 2);
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(matrix[i, j] + "  ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(matrix[i, j] + "  ");
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Environment.NewLine);
              
           

            }
            string result = GetPatientGroups(matrix);
            Console.WriteLine("Number of Clusters: " + result);

            Console.ReadKey();
        }


        private static string GetPatientGroups(int[,] p)
        {
            int count = 0;
            int rowLen = p.GetLength(0);
            int colLen = p.GetLength(1);
            List<Node> NodeToVisit = new List<Node>();


            for (int r = 0; r < rowLen; r++)
            {
                for (int c = 0; c < colLen; c++)
                {
                    if (p[r, c] == 1)
                    {

                        var check = NodeToVisit.Where(x => x.index.c == c && x.index.r == r && x.IsVisited == true).FirstOrDefault();
                        if (check == null)
                        {
                            CheckRight(r, c, p, ref NodeToVisit);
                            CheckLeft(r, c, p, ref NodeToVisit);
                            CheckDown(r, c, p, ref NodeToVisit);
                            CheckUp(r, c, p, ref NodeToVisit);
                            CheckRightBottomCorner(r, c, p, ref NodeToVisit);
                            CheckLeftBottomCorner(r, c, p, ref NodeToVisit);
                            CheckRightUpperCorner(r, c, p, ref NodeToVisit);
                            CheckLeftUpperCorner(r, c, p, ref NodeToVisit);


                            for (int i = 0; i < NodeToVisit.Count(); i++)
                            {
                                if (NodeToVisit[i].IsVisited == false)
                                {
                                    CheckLeft(NodeToVisit[i].index.r, NodeToVisit[i].index.c, p, ref NodeToVisit);
                                    CheckRight(NodeToVisit[i].index.r, NodeToVisit[i].index.c, p, ref NodeToVisit);
                                    CheckUp(NodeToVisit[i].index.r, NodeToVisit[i].index.c, p, ref NodeToVisit);

                                    CheckDown(NodeToVisit[i].index.r, NodeToVisit[i].index.c, p, ref NodeToVisit);
                                    CheckRightBottomCorner(NodeToVisit[i].index.r, NodeToVisit[i].index.c, p, ref NodeToVisit);
                                    CheckRightUpperCorner(NodeToVisit[i].index.r, NodeToVisit[i].index.c, p, ref NodeToVisit);
                                    CheckLeftUpperCorner(NodeToVisit[i].index.r, NodeToVisit[i].index.c, p, ref NodeToVisit);

                                    CheckLeftBottomCorner(NodeToVisit[i].index.r, NodeToVisit[i].index.c, p, ref NodeToVisit);
                                    NodeToVisit[i].IsVisited = true;
                                }
                            }
                            count++;

                        }

                    }


                }
            }

            return count.ToString(); ;
        }



        private static void CheckRight(int r, int c, int[,] p, ref List<Node> nodeToVisit)
        {

            int colLen = p.GetLength(1);

            for (int i = c + 1; i < colLen; i++)
            {
                if (p[r, i] == 1)
                {
                    Index index = new Index();
                    Node aNode = new Node();
                    index.r = r;
                    index.c = i;
                    var check = nodeToVisit.Where(x => x.index.c == index.c && x.index.r == index.r).FirstOrDefault();
                    if (check == null)
                    {
                        aNode.index = index;
                        aNode.IsVisited = false;
                        nodeToVisit.Add(aNode);
                    }
                }
                else
                {
                    return;
                }
            }
        }


        private static void CheckLeft(int r, int c, int[,] p, ref List<Node> nodeToVisit)
        {

            int colLen = p.GetLength(1);

            for (int i = c - 1; i >= 0; i--)
            {
                if (p[r, i] == 1)
                {
                    Index index = new Index();
                    Node aNode = new Node();
                    index.r = r;
                    index.c = i;
                    var check = nodeToVisit.Where(x => x.index.c == index.c && x.index.r == index.r).FirstOrDefault();
                    if (check == null)
                    {
                        aNode.index = index;
                        aNode.IsVisited = false;
                        nodeToVisit.Add(aNode);
                    }
                }
                else
                {
                    return;
                }
            }
        }



        private static void CheckDown(int r, int c, int[,] p, ref List<Node> nodeToVisit)
        {
            int rowLen = p.GetLength(0);


            for (int i = r + 1; i < rowLen; i++)
            {
                if (p[i, c] == 1)
                {
                    Index index = new Index();
                    Node aNode = new Node();
                    index.r = i;
                    index.c = c;
                    var check = nodeToVisit.Where(x => x.index.c == index.c && x.index.r == index.r).FirstOrDefault();
                    if (check == null)
                    {
                        aNode.index = index;
                        aNode.IsVisited = false;
                        nodeToVisit.Add(aNode);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        private static void CheckUp(int r, int c, int[,] p, ref List<Node> nodeToVisit)
        {
            int rowLen = p.GetLength(0);


            for (int i = r - 1; i >= 0; i--)
            {
                if (p[i, c] == 1)
                {
                    Index index = new Index();
                    Node aNode = new Node();
                    index.r = i;
                    index.c = c;
                    var check = nodeToVisit.Where(x => x.index.c == index.c && x.index.r == index.r).FirstOrDefault();
                    if (check == null)
                    {
                        aNode.index = index;
                        aNode.IsVisited = false;
                        nodeToVisit.Add(aNode);
                    }
                }
                else
                {
                    return;
                }
            }
        }


        private static void CheckRightBottomCorner(int r, int c, int[,] p, ref List<Node> nodeToVisit)
        {

            int colLen = p.GetLength(1);
            int rolLen = p.GetLength(0);

            for (int i = c + 1; i < colLen; i++)
            {
                if (r < rolLen - 1)
                {
                    r++;
                    if (p[r, i] == 1)
                    {
                        Index index = new Index();
                        Node aNode = new Node();
                        index.r = r;
                        index.c = i;
                        var check = nodeToVisit.Where(x => x.index.c == index.c && x.index.r == index.r).FirstOrDefault();
                        if (check == null)
                        {
                            aNode.index = index;
                            aNode.IsVisited = false;
                            nodeToVisit.Add(aNode);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }



        private static void CheckLeftBottomCorner(int r, int c, int[,] p, ref List<Node> nodeToVisit)
        {

            int colLen = p.GetLength(1);
            int rolLen = p.GetLength(0);
            for (int i = c - 1; i >= 0; i--)
            {
                if (r < rolLen - 1)
                {

                    r++;
                    if (p[r, i] == 1)
                    {
                        Index index = new Index();
                        Node aNode = new Node();
                        index.r = r;
                        index.c = i;
                        var check = nodeToVisit.Where(x => x.index.c == index.c && x.index.r == index.r).FirstOrDefault();
                        if (check == null)
                        {
                            aNode.index = index;
                            aNode.IsVisited = false;
                            nodeToVisit.Add(aNode);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }


        private static void CheckRightUpperCorner(int r, int c, int[,] p, ref List<Node> nodeToVisit)
        {

            int colLen = p.GetLength(1);
            int rolLen = p.GetLength(0);

            for (int i = c + 1; i < colLen; i++)
            {
                if (r > 0)
                {
                    r--;
                    if (p[r, i] == 1)
                    {
                        Index index = new Index();
                        Node aNode = new Node();
                        index.r = r;
                        index.c = i;
                        var check = nodeToVisit.Where(x => x.index.c == index.c && x.index.r == index.r).FirstOrDefault();
                        if (check == null)
                        {
                            aNode.index = index;
                            aNode.IsVisited = false;
                            nodeToVisit.Add(aNode);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }


        private static void CheckLeftUpperCorner(int r, int c, int[,] p, ref List<Node> nodeToVisit)
        {

            int colLen = p.GetLength(1);
            int rolLen = p.GetLength(0);
            for (int i = c - 1; i >= 0; i--)
            {
                if (r > 0)
                {

                    r--;
                    if (p[r, i] == 1)
                    {
                        Index index = new Index();
                        Node aNode = new Node();
                        index.r = r;
                        index.c = i;
                        var check = nodeToVisit.Where(x => x.index.c == index.c && x.index.r == index.r).FirstOrDefault();
                        if (check == null)
                        {
                            aNode.index = index;
                            aNode.IsVisited = false;
                            nodeToVisit.Add(aNode);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
        }






    }

    public class Node
    {
        public bool IsVisited;
        public Index index;
    }

    public class Index
    {
        public int r;
        public int c;

    }
}
