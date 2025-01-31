using MAZE.Map;
using MAZE.Players;
using System.Net;
using System.Reflection.Metadata.Ecma335;

public class ArtificialIntelligence
{
    //matriz q representa el campo de vision de la IA
    public static string[,]? AImap { get; set; }

    //booleano para terminar de golpe el turno
    public static bool Stop { get; set; }

    //lista de recorrido en el reconocimiento de casillas
    public static List<Position> Reading = new List<Position>();

    //stack usado durante el recorrido
    public static Stack<Position> ReadingStack = new Stack<Position>();

    //objetivo actual para explorar
    public static Position? CurrentExplorePoint {  get; set; }

    //pila actual para llegar a CUrrentExplorePoint, tuve q cambiar de stakc a pila porque el stack no guardaba la info
    public static List<Position> CurrentStack = new List<Position>();

    //lista de checkpoints visitados para excluirlos
    public static List<Position> VisitedCHeckPoints = new List<Position>();
    
    //se llama a la par del creador de personaje para crear el jugador q usa la IA, ojo llamar despues de q se cree el del jugador real para q quede como player 2
    public static void AIPlayerCreator()
    {
        Interface.Interface.Writing("Hola, soy el Bot q jugara contra ti esta partida, mi nombre es Steve Jobs AI, pero llamame solo Steve");
        Interface.Interface.Writing("Fui perfeccionado para jugar con el virus de tipo Spyware, asi que seleccionare este tipo para mi personaje");
        Interface.Interface.Writing("Para mi ficha usare una S");
        Interface.Interface.Writing("En fin buena suerte en el juego");

        Player Bot = new Player();

        AImap = new string[GenerateMaze.size,GenerateMaze.size];
        Bot.PlayerN = "2";
        Bot.Type = "Spyware";
        Bot.Token = "S";
        Bot.Entrance = new Position();
        Bot.Entrance = GenerateMaze.Entrance();
        Bot.Refresh = 7;
        Bot.Speed = 2.7;
        Bot.ExitChar = "2";
        Bot.Archives = 0;
        Bot.IMBot = true;
        Bot.Exit = new Position();
        do
        {
            do
            {
                Bot.Exit = new Position();
                Bot.Exit.xcoordinate = GenerateMaze.RandomCoordinate();
                Bot.Exit.ycoordinate = GenerateMaze.RandomCoordinate();

                var temp = Player.PlayerList.Find(c => c.Exit!.xcoordinate == Bot.Exit.xcoordinate && c.Exit.ycoordinate == Bot.Exit.ycoordinate);

                if (temp == null)
                {
                    break;
                }
            } while (true);
            var temp2 = Objects.Objects.Objectslist.Find(c => c.position!.xcoordinate == Bot.Exit.xcoordinate && c.position.ycoordinate == Bot.Exit.ycoordinate);
            if (temp2 == null)
            {
                break;
            }
        } while (true);
        Bot.Position = new Position();
        Bot.Position = Bot.Entrance;

        //rellenar el mapa
        for (int i = 0; i < GenerateMaze.size; i++)
        {
            for (int j = 0; j < GenerateMaze.size; j++)
            {
                AImap[i, j] = "?";
            }
        }

        Player.PlayerList.Add(Bot);
    }

    //turno de IA
    public static void AITurn()
    {
        Interface.Interface.Writing("Comienza el turno de Steve");

        if (Program.turnmoves == 0)
        {
            Interface.Interface.Writing("He sacado dobles, no tengo derecho a jugar este turno");
        }
        else
        {
            //bucle principal del turno
            do
            {
                //actualiza los valores de las variables q son analizadas
                GenerateMaze.ModMaze(Program.player2.Position!, GenerateMaze.map!, GenerateMaze.truemap!);
                ReadInfo();

                //muestreo de informacion
                Interface.Interface.InfoTable(Program.player1!, Program.player2!, Program.Turns, Program.moves!, Program.turnmoves, false);
                GenerateMaze.ModMaze(Program.player2!.Position!, GenerateMaze.map!, GenerateMaze.truemap!);
                Interface.Interface.PrintMaze(GenerateMaze.map!, GenerateMaze.truemap!, Program.player1, Program.player2!);
                Thread.Sleep(1000);

                //modificacion de la posicion del personaje
                AIModPosition();

                Console.Clear();

                //modifica el contador de turnos
                Program.turnmoves--;

                //comprobacion de casillas
                Gameplay.CheckBox(Program.player2!, false, true);

                if (Program.player2.Victory)
                {
                    break;
                }

                //valora el posible final del juego
                if (Program.turnmoves <= 0 || Stop)
                {
                    //termina el turno del jugador y aumenta la cantidad de turnos
                    Gameplay.CheckBox(Program.player2!, true, true);
                    Console.Clear();
                    //info en pantalla
                    Interface.Interface.InfoTable(Program.player1, Program.player2!, Program.Turns, Program.moves!, Program.turnmoves, false);
                    GenerateMaze.ModMaze(Program.player2.Position!, GenerateMaze.map!, GenerateMaze.truemap!);
                    Interface.Interface.PrintMaze(GenerateMaze.map!, GenerateMaze.truemap!, Program.player1, Program.player2!);
                    Thread.Sleep(200);
                    Interface.Interface.Writing("Su turno ha acabado");
                    
                    Stop = false;

                    Console.Clear();

                    break;
                }
            } while (true);
        }
    }

