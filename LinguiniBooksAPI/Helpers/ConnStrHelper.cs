namespace LinguiniBooksAPI.Helpers
{
    public static class ConnStrHelper
    {
        public static string ReadConnStr()
        {
            return File.ReadAllText((System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\linguiniConnStr.txt"));
        }
    }
}
