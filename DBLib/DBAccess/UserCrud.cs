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

        #region Gammalt
        public async Task<UserModel> UpdateName(UserModel user)
        {
            var newNameForUser = "nyanamnet";
            var userFound = await GetUserById(await UserNameToId(user.Name));
            // hämta nya namnet någonstans ifrån...
            userFound.Name = newNameForUser;

            return userFound;
        }
        #endregion

        #region Update IsBlocked WORK IN PROGRESS
        public Task UpdateUserBlock(UserModel user)
        {
            var collection = Connect<UserModel>(userCollection);
            var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
            // uppdatera IsBlocked-variabeln
            // returnera uppdaterade usern.
            if (user.IsBlockedAccount = true)
            {
                user.IsBlockedAccount = false;
            }
            else if (user.IsBlockedAccount = false)
            {
                user.IsBlockedAccount = true;
            }
            return collection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });


        }
        #endregion

        #region Update IsSeller
        #endregion

        #region Update IsAdmin
        #endregion

        #region Update IsActive
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
