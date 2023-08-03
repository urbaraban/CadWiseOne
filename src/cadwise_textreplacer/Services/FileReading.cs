namespace CadWiseTextReplacer.Services
{
    internal class FileReading
    {
        public static string GetPrewiewFromFile(string filePath, int linescount = 10)
        {
            return string.Join(string.Empty, File.ReadLines(filePath).Take(linescount).ToArray());
        }
    }
}
