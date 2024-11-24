using MAZE.Players;

namespace MAZE.Map
{
    public class GenerateMaze
    {
        //tamanno del laberinot
        public static int size = 39;

        //pila para ir generando caminos
        public static Stack<Position> Stack = new Stack<Position>();

        //lista q contiene las posiciones visitadas
        public static List<Position> List = new List<Position>();

        //crea la matriz q representa al laberinto
        public static string[,] Maze()
        {
            string[,] Maze = new string[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Maze[i, j] = "#";
                }
            }
            return Maze;
        }

        //define una posicion de entrada, decide primero la orientacion (n, s, e, w) y luego genera una posicion aleatoria entre esas mediante un switch
        public static Position Entrance()
        {
            var entrance = new Position();

            Random random = new Random();
            int randomposition = random.Next(0, 4);

            switch (randomposition)
            {
                case 0:
                    entrance.ycoordinate = 1;
                    entrance.xcoordinate = RandomCoordinate();
                    return entrance;
                case 1:
                    entrance.ycoordinate = size - 2;
                    entrance.xcoordinate = RandomCoordinate();
                    return entrance;
                case 2:
                    entrance.ycoordinate = RandomCoordinate();
                    entrance.xcoordinate = 1;
                    return entrance;
                case 3:
                    entrance.ycoordinate = RandomCoordinate();
                    entrance.xcoordinate = size - 2;
                    return entrance;
                default:
                    Console.WriteLine("Error en Entrance()");
                    return entrance;
            }
        }

        //retorna un numero aleatorio para ser usado como una coordenada (impar)
        public static int RandomCoordinate()
        {
            Random random1 = new Random();
            int randomposition;

            do
            {
                randomposition = random1.Next(1, size - 1);
            } while (randomposition % 2 == 0);

            return randomposition;
        }

        //devuelve una nueva posicion para mover la posicion
        public static Position NewWay(int x, int y)
        {
            int[] directions = new int[4];
            var coordinate = new Position();

            for (int i = 0; i < directions.Length; i++)
            {
                directions[i] = 1;
            }
            //0=n, 1=s, 2=e, 3=w

            //cpmprobar arreglo en 0
            if (!(y - 2 <= 0))
            {
                var position0 = List.Find(c => c.ycoordinate == y - 2 && c.xcoordinate == x);
                if (position0 != null)
                {
                    directions[0] = -1;
                }
            }
            else
            {
                directions[0] = -1;
            }

            //comprobar arreglo en 1
            if (!(y + 2 >= size - 1))
            {
                var position1 = List.Find(c => c.ycoordinate == y + 2 && c.xcoordinate == x);
                if (position1 != null)
                {
                    directions[1] = -1;
                }
            }
            else
            {
                directions[1] = -1;
            }

            //comprobar arreglo en 2
            if (!(x + 2 >= size - 1))
            {
                var position2 = List.Find(c => c.xcoordinate == x + 2 && c.ycoordinate == y);
                if (position2 != null)
                {
                    directions[2] = -1;
                }
            }
            else
            {
                directions[2] = -1;
            }

            //comprobar arreglo en 3
            if (!(x - 2 <= 0))
            {
                var position3 = List.Find(c => c.xcoordinate == x - 2 && c.ycoordinate == y);
                if (position3 != null)
                {
                    directions[3] = -1;
                }
            }
            else
            {
                directions[3] = -1;
            }

            //comporbacion de las posiciones accesibles y busqueda en la lista de las mismas para revisar su ya han sido visitadas {arriba}

            int choice;

            //genera una posicion aleatoria hasta q esta sea accesible, en caso de no existir ninguna retorna (-1;-1)
            do
            {
                if (directions[0] == -1 && directions[1] == -1 && directions[2] == -1 && directions[3] == -1)
                {
                    coordinate.xcoordinate = -1;
                    coordinate.ycoordinate = -1;
                    return coordinate;
                }
                else
                {
                    Random random = new Random();
                    choice = random.Next(0, 4);
                }
            } while (directions[choice] == -1);

            switch (choice)
            {
                case 0:
                    y -= 2;
                    coordinate.xcoordinate = x;
                    coordinate.ycoordinate = y;
                    return coordinate;
                case 1:
                    y += 2;
                    coordinate.xcoordinate = x;
                    coordinate.ycoordinate = y;
                    return coordinate;
                case 2:
                    x += 2;
                    coordinate.xcoordinate = x;
                    coordinate.ycoordinate = y;
                    return coordinate;
                case 3:
                    x -= 2;
                    coordinate.xcoordinate = x;
                    coordinate.ycoordinate = y;
                    return coordinate;
                default:
                    Console.WriteLine("Error garrafal en NewWay()");
                    return coordinate;
            }

        }

