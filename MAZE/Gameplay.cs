using MAZE.Map;
using MAZE.Players;

public class Gameplay
{
    //turnos hasta que se hara la limpieza de virus
    public static int VCleaning = 8;
    //retorna los valores de dos dados
    public static int[] Dices()
    {
        Random random = new Random();
        int[] dices = { random.Next(1, 11), random.Next(1, 11) };
        return dices;
    }

    //desarrolla el turno de un jugador
    public static void Turn(Player player1, Player player2, int turns, int[] moves, int turnmoves)
    {
        do
        {
            //info en pantalla
            Interface.Interface.InfoTable(player1, player2, turns, moves, turnmoves);
            MAZE.Map.GenerateMaze.ModMaze(player1.Position, GenerateMaze.map, GenerateMaze.truemap);
            Interface.Interface.PrintMaze(GenerateMaze.map, GenerateMaze.truemap, player1, player2);

            if (turnmoves == 0)
            {
                Console.Clear();
                Interface.Interface.Writing("Lamentablemente sus dados dieron un doble, no podra moverse este turno.");
                break;
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
                    if (GenerateMaze.map[player1.Position.xcoordinate, player1.Position.ycoordinate - 1] != "#")
                    {
                        player1.Position.ycoordinate -= 2;
                        turnmoves--;
                    }
                }

                else if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                {
                    if (GenerateMaze.map[player1.Position.xcoordinate, player1.Position.ycoordinate + 1] != "#")
                    {
                        player1.Position.ycoordinate += 2;
                        turnmoves--;
                    }
                }

                else if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
                {
                    if (GenerateMaze.map[player1.Position.xcoordinate - 1, player1.Position.ycoordinate] != "#")
                    {
                        player1.Position.xcoordinate -= 2;
                        turnmoves--;
                    }
                }

                else if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                {
                    if (GenerateMaze.map[player1.Position.xcoordinate + 1, player1.Position.ycoordinate] != "#")
                    {
                        player1.Position.xcoordinate += 2;
                        turnmoves--;
                    }
                }

                else if (key.Key == ConsoleKey.Spacebar)
                {
                    Interface.Interface.Writing("Su turno ha acabado");
                    break;
                }

                if (turnmoves <= 0)
                {
                    //termina el turno del jugador y aumenta la cantidad de turnos
                    turns++;
                    Console.Clear();
                    //info en pantalla
                    Interface.Interface.InfoTable(player1, player2, turns, moves, turnmoves);  //arreglar aqui, 20 es un ejemplo oka
                    MAZE.Map.GenerateMaze.ModMaze(player1.Position, GenerateMaze.map, GenerateMaze.truemap);
                    Interface.Interface.PrintMaze(GenerateMaze.map, GenerateMaze.truemap, player1, player2);
                    Thread.Sleep(200);
                    Interface.Interface.Writing("Su turno ha acabado");
                    break;
                }
                else
                {
                    //limpia para el siguiente fotograma
                    Console.Clear();
                }
            }
        } while (true);
        Console.Clear();
    }
}