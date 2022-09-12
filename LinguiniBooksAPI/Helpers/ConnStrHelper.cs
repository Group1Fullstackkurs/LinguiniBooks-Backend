namespace LinguiniBooksAPI.Helpers
{
    public static class ConnStrHelper
    {
        public static string ReadConnStr()
        {
            try
            {
                return File.ReadAllText((System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\linguiniConnStr.txt"));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return String.Empty;

        }
    }
}
