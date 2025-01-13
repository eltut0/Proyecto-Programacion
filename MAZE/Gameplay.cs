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

    //booleano para salir al menu sin q salga "su turno ha terminado"
    public static bool Leaving { get; set; }

    //estructura de partida normal pvp
    public static void NormalPvPMatch()
    {
        //recopila la informacion del principio solo si es un juego nuevo, de lo contrario se lee la info del txt y salta directamente al comienzo
        if (Program.NewGame)
        {

            //pide el ingreso de la dimension del laberinto
            Menu.SelectD();

            //llama al metodo de creacion de mapas tras obtener una dimension valida
            GenerateMaze.Start();

            //almacenar personajes en lista para dar lugar a la creacion de los jugadores
            Characters.Createcharacters();
            Player.CreatePlayer(2);

            Program.Turns = 0;

            //ni idea pero asi funciona
            Program.Jump = true;
        }

        else
        {
            //realiza una lectura del txt y asigna los respectivos valores a las variables llamando al metodo
            SaveGame.LoadGame();
            Program.LoadedGame = true;
        }

        //partida normal entre dos jugadores humanos
        NormalPlay();
    }

    //partida con IA
    public static void IAMatch()
    {
        //selecciona una dificultad
        Menu.SelectD();

        //creacion de mapas
        GenerateMaze.Start();

        //creacion de personajes
        Characters.Createcharacters();
        //con el argumento 1 porque solo se crea un jugador de este modo, el otro es mediante el metodo de la clase AI
        Player.CreatePlayer(1);

        //creacion del personaje del tipo AI
        ArtificialIntelligence.AIPlayerCreator();

        //llama a la prtida normal
        IAPlay();
    }

    //partida entre jugadores fisicos
    public static void NormalPlay()
    {
        //lee de las lista, dado q del metodo de carga de juego tambien se carga directo a la lista
        Program.player1 = Player.PlayerList.FirstOrDefault()!;
        Program.player2 = Player.PlayerList.Skip(1).FirstOrDefault()!;

        //bucle principal del juego
        do
        {

            if (Program.Jump)
            {
                //salta la nueva asignacion de valores de movimientos en caso de q se haya cargado una partida
                if (!Program.LoadedGame)
                {
                    //turno jugador 1 waaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    Interface.Interface.Writing("Jugador 1, lance los dados");

                    Moves();
                }

                //reasigna el false a loaded game para q no se salte futuras condiciones
                Program.LoadedGame = false;

                //buclea del turno de jugador 1
                Turn(Program.player1, Program.player2, true);

                //valora el posible cierre de la partida
                if (Program.Break || Program.player1.Victory)
                {
                    break;
                }
            }

            //le asigna false a jump por si las dudas

            Program.Jump = true;

            //turno jugador 2 waaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

            if (!Program.LoadedGame)
            {
                Interface.Interface.Writing("Jugador 2, lance los dados");

                Moves();
            }

            //reasigna el false a loaded game para q no se salte futuras condiciones
            Program.LoadedGame = false;

            //buclea del turno de jugador 2 intercambiando los parametros con respecto a la primera llamada para q el metodo interprete al segundo jugador como el personable jugable

            Turn(Program.player2, Program.player1, false);

            //valora el posible cierre de la partida

            if (Program.Break || Program.player2.Victory)
            {
                break;
            }

            //modificacion de contadores
            Program.Turns++;

            //llama al antivirus
            if (Program.Turns % Gameplay.VCleaning == 0)
            {
                Antivirus();
            }

            //actualizacion de contadores de las habilidades de cada jugador
            Skills.CountSkills(Program.player1);
            Skills.CountSkills(Program.player2);

        } while (true);
    }

    //partida entre jugador e IA
    public static void IAPlay()
    {
        //carga los jugadores al inicio de la partida
        Program.player1 = Player.PlayerList.FirstOrDefault()!;
        Program.player2 = Player.PlayerList.Skip(1).FirstOrDefault()!;

        //bucle pricnipal de la partida
        do
        {
            //turno jugador 1 waaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
            Interface.Interface.Writing("Jugador 1, lance los dados");

            Moves();

            //buclea del turno de jugador 1
            Turn(Program.player1, Program.player2, true);

            //valora el posible cierre de la partida
            if (Program.Break || Program.player1.Victory)
            {
                break;
            }

            //turno de la IA
            Moves();
            ArtificialIntelligence.AITurn();

            //modificacion de los contadores
            Program.Turns++;

            //llama al antivirus
            if (Program.Turns % Gameplay.VCleaning == 0)
            {
                Antivirus();
            }

            //actualizacion de contadores de las habilidades de cada jugador
            Skills.CountSkills(Program.player1);
            Skills.CountSkills(Program.player2);

        } while (true);
    }

    //retorna los valores de dos dados
    public static int[] Dices()
    {
        Random random = new Random();
        int[] dices = { random.Next(1, 11), random.Next(1, 11) };
        return dices;
    }

    //tirar los dados y calular los movimientos
    public static void Moves()
    {
        Program.moves = Dices();

        //hacer positivo el resultado
        if (Program.moves[0] >= Program.moves[1])
        {
            Program.turnmoves = Convert.ToInt32((Program.moves[0] - Program.moves[1]) * Program.player1!.Speed);
        }
        else
        {
            Program.turnmoves = Convert.ToInt32((Program.moves[1] - Program.moves[0]) * Program.player1!.Speed);
        }
    }

    //desarrolla el turno de un jugador
    public static void Turn(Player player1, Player player2, bool Change)
    {
        do
        {
            //el booleano change es usado para intercambiar los valores de los parametros entre player 1 y plaer 2, dado q el metodo se llama en main dos veces intercambiando las variables
            if (Change)
            {
                //info en pantalla
                Interface.Interface.InfoTable(player1, player2, Program.Turns, Program.moves!, Program.turnmoves, Change);
                GenerateMaze.ModMaze(player1.Position!, GenerateMaze.map!, GenerateMaze.truemap!);
                Interface.Interface.PrintMaze(GenerateMaze.map!, GenerateMaze.truemap!, player1, player2);
            }
            else
            {
                //info en pantalla
                Interface.Interface.InfoTable(player2, player1, Program.Turns, Program.moves!, Program.turnmoves, Change);
                GenerateMaze.ModMaze(player1.Position!, GenerateMaze.map!, GenerateMaze.truemap!);
                Interface.Interface.PrintMaze(GenerateMaze.map!, GenerateMaze.truemap!, player1, player2);
            }

            if (Program.turnmoves == 0)
            {
                Console.Clear();
                Interface.Interface.Writing("Lamentablemente sus dados dieron un doble, no podra moverse este turno.");
                break;
            }
            else
            {
                //limpiar la cola de caracteres para el readkey q importa
                Usefulmethods.CleanQueue();

                //readkey para movimiento
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
                {
                    if (GenerateMaze.map![player1.Position!.xcoordinate, player1.Position.ycoordinate - 1] != "#")
                    {
                        player1.Position.ycoordinate -= 2;
                        Program.turnmoves--;
                    }
                    //comprobacion para habilidad de gusano
                    else if (player1.Type == "Gusano" && player1.USkill && player1.Position.ycoordinate - 2 > 0 || player1.ActualType == "Gusano" && player1.USkill && player1.Position.ycoordinate - 2 > 0)
                    {
                        player1.Position.ycoordinate -= 2;
                        Program.turnmoves--;
                        player1.Skill = false;
                        player1.USkill = false;
                        player1.ActualType = "";
                    }
                }

                else if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
                {
                    if (GenerateMaze.map![player1.Position!.xcoordinate, player1.Position.ycoordinate + 1] != "#")
                    {
                        player1.Position.ycoordinate += 2;
                        Program.turnmoves--;
                    }
                    //comprobacion para habilidad de gusano
                    else if (player1.Type == "Gusano" && player1.USkill && player1.Position.ycoordinate + 2 < GenerateMaze.size || player1.ActualType == "Gusano" && player1.USkill && player1.Position.ycoordinate + 2 < GenerateMaze.size)
                    {
                        player1.Position.ycoordinate += 2;
                        Program.turnmoves--;
                        player1.Skill = false;
                        player1.USkill = false;
                        player1.ActualType = "";
                    }
                }

                else if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
                {
                    if (GenerateMaze.map![player1.Position!.xcoordinate - 1, player1.Position.ycoordinate] != "#")
                    {
                        player1.Position.xcoordinate -= 2;
                        Program.turnmoves--;
                    }
                    //comprobacion para habilidad de gusano
                    else if (player1.Type == "Gusano" && player1.USkill && player1.Position.xcoordinate - 2 > 0 || player1.ActualType == "Gusano" && player1.USkill && player1.Position.xcoordinate - 2 > 0)
                    {
                        player1.Position.xcoordinate -= 2;
                        Program.turnmoves--;
                        player1.Skill = false;
                        player1.USkill = false;
                        player1.ActualType = "";
                    }
                }

                else if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
                {
                    if (GenerateMaze.map![player1.Position!.xcoordinate + 1, player1.Position.ycoordinate] != "#")
                    {
                        player1.Position.xcoordinate += 2;
                        Program.turnmoves--;
                    }
                    //comprobacion para habilidad de gusano
                    else if (player1.Type == "Gusano" && player1.USkill && player1.Position.xcoordinate + 2 < GenerateMaze.size || player1.ActualType == "Gusano" && player1.USkill && player1.Position.xcoordinate + 2 < GenerateMaze.size)
                    {
                        player1.Position.xcoordinate += 2;
                        Program.turnmoves--;
                        player1.Skill = false;
                        player1.USkill = false;
                        player1.ActualType = "";
                    }
                }

                else if (key.Key == ConsoleKey.Spacebar)
                {
                    CheckBox(player1, true, false);
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

                //menu de pausa
                else if (key.Key == ConsoleKey.Escape)
                {
                    Menu.Pause(Change);
                }

                CheckBox(player1, false, false);

                if (player1.Victory || Leaving)
                {
                    Leaving = false;
                    break;
                }

                else if (Program.turnmoves <= 0 || Stop)
                {
                    //se finaliza con una condicional para tener en cuenta la posicion de los datos en la tabla dado q se llama al mismo metodo en ambos turnos y es necesario cambiar el orden
                    if (Change)
                    {
                        //termina el turno del jugador y aumenta la cantidad de turnos
                        CheckBox(player1, true, false);
                        Console.Clear();
                        //info en pantalla
                        Interface.Interface.InfoTable(player1, player2, Program.Turns, Program.moves!, Program.turnmoves, Change);
                        GenerateMaze.ModMaze(player1.Position!, GenerateMaze.map!, GenerateMaze.truemap!);
                        Interface.Interface.PrintMaze(GenerateMaze.map!, GenerateMaze.truemap!, player1, player2);
                        Thread.Sleep(200);
                        Interface.Interface.Writing("Su turno ha acabado");
                        Stop = false;
                        break;
                    }
                    else
                    {
                        //termina el turno del jugador y aumenta la cantidad de turnos
                        CheckBox(player1, true, false);
                        Console.Clear();
                        //info en pantalla
                        Interface.Interface.InfoTable(player2, player1, Program.Turns, Program.moves!, Program.turnmoves, Change);
                        GenerateMaze.ModMaze(player1.Position!, GenerateMaze.map!, GenerateMaze.truemap!);
                        Interface.Interface.PrintMaze(GenerateMaze.map!, GenerateMaze.truemap!, player1, player2);
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
    public static void CheckBox(Player player, bool LastMove /*representa si es el ultimo turno*/, bool IACall /*usado para si la llamada se hace desde la clase de IA modificar su respectivo booleano de stop*/)
    {
        //comprueba si existe una salida
        if (GenerateMaze.map![player.Position!.xcoordinate, player.Position.ycoordinate] == player.ExitChar)
        {
            player.Victory = true;
        }
        //de lo contrario hace todas las posibles comprobaciones
        else
        {
            //chequea la existencia de un objeto con las posicion del jugador
            var v = Objects.Objects.Objectslist.Find(c => c.position!.xcoordinate == player.Position.xcoordinate && c.position.ycoordinate == player.Position.ycoordinate);

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
                            GenerateMaze.truemap![player.Exit!.xcoordinate, player.Exit.ycoordinate] = player.ExitChar!;
                            GenerateMaze.map[player.Exit.xcoordinate, player.Exit.ycoordinate] = player.ExitChar!;
                        }

                        GenerateMaze.truemap![v.position!.xcoordinate, v.position.ycoordinate] = " ";
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
                        if (IACall)
                        {
                            ArtificialIntelligence.Stop = true;
                        }
                        else
                        {
                            Stop = true;
                        }
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

                        //si es un bot limpia el objetivo de exploracion actual para q no se maree
                        if ((bool)player.IMBot!)
                        {
                            ArtificialIntelligence.CurrentExplorePoint = null;
                        }
                    }
                }
                else if (v.type == "Formatting" && LastMove == true)
                {
                    Console.Clear();
                    Interface.Interface.Writing("Mala suerte, ha caido en una trampa del tipo formateo del sistema, sera devuelto al inicio");
                    player.Position.xcoordinate = player.Entrance!.xcoordinate;
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
                        player.Position!.xcoordinate = player.Entrance!.xcoordinate;
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
                        player.Position!.xcoordinate = player.Entrance!.xcoordinate;
                        player.Position.ycoordinate = player.Entrance.ycoordinate;

                        //vacia la posicion de la IA para q no se maree
                        if ((bool)player.IMBot!)
                        {
                            ArtificialIntelligence.CurrentExplorePoint = null;
                        }
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

    //chequea la existencia de un ganador
    public static void CheckWinner()
    {
        if (Program.player1!.Victory)
        {
            Interface.Interface.Writing($"Felicidades {Program.player1.Token}, ha infectado el ordenador primero que su oponente y ha resultado ganador");
        }
        else if (Program.player2!.Victory)
        {
            Interface.Interface.Writing($"Felicidades {Program.player2.Token}, ha infectado el ordenador primero que su oponente y ha resultado ganador");
        }
    }
}