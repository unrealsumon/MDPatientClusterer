using System;

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
    }
}
