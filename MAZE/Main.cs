using System;
public class Program
{
    public static void Main(string[] args)
    {
        //muestra la info inicial(definir en su respectiva clase)
        Interface.Tittle();
        Interface.BeginnerInfo();
        
        char[,] maze = GenerateMaze.GeneratingMaze();

        Console.Clear();

        for (int i = 0; i < 31; i++)
        {
            for (int j = 0; j < 31; j++)
            {
                Console.Write(maze[i, j]);
                Console.Write(' ');
            }
            Console.WriteLine();
        }
    }
}