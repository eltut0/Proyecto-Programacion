using MAZE.Map;
using MAZE.Players;

public class Usefulmethods
{
    public static void MazeGetDim()
    {
        do
        {
            Interface.Interface.WritingWOReadKey("Seleccione una dificultad");
            Interface.Interface.WritingWOReadKey("1.Facil, 2.Medio, 3.Dificil");

            while (Console.KeyAvailable)
            {
                Console.ReadKey();
            }

            ConsoleKeyInfo key = Console.ReadKey(true);
            Random rnd = new Random();
            int size;
            Console.Clear();

            if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
            {
                do
                {
                    size = rnd.Next(20, 31);

                    if (size%2==1)
                    {
                        GenerateMaze.size = size;
                        break;
                    }
                } while (true);
                break;
            }
            else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
            {
                do
                {
                    size = rnd.Next(30, 41);

                    if (size % 2 == 1)
                    {
                        GenerateMaze.size = size;
                        break;
                    }

                } while (true);
                break;
            }
            else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
            {
                do
                {
                    size = rnd.Next(40, 51);

                    if (size % 2 == 1)
                    {
                        GenerateMaze.size = size;
                        break;
                    }
                } while (true);
                break;
            }
            else
            {
                Interface.Interface.Writing("Seleccione una opcion valida");
            }
        } while (true);
    }
}