public class Player
{
    //tipo de ficha
    public string type { get; set; }

    //tablero en blanco
    public char[,] emap { get; set; }

    //laberinto definido
    public char[,] map { get; set; }

    //ficha
    public char ficha { get; set; }

    //posicion de inicio
    public Position position { get; set; }
}