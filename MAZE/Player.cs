public class Player
{
    //lista para guardar la info de los jugadores
    public static List<Player> PlayerList = new List<Player>();
    //tipo de personaje
    public string type { get; set; }

    //ficha
    public string token { get; set; }

    //posicion de inicio
    public Position position { get; set; }

    //condicion de victoria
    public bool victory { get; set; }

    //se llama para crear los dos personajes
    public static void CreatePlayer()
    {
        //creacion de jugadores
        for (int i = 0; i < 2; i++)
        {
            Player player = new Player();
            Interface.WritingWOReadKey($"Jugador {i + 1}:");
            do
            {
                Interface.WritingWOReadKey("Elija su tipo de personaje, marque la tecla correspondiente para ver su informacion, y despues elija si desea usarlo o escoger otro.");
                Interface.WritingWOReadKey("1.Troyano, 2.Gusano, 3.Spyware");

                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();

                if (key.Key == ConsoleKey.NumPad1 || key.Key == ConsoleKey.D1)
                {
                    player = PlayerTypeSelect("Troyano", player);
                }
                else if (key.Key == ConsoleKey.NumPad2 || key.Key == ConsoleKey.D2)
                {
                    player = PlayerTypeSelect("Gusano", player);
                }
                else if (key.Key == ConsoleKey.NumPad3 || key.Key == ConsoleKey.D3)
                {
                    player = PlayerTypeSelect("Spyware", player);
                }
                else
                {
                    Interface.Writing("Seleccione una opcion valida");
                }

                if (player.type != null)
                {
                    break;
                }
                else
                {
                    continue;
                }

            } while (true);

            player.victory = false;

            if (i == 0)
            {
                player.token = "P";
            }
            else
            {
                player.token = "Q";
            }
            Interface.Writing($"Su ficha sera: {player.token}");
        }
    }

    //seleccion de tipo de personaje
    public static Player PlayerTypeSelect(string type, Player player)
    {

        var temp = Characters.CharactersList.Find(c => c.name == type);
        Interface.WritingWOReadKey(temp.name);
        Interface.WritingWOReadKey(temp.description);
        Interface.Writing($"La velocidad es {temp.speed}");

        do
        {
            Interface.WritingWOReadKey("Desea usar este personaje? Marque Enter para aceptar o cualquier otra tecla para regresar.");
            ConsoleKeyInfo key1 = Console.ReadKey(true);
            Console.Clear();
            if (key1.Key == ConsoleKey.Enter)
            {
                player.type = type;
                return player;
            }
            else
            {
                return player;
            }

        } while (true);
    }
}