        //hace toda la pincha de generar el laberinto
        public static string[,] GeneratingMaze()
        {
            string[,] maze = Maze();

            var position = Entrance();
            var temp = new Position();

            Stack.Push(position);
            List.Add(position);

            do
            {
                position = Stack.Peek();
                temp = NewWay(position.xcoordinate, position.ycoordinate);

                if (temp.xcoordinate == -1)
                {
                    Stack.Pop();

                    if (Stack.Count != 0)
                    {
                        var probando = Stack.Peek();
                        /*
                        //borrarrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr
                        Console.Clear();
                        maze[probando.xcoordinate, probando.ycoordinate] = "E";
                        Interface.PrintMaze(maze);
                        Thread.Sleep(200);
                        maze[probando.xcoordinate, probando.ycoordinate] = " ";
                        //borrarrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr*/
                    }
                }
                else
                {
                    //comprobar en que direccion se mueve

                    //norte-sur
                    if (position.xcoordinate - temp.xcoordinate == 0)
                    {
                        //direccion sur
                        if (position.ycoordinate - temp.ycoordinate > 0)
                        {
                            maze[temp.xcoordinate, temp.ycoordinate] = " ";
                            maze[temp.xcoordinate, temp.ycoordinate + 1] = " ";
                        }
                        //direccion norete
                        else
                        {
                            maze[temp.xcoordinate, temp.ycoordinate] = " ";
                            maze[temp.xcoordinate, temp.ycoordinate - 1] = " ";
                        }



                        /*
                        //borrarrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr

                        Console.Clear();
                        maze[temp.xcoordinate, temp.ycoordinate] = "E";
                        Interface.PrintMaze(maze);
                        Thread.Sleep(200);
                        maze[temp.xcoordinate, temp.ycoordinate] = " "; //hasta aquiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii*/




                    }
                    //este-oeste
                    else
                    {
                        if (position.xcoordinate - temp.xcoordinate > 0)
                        {
                            maze[temp.xcoordinate, temp.ycoordinate] = " ";
                            maze[temp.xcoordinate + 1, temp.ycoordinate] = " ";
                        }
                        else
                        {
                            maze[temp.xcoordinate, temp.ycoordinate] = " ";
                            maze[temp.xcoordinate - 1, temp.ycoordinate] = " ";
                        }




                        /*
                        //probandooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
                        Console.Clear();
                        maze[temp.xcoordinate, temp.ycoordinate] = "E";
                        Interface.PrintMaze(maze);
                        Thread.Sleep(200);
                        maze[temp.xcoordinate, temp.ycoordinate] = " ";

                        //probadnooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo*/
                    }
                    List.Add(temp);
                    Stack.Push(temp);
                }




            } while (Stack.Count > 0); //condicionar con el stack // ojo // reojo

            return maze;

        }

        //desbloqeua casillas en la matriz
        public static void ModMaze(Position position, string[,] maze, string[,] map)
        {
            maze[position.xcoordinate, position.ycoordinate] = map[position.xcoordinate, position.ycoordinate];
            maze[position.xcoordinate+1, position.ycoordinate] = map[position.xcoordinate+1, position.ycoordinate];
            maze[position.xcoordinate+1, position.ycoordinate+1] = map[position.xcoordinate+1, position.ycoordinate+1];
            maze[position.xcoordinate, position.ycoordinate+1] = map[position.xcoordinate, position.ycoordinate+1];
            maze[position.xcoordinate-1, position.ycoordinate] = map[position.xcoordinate-1, position.ycoordinate];
            maze[position.xcoordinate-1, position.ycoordinate-1] = map[position.xcoordinate-1, position.ycoordinate-1];
            maze[position.xcoordinate, position.ycoordinate-1] = map[position.xcoordinate, position.ycoordinate-1];
            maze[position.xcoordinate+1, position.ycoordinate-1] = map[position.xcoordinate+1, position.ycoordinate-1];
            maze[position.xcoordinate-1, position.ycoordinate+1] = map[position.xcoordinate-1, position.ycoordinate+1];
        }
    }
}