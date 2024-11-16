using System;

public class Character
{
    //lista para guardar los personajes
    public static List<Character> CharactersList = new List<Character>();

    //nombre del personaje
    public string name {  get; set; }
    //descripcion del personaje
    public string description { get; set; }
    //velocidad (se multiplica por el valor del dado)
    public double speed { get; set; }

    public static void Createcharacters()
    {
        Character trojan = new Character();
        trojan.name = "Troyano";
        trojan.description = "Habilidad especial: Tiene 95% de probabilidad de no ser detectado durante la limpieza de virus. Tiempo de refresco: 10 turnos";
        trojan.speed = 1.5;
        CharactersList.Add(trojan);

        Character worm = new Character();
        worm.name = "Gusano";
        worm.description = "Habilidad especial: Tiene la capacidad de atravesar una pared. Tiempo de refresco: 8 turnos";
        worm.speed = 1.0;
        CharactersList.Add(worm);

        Character  spyware = new Character();
        spyware.name = "Spyware";
        spyware.description = "Habilidad especial: Su campo de vision es el doble de grande que los demas. Habilidad especial: Muestra un objeto aleatorio del mapa, ya sea un archivo, " +
            "punto de resguardo o la posicion de una trampa. Tiempo de refresco: 5 turnos";
        spyware.speed = 1.7;
        CharactersList.Add(spyware);
    }

}