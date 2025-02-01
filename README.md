# EJECUCION
Para ejecutar el juego es necesario tener instalado Microsoft .NET .SDK (6.0 o superior). Una vez instalado, abres en la terminal de windows la 
ruta en la que esta localizado el archivo MAZE.csproj, una vez en la consola, la pones en pantalla completa y escribes **dotnet run**, luego das enter y el juego se ejecuta.
# COMO JUGAR
Para navegar por el sistema de menus, se puede usar tanto las flechas como las teclas w, a, s, d. Luego, en el menu principal hay una opcion de tutorial, ahi esta descrito como jugar.

# FUNCIONAMIENTO DEL CODIGO
Aqui encontraras una descripcion basica de como funciona el codigo del juego, clase por clase

## PROGRAM
Se definen algunas variables estaticas necesarias para el funcionamiento del juego, son las principales, como los objetos q definen a cada jugador, y algunos booleanos necesarios, por ejemplo
para cuando se carga una partida y hay q saber si tenemos q saltar la asignacion de nuevos valores, o si hay q saltar directamente al turno del segundo jugador, ya q fue este quien
se quedo jugando. Tambien ahi esta el metodo Main.

## MUSIC
Hay un solo metodo, el cual reproduce la banda sonora del juego usando la libreria NAudio. Este metodo es llamado y se ejecuta en bucle en un proceso aparte.

## Interfaz
En esta estan los metodos relacionados con la informacion impresa en pantalla. Por ejemplo una tabla para la informacion que es generada con la libreria Spectre, al igual que los caracteres en verde para darle la estetica que vaya bien con el juego.

### Errores enfrentados que se solucionan en esta clase
La impresion del laberinto con dos bucles for anidados hacia que se notara un cierto retraso en la impresion algo desagradable, lo cual solucione en el metodo PrintMaze, en el cual
uso un stringbuilder para reducir la cantidad de impresiones en pantalla, y en lugar de imprimir cada caracter, voy anadiendolo al string, y lo imprimo de golpe, en caso de que
necesite imprimir algun objeto de otro color, por ejemplo un archivo (i), simplemente imprimo lo q llevo del stringbuilder de su respectivo color verde y entro a un grupo de condicionales
para determinar que voy a imprimir y en que color, luego de el, borro el stringbuilder y continuo haciendo lo mismo.
La impresion de informacion iterando cada caracter para dar ese efecto por estetica podria llegar a ser tedioso para algunos, asi que implemente que con una tecla se salte la iteracion del texto, para lo cual por cada caracter que imprime chequea si se introdujo input, si es asi comprueba que sea la tecla asignada, y de ser asi rompe el bucle, limpia la consola e imprime todo el string.

## ARTIFICIALINTELLIGENCE
En esta clase estan todos los metodos relacionados con el funcionamiento del Bot.
EL bot funciona mediante DFS, hace una exploracion de todas las casillas desbloqueadas que estan a su akcance, y revisa si hay algo de interes, como un archivo o una salida, si es asi, toma su posicion como ruta actual y llama al metodo Nextep en esa posicion. Este metodo se encarga de buscar la ruta de posiciones hasta una posicion especifica, luegpo da vuelta a la pila y retira el primer elemento, que es la posicion actual del jugador, y le asigna al jugador como posicion nueva la q le sigue. Tambien mantiene esa ruta en una lista para mantenerla como su ruta actual, entonces, siempre que la lista de ruta actual tenga valores,  el metodo seguira tomando las posiciones de ahi. En el caso de que no haya un punto de interes, realiza este mismo proceso pero devuelve una posicion con un "?" para seguir explorando, siemrpe que se llegue al "?", si no hay ningun punto de interes, se llama a un metodo para chequear si se puede seguir explorando en alguna casillas adyacente, o sea, chequea si de las adyacentes alguna tiene un "?" y de ser asi devuelve esta, porque de lo contrario siempre esta la posibilidad de que cuando busque, devuelva una casilla a explorar en la otra punta del mapa, cuando puede seguir en esta zona ahorrando pasos.

El bot tiene siu propio mapa, el cual va actualizando cada vez que se mueve o cada vez que activa su habilidad especial, la cual le permite ver lo que tiene alrededor. En su mapa no registra los Chechpints (O), pero bueno la razon sera dada mas adelante

### Errores enfrentados que se solucionan en esta clase
Como puede existir mas de una ruta posible para llegar a una posicion, dado que se eliminan casillas aleatorias del laberinto con este proposito, entonces que la IA compruebe cada vez que se llame al metodo NextStep la ruta a seguir, seria muy poco viable, dado que como es aleatorio, podria comenzarva tomar rutas distintas hacia un mismo punto, y comenzar a caminar sin sentido. La solucion fue mantener la ruta actual en una lista, siempre y cuando no se cambie buscamente de posicion, como podria ser en una trampa tipo redistribucion, formateo del sistema o el antivirus, de lo contarrio, entonces la ruta actual es borrada y se busca una nueva.

