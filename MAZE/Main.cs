using Spectre.Console;
using System;
public class Program
{
    public static void Main(string[] args)
    {
        //muestra la info inicial(definir en su respectiva clase)
        Interface.Tittle();
        Interface.BeginnerInfo();

        
        //genera el mapa
        string[,] maze = GenerateMaze.GeneratingMaze();
        string[,] map = MapCreation.MapCreate(maze);


    }
}