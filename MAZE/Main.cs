using MAZE.Map;
using MAZE.Players;
public class Program
{
    //contador de turnos
    public static int Turns;

    //booleano para romper el bucle del juego
    public static bool Break {  get; set; }

    //nuevo juego, de lo contrario salta directo a jugar
    public static bool NewGame { get; set; }

    //variables principales de la partida

    public static int turnmoves { get; set; }

    public static int[] moves { get; set; }

    //bool para diferenciar entre partida cargada
    public static bool LoadedGame { get; set; }

    //bool para saltar al segndo turno
    public static bool Jump {  get; set; }

    public static Player player1 { get; set;}

    public static Player player2 { get; set;}

    public static void Main(string[] args)
    {
        Console.Clear();
        //muestra la info inicial(definir en su respectiva clase)
        Interface.Interface.Tittle();

        do
        {
            //llama al menu de inicio
            Menu.MainMenu();

            //recopila la informacion del principio solo si es un juego nuevo, de lo contrario se lee la info del txt y salta directamente al comienzo
            if (NewGame)
            {

                //pide el ingreso de la dimension del laberinto
                Menu.SelectD();

                //llama al metodo de creacion de mapas tras obtener una dimension valida
                GenerateMaze.Start();

                //almacenar personajes en lista para dar lugar a la creacion de los jugadores
                Characters.Createcharacters();
                Player.CreatePlayer();

                Turns = 0;

                //ni idea pero asi funciona
                Jump = true;
            }

            else
            {
                //realiza una lectura del txt y asigna los respectivos valores a las variables llamando al metodo
                SaveGame.LoadGame();
                LoadedGame = true;
            }

            //lee de las lista, dado q del metodo de carga de juego tambien se carga directo a la lista
            player1 = Player.PlayerList.FirstOrDefault()!;
            player2 = Player.PlayerList.Skip(1).FirstOrDefault()!;

            //bucle principal del juego
            do
            {

                if (Jump)
                {
                    //salta la nueva asignacion de valores de movimientos en caso de q se haya cargado una partida
                    if (!LoadedGame)
                    {
                        //turno jugador 1 waaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                        Interface.Interface.Writing("Jugador 1, lance los dados");

                        Gameplay.Moves();
                    }

                    //reasigna el false a loaded game para q no se salte futuras condiciones
                    LoadedGame = false;

                    //buclea del turno de jugador 1
                    Gameplay.Turn(player1, player2, Turns, moves, true);

                    //valora el posible cierre de la partida
                    if (Break || player1.Victory)
                    {
                        break;
                    }
                }
                  
                //le asigna false a jump por si las dudas
                
                Jump = true;

                //turno jugador 2 waaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

                if (!LoadedGame)
                {
                    Interface.Interface.Writing("Jugador 2, lance los dados");

                    Gameplay.Moves();
                }

                //reasigna el false a loaded game para q no se salte futuras condiciones
                LoadedGame = false;

                //buclea del turno de jugador 2 intercambiando los parametros con respecto a la primera llamada para q el metodo interprete al segundo jugador como el personable jugable

                Gameplay.Turn(player2, player1, Turns, moves, false);
                
                //valora el posible cierre de la partida
                
                if (Break || player2.Victory)
                {
                    break;
                }

                //modificacion de contadores
                Turns++;

                //llama al antivirus
                if (Turns % Gameplay.VCleaning == 0)
                {
                    Gameplay.Antivirus();
                }

                //actualizacion de contadores de las habilidades de cada jugador
                Skills.CountSkills(player1);
                Skills.CountSkills(player2);

            } while (true);

            Console.Clear();

            if (player1.Victory)
            {
                Interface.Interface.Writing($"Felicidades {player1.Token}, ha infectado el ordenador primero que su oponente y ha resultado ganador");
            }
            else if (player2.Victory)
            {
                Interface.Interface.Writing($"Felicidades {player2.Token}, ha infectado el ordenador primero que su oponente y ha resultado ganador");
            }

            //una vez q termina una partida se vacian los valores de las variables importantes para q no haya inconvenientes
            Usefulmethods.Clear();

            //reasignar false para q no se vuelva a detener el programa
            Break = false;

        } while (true);
    }
}

//pendientes:

//resuelto:
//menus mejorados
//sistema de guardado implementado