using MAZE.Players;
using Spectre.Console;
using System;
public class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();
        //muestra la info inicial(definir en su respectiva clase)
        Interface.Interface.Tittle();
        Interface.Interface.BeginnerInfo();

        //genera el mapa y genera los personajes
        string[,] map = MAZE.Map.MapCreation.MapCreate(MAZE.Map.GenerateMaze.GeneratingMaze());
        //laberinto q sera mostrado en pantalla totlmente bloqueado
        string[,] truemap = MAZE.Map.GenerateMaze.Maze();
        MAZE.Players.Characters.Createcharacters();
        MAZE.Players.Player.CreatePlayer();

        var player1 = Player.PlayerList.FirstOrDefault();
        var player2 = Player.PlayerList.Skip(1).FirstOrDefault();

        //bucle principal del juego
        do
        {
            //entero q representa la cantidad de turnos completos
            int turns = 0;
            //turno jugador 1
            Interface.Interface.Writing("Jugador 1, lance los dados");
            int[] moves = Gameplay.Dices();

            //buclea del turno de jugador 1
            do
            {
                Interface.Interface.InfoTable(player1, player2, turns, moves);  //arreglar aqui, 20 es un ejemplo oka
                MAZE.Map.GenerateMaze.ModMaze(player1.Position, truemap, map);
                Interface.Interface.PrintMaze(truemap, player1, player2);

                int turnmoves = Convert.ToInt32((moves[0] - moves[1]) * player1.Speed);

                if (turnmoves == 0)
                {
                    Interface.Interface.Writing("Lamentablemente sus dados dieron un doble, no podra moverse este turno.");
                }
                else
                {
                    //limpiar la cola de caracteres para el readkey q importa
                    do
                    {
                        if (Console.KeyAvailable)
                        {
                            Console.ReadKey();
                        }
                        else
                        {
                            break;
                        }
                    } while (true);

                    //readkey para movimiento
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
                    {
                        if (map[player1.Position.xcoordinate, player1.Position.ycoordinate - 1] != "#")
                        {
                            player1.Position.ycoordinate -= 2;
                            turnmoves--;
                        }
                    }

                    else if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                    {
                        if (map[player1.Position.xcoordinate, player1.Position.ycoordinate + 1] != "#")
                        {
                            player1.Position.ycoordinate += 2;
                            turnmoves--;
                        }
                    }

                    else if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
                    {
                        if (map[player1.Position.xcoordinate - 1, player1.Position.ycoordinate] != "#")
                        {
                            player1.Position.xcoordinate -= 2;
                            turnmoves--;
                        }
                    }

                    else if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                    {
                        if (map[player1.Position.xcoordinate + 1, player1.Position.ycoordinate] != "#")
                        {
                            player1.Position.xcoordinate += 2;
                            turnmoves--;
                        }
                    }

                    if (turnmoves == 0)
                    {
                        turns++;
                        break;
                    }
                    //limpia para el siguiente fotograma
                    Console.Clear();
                }

                //limpia para el siguiente fotograma
                Console.Clear();

            } while (true);
        } while (true);
    }
}