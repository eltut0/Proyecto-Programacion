using MAZE.Map;
using MAZE.Players;
using System.ComponentModel.Design;
using System.Numerics;
using System.Runtime.CompilerServices;

public class SaveGame
{
    public static string route = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SavedGame.txt");

    public static void SavingGame(bool change)
    {
        bool choice = Menu.BooleanMenu("Esta seguro de que desea guardar partida. Se sobreescribiran todos los datos");

        if (choice)
        {
            //crear el txt
            using (StreamWriter sw = File.CreateText(route))
            {
                sw.Close();
            }

            using (StreamWriter sw = File.AppendText(route))
            {
                //guarda la dimension del laberinto
                sw.WriteLine(GenerateMaze.size);

                //guarda los objetos
                foreach (var obj in Objects.Objects.Objectslist)
                {
                    sw.WriteLine(obj.type);
                    sw.WriteLine(obj.position.xcoordinate);
                    sw.WriteLine(obj.position.ycoordinate);
                }

                sw.WriteLine("Stop");

                //guarda los mapas
                foreach (string strings in GenerateMaze.map)
                {
                    sw.WriteLine(strings);
                }

                foreach (string strings in GenerateMaze.truemap)
                {
                    sw.WriteLine(strings);
                }

                //guarda los jugadores
                foreach (var player in Player.PlayerList)
                {
                    sw.WriteLine(player.Type);
                    sw.WriteLine(player.Position.xcoordinate);
                    sw.WriteLine(player.Position.ycoordinate);
                    sw.WriteLine(player.Archives);
                    sw.WriteLine(player.Token);
                    sw.WriteLine(player.ExitChar);
                    sw.WriteLine(player.ActualType);
                    sw.WriteLine(player.SCount);
                    sw.WriteLine(player.Entrance.xcoordinate);
                    sw.WriteLine(player.Entrance.ycoordinate);
                    sw.WriteLine(player.IsSafe);
                    sw.WriteLine(player.Refresh);
                    sw.WriteLine(player.Exit.xcoordinate);
                    sw.WriteLine(player.Exit.ycoordinate);
                    sw.WriteLine(player.Skill);
                    sw.WriteLine(player.Speed);
                    sw.WriteLine(player.USkill);
                    sw.WriteLine(player.PlayerN);
                }

                //guarda variables importantes de la partida
                sw.WriteLine(Program.Turns);

                sw.WriteLine(change);

                //parametros de movimiento
                sw.WriteLine(Program.turnmoves);

                //las posiciones del arreglo de dados
                sw.WriteLine(Program.moves[0]);
                sw.WriteLine(Program.moves[1]);

                sw.Close();
            }

            Console.Clear();
            Interface.Interface.Writing("Partida guardada");
        }

    }

    public static void LoadGame()
    {
        using (StreamReader sr = new StreamReader(route))
        {
            //lee el primer dato q es el tamanno del laberinto
            GenerateMaze.size = int.Parse(sr.ReadLine()!);

            //lee la informacion de los objetos en bucle hasta q llegue el stop
            do
            {
                string temp = sr.ReadLine()!;

                if (temp == "Stop")
                {
                    break;
                }
                else
                {
                    Objects.Objects obj = new Objects.Objects();

                    obj.type = temp;

                    obj.position = new Position();
                    obj.position.xcoordinate = int.Parse(sr.ReadLine()!);
                    obj.position.ycoordinate = int.Parse(sr.ReadLine()!);

                    Objects.Objects.Objectslist.Add(obj);
                }
            } while (true);

            //lee los mapas
            GenerateMaze.map = new string[GenerateMaze.size,GenerateMaze.size];
            GenerateMaze.truemap = new string[GenerateMaze.size, GenerateMaze.size];
            for (int i = 0; i < GenerateMaze.size; i++)
            {
                for (int j = 0; j < GenerateMaze.size; j++)
                {
                    GenerateMaze.map[i,j] = sr.ReadLine()!;
                }
            }
            for (int i = 0; i < GenerateMaze.size; i++)
            {
                for (int j = 0; j < GenerateMaze.size; j++)
                {
                    GenerateMaze.truemap[i, j] = sr.ReadLine()!;
                }
            }

            //lee la info de los jugadores
            for(int i = 0; i < 2; i++)
            {
                Player player = new Player();
                player.Position = new Position();
                player.Entrance = new Position();
                player.Exit = new Position();

                player.Type = sr.ReadLine()!;
                player.Position.xcoordinate = int.Parse(sr.ReadLine()!);
                player.Position.ycoordinate = int.Parse(sr.ReadLine()!);
                player.Archives = int.Parse(sr.ReadLine()!);
                player.Token = sr.ReadLine()!;
                player.ExitChar = sr.ReadLine()!;
                player.ActualType = sr.ReadLine()!;
                player.SCount = int.Parse(sr.ReadLine()!);
                player.Entrance.xcoordinate = int.Parse(sr.ReadLine()!);
                player.Entrance.ycoordinate = int.Parse(sr.ReadLine()!);
                string temp = sr.ReadLine()!;
                if (temp == "True")
                {
                    player.IsSafe = true;
                }
                else
                {
                    player.IsSafe = false;
                }
                player.Refresh = int.Parse(sr.ReadLine()!);
                player.Exit.xcoordinate = int.Parse(sr.ReadLine()!);
                player.Exit.ycoordinate = int.Parse(sr.ReadLine()!);
                string temp2 = sr.ReadLine()!;
                if (temp2 == "True")
                {
                    player.Skill = true;
                }
                else
                {
                    player.Skill = false;
                }
                player.Speed = double.Parse(sr.ReadLine()!);
                string temp3 = sr.ReadLine()!;
                if (temp3 == "True")
                {
                    player.USkill = true;
                }
                else
                {
                    player.USkill = false;
                }

                player.PlayerN = sr.ReadLine()!;

                Player.PlayerList.Add(player);
            }

            Program.Turns = int.Parse(sr.ReadLine()!);

            string temp4 = sr.ReadLine()!;
            if (temp4 == "True") { Program.Jump = true; }
            else if (temp4 == "False") { Program.Jump = false; }

            //parametro de movimientos
            Program.turnmoves = int.Parse(sr.ReadLine()!);

            //parametro de los dados
            Program.moves = new int[2];
            Program.moves[0] = int.Parse(sr.ReadLine()!);
            Program.moves[1] = int.Parse(sr.ReadLine()!);

            Interface.Interface.Writing("La partida ha sido cargada con exito");
        }
    }
}

