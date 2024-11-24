public class Gameplay
{
    //turnos hasta que se hara la limpieza de virus
    public static int VCleaning = 8;
    //retorna los valores de dos dados
    public static int[] Dices()
    {
        Random random = new Random();
        int[] dices = { random.Next(1, 7), random.Next(1, 7) };
        return dices;
    }
}