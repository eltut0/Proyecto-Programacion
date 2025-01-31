using NAudio;
using NAudio.Wave;

public class Music
{
    public static void MainTheme()
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string relativePath = Path.Combine(currentDirectory, "..", "..", "..", "Musicfile", "MainTheme.mp3");
        string absolutePath = Path.GetFullPath(relativePath);

        do
        {
            using (var maintheme = new AudioFileReader(absolutePath))
            using (var output = new WaveOutEvent())
            {
                output.Init(maintheme);
                output.Play();

                maintheme.Volume = 0.5f;

                Thread.Sleep(90200);
            }
        } while (true);
    }
}