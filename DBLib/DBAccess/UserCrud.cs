using DBDataAccess.Models;
using MongoDB.Driver;


namespace DBDataAccess.DBAccess
{
    public class UserCrud
    {
        private readonly string connectionString;
        private const string DBName = "BookStore";
        private const string userCollection = "Users";

        public UserCrud(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private IMongoCollection<T> Connect<T>(in string collection)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(DBName);
            return db.GetCollection<T>(collection);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var collection = Connect<UserModel>(userCollection);
            var results = await collection.FindAsync(hej => true);
            return results.ToList().OrderBy(x => x.Name).ToList();
        }

        public async Task<UserModel> GetUser(string id)
        {
            var collection = Connect<UserModel>(userCollection);
            return (await collection.FindAsync(u => u.Id == id)).FirstOrDefault();
        }
    }
}
