public class MapCreation
{

    public static string[,] MapCreate(string[,] maze)
    {
        Files(maze);
        Checkpoints(maze);

        return maze;
    }

    //distribuir los 10 archivos de forma aleatoria en el mapa, y guardar sus posiciones en la lista
    public static void Files(string[,] maze)
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
                var temp = Objects.Objectslist.Find(c => c.position == objects.position);

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
        for (int i = 0; i < 10; i++) 
        {
            Objects objects = new Objects();
            Position position = new Position();
            objects.position = position;
            objects.type = "Checkpoint";

            do {
                objects.position.xcoordinate = GenerateMaze.RandomCoordinate();
                objects.position.ycoordinate = GenerateMaze.RandomCoordinate();
                var temp = Objects.Objectslist.Find(c => c.position == objects.position);

                if (temp == null)
                {
                    break;
                }
            }while(true);


            maze[objects.position.xcoordinate, objects.position.ycoordinate] = "O";

        }
        
    }

    //public static void 
}