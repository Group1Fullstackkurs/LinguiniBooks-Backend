namespace LinguiniBooksAPI.Helpers
{
    public static class ConnStrHelper
    {
        public static string ReadConnStr()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\linguiniConnStr.txt"))
            {
                return File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\linguiniConnStr.txt");
            } else {
                return Environment.GetEnvironmentVariable("CONNSTR") ?? string.Empty;
            }
        }
    }
}
