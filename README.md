# EJECUCION
Para ejecutar el juego es necesario tener instalado Microsoft .NET 8.0. Una vez instalado, abres en la terminal de windows la 
ruta en la que esta localizado el archivo MAZE.csproj, una vez en la consola, la pones en pantalla completa y escribes *dotnet run*,
luego das enter y el juego se ejecuta.
#Como jugar
Para navegar por el sistema de menus, se puede usar tanto las flechas como las teclas w, a, s, d. Luego, en el menu principal hay una opcion de tutorial, ahi esta descrito como jugar.

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
EL bot funciona mediante DFS, 
