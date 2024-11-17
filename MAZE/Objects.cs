public class Objects
{
    //lista que almacena los objetos distribuidos por el mapa(archivos, trampas, carpetas(checkpoints))
    public static List<Objects> Objectslist = new List<Objects>();
    //tipo
    //-Archive-Trap-Checkpoint-...
    public string type {  get; set; }
    //coordenada del objeto
    public Position position { get; set; }
    //estado activa o no
    public bool state { get; set; }
}