    //identifica la mejor posicion para moverse
    public static void AIModPosition()
    {
        //chequear la disponibilidad de una habilidad especial
        if (Program.player2.Skill)
        {
            Skills.Skill(Program.player2);
        }

        //tomar un caracter aleatorio
        //para esto busca un ?, lo toma por defecto y se mueve hacia ahi para seguir explorando
        //entonces si es transportado as un lugar aleatorio desde la activacion de la trampa le da valor null para q no se rompa intentando buscar la misma
        //buscamos si existe un punto a revisar

        if (CurrentExplorePoint == null || (CurrentExplorePoint.xcoordinate == Program.player2.Position!.xcoordinate && CurrentExplorePoint.ycoordinate == Program.player2.Position.ycoordinate) || AImap![CurrentExplorePoint.xcoordinate, CurrentExplorePoint.ycoordinate] == " ")
        {
            if (CurrentStack != null)
            {
                CurrentStack.Clear();
            }
            //primero llama a INterestspot para chequear la existencia de alguna posicion interesante ya desbloqueada
            Position interest = new Position();
            interest = InterestSpot();

            if (interest.xcoordinate == -1)
            {
                //buscamos un nuevo punto
                //para esto utilizaremos alg similar al analisis de NewWay, pero en este caso, en vez de retornar un -1,-1, retornamos la posicion con el ?

                Position temp = new Position();
                temp = NewNearExploringSpot();

                if (temp.xcoordinate != -1)
                {
                    CurrentExplorePoint = temp;
                    Program.player2.Position = NextStep(CurrentExplorePoint);
                }
                else
                {
                    CurrentExplorePoint = NewExploringSpot();
                    Program.player2.Position = NextStep(CurrentExplorePoint);
                }                
            }
            else
            {
                CurrentExplorePoint = interest;
                Program.player2.Position = NextStep(CurrentExplorePoint!);
            }
        }
        else
        {
            //llamamos a NextSTep para el valor del punto
            Program.player2.Position = NextStep(CurrentExplorePoint);
        }
    }

    //metodo para localizar una casilla de interes, archivo o salida
    public static Position InterestSpot()
    {
        //localizacion de cualquier archivo silvestre jajajajaj
        //recorro todos los posibles caminos q tenga el personaje para hacer un reconocimiento de que esta a su alcance y se guardan en una lista

        Reading.Clear();
        ReadingStack.Clear();

        //annadimos el valor inicial
        ReadingStack.Push(Program.player2!.Position!);
        Reading.Add(Program.player2!.Position!);

        do
        {
            Position actual = new Position();
            Position temp = new Position();
            actual = ReadingStack.Peek();
            temp = NewWay2(actual);

            if (temp.xcoordinate != -1)
            {
                Reading.Add(temp);
                ReadingStack.Push(temp);
            }
            else
            {
                if (ReadingStack.Count > 0)
                {
                    ReadingStack.Pop();
                }
            }
        } while (ReadingStack.Count > 0);

        Position lastchoice = new Position();

        //chequea q exista un archivo o salida en la ruta actual
        foreach (var item in Reading)
        {
            if (AImap![item.xcoordinate,item.ycoordinate] == "2")
            {
                return item;
            }
            //tengo q condicionar q junto con la i q sea menor q 5 el conteo de archivos para q no vaya por gusto, igualmente desecha la posibilidad de q se maree en caso de q hayan una salida y un archvio dado q una salida implicarian 5 archvios
            else if (AImap![item.xcoordinate, item.ycoordinate] == "i" && Program.player2.Archives < 5)
            {
                //se guarda temporalmente dando prioridad a la salida
                lastchoice.xcoordinate = item.xcoordinate;
                lastchoice.ycoordinate = item.ycoordinate;
            }
        }

        if (lastchoice.xcoordinate != 0)
        {
            return lastchoice;
        }

        //si no esa talla devuelve -1,-1
        Position fail = new Position();
        fail.xcoordinate = -1;
        fail.ycoordinate = -1;

        return fail;
    }

