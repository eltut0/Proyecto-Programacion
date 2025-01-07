using MAZE.Map;

namespace MAZE.Players
{
    public class Player
    {
        //lista para guardar la info de los jugadores
        public static List<Player> PlayerList = new List<Player>();
        //tipo de personaje
        public string Type { get; set; }

        //solo aplicable para el metamorfico
        public string ActualType { get; set; }

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

        //booleano q representa q se activa el uso de la habilidad
        public bool USkill { get; set; }

        //contador para habilidad especial
        public int SCount { get; set; }

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

                    var temp2 = Objects.Objects.Objectslist.Find(c => c.position.xcoordinate == player.Exit.xcoordinate && c.position.ycoordinate == player.Exit.ycoordinate);

                    if (temp2 == null)
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

                do
                {
                    Menu.SelectCh(player, i+1);

                    player.Victory = false;
                    player.Token = Tokken(i+1);
                    player.SCount = player.Refresh;

                    bool choice = Menu.BooleanMenu($"Su personaje es del tipo {player.Type}, su ficha sera: {player.Token}. Desea mantener esta configuracion para su personaje?");

                    if (choice)
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        //si se regresa vaciar el tipo de jugador para q no interfiera despues en el menu de seleccion
                        player.Type = null!;
                    }

                } while (true);

                PlayerList.Add(player);
            }
        }

        //seleccion de tipo de personaje
        public static Player PlayerTypeSelect(string type, Player player)
        {

            var temp = Characters.CharactersList.Find(c => c.name == type);
            Interface.Interface.WritingWOReadKey(temp!.name);
            Interface.Interface.WritingWOReadKey(temp.description);
            Interface.Interface.Writing($"La velocidad es {temp.speed}");

            bool choice = Menu.BooleanMenu("Desea usar este personaje? Marque Enter para aceptar o cualquier otra tecla para regresar.");

            if (choice)
            {
                player.Type = type;
                player.Refresh = temp.refresh;
                player.Speed = temp.speed;
            }
            else
            {
                Console.Clear();
            }

            return player;
        }

        //seleccion de ficha
        public static string Tokken(int ID)
        {
            do
            {
                string temp;
                do
                {
                    Interface.Interface.WritingWOReadKey($"Jugador {ID}, ingrese un caracter el cual sera su ficha en el juego, no usar emojis:");
                    temp = Console.ReadLine()!;

                    //no se puede usar 1 o 2 porque se usan para generar salidas
                    if (temp == null || temp.Length != 1 || temp == "1" || temp == "2")
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
                    Console.Clear();
                    return temp;
                }
                else
                {
                    Interface.Interface.Writing("Esa ficha esta en uso, elija otra");
                }
            } while (true);
        }
    }
}