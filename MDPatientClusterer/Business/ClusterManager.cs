using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDPatientClusterer.Business
{
    public class ClusterManager
    {
        //returns the number of clusters in a matrix
        public int GetClusters(JObject PatientObj/*, int[,] TestMatrix*/)                               // the secon parameter is for unit testing.
        {
            /*var PatientMatrix = TestMatrix; */                                                        //for testing purpose
            var PatientMatrix = ConvertJObjectTo2DArray(PatientObj);                                    // Converting Json Object to 2D array
            int ClustCount = 0;                                                                         // Cluster counter 
            int RowSize = PatientMatrix.GetLength(0);                                                   
            int ColumnSize = PatientMatrix.GetLength(1);
            bool[,] VisitedMatrix = new bool[RowSize, ColumnSize];                                      // boolean matrix for tracking the visited nodes
            VisitedMatrix.Initialize();                                                                 // Initialize the visited matrix as false
            Dictionary<string, Node> NodeToVisit = new Dictionary<string, Node>();                      // List of Nodes will be visited.

            for (int r = 0; r < RowSize; r++)
            {
                for (int c = 0; c < ColumnSize; c++)
                {
                    if (PatientMatrix[r, c] == 1 && VisitedMatrix[r, c] == false)                       // if unvisited node found with value 1 
                    {
                        CheckRight(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);                // check the right neighbours until found a 0
                        CheckDown(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);                 // check down neighbours unit found a 0
                        CheckLeftBottomCorner(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);     // check bottom left corner neighbours until found a 0
                        CheckRightBottomCorner(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);    // check bottom right corner neighbours until found a 0

                        VisitedMatrix[r, c] = true;                                                     // mark the current node as visited 
                        var keys = NodeToVisit.Keys.ToList();                                           // getting the keys (indexes) from dictionary

                        for (int i = 0; i < keys.Count; i++)                                            // search the dictionary using key
                        {
                            var key = keys[i];
                            if (NodeToVisit[key].IsVisited == false)                                    // if the neighbour node is not visited yet
                            {
                                SearchNeighbours(NodeToVisit[key].r, NodeToVisit[key].c, PatientMatrix, VisitedMatrix, ref NodeToVisit);  // search neighbours           //To check all the neghbours of the current cell                      

                                NodeToVisit[key].IsVisited = true;                                      // update the dictionary node as visited
                                VisitedMatrix[NodeToVisit[key].r, NodeToVisit[key].c] = true;           // update the current node of boolean matrix as visited 
                                keys = NodeToVisit.Keys.ToList();                                       // update the key list while found new unvisited neighbours
                            }
                        }

                        ClustCount++;                                                                   // Cluster counter increments

                    }

                }
            }


            return ClustCount;                                                                          // return the number of cluster to controller
        }

        //To check all the neghbours of the current cell
        private void SearchNeighbours(int r, int c, int[,] PatientMatrix, bool[,] VisitedMatrix, ref Dictionary<string, Node> NodeToVisit)
        {
            CheckLeft(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);                    // To check all the cells which are at the left side of current cell
            CheckRight(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);                   // To check all the cells which are at the right side of current cell
            CheckTop(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);                     // To check all the cells which are at the top of current cell
            CheckDown(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);                    // To check all the cells which are at the bottom of current cell
            CheckRightBottomCorner(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);       // To check all the cells which are at the right bottom corner of current cell
            CheckRightUpperCorner(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);        // To check all the cells which are at the right upper corner of current cell
            CheckLeftUpperCorner(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);         // To check all the cells which are at the left bottom corner of current cell    
            CheckLeftBottomCorner(r, c, PatientMatrix, VisitedMatrix, ref NodeToVisit);        // To check all the cells which are at the left upper corner of current cell
        }

        //To convert the json object into 2D int array
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


        //To check all the cells which are at the left upper corner of current cell
        private static void CheckLeftUpperCorner(int row, int col, int[,] PatientMatrix, bool[,] IsVisited, ref Dictionary<string, Node> NodeToVisit)
        {

            for (int c = col - 1; c >= 0; c--)                                              // start moving left from the current cell
            {
                if (row > 0)                                                                // stopping just before the first row
                {
                    row--;                                                                  // moving to the next top row
                    if (PatientMatrix[row, c] == 1 && !IsVisited[row, c])                                 // if unvisited node found with value 1
                    {
                        Node aNode = new Node();
                        aNode.r = row;
                        aNode.c = c;
                        aNode.key = aNode.r.ToString() + aNode.c.ToString();               //creating the dictionary key using the index

                        if (NodeToVisit.ContainsKey(aNode.key) == false)                   //if not found in NodetoVisit Dictionary
                        {
                            aNode.IsVisited = false;
                            NodeToVisit.Add(aNode.key, aNode);                             //adding to dictionary to visit later.
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