    //metodo usado para retornar una position a la q se movera el 
    public static Position NewWay2(Position actual)
    {
        Position returnable = new Position();

        //creamos el arreglo q albergara las 4 posibles movimientos
        Position[] positions = new Position[4];

        //norte
        Position north = new Position();
        if (actual.xcoordinate - 2 > 0)
        {
            if (AImap![actual.xcoordinate - 1, actual.ycoordinate] == " ")
            {
                north.xcoordinate = actual.xcoordinate-2;
                north.ycoordinate = actual.ycoordinate;
            }
            else
            {
                north.xcoordinate = -1;
                north.ycoordinate = -1;
            }
        }
        else
        {
            north.xcoordinate = -1;
            north.ycoordinate = -1;
        }

        //sur
        Position south = new Position();
        if (actual.xcoordinate + 2 < GenerateMaze.size)
        {
            if (AImap![actual.xcoordinate + 1, actual.ycoordinate] == " ")
            {
                south.xcoordinate = actual.xcoordinate + 2;
                south.ycoordinate = actual.ycoordinate;
            }
            else
            {
                south.xcoordinate = -1;
                south.ycoordinate = -1;
            }
        }
        else
        {
            south.xcoordinate = -1;
            south.ycoordinate = -1;
        }

        //este
        Position west = new Position();
        if (actual.ycoordinate - 2 > 0)
        {
            if (AImap![actual.xcoordinate, actual.ycoordinate - 1] == " ")
            {
                west.xcoordinate = actual.xcoordinate;
                west.ycoordinate = actual.ycoordinate - 2;
            }
            else
            {
                west.xcoordinate = -1;
                west.ycoordinate = -1;
            }
        }
        else
        {
            west.xcoordinate = -1;
            west.ycoordinate = -1;
        }

        //oeste
        Position east = new Position();
        if (actual.ycoordinate + 2 < GenerateMaze.size)
        {
            if (AImap![actual.xcoordinate, actual.ycoordinate + 1] == " ")
            {
                east.xcoordinate = actual.xcoordinate;
                east.ycoordinate = actual.ycoordinate + 2;
            }
            else
            {
                east.xcoordinate = -1;
                east.ycoordinate = -1;
            }
        }
        else
        {
            east.xcoordinate = -1;
            east.ycoordinate = -1;
        }

        positions[0] = north;
        positions[1] = south;
        positions[2] = east;
        positions[3] = west;

        //terminar de rectificar las posicions q hallan sido visitadas
        foreach (var position in positions)
        {
            var temp = Reading.Find(c => c.xcoordinate == position.xcoordinate && c.ycoordinate == position.ycoordinate);

            if (temp != null)
            {
                position.xcoordinate = -1;
                position.ycoordinate = -1;
            }
        }

        //contador para chequear si todas las posiciones tienen una coordenada -1, por tanto q son inaccesibles, por lo cual retornaremos in -1,-1
        int count = 0;

        foreach (var position in positions)
        {
            if (position.xcoordinate == -1)
            {
                count++;
            }
        }

        //si el contador vale 4 signidica q todas son inaccesibles, asi q devolvemos -1,-1, en este caso morth q al final tiene ese valor
        if (count == 4)
        {
            return north;
        }
        //de lo contrario devolvemos un random de los posibles
        else
        {
            Random rnd = new Random();
            do
            {
                int choice = rnd.Next(0,4);

                if (positions[choice].xcoordinate != -1)
                {
                    Reading.Add(positions[choice]);
                    return positions[choice];
                }
            } while (true);
        }

    }

    //algoritmo de persecucion de una casilla*****************************************************************************
    //lectura de informacion al principio de cada turno

