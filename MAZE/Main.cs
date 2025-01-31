using MAZE.Map;
using MAZE.Players;
public class Program
{

    //especifica el tipo de juego, pvp o IA
    public static bool GameMode { get; set; }

    //contador de turnos
    public static int Turns;

    //booleano para romper el bucle del juego
    public static bool Break {  get; set; }

    //nuevo juego, de lo contrario salta directo a jugar
    public static bool NewGame { get; set; }

    //variables principales de la partida

    public static int turnmoves { get; set; }

    public static int[]? moves { get; set; }

    //bool para diferenciar entre partida cargada
    public static bool LoadedGame { get; set; }

    //bool para saltar al segndo turno
    public static bool Jump {  get; set; }

    public static Player player1 { get; set;}

    public static Player player2 { get; set; }

    public static void Main(string[] args)
    {
        Console.Clear();
        //muestra la info inicial(definir en su respectiva clase)
        Interface.Interface.Tittle();

        Task maintheme = Task.Run(() => Music.MainTheme());

        do
        {
            //llama al menu de inicio
            Menu.MainMenu();

            if (GameMode || LoadedGame)
            {
                Gameplay.NormalPvPMatch();
            }
            else
            {
                //llama a una partida de IA
                Gameplay.IAMatch();
            }

            Console.Clear();

            //chequea la existencia de un ganador
            Gameplay.CheckWinner();

            //una vez q termina una partida se vacian los valores de las variables importantes para q no haya inconvenientes
            Usefulmethods.Clear();

            //reasignar false para q no se vuelva a detener el programa
            Break = false;

        } while (true);
    }
}