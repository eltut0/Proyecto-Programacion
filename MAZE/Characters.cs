using System;

public class Characters
{
    //lista para guardar los personajes
    public static List<Characters> CharactersList = new List<Characters>();

    //nombre del personaje
    public string name {  get; set; }
    //descripcion del personaje
    public string description { get; set; }
    //velocidad (se multiplica por el valor del dado)
    public double speed { get; set; }

}