namespace LinguiniBooksAPI.Helpers
{
    public static class ConnStrHelper
    {
        public static string ReadConnStr()
        {
            var connStr = Environment.GetEnvironmentVariable("CONNSTR");
            var connStrAzure = Environment.GetEnvironmentVariable("CONNSTR_MONGODB");

            if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\linguiniConnStr.txt")) 
            {
                File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\linguiniConnStr.txt");
            } else if(connStr != "" && connStr != null)
            {
                return connStr;
            } else if (connStrAzure != "" && connStrAzure != null){
                return connStrAzure;
            }
            return string.Empty;
                
                
 
        }
    }
}
