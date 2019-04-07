using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsolApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[5, 5];
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrix[i, j] = random.Next(0, 2);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(matrix[i, j] + "  ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(matrix[i, j] + "  ");
                    }
                }

                Console.WriteLine(Environment.NewLine);


            }

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
                         
                            for (int i = 0; i < NodeToVisit.Count(); i++)
                            {
                                if (NodeToVisit[i].IsVisited == false)
                                {

                                    
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
