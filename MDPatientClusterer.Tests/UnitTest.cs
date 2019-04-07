using MDPatientClusterer.Business;
using NUnit.Framework;
using System;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
      
        public void TestMatrix()
        {
            Random random = new Random();
            int[,] matrix1 = new int[8, 8] { { 0, 1, 1, 1, 1, 0, 1, 1 }, { 1, 1, 0, 1, 1, 0, 0, 1 }, { 0, 1, 1, 1, 0, 1, 1, 1 }, { 1, 0, 1, 1, 0, 1, 1, 0 }, { 0, 1, 1, 0, 0, 0, 0, 1 }, { 1, 0, 0, 1, 0, 0, 1, 0 }, { 0, 0, 0, 0, 1, 1, 1, 1 }, { 1, 1, 1, 0, 0, 1, 1, 0 } };
            int[,] matrix2 = new int[4, 4] { { 1, 1, 0, 0 }, { 1, 1, 0, 1 }, { 0, 0, 1, 1 }, { 1, 1, 0, 1 } };

            int[,] matrix3Rand = new int[10, 10];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrix3Rand[i, j] = random.Next(0, 2);
                }
            }
            ClusterManager manager = new ClusterManager();
            //int clusters=manager.GetClusters(matrix3Rand);  //for testing
           
        }
    }
}