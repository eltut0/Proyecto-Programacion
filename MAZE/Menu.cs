using Spectre.Console;

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

        //bucle principal del menu
        do
        {
            //bucle de muestreo de informacion y lectura de teclas
            do
            {
                //limpieza de cola de teclas
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
                    break;
                }
                else if (choice == 1)
                {
                    //llama al sistema de carga de partidas
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
    public static void Pause()
    {
        Console.Clear();

        string MMenu1 = "1. Continuar";
        string MMenu2 = "2. Guardar partida";
        string MMenu3 = "3. Salir al Menu Principal";
        string MMenu4 = "4. Salir del juego";

        int choice = 0;

        //bucle principal del menu
        do
        {
            //bucle de muestreo de informacion y lectura de teclas
            do
            {
                //limpieza de cola de teclas
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
                }
                else if (choice == 2)
                {
                    //rompe la partida
                    Gameplay.Stop = true;
                    Program.Break = true;
                    break;
                }
                else if (choice == 3)
                {
                    //rompre todo el programa
                    Environment.Exit(0);
                }
            }

        } while (true);
    }
}

