using MAZE.Map;

namespace Objects
{
    public class Objects
    {
        //lista que almacena los objetos distribuidos por el mapa(archivos, trampas, carpetas(checkpoints))
        public static List<Objects> Objectslist = new List<Objects>();
        //tipo
        //-Archive-Trap-Checkpoint-...
        public string type { get; set; }
        //coordenada del objeto
        public Position position { get; set; }

        //distribuir los 10 archivos de forma aleatoria en el mapa, y guardar sus posiciones en la lista
        public static void Archive(string[,] maze)
        {
            for (int i = 0; i < 10; i++)
            {
                Objects objects = new Objects();
                Position position = new Position();
                objects.position = position;
                objects.type = "Archive";
                do
                {
                    objects.position.xcoordinate = GenerateMaze.RandomCoordinate();
                    objects.position.ycoordinate = GenerateMaze.RandomCoordinate();
                    var temp = Objects.Objectslist.Find(c => c.position.xcoordinate == objects.position.xcoordinate && c.position.ycoordinate == objects.position.ycoordinate);

                    if (temp == null)
                    {
                        break;
                    }
                } while (true);

                maze[objects.position.xcoordinate, objects.position.ycoordinate] = "i";

                Objects.Objectslist.Add(objects);
            }
        }

        //defino 10 checkpoints aleatorios por el mapa
        public static void Checkpoints(string[,] maze)
        {
            for (int i = 0; i < GenerateMaze.size / 2; i++)
            {
                Objects objects = new Objects();
                Position position = new Position();
                objects.position = position;
                objects.type = "Checkpoint";

                do
                {
                    objects.position.xcoordinate = GenerateMaze.RandomCoordinate();
                    objects.position.ycoordinate = GenerateMaze.RandomCoordinate();
                    var temp = Objects.Objectslist.Find(c => c.position.xcoordinate == objects.position.xcoordinate && c.position.ycoordinate == objects.position.ycoordinate);

                    if (temp == null)
                    {
                        break;
                    }
                } while (true);

                maze[objects.position.xcoordinate, objects.position.ycoordinate] = "O";

                Objects.Objectslist.Add(objects);
            }

        }

        //annadir 7 tampas del tipo DESCONEXION
        public static void DesconnectionTrap(string[,] maze)
        {
            for (int i = 0; i < GenerateMaze.size / 5; i++)
            {
                Objects objects = new Objects();
                Position position = new Position();
                objects.position = position;
                objects.type = "Desconnection";

                do
                {
                    objects.position.xcoordinate = GenerateMaze.RandomCoordinate();
                    objects.position.ycoordinate = GenerateMaze.RandomCoordinate();
                    var temp = Objects.Objectslist.Find(c => c.position.xcoordinate == objects.position.xcoordinate && c.position.ycoordinate == objects.position.ycoordinate);

                    if (temp == null)
                    {
                        break;
                    }
                } while (true);

                Objects.Objectslist.Add(objects);
            }
        }

        //annadir 7 trampas del tipo REDISTRIBUCION
        public static void RedistributionTrap(string[,] maze)
        {
            for (int i = 0; i < GenerateMaze.size / 5; i++)
            {
                Objects objects = new Objects();
                Position position = new Position();
                objects.position = position;
                objects.type = "Redistribution";

                do
                {
                    objects.position.xcoordinate = GenerateMaze.RandomCoordinate();
                    objects.position.ycoordinate = GenerateMaze.RandomCoordinate();
                    var temp = Objects.Objectslist.Find(c => c.position.xcoordinate == objects.position.xcoordinate && c.position.ycoordinate == objects.position.ycoordinate);

                    if (temp == null)
                    {
                        break;
                    }
                } while (true);

                Objects.Objectslist.Add(objects);
            }
        }

        //annadir 7 trampas del tipo DESCONEXION
        public static void FormattingTrap(string[,] maze)
        {
            for (int i = 0; i < GenerateMaze.size / 5; i++)
            {
                Objects objects = new Objects();
                Position position = new Position();
                objects.position = position;
                objects.type = "Formatting";

                do
                {
                    objects.position.xcoordinate = GenerateMaze.RandomCoordinate();
                    objects.position.ycoordinate = GenerateMaze.RandomCoordinate();
                    var temp = Objects.Objectslist.Find(c => c.position.xcoordinate == objects.position.xcoordinate && c.position.ycoordinate == objects.position.ycoordinate);

                    if (temp == null)
                    {
                        break;
                    }
                } while (true);

                Objects.Objectslist.Add(objects);
            }
        }
    }
}