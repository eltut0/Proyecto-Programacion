using Spectre.Console;
using System;
public class Program
{
    public static void Main(string[] args)
    {
        //muestra la info inicial(definir en su respectiva clase)
        Interface.Tittle();
        Interface.BeginnerInfo();
        
        //genera el mapa y genera los personajes
        string[,] map = MapCreation.MapCreate();
        Characters.Createcharacters();
        Player.CreatePlayer();
    }
}