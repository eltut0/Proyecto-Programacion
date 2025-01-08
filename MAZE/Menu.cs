using MAZE.Map;
using MAZE.Players;
using Spectre.Console;
using System.Numerics;

class Menu
{ 
    //menu principal
    public static void MainMenu()
    {
        string MMenu1 = "1. Nuevo juego";
        string MMenu2 = "2. Cargar partida";
        string MMenu3 = "3. Tutorial";
        string MMenu4 = "4. Salir";

        int choice = 0;

        Interface.Interface.WritingWOReadKey("Menu Principal");
        Console.Clear();

        //bucle principal del menu
        do
        {
            //bucle de muestreo de informacion y lectura de teclas
            do
            {
                //limpieza de cola de teclas
                Usefulmethods.CleanQueue();

                AnsiConsole.MarkupLine("[green]{0}[/]", "Menu Principal");

                if (choice == 0)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu1);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu1);
                }

                if (choice == 1)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu2);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu2);
                }

                if (choice == 2)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu3);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu3);
                }

                if (choice == 3)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu4);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu4);
                }

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

                AnsiConsole.MarkupLine("[green]{0}[/]", "Menu Principal");

                Interface.Interface.RegularMarkup(MMenu1);
                Interface.Interface.RegularMarkup(MMenu2);
                Interface.Interface.RegularMarkup(MMenu3);
                Interface.Interface.RegularMarkup(MMenu4);

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

            } while (true);

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
            {
                if (choice > 0)
                {
                    choice--;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
            {
                if (choice < 3)
                {
                    choice++;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (choice == 0)
                {
                    //rompe e inicia el juego
                    Program.NewGame = true;
                    break;
                }
                else if (choice == 1)
                {
                    //rompe y salta a la carga de partida en caso de que haya un txt
                    if (File.Exists(SaveGame.route))
                    {
                        Program.NewGame = false;
                        break;
                    }
                    else
                    {
                        Interface.Interface.Writing("No hay ninguna partida guardada");
                    }
                }
                else if (choice == 2)
                {
                    Interface.Interface.BeginnerInfo();
                }
                else if (choice == 3)
                {
                    Environment.Exit(0);
                }
            }

        } while (true);
    }

    //menu de pausa
    public static void Pause(bool change)
    {
        Console.Clear();

        string MMenu1 = "1. Continuar";
        string MMenu2 = "2. Guardar partida";
        string MMenu3 = "3. Salir al Menu Principal";
        string MMenu4 = "4. Salir del juego";

        int choice = 0;

        Interface.Interface.WritingWOReadKey("Juego Pausado");
        Console.Clear();

        //bucle principal del menu
        do
        {
            //bucle de muestreo de informacion y lectura de teclas
            do
            {
                //limpieza de cola de teclas
                Usefulmethods.CleanQueue();

                AnsiConsole.MarkupLine("[green]{0}[/]", "Juego Pausado");

                if (choice == 0)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu1);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu1);
                }

                if (choice == 1)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu2);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu2);
                }

                if (choice == 2)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu3);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu3);
                }

                if (choice == 3)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu4);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu4);
                }

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

                AnsiConsole.MarkupLine("[green]{0}[/]", "Juego Pausado");

                Interface.Interface.RegularMarkup(MMenu1);
                Interface.Interface.RegularMarkup(MMenu2);
                Interface.Interface.RegularMarkup(MMenu3);
                Interface.Interface.RegularMarkup(MMenu4);

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

            } while (true);

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
            {
                if (choice > 0)
                {
                    choice--;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
            {
                if (choice < 3)
                {
                    choice++;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (choice == 0)
                {
                    //rompe y continua el juego
                    break;
                }
                else if (choice == 1)
                {
                    //llama al metodo de guardado de partidas
                    SaveGame.SavingGame(change);
                }
                else if (choice == 2)
                {
                    //rompe la partida

                    bool choice2 = BooleanMenu("Esta seguro de que quiere salir? Se perderan los datos no guardados");

                    if (choice2)
                    {
                        Gameplay.Leaving = true;
                        Program.Break = true;
                        break;
                    }
                }
                else if (choice == 3)
                {
                    //rompre todo el programa

                    bool choice2 = BooleanMenu("Esta seguro de que quiere salir? Se perderan los datos no guardados");

                    if (choice2)
                    {
                        Environment.Exit(0);
                    }
                }
            }

        } while (true);
    }

    //menu de selecci5on de dificultad
    public static void SelectD()
    {
        string MMenu1 = "1. Facil";
        string MMenu2 = "2. Medio";
        string MMenu3 = "3. Dificil";
        string MMenu4 = "4. Ayuda";

        int choice = 0;

        Interface.Interface.WritingWOReadKey("Seleccione una dificultad");
        Console.Clear();

        //bucle principal del menu
        do
        {
            //bucle de muestreo de informacion y lectura de teclas
            do
            {
                //limpieza de cola de teclas
                Usefulmethods.CleanQueue();

                AnsiConsole.MarkupLine("[green]{0}[/]", "Seleccione una dificultad");

                if (choice == 0)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu1);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu1);
                }

                if (choice == 1)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu2);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu2);
                }

                if (choice == 2)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu3);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu3);
                }

                if (choice == 3)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu4);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu4);
                }

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

                AnsiConsole.MarkupLine("[green]{0}[/]", "Seleccione una dificultad");

                Interface.Interface.RegularMarkup(MMenu1);
                Interface.Interface.RegularMarkup(MMenu2);
                Interface.Interface.RegularMarkup(MMenu3);
                Interface.Interface.RegularMarkup(MMenu4);

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

            } while (true);

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
            {
                if (choice > 0)
                {
                    choice--;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
            {
                if (choice < 3)
                {
                    choice++;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Random rnd = new Random();
                int size;

                if (choice == 0)
                {
                    do
                    {
                        size = rnd.Next(20, 31);

                        if (size % 2 == 1)
                        {
                            GenerateMaze.size = size;
                            break;
                        }
                    } while (true);
                    break;
                }
                else if (choice == 1)
                {
                    do
                    {
                        size = rnd.Next(30, 41);

                        if (size % 2 == 1)
                        {
                            GenerateMaze.size = size;
                            break;
                        }

                    } while (true);
                    break;
                }
                else if (choice == 2)
                {
                    do
                    {
                        size = rnd.Next(40, 51);

                        if (size % 2 == 1)
                        {
                            GenerateMaze.size = size;
                            break;
                        }
                    } while (true);
                    break;
                }
                else if (choice == 3)
                {
                    Interface.Interface.Writing("La dificultad solo modifica la dimension del laberinto de la partida, cada dificultad tiene un rango de valores");
                    Interface.Interface.Writing("Una vez que usted selecciona una dificultad, se genera una dimension aleatoria en el rango de la dificultad que selecciono");
                }
            }

        } while (true);
    }

    //menu de seleccion de pesonajes
    public static void SelectCh(Player player, int ID)
    {

        string MMenu1 = "1. Troyano";
        string MMenu2 = "2. Gusano";
        string MMenu3 = "3. Spyware";
        string MMenu4 = "4. Reboot";
        string MMenu5 = "5. Metamorfico";
        string MMenu6 = "6. Ayuda";

        int choice = 0;

        Interface.Interface.WritingWOReadKey($"Jugador {ID}, seleccione un personaje");
        Console.Clear();

        //bucle principal del menu
        do
        {
            //bucle de muestreo de informacion y lectura de teclas
            do
            {
                //limpieza de cola de teclas
                Usefulmethods.CleanQueue();

                AnsiConsole.MarkupLine("[green]{0}[/]", $"Jugador {ID}, seleccione un personaje");

                if (choice == 0)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu1);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu1);
                }

                if (choice == 1)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu2);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu2);
                }

                if (choice == 2)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu3);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu3);
                }

                if (choice == 3)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu4);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu4);
                }

                if (choice == 4)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu5);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu5);
                }

                if (choice == 5)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu6);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu6);
                }

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

                AnsiConsole.MarkupLine("[green]{0}[/]", $"Jugador {ID}, seleccione un personaje");

                Interface.Interface.RegularMarkup(MMenu1);
                Interface.Interface.RegularMarkup(MMenu2);
                Interface.Interface.RegularMarkup(MMenu3);
                Interface.Interface.RegularMarkup(MMenu4);
                Interface.Interface.RegularMarkup(MMenu5);
                Interface.Interface.RegularMarkup(MMenu6);

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

            } while (true);

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
            {
                if (choice > 0)
                {
                    choice--;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
            {
                if (choice < 5)
                {
                    choice++;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (choice == 0)
                {
                    player = Player.PlayerTypeSelect("Troyano", player);
                }
                else if (choice == 1)
                {
                    player = Player.PlayerTypeSelect("Gusano", player);
                }
                else if (choice == 2)
                {
                    player = Player.PlayerTypeSelect("Spyware", player);
                }
                else if (choice == 3)
                {
                    player = Player.PlayerTypeSelect("Reboot", player);
                }
                else if (choice == 4)
                {
                    player = Player.PlayerTypeSelect("Metamorfico", player);
                }
                else if (choice == 5)
                {
                    Interface.Interface.Writing("Seleccione un personaje para ver su informacion, ahi podra decidir si usarlo o elegir otro");
                    Interface.Interface.Writing("Al final de la creacion de su jugador, se le volvera a preguntar si desea mantener el jugador creado");
                }
            }

        } while (player.Type == null);
    }

    //menu booleano de seleccion
    public static bool BooleanMenu(string info)
    {
        string MMenu1 = "1. Aceptar";
        string MMenu2 = "2. Cancelar";

        int choice = 0;

        //bucle principal del menu
        do
        {
            //bucle de muestreo de informacion y lectura de teclas
            do
            {
                //limpieza de cola de teclas
                Usefulmethods.CleanQueue();

                AnsiConsole.MarkupLine("[green]{0}[/]", info);

                if (choice == 0)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu1);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu1);
                }

                if (choice == 1)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu2);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu2);
                }

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

                AnsiConsole.MarkupLine("[green]{0}[/]", info);

                Interface.Interface.RegularMarkup(MMenu1);
                Interface.Interface.RegularMarkup(MMenu2);

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

            } while (true);

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
            {
                if (choice > 0)
                {
                    choice--;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
            {
                if (choice < 1)
                {
                    choice++;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (choice == 0)
                {
                    return true;
                }
                else if (choice == 1)
                {
                    return false;
                }
            }

        } while (true);
    }

    //menu oara la seleccion de una habilidad de un metamorfico, devuelve true si se cancela la asignacion
    public static void MetamorficChMenu(Player player)
    {
        string MMenu1 = "1. Troyano";
        string MMenu2 = "2. Gusano";
        string MMenu3 = "3. Spyware";
        string MMenu4 = "4. Reboot";
        string MMenu5 = "5. Cancelar";

        int choice = 0;

        Interface.Interface.WritingWOReadKey("Seleccione una habilidad especial");
        Console.Clear();

        //bucle principal del menu
        do
        {
            //bucle de muestreo de informacion y lectura de teclas
            do
            {
                //limpieza de cola de teclas
                Usefulmethods.CleanQueue();

                AnsiConsole.MarkupLine("[green]{0}[/]", "Seleccione una habilidad especial");

                if (choice == 0)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu1);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu1);
                }

                if (choice == 1)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu2);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu2);
                }

                if (choice == 2)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu3);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu3);
                }

                if (choice == 3)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu4);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu4);
                }

                if(choice == 4)
                {
                    Interface.Interface.MarkupWhiteBack(MMenu5);
                }
                else
                {
                    Interface.Interface.RegularMarkup(MMenu5);
                }

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

                AnsiConsole.MarkupLine("[green]{0}[/]", "Seleccione una habilidad especial");

                Interface.Interface.RegularMarkup(MMenu1);
                Interface.Interface.RegularMarkup(MMenu2);
                Interface.Interface.RegularMarkup(MMenu3);
                Interface.Interface.RegularMarkup(MMenu4);
                Interface.Interface.RegularMarkup(MMenu5);

                Thread.Sleep(150);
                Console.Clear();

                //rompe el bucle si
                if (Console.KeyAvailable)
                {
                    break;
                }

            } while (true);

            ConsoleKeyInfo key = Console.ReadKey(true);

            Random rnd = new Random();
            int probability = rnd.Next(1, 6);

            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
            {
                if (choice > 0)
                {
                    choice--;
                }
            }
            else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
            {
                if (choice < 4)
                {
                    choice++;
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (choice == 0)
                {
                    if (probability != 3)
                    {
                        player.ActualType = "Troyano";
                        Skills.Skill(player);
                        break;
                    }
                }
                else if (choice == 1)
                {
                    if (probability != 3)
                    {
                        player.ActualType = "Gusano";
                        Skills.Skill(player);
                        break;
                    }
                }
                else if (choice == 2)
                {
                    if (probability != 3)
                    {
                        player.ActualType = "Spyware";
                        Skills.Skill(player);
                        break;
                    }
                }
                else if (choice == 3)
                {
                    if (probability != 3)
                    {
                        player.ActualType = "Reboot";
                        Skills.Skill(player);
                        break;
                    }
                }
                else if (choice == 4)
                {
                    break;
                }

                //de lo contrario imprime fallo
                Interface.Interface.Writing("Ha fallado la asignacion de una habilidad especial");
                player.Skill = false;
                break;
            }

        } while (true);
    }
}

