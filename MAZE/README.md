# EJECUCION
Para ejecutar el juego es necesario tener instalado Microsoft .NET .SDK (6.0 o superior). Una vez instalado, abres en la terminal de windows la 
ruta en la que esta localizado el archivo MAZE.csproj, una vez en la consola, la pones en pantalla completa y escribes *dotnet run*,
luego das enter y el juego se ejecuta.
#Como jugar
Para navegar por el sistema de menus, se puede usar tanto las flechas como las teclas w, a, s, d. Luego, en el menu principal hay una opcion de 
tutorial, ahi esta descrito como jugar.

# EXPLICACION


# FUNCIONAMIENTO DEL CODIGO
Aqui encontraras una descripcion basica de como funciona el codigo del juego, clase por clase

## PROGRAM
Se definen algunas variables estaticas necesarias para el funcionamiento del juego, son las principales, como los objetos q definen a cada jugador, y algunos booleanos necesarios, por ejemplo
para cuando se carga una partida y hay q saber si tenemos q saltar la asignacion de nuevos valores, o si hay q saltar directamente al turno del segundo jugador, ya q fue este quien
se quedo jugando. Tambien ahi esta el metodo Main.

## MUSIC
Hay un solo metodo, el cual reproduce la banda sonora del juego usando la libreria NAudio. Este metodo es llamado y se ejecuta en bucle en una tarea aparte.

## Interfaz
En esta estan los metodos relacionados con la informacion impresa en pantalla. Por ejemplo una tabla para la informacion que es generada con la libreria Spectre, al igual que los
caracteres en verde para darle la estetica que vaya bien con el juego.

### Errores enfrentados que se solucionan en esta clase
La impresion del laberinto con dos bucles for anidados hacia que se notara un cierto retraso en la impresion algo desagradable, lo cual solucione en el metodo PrintMaze, en el cual
uso un stringbuilder para reducir la cantidad de impresiones en pantalla, y en lugar de imprimir cada caracter, voy anadiendolo al string, y lo imprimo de golpe, en caso de que
necesite imprimir algun objeto de otro color, por ejemplo un archivo (i), simplemente imprimo lo q llevo del stringbuilder de su respectivo color verde y entro a un grupo de condicionales
para determinar que voy a imprimir y en que color, luego de el, borro el stringbuilder y continuo haciendo lo mismo.

## ARTIFICIALINTELLIGENCE
En esta clase estan todos los metodos relacionados con el funcionamiento del Bot.
EL bot funciona mediante DFS, hace una exploracion de todas las casillas desbloqueadas que estan a su akcance, y revisa si hay algo de interes, como un
archivo o una salida, si es asi, toma su posicion como ruta actual y llama al metodo Nextep en esa posicion. Este metodo se encarga de buscar la ruta de posiciones
hasta una posicion especifica, luegpo da vuelta a la pila y retira el primer elemento, que es la posicion actual del jugador, y le asigna al jugador como
posicion nueva la q le sigue. Tambien mantiene esa ruta en una lista para mantenerla como su ruta actual, entonces, siempre que la lista de ruta actual tenga valores, 
el metodo seguira tomando las posiciones de ahi. En el caso de que no haya un punto de interes, realiza este mismo proceso pero devuelve una posicion con un
"?" para seguir explorando, siemrpe que se llegue al "?", si no hay ningun punto de interes, se llama a un metodo para chequear si se puede seguir explorando
en alguna casillas adyacente, o sea, chequea si de las adyacentes alguna tiene un "?" y de ser asi devuelve esta, porque de lo contrario siempre esta la
posibilidad de que cuando busque, devuelva una casilla a explorar en la otra punta del mapa, cuando puede seguir en esta zona ahorrando pasos.

El bot tiene siu propio mapa, el cual va actualizando cada vez que se mueve o cada vez que activa su habilidad especial, la cual le permite ver lo que
tiene alrededor. En su mapa no registra los Chechpints (O), pero bueno la razon sera dada mas adelante

### Errores enfrentados que se solucionan en esta clase
Como puede existir mas de una ruta posible para llegar a una posicion, dado que se eliminan casillas aleatorias del laberinto con este proposito, entonces
que la IA compruebe cada vez que se llame al metodo NextStep la ruta a seguir, seria muy poco viable, dado que como es aleatorio, podria comenzarva tomar 
rutas distintas hacia un mismo punto, y comenzar a caminar sin sentido. La solucion fue mantener la ruta actual en una lista, siempre y cuando no
se cambie buscamente de posicion, como podria ser en una trampa tipo redistribucion, formateo del sistema o el antivirus, de lo contarrio, entonces la ruta
actual es borrada y se busca una nueva.

Otro problema fue el de los Checkpoints, el metodo que devuelve una casilla a la cual moverse se dedica a buscar una casilla distinta del vacio, 
que normalmente seria un archivo (i) o un caracter que represente "?", los cuales una vez q pases sobre ellos desaparecen, pero surgio un problema, el caso
de los checkpoints, que siempre estan ahi, o los archivos una vez que se alcanzan los 5, ya que no se pueden recoger. Por eso los checkpoints no se 
registran en el mapa de la IA, cada vez que pasa sobre uno su posicion se guarda en una lista, y cada vez q se escribe el mapa de la IA, se revisan las
posiciones de checkpoints ya visitados y se rellenan con un espacio para que no interfieran. Ese mismo proceso se realiza con los archivos una vez
que se llega a la cantidad de 5, pues es el limite que se pueden recojer.