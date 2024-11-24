namespace MAZE.Map
{
    public class MapCreation
    {

        public static string[,] MapCreate(string[,] maze)
        {
            Objects.Objects.Files(maze);
            Objects.Objects.Checkpoints(maze);
            Objects.Objects.DesconnectionTrap(maze);
            Objects.Objects.RedistributionTrap(maze);
            Objects.Objects.FormattingTrap(maze);

            return maze;
        }
    }
}