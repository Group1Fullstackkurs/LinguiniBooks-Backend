using DBDataAccess.Interfaces;
using DBDataAccess.Models;
using MongoDB.Driver;
using DBDataAccess.Helpers;


namespace DBDataAccess.DBAccess
{
    public class UserCrud
    {
        private readonly string connectionString;
        private const string DBName = "BookStore";
        private const string userCollection = "Users";
        private PwdHelper pwdHelper = new();
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


        // Create
        public Task CreateUser(UserModel user)
        {
            var collection = Connect<UserModel>(userCollection);
            user.Id = String.Empty;
            user.BoughtBooks = new List<BookModel>();

            //Pwd creation
            var salt = pwdHelper.GetSalt();
            user.Hash = pwdHelper.GetSaltedHash(user.Hash, salt);
            user.Salt = salt;

            return collection.InsertOneAsync(user);
        }

        // Read
        public async Task<List<UserModel>> GetAllUsers()
        {
            var collection = Connect<UserModel>(userCollection);
            var results = await collection.FindAsync(hej => true);
            return results.ToList().OrderBy(x => x.Name).ToList();
        }

        public async Task<UserModel> GetUserByName(string name, string pwd)
        {
            var user = await GetUserById(await UserNameToId(name));
            if (pwdHelper.IsPwdValid(user, pwd)) {
                return user;
            } else
            {
                return null;
            }
        }

        #region Work in progress
        public async Task<UserModel> UpdateName(UserModel user)
        {
            var newNameForUser = "";
            var userFound = await GetUserById(await UserNameToId(user.Name));
            // hämta nya namnet någonstans ifrån...
            userFound.Name = newNameForUser;

            return userFound;
        }
        #endregion

        #region Other work in progress trying to update user...
        public Task UpdateUser(UserModel user)
        {
            var colleciton = Connect<UserModel>(userCollection);
            var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
            return colleciton.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert= true });
        }
        #endregion


        //public async Task<UserModel> GetUserByName(string name, string pwd);
        //{
        //    var collection = Connect<UserModel>(userCollection);
        //    return (await collection.FindAsync(u => u.Name == name)).FirstOrDefault();

        //}

        // Do not use outside of CRUD
        protected private async Task<UserModel> GetUserById(string id)
        {
            var collection = Connect<UserModel>(userCollection);
            return (await collection.FindAsync(u => u.Id == id)).FirstOrDefault();
        }
        protected private async Task<string> UserNameToId(string name)
        {
            var collection = Connect<UserModel>(userCollection);
            return(await collection.FindAsync(u => u.Name == name)).FirstOrDefault().Id;
        }
    }
}
