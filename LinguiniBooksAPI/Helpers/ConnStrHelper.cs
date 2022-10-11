namespace LinguiniBooksAPI.Helpers
{
    public static class ConnStrHelper
    {
        /// <summary>
        /// Reads connection string from docs, git or azure.
        /// </summary>
        /// <returns>The connection string</returns>
        public static string ReadConnStr()
        {
            var connStrDocs = string.Empty;
            var connStrGit = Environment.GetEnvironmentVariable("CONNSTR");
            var connStrAzure = Environment.GetEnvironmentVariable("CONNSTR_MONGODB");

            // HACK: It is made in this specific way for a specific reason, do not change! @Leon-Hobby
            try
            {
                connStrDocs = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\linguiniConnStr.txt");
            }
            catch(Exception ex)
            {
              
            }

            if(!string.IsNullOrEmpty(connStrDocs))
            {
                return connStrDocs;
            } else if(!string.IsNullOrEmpty(connStrGit))
            {
                return connStrGit;
            } else if (!string.IsNullOrEmpty(connStrAzure))
            {
                return connStrAzure;
            }
            return string.Empty;
        }
    }
}
