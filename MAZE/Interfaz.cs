using System.Globalization;
using System.Reflection.Metadata.Ecma335;

public class Interface
{
    //primero que se corre en el programa
    public static void Tittle()
    {
        string titulo = "Titulo";
        Console.WriteLine(titulo);
        Thread.Sleep(2000);
        Console.Clear();
    }

    //muestra infomracion importante para el jugador y se corre al principio
    public static void BeginnerInfo()
    {
        bool choice1 = false;
        do
        {
            WritingWOReadKey("Ha jugado antes?");

            string choice = Console.ReadLine();
            Console.Clear();
            //muestra o no muestra el tutorial en dependencia del usuario, y devuekve un error y repite en caso de poner un input invalido
            switch (choice)
            {
                case "No":
                    string info1 = "algo tanque 1 2 3 probando";
                    string info2 = "algo mas tanke todavia probando";
                    string info3 = "seguimos subiendo el nivel";
                    //annado los textos del tutorial y la info del juego a un arreglo y voy escribiendolas una por una
                    string[] strings = { info1, info2, info3 };

                    foreach (string info in strings)
                    {
                        Writing(info);
                    }

                    choice1 = true;
                    break;
                case "Si":
                    Writing("Continuemos");
                    choice1 = true;
                    break;
                default:
                    WritingWOReadKey("Elija una opcion valida (Si, No)");
                    break;
            }
        } while (!choice1);
    }

    //escribe con un retraso pa que quede tiza
    public static void Writing(string info)
    {
        for (int j = 0; j < info.Length; j++)
        {
            Console.Write(info[j]);
            Thread.Sleep(150);
        }
        Console.WriteLine();
        Tips.Tips1();
        Console.ReadKey();
        Console.Clear();
    }

    //hace lo mismo q el anterior pero pincha si hay q meterle input
    public static void WritingWOReadKey(string info)
    {
            for (int j = 0; j < info.Length; j++)
            {
                Console.Write(info[j]);
            Thread.Sleep(150);
            }
            Console.WriteLine();
    }

    //printea tips para el jugador
    public class Tips
    {
        public static void Tips1()
        {
            Console.WriteLine("Pulse cualquier tecla para continuar");
        }
    }
}