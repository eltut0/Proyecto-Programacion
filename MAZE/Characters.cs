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

    public static void Createcharacters()
    {
        Characters trojan = new Characters();
        trojan.name = "Troyano";
        trojan.description = "Habilidad especial: Tiene 95% de probabilidad de no ser detectado durante la limpieza de virus. Tiempo de refresco: 10 turnos.";
        trojan.speed = 1.5;
        CharactersList.Add(trojan);

        Characters worm = new Characters();
        worm.name = "Gusano";
        worm.description = "Habilidad especial: Tiene la capacidad de atravesar una pared. Tiempo de refresco: 8 turnos.";
        worm.speed = 1.0;
        CharactersList.Add(worm);

        Characters  spyware = new Characters();
        spyware.name = "Spyware";
        spyware.description = "Su campo de vision es el doble de grande que los demas. Habilidad especial: Muestra un objeto aleatorio del mapa, ya sea un archivo, " +
            "punto de resguardo o la posicion de una trampa. Tiempo de refresco: 5 turnos.";
        spyware.speed = 1.7;
        CharactersList.Add(spyware);
    }

}