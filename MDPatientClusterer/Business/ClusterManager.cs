using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDPatientClusterer.Business
{
    public class ClusterManager
    {
        public int GetClusters(JObject PatientObj)
        {
            var PatientMatrix = ConvertJObjectTo2DArray(PatientObj);
            int ClustCount = 0;
            int RowSize = PatientMatrix.GetLength(0);
            int ColumnSize = PatientMatrix.GetLength(1);
            bool[,] VisitedMatrix = new bool[RowSize, ColumnSize];
            VisitedMatrix.Initialize();
          

            return 0;
        }

        private int[,] ConvertJObjectTo2DArray(JObject PatientObj)
        {
            string key = "matrix";
            var PatientArray = PatientObj[key].ToArray();                     // converting the json object into array.
            int row = PatientArray.Count();                                   // getting the number of rows from the array.
            int col = PatientArray[0].Count();                                // getting the number of columns from the array.
            int[,] PatientMatrix = new int[row, col];                         // Creating a 2d array based on the row and column 
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    PatientMatrix[i, j] = Convert.ToInt16(PatientArray[i][j]);// populatting the 2d array 
                }
            }

            return PatientMatrix;


        }


        //To check all the cells which are at the left side of current cell
        private void CheckLeft(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {

            for (int c = col - 1; c >= 0; c--)
            {
                if (PatientMatrix[row, c] == 1 && IsVisited[row, c] == false)   //if unvisited node found with value 1
                {
                    Node aNode = new Node();
                    aNode.r = row;
                    aNode.c = c;
                    aNode.key = aNode.r.ToString() + aNode.c.ToString();        //creating the dictionary key using the index  
                    if (NodeToVisit.ContainsKey(aNode.key) == false)            //if not found in NodetoVisit Dictionary
                    {
                        aNode.IsVisited = false;
                        NodeToVisit.Add(aNode.key, aNode);                      //adding to dictionary to visit later.
                    }
                }
                else
                {
                    return;                                                     //if no more cells found with value 1 or went through all the cells
                }
            }
        }


        //To check all the cells which are at the right side of current cell
        private static void CheckRight(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {
            int length = PatientMatrix.GetLength(1);                            //the length of columns                


            for (int c = col + 1; c < length; c++)
            {
                if (PatientMatrix[row, c] == 1 && !IsVisited[row, c])           //if unvisited node found with value 1
                {
                    Node aNode = new Node();
                    aNode.r = row;
                    aNode.c = c;
                    aNode.key = aNode.r.ToString() + aNode.c.ToString();        //creating the dictionary key using the index  

                    if (NodeToVisit.ContainsKey(aNode.key) == false)            //if not found in NodetoVisit Dictionary
                    {
                        aNode.IsVisited = false;
                        NodeToVisit.Add(aNode.key, aNode);                      //adding to dictionary to visit later.
                    }
                }
                else
                {
                    return;                                                     //if no more cells found with value 1 or went through all the cells
                }
            }
        }


        //To check all the cells which are at the top of current cell
        private void CheckTop(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {

            for (int r = row - 1; r >= 0; r--)
            {
                if (PatientMatrix[r, col] == 1 && !IsVisited[r, col])            //if unvisited node found with value 1
                {
                    Node aNode = new Node();
                    aNode.r = r;
                    aNode.c = col;
                    aNode.key = aNode.r.ToString() + aNode.c.ToString();         //creating the dictionary key using the index  

                    if (NodeToVisit.ContainsKey(aNode.key) == false)             //if not found in NodetoVisit Dictionary
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


        //To check all the cells which are at the bottom of current cell
        private static void CheckDown(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {
            int length = PatientMatrix.GetLength(0);

            for (int r = row + 1; r < length; r++)
            {
                if (PatientMatrix[r, col] == 1 && !IsVisited[r, col])              //if unvisited node found with value 1
                {
                    Node aNode = new Node();
                    aNode.r = r;
                    aNode.c = col;
                    aNode.key = aNode.r.ToString() + aNode.c.ToString();           //creating the dictionary key using the index  

                    if (NodeToVisit.ContainsKey(aNode.key) == false)               //if not found in NodetoVisit Dictionary
                    {
                        aNode.IsVisited = false;
                        NodeToVisit.Add(aNode.key, aNode);                         //adding to dictionary to visit later.
                    }
                }
                else
                {
                    return;                                                        //if no more cells found with value 1 or went through all the cells
                }
            }
        }


        //To check all the cells which are at the right bottom corner of current cell
        private static void CheckRightBottomCorner(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {

            int colLen = PatientMatrix.GetLength(1);                                // getting the columns length        
            int rowLen = PatientMatrix.GetLength(0);                                // getting the row length    

            for (int c = col + 1; c < colLen; c++)                                  // start moving right from the current cell
            {
                if (row < rowLen - 1)                                               // stopping just before the last row
                {
                    row++;                                                          // moving to next row
                    if (PatientMatrix[row, c] == 1 && !IsVisited[row, c])           // if unvisited node found with value 1
                    {
                        Node aNode = new Node();
                        aNode.r = row;
                        aNode.c = c;
                        aNode.key = aNode.r.ToString() + aNode.c.ToString();         //creating the dictionary key using the index  

                        if (NodeToVisit.ContainsKey(aNode.key) == false)             //if not found in NodetoVisit Dictionary
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


        //To check all the cells which are at the right upper corner of current cell
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


        //To check all the cells which are at the left bottom corner of current cell
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


        public class Node
        {
            public string key;                                                  //concatenation of row and column.
            public bool IsVisited;
            public int r;                                                       //row
            public int c;                                                       //column
        }
    }
}
