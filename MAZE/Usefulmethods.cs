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
}