    public static void ReadInfo()
    {
        //modificacion del mapa de vision de la IA tras cada movimiento
        for (int i = 0; i < GenerateMaze.size; i++)
        {
            for (int j = 0; j < GenerateMaze.size; j++)
            {
                if (GenerateMaze.map![i,j] != "O")
                {
                    AImap![i, j] = GenerateMaze.map![i, j];
                }
                else if (GenerateMaze.map![i,j] == "1")
                {
                    AImap![i, j] = " ";
                }
            }
        }

        //lectura desde el mapa de la habilidad del spyware, condicionando q esa posicion ya este desbloqueada en el mapa de la IA, y que en esta posicion haya la ficha de un jugador
        for (int i = 0; i < GenerateMaze.size; i++)
        {
            for (int j = 0; j < GenerateMaze.size; j++)
            {
                if (Skills.tempMap != null)
                {
                    if (Skills.tempMap![i, j] != "?" && AImap![i, j] == "?")
                    {
                        if (!(Skills.tempMap[i, j] == Program.player1!.Token) && !(Skills.tempMap[i, j] == Program.player2!.Token))
                        {
                            if (Skills.tempMap![i, j] != "O")
                            {
                                AImap![i, j] = Skills.tempMap![i, j];
                            }
                        }
                    }
                }
            }
        }

        //chequea los checkpoints visitados para excluirlos
        foreach (Position position in VisitedCHeckPoints)
        {
            AImap![position.xcoordinate, position.ycoordinate] = " ";
        }
        //fue necesario excluir los puntos de gaurdado porque si no se rompia el programa en determinador puntos, asi q los exclui y cambie la bsuqeda de un punto de guardado por su posicion
    }

    //reliza una busqueda de la ruta hacia una posicion de interes y devuelve el siguiente paso a dar
    public static Position NextStep(Position interest)
    {
        //haremos una busqueda de una ruta hacia dicha posicion y emplearemos la pila como la secuencia de caminos a ir recorriendo, para ello, utilizaremos otra pila para vaciar la primera en esta y de esa forma cambiar el orden de los objetos, dado q deberia comenar por el final de la pila
        //vaciamos los valores de la lista y pila para reciclarlos
        Reading.Clear();
        ReadingStack.Clear();

        Position actual = new Position();
        Position temp = new Position();

        if (CurrentStack == null || CurrentStack.Count == 0)
        {
            //annadimos los parametros iniciales
            Reading.Add(Program.player2.Position!);
            ReadingStack.Push(Program.player2.Position!);

            do
            {   
                actual = ReadingStack.Peek();
                temp = NewWay2(actual);

                if (temp.xcoordinate == interest.xcoordinate && temp.ycoordinate == interest.ycoordinate)
                {
                    ReadingStack.Push(temp);
                    break;
                }

                if (temp.xcoordinate == -1)
                {
                    if (ReadingStack.Count > 1)
                    {
                        ReadingStack.Pop();
                    }
                }
                else
                {
                    ReadingStack.Push(temp);
                    Reading.Add(temp);
                }
            } while (true);

        }
        else
        {
            //cuando carga de la lista me develve la posicion actual y se pierde un turno, asi q comprobamos en caso de q esto pueda ocurrir
            if (Program.player2.Position!.xcoordinate == CurrentStack[CurrentStack.Count-1].xcoordinate && Program.player2.Position.ycoordinate == CurrentStack[CurrentStack.Count - 1].ycoordinate)
            {
                temp = CurrentStack[CurrentStack.Count - 1];
                CurrentStack.Remove(CurrentStack[CurrentStack.Count - 1]);
            }
            //recoger directamente de la lista actual
            temp = CurrentStack[CurrentStack.Count - 1];
            CurrentStack.Remove(CurrentStack[CurrentStack.Count - 1]);

            //condicionar q no sea la misma posicion jajajajaj
            return temp;
        }

        //revertimos el orden de la pila con otra lista
        Stack<Position> ordered = new Stack<Position>();

        do
        {
            var v = ReadingStack.Pop();
            ordered.Push(v);
            CurrentStack!.Add(v);
        } while (ReadingStack.Count > 0);

        if (ordered.Count == 1)
        {
            return interest;
        }
        else
        {
            Position returnable = new Position();

            ordered.Pop();
            returnable = ordered.Peek();

            CurrentStack.Remove(CurrentStack[CurrentStack.Count - 1]);

            if (GenerateMaze.truemap![returnable.xcoordinate, returnable.ycoordinate] == "O")
            {
                VisitedCHeckPoints.Add(returnable);
            }

            if (Program.player2.Archives == 5)
            {
                if (GenerateMaze.truemap![returnable.xcoordinate, returnable.ycoordinate] == "i")
                {
                    VisitedCHeckPoints.Add(returnable);
                }
            }
            return returnable;
        }

    }

