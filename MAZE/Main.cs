using MAZE.Map;
using MAZE.Players;
using Spectre.Console;
using System;
public class Program
{
    //contador de turnos
    public static int Turns { get; set; }
    public static void Main(string[] args)
    {
        do
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
            Turns = 0;
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
                Gameplay.Turn(player1, player2, Turns, moves, turnmoves, true);

                if (player1.Victory)
                {
                    break;
                }

                //turno jugador 2 waaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa

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
                Gameplay.Turn(player2, player1, Turns, moves, turnmoves, false);

                if (player2.Victory)
                {
                    break;
                }

                //modificacion de contadores
                Turns++;

                //llama al antivirus
                if (Turns % Gameplay.VCleaning == 0)
                {
                    Gameplay.Antivirus();
                }

                //actualizacion de contadores de las habilidades de cada jugador
                Skills.CountSkills(player1);
                Skills.CountSkills(player2);

            } while (true);

            Console.Clear();

            if (player1.Victory)
            {
                Interface.Interface.Writing($"Felicidades {player1.Token}, ha infectado el ordenador primero que su oponente y ha resultado ganador");
            }
            else
            {
                Interface.Interface.Writing($"Felicidades {player2.Token}, ha infectado el ordenador primero que su oponente y ha resultado ganador");
            }

            //annadir una salida del juego o repetir la partida
        } while (true);
    }
}

//pendientes:
//casilla de guardado fantasma

//resuelto:
//hablidades especiales activadas