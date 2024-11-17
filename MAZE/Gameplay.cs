public class Gameplay
{
    //retorna los valores de dos dados
    public static int[] Dices()
    {
        Random random = new Random();
        int[] dices = new int[2];
        dices[0] = random.Next(1, 7);
        dices[1] = random.Next(1, 7);
        return dices;
    }
}