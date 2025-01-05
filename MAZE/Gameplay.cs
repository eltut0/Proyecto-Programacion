using MAZE.Map;
using MAZE.Players;
using Spectre.Console;
using System.Diagnostics;

public class Gameplay
{
    //turnos hasta que se hara la limpieza de virus
    public static int VCleaning = 8;

    //booleano para abortar turno
    public static bool Stop { get; set; }

    //retorna los valores de dos dados
    public static int[] Dices()
    {
        Random random = new Random();
        int[] dices = { random.Next(1, 11), random.Next(1, 11) };
        return dices;
    }

    //desarrolla el turno de un jugador
    public static void Turn(Player player1, Player player2, int turns, int[] moves, int turnmoves, bool Change)
    {
        do
        {
            //el booleano change es usado para intercambiar los valores de los parametros entre player 1 y plaer 2, dado q el metodo se llama en main dos veces intercambiando las variables
            if (Change)
            {
                //info en pantalla
                Interface.Interface.InfoTable(player1, player2, turns, moves, turnmoves);
                MAZE.Map.GenerateMaze.ModMaze(player1.Position, GenerateMaze.map, GenerateMaze.truemap);
                Interface.Interface.PrintMaze(GenerateMaze.map, GenerateMaze.truemap, player1, player2);
            }
            else
            {
                //info en pantalla
                Interface.Interface.InfoTable(player2, player1, turns, moves, turnmoves);
                MAZE.Map.GenerateMaze.ModMaze(player1.Position, GenerateMaze.map, GenerateMaze.truemap);
                Interface.Interface.PrintMaze(GenerateMaze.map, GenerateMaze.truemap, player1, player2);
            }

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
                    //comprobacion para habilidad de gusano
                    else if (player1.Type == "Gusano" && player1.USkill && player1.Position.ycoordinate - 2 > 0 || player1.ActualType == "Gusano" && player1.USkill && player1.Position.ycoordinate - 2 > 0)
                    {
                        player1.Position.ycoordinate -= 2;
                        turnmoves--;
                        player1.Skill = false;
                        player1.USkill = false;
                        player1.ActualType = "";
                    }
                }

                else if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                {
                    if (GenerateMaze.map[player1.Position.xcoordinate, player1.Position.ycoordinate + 1] != "#")
                    {
                        player1.Position.ycoordinate += 2;
                        turnmoves--;
                    }
                    //comprobacion para habilidad de gusano
                    else if (player1.Type == "Gusano" && player1.USkill && player1.Position.ycoordinate + 2 < GenerateMaze.size || player1.ActualType == "Gusano" && player1.USkill && player1.Position.ycoordinate + 2 < GenerateMaze.size)
                    {
                        player1.Position.ycoordinate += 2;
                        turnmoves--;
                        player1.Skill = false;
                        player1.USkill = false;
                        player1.ActualType = "";
                    }
                }

                else if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
                {
                    if (GenerateMaze.map[player1.Position.xcoordinate - 1, player1.Position.ycoordinate] != "#")
                    {
                        player1.Position.xcoordinate -= 2;
                        turnmoves--;
                    }
                    //comprobacion para habilidad de gusano
                    else if (player1.Type == "Gusano" && player1.USkill && player1.Position.xcoordinate - 2 > 0 || player1.ActualType == "Gusano" && player1.USkill && player1.Position.xcoordinate - 2 > 0)
                    {
                        player1.Position.xcoordinate -= 2;
                        turnmoves--;
                        player1.Skill = false;
                        player1.USkill = false;
                        player1.ActualType = "";
                    }
                }

                else if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                {
                    if (GenerateMaze.map[player1.Position.xcoordinate + 1, player1.Position.ycoordinate] != "#")
                    {
                        player1.Position.xcoordinate += 2;
                        turnmoves--;
                    }
                    //comprobacion para habilidad de gusano
                    else if (player1.Type == "Gusano" && player1.USkill && player1.Position.xcoordinate + 2 < GenerateMaze.size || player1.ActualType == "Gusano" && player1.USkill && player1.Position.xcoordinate + 2 < GenerateMaze.size)
                    {
                        player1.Position.xcoordinate += 2;
                        turnmoves--;
                        player1.Skill = false;
                        player1.USkill = false;
                        player1.ActualType = "";
                    }
                }

                else if (key.Key == ConsoleKey.Spacebar)
                {
                    CheckBox(player1, true);
                    Interface.Interface.Writing("Su turno ha acabado");
                    Stop = false;
                    break;
                }

                else if (key.Key == ConsoleKey.G)
                {
                    if (player1.Skill)
                    {
                        Skills.Skill(player1);
                    }
                }

                CheckBox(player1, false);

                if (player1.Victory)
                {
                    break;
                }

                else if (turnmoves <= 0 || Stop)
                {
                    if (Change)
                    {
                        //termina el turno del jugador y aumenta la cantidad de turnos
                        CheckBox(player1, true);
                        turns++;
                        Console.Clear();
                        //info en pantalla
                        Interface.Interface.InfoTable(player1, player2, turns, moves, turnmoves);
                        GenerateMaze.ModMaze(player1.Position, GenerateMaze.map, GenerateMaze.truemap);
                        Interface.Interface.PrintMaze(GenerateMaze.map, GenerateMaze.truemap, player1, player2);
                        Thread.Sleep(200);
                        Interface.Interface.Writing("Su turno ha acabado");
                        Stop = false;
                        break;
                    }
                    else
                    {
                        //termina el turno del jugador y aumenta la cantidad de turnos
                        CheckBox(player1, true);
                        turns++;
                        Console.Clear();
                        //info en pantalla
                        Interface.Interface.InfoTable(player2, player1, turns, moves, turnmoves);
                        GenerateMaze.ModMaze(player1.Position, GenerateMaze.map, GenerateMaze.truemap);
                        Interface.Interface.PrintMaze(GenerateMaze.map, GenerateMaze.truemap, player1, player2);
                        Thread.Sleep(200);
                        Interface.Interface.Writing("Su turno ha acabado");
                        Stop = false;
                        break;
                    }
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

    //se usa para chequear que hay en la casilla en la cual el jugador cayo, tambien recibe un booleano para hacer la comprobacion en dependencia de si es el ultimo movimiento del turno, util para algunas trampas y la condicion de punto seguro
    //el metodo chequea directamente en la lista de objetos
    public static void CheckBox(Player player, bool LastMove /*representa si es el ultimo turno*/)
    {
        //comprueba si existe una salida
        if (GenerateMaze.map[player.Position.xcoordinate, player.Position.ycoordinate] == player.ExitChar)
        {
            player.Victory = true;
        }
        //de lo contrario hace todas las posibles comprobaciones
        else
        {
            //chequea la existencia de un objeto con las posicion del jugador
            var v = Objects.Objects.Objectslist.Find(c => c.position.xcoordinate == player.Position.xcoordinate && c.position.ycoordinate == player.Position.ycoordinate);

            if (v != null)
            {
                if (v.type == "Checkpoint" && LastMove)
                {
                    player.IsSafe = true;
                }
                else
                {
                    player.IsSafe = false;
                }
                if (v.type == "Archive")
                {
                    //condiciona q el jugador tenga menos de 5 archivos
                    if (player.Archives < 5)
                    {
                        player.Archives++;

                        //muestra la salida una vez q el jugador llegue a 5 archivos
                        if (player.Archives == 5)
                        {
                            GenerateMaze.truemap[player.Exit.xcoordinate, player.Exit.ycoordinate] = player.ExitChar;
                            GenerateMaze.map[player.Exit.xcoordinate, player.Exit.ycoordinate] = player.ExitChar;
                        }

                        GenerateMaze.truemap[v.position.xcoordinate, v.position.ycoordinate] = " ";
                        GenerateMaze.map[v.position.xcoordinate, v.position.ycoordinate] = " ";
                        Objects.Objects.Objectslist.Remove(v);
                    }
                }
                else if (v.type == "Desconnection")
                {
                    Random rnd = new Random();

                    int probability = rnd.Next(1, 5);

                    Console.Clear();
                    Interface.Interface.Writing("Has caido en una trampa del tipo desconeccion");
                    Objects.Objects.Objectslist.Remove(v);

                    if (probability == 3)
                    {
                        Interface.Interface.Writing("Corriste con suerte, no se ha activado");
                    }
                    else
                    {
                        Interface.Interface.Writing("Mala suerte, has perdido el resto de tu turno");
                        Gameplay.Stop = true;
                    }
                }
                else if (v.type == "Redistribution")
                {
                    Random rnd = new Random();

                    int probability = rnd.Next(1, 5);

                    Console.Clear();
                    Interface.Interface.Writing("Has caido en una trampa del tipo redistribucion");
                    Objects.Objects.Objectslist.Remove(v);

                    if (probability == 3)
                    {
                        Interface.Interface.Writing("Corriste con suerte, no se ha activado");
                    }
                    else
                    {
                        Interface.Interface.Writing("Mala suerte, seras transportado a un lugar aleatorio del tablero");

                        player.Position.xcoordinate = GenerateMaze.RandomCoordinate();
                        player.Position.ycoordinate = GenerateMaze.RandomCoordinate();
                    }
                }
                else if (v.type == "Formatting" && LastMove == true)
                {
                    Console.Clear();
                    Interface.Interface.Writing("Mala suerte, ha caido en una trampa del tipo formateo del sistema, sera devuelto al inicio");
                    player.Position.xcoordinate = player.Entrance.xcoordinate;
                    player.Position.ycoordinate = player.Entrance.ycoordinate;
                    Objects.Objects.Objectslist.Remove(v);
                }
            }
        }
    }

    //metodo de antivirus
    public static void Antivirus()
    {
        Console.Clear();

        Interface.Interface.Writing("La limpieza de virus comenzo");

        for (int i = 0; i < 10; i++)
        {
            Console.Clear();

            AnsiConsole.Markup("[green]{0}[/]", "Limpiando");

            for (int j = 0; j < 3; j++)
            {
                Thread.Sleep(75);
                AnsiConsole.Markup("[green]{0}[/]", ".");
            }
        }

        Console.Clear();

        foreach (var player in Player.PlayerList)
        {
            if (!player.IsSafe)
            {
                //condiciona q el personaje sea troyano
                if (player.Type == "Troyano" && player.USkill || player.ActualType == "Troyano" && player.USkill)
                {
                    //lanza el 90% de probabilidad de ser limpiado por el antivirus
                    Random rnd = new Random();
                    int probability = rnd.Next(0, 11);

                    if (probability == 5)
                    {
                        Interface.Interface.Writing("Mala suerte, a pesar de su condicion de Troyano, el antivirus ha detectado su presencia");
                        Interface.Interface.Writing($"EL jugador {player.Token} ha sido eliminado por el antivirus");
                        player.Position.xcoordinate = player.Entrance.xcoordinate;
                        player.Position.ycoordinate = player.Entrance.ycoordinate;
                        player.USkill = player.Skill = false;
                    }
                    else
                    {
                        Interface.Interface.Writing($"El jugador {player.Token} ha sobrevivido a la limpieza dada que su personaje es del tipo Troyano");
                        player.USkill = player.Skill = false;
                    }

                    player.ActualType = "";
                }
                else
                {
                    Random rnd = new Random();
                    int probability = rnd.Next(0, 11);

                    if (probability != 3 && probability != 7)
                    {
                        Interface.Interface.Writing($"EL jugador {player.Token} ha sido eliminado por el antivirus");
                        player.Position.xcoordinate = player.Entrance.xcoordinate;
                        player.Position.ycoordinate = player.Entrance.ycoordinate;
                    }
                    else
                    {
                        Interface.Interface.Writing($"EL jugador {player.Token} ha superado la limpieza de antivirus");
                    }
                }
                
            }
            else
            {
                Interface.Interface.Writing($"El jugador {player.Token} termino su turno en una casilla segura, por tanto ha superado la limpieza de virus");
            }
        }
    }
}