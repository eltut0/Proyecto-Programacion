using MAZE.Map;

namespace MAZE.Players
{
    public class Player
    {
        //lista para guardar la info de los jugadores
        public static List<Player> PlayerList = new List<Player>();
        //tipo de personaje
        public string Type { get; set; }

        //ficha
        public string Token { get; set; }

        //posicion actual
        public Position Position { get; set; }

        //posicion de inicio
        public Position Entrance { get; set; }

        //archivos recogidos
        public int Archives { get; set; }

        //tiempo de refresco
        public int Refresh {  get; set; }

        //habilidad especial, se puede usar o no
        public bool Skill { get; set; }

        //condicion de victoria
        public bool Victory { get; set; }

        //velocidad
        public double Speed { get; set; }

        //posicion segura
        public bool IsSafe { get; set; }

        //salida aleatoria generada
        public Position Exit {  get; set; }

        //caracter q representa la salida del jugador
        public string ExitChar {  get; set; }

        //se llama para crear los dos personajes
        public static void CreatePlayer()
        {
            //creacion de jugadores
            for (int i = 0; i < 2; i++)
            {
                Player player = new Player();
                player.Archives = 0;
                player.Skill = false;

                //crea la salida aleatoria q sera mostrada una vez q el jugador alcance los 5 archivos
                do
                {
                    player.Exit = new Position();
                    player.Exit.xcoordinate = GenerateMaze.RandomCoordinate();
                    player.Exit.ycoordinate = GenerateMaze.RandomCoordinate();

                    var temp = PlayerList.Find(c => c.Exit.xcoordinate == player.Exit.xcoordinate && c.Exit.ycoordinate == player.Exit.ycoordinate);

                    if (temp == null)
                    {
                        break;
                    }

                } while (true);

                player.ExitChar = Convert.ToString(i + 1);

                do {
                    player.Position = GenerateMaze.Entrance();

                    var temp = PlayerList.Find(c  => c.Position.xcoordinate == player.Position.xcoordinate && c.Position.ycoordinate == player.Position.ycoordinate);

                    if (temp == null)
                    {
                        player.Entrance = new Position();
                        player.Entrance.xcoordinate = player.Position.xcoordinate;
                        player.Entrance.ycoordinate = player.Position.ycoordinate;
                        break;
                    }

                } while (true);

                Interface.Interface.Writing($"Jugador {i + 1}:");
                do
                {
                    Interface.Interface.Writing("Elija su tipo de personaje, marque la tecla correspondiente para ver su informacion, y despues elija si desea usarlo o escoger otro.");
                    Interface.Interface.WritingWOReadKey("1.Troyano, 2.Gusano, 3.Spyware, 4.Reboot, 5.Metamorfico");

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
                    else if (key.Key == ConsoleKey.NumPad4 || key.Key == ConsoleKey.D4)
                    {
                        player = PlayerTypeSelect("Reboot", player);
                    }
                    else if (key.Key == ConsoleKey.NumPad5 || key.Key == ConsoleKey.D5)
                    {
                        player = PlayerTypeSelect("Metamorfico", player);
                    }
                    else
                    {
                        Interface.Interface.Writing("Seleccione una opcion valida");
                    }

                    if (player.Type != null)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }

                } while (true);

                player.Victory = false;
                player.Token = Tokken();

                Interface.Interface.Writing($"Su ficha sera: {player.Token}");

                PlayerList.Add(player);
            }
        }

        //seleccion de tipo de personaje
        public static Player PlayerTypeSelect(string type, Player player)
        {

            var temp = Characters.CharactersList.Find(c => c.name == type);
            Interface.Interface.WritingWOReadKey(temp.name);
            Interface.Interface.WritingWOReadKey(temp.description);
            Interface.Interface.Writing($"La velocidad es {temp.speed}");

            do
            {
                Interface.Interface.WritingWOReadKey("Desea usar este personaje? Marque Enter para aceptar o cualquier otra tecla para regresar.");
                ConsoleKeyInfo key1 = Console.ReadKey(true);
                Console.Clear();
                if (key1.Key == ConsoleKey.Enter)
                {
                    player.Type = type;
                    player.Refresh = temp.refresh;
                    player.Speed = temp.speed;
                    return player;
                }
                else
                {
                    return player;
                }

            } while (true);
        }

        //seleccion de ficha
        public static string Tokken()
        {
            do
            {
                string temp;
                do
                {
                    Interface.Interface.WritingWOReadKey("Ingrese un caracter el cual sera su ficha en el juego, no usar emojis:");
                    temp = Console.ReadLine();
                    Console.Clear();

                    if (temp == null || temp.Length != 1)
                    {
                        Interface.Interface.Writing("Seleccione un caracter valido");

                    }
                    else
                    {
                        break;
                    }
                } while (true);

                var temp1 = PlayerList.Find(c => c.Token == temp);

                if (temp1 == null)
                {
                    return temp;
                }
                else
                {
                    Interface.Interface.Writing("Esa ficha esta en uso, elija otra.");
                }
            } while (true);
        }
    }
}