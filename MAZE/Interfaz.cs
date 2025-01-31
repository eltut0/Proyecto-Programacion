using MAZE.Map;
using MAZE.Players;
using Spectre.Console;
using System.Globalization;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace Interface
{
    public class Interface
    {
        //primero que se corre en el programa
        public static void Tittle()
        {
            string titulo = "Infect!";
            Console.WriteLine(titulo);
            Thread.Sleep(5000);
            Console.Clear();
        }

        //muestra infomracion importante para el jugador y se corre al principio
        public static void BeginnerInfo()
        {
            string info0 = "Puedes saltar la iteracion del texto pulsando Barra Espaciadora.";
            string info1 = "Eres un virus en los 70's y tu objetivo es intentar infectar un ordenador primero que el contrario.";
            string info2 = "Para ello, los jugadores se moveran por turnos, lanzando dados. El personaje podra moverse, como maximo, la cantidad de la diferencia entre ambos dados, multiplicada por la velocidad de su personaje.";
            string info3 = "El tablero comenzara totalmente bloqueado, y segun avancen, las casillas adyacentes se iran desbloqueando.";
            string info4 = "El objetivo principal, sera recolectar 5 archivos (i) de los esparcidos por el tablero, y una vez haya recogido dicha cantidad, se generara una salida aleatoria en el tablero.";
            string info5 = "La salida que se genere sera exclusivamente para el jugador que logro recolectar los archivos, y una vez que la alcance, el jugador gana la partida. Para el Jugador 1 es un 1 en rojo, y para el Jugador 2 es un 2 en rojo.";
            string info6 = "Cada 8 turnos, se hara una limpieza de virus en el tablero, y los jugadores deberan estar posicionados en casillas seguras (O), o tienen un 80% de probabilidad de ser devueltos al principio del tablero.";
            string info7 = "Las trampas esparcidas en el tablero no se pueden ver, algunas se activan cuando pasas sobre ellas, y otras solo cuando el personaje descansa sobre una.";
            string info8 = "La trampa Desconexion se activa cuando se pasa sobre ella, con una probabilidad del 75%, y hace al jugador perder el resto del turno, una vez que se cae en ella se desactiva para siempre.";
            string info9 = "La trampa Formateo del sistema, se activa cuando el jugador termina su turno y esta sobre una, devuelve al principio del tablero al jugador.";
            string info10 = "La trampa Redistribucion, se activa al pasar sobre ella y envia al jugador a una posicion aleatoria del tablero. Con una probabilidad del 75%, se desactiva despues de caer sobre ella.";
            string info11 = "La habilidad especial se activa con la tecla G una vez que el tiempo de refresco haya llegado al limite";
            string info12 = "Siempre puede pulsar Esc en la partida para acceder al menu de pausa";
            string info13 = "Puedes terminar tu turno en todo momento pulsando barra espaciadora";
            string info14 = "Para desplazarte tanto en los menus como en el juego, se pueden usar tanto las flechas, como las teclas w,a,s,d";
            string info15 = "De momento te bastara con esto para comenzar, buena suerte infectando!";
            //annado los textos del tutorial y la info del juego a un arreglo y voy escribiendolas una por una
            string[] strings = { info0, info1, info2, info3, info4, info5, info6, info7, info8, info9, info10, info11, info12, info13, info14, info15 };

            foreach (string info in strings)
            {
                Writing(info);
            }
        }
         
        //escribe con un retraso pa que quede tiza
        public static void Writing(string info)
        {
            Console.Clear();
            Usefulmethods.CleanQueue();

            for (int j = 0; j < info.Length; j++)
            {
                AnsiConsole.Markup("[green]{0}[/]", info[j]);
                if (Console.KeyAvailable)
                { 
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        Console.Clear();
                        AnsiConsole.Markup("[green]{0}[/]", info);
                        break;
                    }
                }
                Thread.Sleep(25);
            }
            Console.Clear();

            do
            {
                Usefulmethods.CleanQueue();
                do
                {
                    RegularMarkup(info);
                    RegularMarkup("Continuar");
                    Thread.Sleep(150);
                    Console.Clear();

                    if (Console.KeyAvailable)
                    {
                        break;
                    }

                    RegularMarkup(info);
                    MarkupWhiteBack("Continuar");
                    Thread.Sleep(150);
                    Console.Clear();

                    if (Console.KeyAvailable)
                    {
                        break;
                    }
                } while (true);

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (true);

            Console.Clear();
        }

        //hace lo mismo q el anterior pero pincha si hay q meterle input
        public static void WritingWOReadKey(string info)
        {
            do
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                else
                {
                    break;
                }
            } while (true);

            for (int j = 0; j < info.Length; j++)
            {
                AnsiConsole.Markup("[green]{0}[/]", info[j]);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        Console.Clear() ;
                        AnsiConsole.Markup("[green]{0}[/]", info);
                        break;
                    }
                }
                Thread.Sleep(25);

            }
            Console.WriteLine();

            do
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                else
                {
                    break;
                }
            } while (true);
        }

        //genera un fotograma de la matriz sin retraso
        public static void PrintMaze(string[,] maze, string[,] truemap, Player player1, Player player2)
        {
            maze[player1.Position!.xcoordinate, player1.Position.ycoordinate] = player1.Token!;
            maze[player2.Position!.xcoordinate, player2.Position.ycoordinate] = player2.Token!;

            for (int i = 0; i < GenerateMaze.size; i++)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                for (int j = 0; j < GenerateMaze.size; j++)
                {
                    //separa los objetos variables para ponerlos en string aparte
                    if (maze[i,j] == "#" || maze[i,j] == " "|| maze[i,j] == "?")
                    {
                        sb.Append(maze[i, j]);
                        sb.Append(' ');
                    }
                    else
                    {
                        //si aparece algo distinto imprime lo q ya tiene en verde y comrpueba q es lo otro para imoprimirlo en otro color
                        AnsiConsole.Markup("[bold green]{0}[/]", sb.ToString());

                        if (maze[i,j] == "i")
                        {
                            AnsiConsole.Markup("[bold yellow]{0}[/]", "i ");
                        }
                        else if (maze[i,j] == "O")
                        {
                            AnsiConsole.Markup("[bold blue]{0}[/]", "O ");
                        }
                        else if (maze[i,j] == "1")
                        {
                            AnsiConsole.Markup("[bold red]{0}[/]", "1 ");
                        }
                        else if (maze[i, j] == "2")
                        {
                            AnsiConsole.Markup("[bold red]{0}[/]", "2 ");
                        }
                        else if (maze[i,j] == player1.Token && GenerateMaze.truemap![i,j] == "O")
                        {
                            AnsiConsole.Markup("[bold blue]{0}[/]", $"{player1.Token} ");
                        }
                        else if (maze[i, j] == player2.Token && GenerateMaze.truemap![i, j] == "O")
                        {
                            AnsiConsole.Markup("[bold blue]{0}[/]", $"{player2.Token} ");
                        }
                        else
                        {
                            Console.Write($"{maze[i, j]} ");
                        }

                        //vaciar el string builder
                        sb.Clear();
                    }
                }
                AnsiConsole.Markup("[green]{0}[/]", sb.ToString());
                Console.WriteLine();
            }

            maze[player1.Position.xcoordinate, player1.Position.ycoordinate] = truemap[player1.Position.xcoordinate, player1.Position.ycoordinate];
            maze[player2.Position.xcoordinate, player2.Position.ycoordinate] = truemap[player2.Position.xcoordinate, player2.Position.ycoordinate];
        }

        //tabla de info
        public static void InfoTable(Player player1, Player player2, int turno, int[] dices, int moves, bool change)
        {
            Table table = new Table();

            table.AddColumn(" ");
            table.AddColumn("Jugador 1");
            table.AddColumn("Jugador 2");
            table.AddColumn("Turnos hasta limpieza de virus");
            table.AddColumns("Dados");
            table.AddColumns("Movimientos restantes");
            table.AddColumns("Turno actual");
            table.AddRow("Tipo de virus", player1.Type!, player2.Type!);

            if (change) 
            {
                table.AddRow("Archivos recogidos", Convert.ToString(player1.Archives), Convert.ToString(player2.Archives), Convert.ToString(Gameplay.VCleaning - (turno % Gameplay.VCleaning)), $"{dices[0]}, {dices[1]}", Convert.ToString(moves), player1.PlayerN + ": " + player1.Token);
            }
            else
            {
                table.AddRow("Archivos recogidos", Convert.ToString(player1.Archives), Convert.ToString(player2.Archives), Convert.ToString(Gameplay.VCleaning - (turno % Gameplay.VCleaning)), $"{dices[0]}, {dices[1]}", Convert.ToString(moves), player2.PlayerN + ": " + player2.Token);
            }

            if (!player1.Skill && !player2.Skill)
            {
                table.AddRow("Turnos hasta habilidad", Convert.ToString(player1.SCount), Convert.ToString(player2.SCount));
            }
            else if (player1.Skill && !player2.Skill)
            {
                table.AddRow("Turnos hasta habilidad", "Activo", Convert.ToString(player2.SCount));
            }
            else if (!player1.Skill && player2.Skill)
            {
                table.AddRow("Turnos hasta habilidad", Convert.ToString(player1.SCount), "Activo");
            }
            else
            {
                table.AddRow("Turnos hasta habilidad", "Activo", "Activo");
            }

            AnsiConsole.Write(table);
        }

        //imprimir texto para menu
        public static void RegularMarkup(string text)
        {
            AnsiConsole.MarkupLine("[green]{0}[/]", text);
        }
        //imprimir exto para menu con trasero en blanco
        public static void MarkupWhiteBack(string text)
        {
            AnsiConsole.MarkupLine($"[black on green]{text}[/]");
        }
    }
}