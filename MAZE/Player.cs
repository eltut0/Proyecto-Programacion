public class Player
{
    //lista para guardar la info de los jugadores
    public static List<Player> PlayerList = new List<Player>();
    //tipo de personaje
    public string type { get; set; }

    //ficha
    public char ficha { get; set; }

    //posicion de inicio
    public Position position { get; set; }

    //condicion de victoria
    public static bool victory { get; set; }
}