    //un similar a newway pero te devuelve la posicionuna vez q se estanca, el lugar de regresar en la pila, para tener una casilla para continuar la exploracion
    public static Position NewExploringSpot()
    {
        //reutilizamos la lista y la pila, vaciando sus valores
        Reading.Clear();
        ReadingStack.Clear();

        Reading.Add(Program.player2!.Position!);
        ReadingStack.Push(Program.player2!.Position!);

        Position actual = new Position();
        Position temp = new Position();
        do
        {
            actual = ReadingStack.Peek();
            temp = NewWay2(actual);

            if (temp.xcoordinate != -1)
            {
                if (AImap![temp.xcoordinate, temp.ycoordinate] == "?")
                {
                    break;
                }
                else
                {
                    ReadingStack.Push(temp);
                }
            }
            else
            {
                ReadingStack.Pop();
            }
        } while (true);

        return temp;
    }

    //similar al anterior pero devuelve un punto q sea adyacente, para q no vire por gusto
    public static Position NewNearExploringSpot()
    {
        Position[] positions = new Position[4];
        //norte
        Position north = new Position();
        if (Program.player2.Position!.xcoordinate - 2 > 0)
        {
            if ((AImap![Program.player2.Position.xcoordinate - 2, Program.player2.Position.ycoordinate] == "?") && (AImap![Program.player2.Position.xcoordinate - 1, Program.player2.Position.ycoordinate] == " "))
            {
                north.xcoordinate = Program.player2.Position.xcoordinate - 2;
                north.ycoordinate = Program.player2.Position.ycoordinate;

                return north;
            }
            else
            {
                north.xcoordinate = -1;
            }
        }
        else
        {
            north.xcoordinate = -1;
        }

        //sur
        Position south = new Position();
        if (Program.player2.Position.xcoordinate + 2 < GenerateMaze.size)
        {
            if ((AImap![Program.player2.Position.xcoordinate + 2, Program.player2.Position.ycoordinate] == "?") && (AImap![Program.player2.Position.xcoordinate + 1, Program.player2.Position.ycoordinate] == " "))
            {
                south.xcoordinate = Program.player2.Position.xcoordinate + 2;
                south.ycoordinate = Program.player2.Position.ycoordinate;

                return south;
            }
            else
            {
                south.xcoordinate = -1;
            }
        }
        else
        {
            south.xcoordinate = -1;
        }

        //este
        Position west = new Position();
        if (Program.player2.Position.ycoordinate - 2 > 0)
        {
            if ((AImap![Program.player2.Position.xcoordinate, Program.player2.Position.ycoordinate - 2] == "?") && (AImap![Program.player2.Position.xcoordinate, Program.player2.Position.ycoordinate - 1] == " "))
            {
                west.xcoordinate = Program.player2.Position.xcoordinate;
                west.ycoordinate = Program.player2.Position.ycoordinate - 2;

                return west;
            }
            else
            {
                west.xcoordinate = -1;
            }
        }
        else
        {
            west.xcoordinate = -1;
        }

        //oeste
        Position east = new Position();
        if (Program.player2.Position.ycoordinate + 2 < GenerateMaze.size)
        {
            if ((AImap![Program.player2.Position.xcoordinate, Program.player2.Position.ycoordinate + 2] == "?") && (AImap![Program.player2.Position.xcoordinate, Program.player2.Position.ycoordinate + 1] == " "))
            {
                east.xcoordinate = Program.player2.Position.xcoordinate;
                east.ycoordinate = Program.player2.Position.ycoordinate + 2;

                return east;
            }
            else
            {
                east.xcoordinate = -1;
            }
        }
        else
        {
            east.xcoordinate = -1;
        }

        positions[0] = north;
        positions[1] = south;
        positions[2] = east;
        positions[3] = west;

        //contador para chequear si todas las posiciones tienen una coordenada -1, por tanto q son inaccesibles, por lo cual retornaremos in -1,-1
        int count = 0;

        foreach (var position in positions)
        {
            if (position.xcoordinate == -1)
            {
                count++;
            }
        }

        //si el contador vale 4 signidica q todas son inaccesibles, asi q devolvemos -1,-1, en este caso morth q al final tiene ese valor
        if (count == 4)
        {
            Position noway = new Position();
            noway.xcoordinate = -1;
            noway.ycoordinate = -1;
            return noway;
        }
        //de lo contrario devolvemos un random de los posibles
        else
        {
            Random rnd = new Random();

            do
            {
                int choice = rnd.Next(0, 4);

                if (positions[choice].xcoordinate != -1)
                {
                    return positions[choice];
                }
            } while (true);
        }
    }

}