Otro problema fue el de los Checkpoints, el metodo que devuelve una casilla a la cual moverse se dedica a buscar una casilla distinta del vacio, que normalmente seria un archivo (i) o un caracter que represente "?", los cuales una vez q pases sobre ellos desaparecen, pero surgio un problema, el caso de los checkpoints, que siempre estan ahi, o los archivos una vez que se alcanzan los 5, ya que no se pueden recoger. Por eso los checkpoints no se registran en el mapa de la IA, cada vez que pasa sobre uno su posicion se guarda en una lista, y cada vez q se escribe el mapa de la IA, se revisan las posiciones de checkpoints ya visitados y se rellenan con un espacio para que no interfieran. Ese mismo proceso se realiza con los archivos una vez que se llega a la cantidad de 5, pues es el limite que se pueden recojer.

## GENERATEMAZE
En esta clase estan todos los metodos relacionados con la creacion del mapa. Con un DFS se esculpe el laberinto y despues con DeleteWalls se eliminan algunas casillas para q existan mas caminos. En MapCreate se anaden los objetos con metodos desde la clase Objects.
Tambien hay localizadas las variables estaticas que representan al mapa, al mapa de muestreo y a la dimension del mapa.

## PLAYER
Define objetos de ese tipo. En esta clase esta todo lo relacionado con la creacion de jugadores, todas las variables que definen a cada objeto, como booleanos para definir si es o no es un Bot o si tiene su habilidad especial disponible, enteros para definir contadores, entre otros. 

## CHARACTERS
En esta clase se crean objetos para definir a cada personaje, con su informacion, velocidad y tiempo de refresco. Una vez creados se guardan en una lista para ser utilizados en la clase Player a la hora de crear los personajes.

## GAMEPLAY
En esta clase estan estructuradas las partidas, tanto PvP como PvIA. Aqui estan los metodos mediante los cuales se asignan valores de movimientos (Dices y Moves) y la estructura de un turno en el metodo Turn. Tambien esta el metodo del Antivirus, y Checkbox que puede o no ejecutar una accion en dependencia de si hay algun objeto en la casilla a la que se mueve el personaje. 

## SKILLS
En esta clase esta el metodo que se llama una vez que un jugador activa su habilidad especial, donde a traves de una serie de condicionales se determina que se hara, dependiendo del tipo de personaje del jugador, ademas de un metodo q se usa para llevar el conteo de turnos para la habilidad especial.

## POSITION
Se usa para definir objetos del tipo posicion, con una variable x e y para representar coordenadas.

## OBJECTS
En esta clase se crean los objetos que representan las trampas, checkpoints y los archivos, se les asigna una posicion, se ubican en el mapa y son anadidos a la lista de objetos. Hay un metodo con un bucle que crea n objetos de cada tipo, que dependen de la dimension que tenga el laberinto.

## MENU
En esta clase estan implementados todos los menus del juego, se utilizan dos bucles do while true, un bucle externo que seria el principal del menu y uno que va imprimiendo y borrando para dar el aspecto al menu, de tal modo que se rompe cuando el jugador presiona una tecla, ahi se entra a un grupo de condicionales donde en dependencia de la tecla que se presiono se ejecuta o no una accion, si no se ejecuta nada, simpliemente repite el bucle de afuera y vuelve a entrar al bucle de muestreo.

## USEFULMETHODS
Aqui se implementaron algunos metodos utiles, como por ejemplo Clear, con el cual limpio los valores de algunas variables que podrian interferir a la hora de empezar una partida despues de haber terminado una

### Errores enfrentados que se solucionan en esta clase
Como para la lectura de input use Console.ReadKey() tuve un problema grave, y es que este metodo lo que hace es leer un caracter de la cola de la consola, por lo cual si tocas sin querer una tecla un monton de veces, el juego seguiria leyendo de ahi hasta que se acabe la cola. Esto lo solucione con un metodo llamado CleanQueue() en esta clase, con el cual condiciono, y si hay alguna tecla disponible, la leo sin asignar, asi en un bucle do while true limpio la cola de caracteres, y se rompe el bucle cuando no haya nada q leer. Este metodo se usa cada vez a el usuario necesita insertar input.

## SAVINGGAME
En esta clase estan los metodos relacionados al guardado de partida, en SavingGame se usa un streamwriter para escribir en el txt toda la informacion indispensable de cada partida, en LoadGame se hace lo opuesto, se lee del txt y se asignan los valores.

### Errores enfrentados que se solucionan en esta clase
Fue necesario implementar una serie de booleanos para comprobar si se habia terminado la partida en el turno del segundo jugador, ya q de lo contrario la partida se cargaba pero comenzaba desde el principio. Estos booleanos se usan para realizar comprobaciones en el metodo de la partida en la clase Gameplay, de modo que si el salto es true la partida comienza en el jugador 2, saltando tambien la asignacion de un nuevo valor de movimientos, ya que desde la partida pasada se tenia uno. El txt
