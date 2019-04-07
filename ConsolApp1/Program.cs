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
            bool[,] visitedMatrix = new bool[rowLen, colLen];
            visitedMatrix.Initialize();
            Dictionary<string, Node> NodeToVisit = new Dictionary<string, Node>();


            for (int r = 0; r < rowLen; r++)
            {
                for (int c = 0; c < colLen; c++)
                {
                    if (p[r, c] == 1 && visitedMatrix[r, c] == false)
                    {


                        CheckRight(r, c, p, visitedMatrix, ref NodeToVisit);
                        CheckDown(r, c, p, visitedMatrix, ref NodeToVisit);
                        CheckRightBottomCorner(r, c, p, visitedMatrix, ref NodeToVisit);
                        CheckLeftBottomCorner(r, c, p, visitedMatrix, ref NodeToVisit);
                        visitedMatrix[r, c] = true;



                        var keys = NodeToVisit.Keys.ToList();

                        for (int i = 0; i < keys.Count; i++)

                        {
                            var key = keys[i];

                            if (NodeToVisit[key].IsVisited == false)
                            {

                                CheckLeft(NodeToVisit[key].r, NodeToVisit[key].c, p, visitedMatrix, ref NodeToVisit);
                                CheckRight(NodeToVisit[key].r, NodeToVisit[key].c, p, visitedMatrix, ref NodeToVisit);
                                CheckUp(NodeToVisit[key].r, NodeToVisit[key].c, p, visitedMatrix, ref NodeToVisit);

                                CheckDown(NodeToVisit[key].r, NodeToVisit[key].c, p, visitedMatrix, ref NodeToVisit);
                                CheckRightBottomCorner(NodeToVisit[key].r, NodeToVisit[key].c, p, visitedMatrix, ref NodeToVisit);
                                CheckRightUpperCorner(NodeToVisit[key].r, NodeToVisit[key].c, p, visitedMatrix, ref NodeToVisit);
                                CheckLeftUpperCorner(NodeToVisit[key].r, NodeToVisit[key].c, p, visitedMatrix, ref NodeToVisit);

                                CheckLeftBottomCorner(NodeToVisit[key].r, NodeToVisit[key].c, p, visitedMatrix, ref NodeToVisit);
                                NodeToVisit[key].IsVisited = true;
                                visitedMatrix[NodeToVisit[key].r, NodeToVisit[key].c] = true;
                                keys = NodeToVisit.Keys.ToList();
                            }
                        }

                        count++;



                    }


                }
            }

            return count.ToString(); ;
        }



        private static void CheckRight(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {

            int length = PatientMatrix.GetLength(1);


            for (int c = col + 1; c < length; c++)
            {
                if (PatientMatrix[row, c] == 1 && !IsVisited[row, c])           //
                {
                    Node aNode = new Node();
                    aNode.r = row;
                    aNode.c = c;
                    aNode.key = aNode.r.ToString() + aNode.c.ToString();

                    if (NodeToVisit.ContainsKey(aNode.key) == false)
                    {
                        aNode.IsVisited = false;
                        NodeToVisit.Add(aNode.key, aNode);
                    }
                }
                else
                {
                    return;
                }
            }


        }




        private static void CheckLeft(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {




            for (int c = col - 1; c >= 0; c--)
            {
                if (PatientMatrix[row, c] == 1 && IsVisited[row, c] == false)      //if new unvisited node found
                {
                    Node aNode = new Node();
                    aNode.r = row;
                    aNode.c = c;
                    aNode.key = aNode.r.ToString() + aNode.c.ToString();
                    if (NodeToVisit.ContainsKey(aNode.key) == false)            //if not in Node to Visit Dictionary
                    {
                        aNode.IsVisited = false;
                        NodeToVisit.Add(aNode.key, aNode);                      //adding to dictionary to visit later.
                    }
                }
                else
                {
                    return;
                }
            }


        }



        private static void CheckDown(int r, int c, int[,] p, bool[,] IsVisited, ref Dictionary<string, Node> nodeToVisit)
        {
            int rowLen = p.GetLength(0);


            for (int i = r + 1; i < rowLen; i++)
            {
                if (p[i, c] == 1 && !IsVisited[i, c])
                {

                    Node aNode = new Node();
                    aNode.r = i;
                    aNode.c = c;
                    aNode.key = aNode.r.ToString() + aNode.c.ToString();
                    if (nodeToVisit.ContainsKey(aNode.key) == false)
                    {

                        aNode.IsVisited = false;
                        nodeToVisit.Add(aNode.key, aNode);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        private static void CheckUp(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {


            for (int r = row - 1; r >= 0; r--)
            {
                if (PatientMatrix[r, col] == 1 && !IsVisited[r, col])            //if unvisited node found with value 1
                {
                    Node aNode = new Node();
                    aNode.r = r;
                    aNode.c = col;
                    aNode.key = aNode.r.ToString() + aNode.c.ToString();         //creating the dictionary key using the index  

                    if (NodeToVisit.ContainsKey(aNode.key) == false)             //if not found in Node to Visit Dictionary
                    {
                        aNode.IsVisited = false;
                        NodeToVisit.Add(aNode.key, aNode);
                    }                                                            //adding to dictionary to visit later.
                }
                else
                {
                    return;                                                      //if no more cells found with value 1 or went through all the cells
                }
            }

        }



        private static void CheckRightBottomCorner(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {


            int colLen = PatientMatrix.GetLength(1);                                // getting the columns length        
            int rowLen = PatientMatrix.GetLength(0);                                // getting the row length    

            for (int c = col + 1; c < colLen; c++)                                  // start moving right from the current cell
            {
                if (row < rowLen - 1)                                               // stopping just before the last row
                {
                    row++;
                    if (PatientMatrix[row, c] == 1 && !IsVisited[row, c])           // if unvisited node found with value 1
                    {
                        Node aNode = new Node();
                        aNode.r = row;
                        aNode.c = c;
                        aNode.key = aNode.r.ToString() + aNode.c.ToString();         //creating the dictionary key using the index  

                        if (NodeToVisit.ContainsKey(aNode.key) == false)             //if not found in Node to Visit Dictionary
                        {
                            aNode.IsVisited = false;
                            NodeToVisit.Add(aNode.key, aNode);                       //adding to dictionary to visit later.
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



        private static void CheckLeftBottomCorner(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {
            int rowLen = PatientMatrix.GetLength(0);                                    // getting the row length    

            for (int c = col - 1; c >= 0; c--)                                          // start moving left from the current cell
            {
                if (row < rowLen - 1)                                                   // stopping just before the last row
                {
                    row++;                                                              // moving to the next bottom row
                    if (PatientMatrix[row, c] == 1 && !IsVisited[row, c])               // if unvisited node found with value 1
                    {
                        Node aNode = new Node();
                        aNode.r = row;
                        aNode.c = c;
                        aNode.key = aNode.r.ToString() + aNode.c.ToString();             //creating the dictionary key using the index

                        if (NodeToVisit.ContainsKey(aNode.key) == false)                 //if not found in NodetoVisit Dictionary
                        {
                            aNode.IsVisited = false;
                            NodeToVisit.Add(aNode.key, aNode);                           //adding to dictionary to visit later.
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



        private static void CheckRightUpperCorner(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {

            int colLen = PatientMatrix.GetLength(1);                                 // getting the column length    

            for (int c = col + 1; c < colLen; c++)                                   // start moving right from the current cell
            {
                if (row > 0)                                                         // stopping just before the last row
                {
                    row--;                                                           // moving to the previous row
                    if (PatientMatrix[row, c] == 1 && !IsVisited[row, c])            // if unvisited node found with value 1
                    {
                        Node aNode = new Node();
                        aNode.r = row;
                        aNode.c = c;
                        aNode.key = aNode.r.ToString() + aNode.c.ToString();          //creating the dictionary key using the index

                        if (NodeToVisit.ContainsKey(aNode.key) == false)              //if not found in NodetoVisit Dictionary
                        {
                            aNode.IsVisited = false;
                            NodeToVisit.Add(aNode.key, aNode);                        //adding to dictionary to visit later.
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


        private static void CheckLeftUpperCorner(int r, int c, int[,] p, bool[,] IsVisited, ref Dictionary<string, Node> nodeToVisit)
        {

            int colLen = p.GetLength(1);
            int rolLen = p.GetLength(0);
            for (int i = c - 1; i >= 0; i--)
            {
                if (r > 0)
                {

                    r--;
                    if (p[r, i] == 1 && !IsVisited[r, i])
                    {

                        Node aNode = new Node();
                        aNode.r = r;
                        aNode.c = i;
                        aNode.key = aNode.r.ToString() + aNode.c.ToString();
                        if (nodeToVisit.ContainsKey(aNode.key) == false)
                        {

                            aNode.IsVisited = false;
                            nodeToVisit.Add(aNode.key, aNode);
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
        public string key;
        public bool IsVisited;
        public int r;
        public int c;
    }
}
