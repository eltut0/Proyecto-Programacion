namespace MAZE.Players
{
    public class Characters
    {
        //lista para guardar los personajes
        public static List<Characters> CharactersList = new List<Characters>();

        //nombre del personaje
        public string name { get; set; }

        //descripcion del personaje
        public string description { get; set; }
        //velocidad (se multiplica por el valor del dado)

        public double speed { get; set; }

        //tiempo de refresco
        public int refresh { get; set; }

        //genera y anade a una lista los personajes
        public static void Createcharacters()
        {
            Characters trojan = new Characters();
            trojan.name = "Troyano";
            trojan.description = "Habilidad especial: Tiene 90% de probabilidad de no ser detectado durante la limpieza de virus. Tiempo de refresco: 10 turnos.";
            trojan.speed = 1.5;
            trojan.refresh = 10;
            CharactersList.Add(trojan);

            Characters worm = new Characters();
            worm.name = "Gusano";
            worm.description = "Habilidad especial: Tiene la capacidad de atravesar una pared. Tiempo de refresco: 8 turnos.";
            worm.speed = 1.0;
            worm.refresh = 8;
            CharactersList.Add(worm);

            Characters spyware = new Characters();
            spyware.name = "Spyware";
            spyware.description = "Su campo de vision es el doble de grande que los demas. Habilidad especial: Muestra un objeto aleatorio del mapa, ya sea un archivo, " +
                "punto de resguardo o la posicion de una trampa. Tiempo de refresco: 5 turnos.";
            spyware.speed = 1.7;
            spyware.refresh = 5;
            CharactersList.Add(spyware);

            Characters metamorf = new Characters();
            metamorf.name = "Metamorfico";
            metamorf.description = "Habilidad especial: Cada vez que se cumple su tiempo de refresco puede usar una habilidad de los demas personajes, la cual se asigna de manera aleatoria. Hay una probabilidad del 20% de que no obtenga una habilidad." +
                "Tiempo de refresco: 7 turnos.";
            metamorf.refresh = 7;
            metamorf.speed = 1.3;
            CharactersList.Add(metamorf);

            Characters reboot = new Characters();
            reboot.name = "Reboot";
            reboot.description = "Habilidad especial: Redistribuye todos los objetos esparcidos en el mapa (trampas, puntos de guardado, archivos). Tiempo de refresco 10 turnos.";
            reboot.refresh = 10;
            reboot.speed = 1.6;
            CharactersList.Add(reboot);
        }

        //habilidades especiales

        //devuelve true si se puede retornar al principio
        public static bool Trojan()
        {
            Random random = new Random();
            int probability = random.Next(0, 100);

            if (probability % 10 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}