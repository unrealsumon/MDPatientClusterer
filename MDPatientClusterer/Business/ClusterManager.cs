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



        public class Node
        {
            public string key;                                                  //concatenation of row and column.
            public bool IsVisited;
            public int r;                                                       //row
            public int c;                                                       //column
        }
    }
}
