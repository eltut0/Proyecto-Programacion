using MAZE.Map;
using MAZE.Players;
using Spectre.Console;
using System;
public class Program
{
    public static void Main(string[] args)
    {
        Console.Clear();
        //muestra la info inicial(definir en su respectiva clase)
        Interface.Interface.Tittle();
        Interface.Interface.BeginnerInfo();

        //pide el ingreso de la dimension del laberinto
        Usefulmethods.MazeGetDim();

        //llama al metodo de creacion de mapas tras obtener una dimension valida
        GenerateMaze.Start();

        //almacenar personajes en lista para dar lugar a la creacion de los jugadores
        MAZE.Players.Characters.Createcharacters();
        MAZE.Players.Player.CreatePlayer();

        var player1 = Player.PlayerList.FirstOrDefault();
        var player2 = Player.PlayerList.Skip(1).FirstOrDefault();

        //entero q representa la cantidad de turnos completos
        int turnmoves;
        int turns = 0;
        int[] moves = new int[2];

        //bucle principal del juego
        do
        {   
            //turno jugador 1 waaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
            Interface.Interface.Writing("Jugador 1, lance los dados");
            moves = Gameplay.Dices();

            //hacer positivo el resultado
            if (moves[0] >= moves[1])
            {
                turnmoves = Convert.ToInt32((moves[0] - moves[1]) * player1.Speed);
            }
            else
            {
                turnmoves = Convert.ToInt32((moves[1] - moves[0]) * player1.Speed);
            }

            //buclea del turno de jugador 1
            Gameplay.Turn(player1, player2, turns, moves, turnmoves);

            //turno jugador 2 waaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

            Interface.Interface.Writing("Jugador 2, lance los dados");
            moves = Gameplay.Dices();

            //hacer positivo el resultado
            if (moves[0] >= moves[1])
            {
                turnmoves = Convert.ToInt32((moves[0] - moves[1]) * player2.Speed);
            }
            else
            {
                turnmoves = Convert.ToInt32((moves[1] - moves[0]) * player2.Speed);
            }

            //buclea del turno de jugador 2 intercambiando los parametros con respecto a la primera llamada para q el metodo interprete al segundo jugador como el personable jugable
            Gameplay.Turn(player2, player1, turns, moves, turnmoves);

        } while (true);
    }
}

//arreglar la eliminacion de casillas
//modificar el string builder para dejar los objetos y personajes aparte para ponerlos de un color aparte
//activar los contadores