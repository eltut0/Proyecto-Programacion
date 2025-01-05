using MAZE.Map;
using MAZE.Players;
using Spectre.Console;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

class Skills
{ 
    //cuenta habilidades y activa el booleano cuando se llega al final de la cuenta
    public static void CountSkills(Player player)
    {
        //contador para player1
        if (!player.Skill && player.SCount <= 0)
        {
            player.SCount = player.Refresh;
        }
        else if (player.SCount == 1)
        {
            player.SCount--;
            player.Skill = true;
        }
        else if (!player.Skill)
        {
            player.SCount--;
        }
    }

    public static void Skill(Player player)
    {
        //troyano
        if (player.Type == "Troyano" || player.ActualType == "Troyano")
        {
            player.USkill = true;
            Interface.Interface.Writing($"{player.Token} ha activado su habilidad especial, tiene 90% de probabilidades de sobrevivir a la proxima limpieza de virus!");
            //la comprobacion se realiza cuando se activa el antivirus, donde se chequea si ek personaje es un troyano y si tiene activa la habilidad, entonces aplica la probabilidad de sobrevivir a la limpieza
        }
        else if (player.Type == "Gusano" || player.ActualType == "Gusano")
        {
            player.USkill = true;
            Interface.Interface.Writing($"{player.Token} ha activado su habilidad especial, puede atravesar una pared!");
            //la comprobacion se realiza en el momento q se comprueba el pasode casilla a casilla
        }
        else if (player.Type == "Spyware" || player.ActualType == "Spyware")
        {
            string[,] temp = new string[GenerateMaze.size, GenerateMaze.size];

            for (int i = 0; i < GenerateMaze.size; i++)
            {
                for (int j = 0; j < GenerateMaze.size; j++)
                {
                    temp[i, j] = "?";
                }
            }

            temp[player.Position.xcoordinate, player.Position.ycoordinate] = player.Token;

            //establecemos una relacion entre la posicion del jugador y la dimension del laberinto de modo q se puedan con 4 bucles
            //comenzamos definiendo los limites, en un rango de 11 unidades

            int x = player.Position.xcoordinate;
            int y = player.Position.ycoordinate;

            //limite superior en eje x
            int xsuplim;
            if (player.Position.xcoordinate + 11 > GenerateMaze.size)
            {
                xsuplim = GenerateMaze.size;
            }
            else
            {
                xsuplim = player.Position.xcoordinate + 11;
            }

            //limite inferior en eje x
            int xinflim;
            if (player.Position.xcoordinate - 11 < 0)
            {
                xinflim = 0;
            }
            else
            {
                xinflim = player.Position.xcoordinate - 11;
            }

            //limite superior en eje y
            int ysuplim;
            if (player.Position.ycoordinate + 11 > GenerateMaze.size)
            {
                ysuplim = GenerateMaze.size;
            }
            else
            {
                ysuplim = player.Position.ycoordinate + 11;
            }

            //limite inferior en eje y
            int yinflim;
            if (player.Position.ycoordinate - 11 < 0)
            {
                yinflim = 0;
            }
            else
            {
                yinflim = player.Position.xcoordinate - 11;
            }

            //ahora mediante 4 pares de bucles for se llena la matriz leyendo del truemap

            //sureste
            for (int i = x; i < xsuplim; i++)
            {
                for (int j = y; j < ysuplim; j++)
                {
                    temp[i, j] = GenerateMaze.truemap[i, j];
                }
            }

            //suroeste
            for (int i = x; i < xsuplim; i++)
            {
                for (int j = y; j > yinflim; j--)
                {
                    temp[i, j] = GenerateMaze.truemap[i, j];
                }
            }

            //noreste
            for (int i = x; i > xinflim; i--)
            {
                for (int j = y; j < ysuplim; j++)
                {
                    temp[i, j] = GenerateMaze.truemap[i, j];
                }
            }

            //noroeste
            for (int i = x; i > xinflim; i--)
            {
                for (int j = y; j > yinflim; j--)
                {
                    temp[i, j] = GenerateMaze.truemap[i, j];
                }
            }

            Console.Clear();
            Interface.Interface.Writing($"{player.Token} ha activado su habilidad, podra ver que tiene a su alrededor durante 5 segundos!");
            Interface.Interface.PrintMaze(temp, temp, player, player);
            Thread.Sleep(5000);
            Console.Clear();

            player.Skill = false;
            player.ActualType = "";
        }
        else if (player.Type == "Reboot" || player.ActualType == "Reboot")
        {
            //eliminamos la representacion grafica de los objetos del tipo archiuvo y cheackpoint
            for (int i = 0; i < GenerateMaze.size; i++)
            {
                for (int j = 0; j < GenerateMaze.size; j++)
                {
                    if (GenerateMaze.map[i, j] == "i" || GenerateMaze.map[i, j] == "O")
                    {
                        GenerateMaze.map[i, j] = "?";
                    }
                    if (GenerateMaze.truemap[i, j] == "i" || GenerateMaze.truemap[i, j] == "O")
                    {
                        GenerateMaze.truemap[i, j] = " ";
                    }
                }
            }

            //recorremos la lista de objetos asignandole una nueva posicion
            foreach (var temp in Objects.Objects.Objectslist)
            {
                do
                {
                    temp.position.xcoordinate = GenerateMaze.RandomCoordinate();
                    temp.position.ycoordinate = GenerateMaze.RandomCoordinate();

                    //condiciona q no exista una salida en esa posicion, pues estas no figuran como objetos
                    if (!(GenerateMaze.truemap[temp.position.xcoordinate, temp.position.ycoordinate] == "1" || GenerateMaze.truemap[temp.position.xcoordinate, temp.position.ycoordinate] == "2" || temp.position == player.Position))
                    {
                        var c = Objects.Objects.Objectslist.Find(c => c.position.xcoordinate == temp.position.xcoordinate && c.position.ycoordinate == temp.position.ycoordinate);

                        if (c != null)
                        {
                            if (c == temp)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                } while (true);

                //volvemos a representar los objetos q lleven representacion una vez q su posicion haya sido modificada
                if (temp.type == "Archive")
                {
                    GenerateMaze.truemap[temp.position.xcoordinate, temp.position.ycoordinate] = "i";
                    GenerateMaze.map[temp.position.xcoordinate, temp.position.ycoordinate] = "i";
                }
                else if (temp.type == "Checkpoint")
                {
                    GenerateMaze.truemap[temp.position.xcoordinate, temp.position.ycoordinate] = "O";
                    GenerateMaze.map[temp.position.xcoordinate, temp.position.ycoordinate] = "O";
                }
            }

            player.Skill = false;
            player.ActualType = "";

            Console.Clear();
            Interface.Interface.Writing($"El jugador {player.Token} ha activado su habilidad, los objetos del mapa seran redistribuidos");

            for (int i = 0; i < 10; i++)
            {
                Console.Clear();

                AnsiConsole.Markup("[green]{0}[/]", "Repartiendo");

                for (int j = 0; j < 3; j++)
                {
                    Thread.Sleep(75);
                    AnsiConsole.Markup("[green]{0}[/]", ".");
                }
            }
        }
        else if (player.Type == "Metamorfico")
        {
            Console.Clear();
            Interface.Interface.Writing($"Jugador {player.Token}, como es del tipo metamorfico, debe elegir una habilidad especial");
            Interface.Interface.WritingWOReadKey("1.Troyano, 2.Gusano, 3.Spyware, 4.Reboot");

            do
            {
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

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    player.ActualType = "Troyano";
                    Skill(player);
                    break;
                }
                else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    player.ActualType = "Gusano";
                    Skill(player);
                    break;
                }
                else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                {
                    player.ActualType = "Spyware";
                    Skill(player);
                    break;
                }
                else if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                {
                    player.ActualType = "Reboot";
                    Skill(player);
                    break;
                }
            } while (true);
        }
    }
}