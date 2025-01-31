using MAZE.Players;

public class Usefulmethods
{
    //metodo para limpiar cola de teclas
    public static void CleanQueue()
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
    }

    //limpia parametros de variables para volver a usarlos en una nueva partida
    public static void Clear()
    {
        Objects.Objects.Objectslist.Clear();
        Player.PlayerList.Clear();
        Program.player1 = null!;
        Program.player2 = null!;
        ArtificialIntelligence.AImap = null;
        ArtificialIntelligence.Stop = false;
        ArtificialIntelligence.CurrentExplorePoint = null;
        ArtificialIntelligence.VisitedCHeckPoints.Clear();
        Program.Turns = 0;
        Program.Break = false;
        MAZE.Map.GenerateMaze.size = 0;
        Skills.tempMap = null;
    }
}