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

        /// <summary>
        /// CREATE. Creates a user and adds it to the database.
        /// </summary>
        /// <param name="user">The user object to insert into collection.</param>
        /// <returns>A collection with the added user.</returns>
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

        /// <summary>
        /// READ. Gets all users from database.
        /// </summary>
        /// <returns>A collection of all users in database.</returns>
        public async Task<List<UserModel>> GetAllUsers()
        {
            var collection = Connect<UserModel>(userCollection);
            var results = await collection.FindAsync(hej => true);
            return results.ToList().OrderBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="name">The name of the user.</param>
        /// <param name="pwd">The password of the user.</param>
        /// <returns>A user object</returns>
        public async Task<UserModel> GetUserByName(string name, string pwd)
        {
            var user = await GetUserById(await UserNameToId(name));
            if (pwdHelper.IsPwdValid(user, pwd))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// UPDATE. Updates the user object.
        /// </summary>
        /// <param name="user">The user to be updated.</param>
        /// <returns>Returns true if successful</returns>
        public async Task<bool> UpdateUser(UserModel user, string pwd)
        {
            var oldUser = await GetUserById(user.Id);
            if(!pwdHelper.IsPwdValid(oldUser, pwd) ||oldUser == null) 
            {
                return false;
            }
            if(oldUser.Hash != user.Hash)
            {
                //Pwd creation
                var salt = pwdHelper.GetSalt();
                user.Hash = pwdHelper.GetSaltedHash(user.Hash, salt);
                user.Salt = salt;
            }

            var collection = Connect<UserModel>(userCollection);
            var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
            await collection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
            return true;
        }

        /// <summary>
        /// UPDATE. Updates the user's bought books.
        /// </summary>
        /// <param name="id">The active user's id.</param>
        /// <param name="pwd">The active user's password</param>
        /// <param name="book">The bought books</param>
        /// <returns>Returns true if successful</returns>
        public async Task<bool> BuyBook(string id, string pwd, BookModel[] book)
        {
            var user = await GetUserById(id);
            if (!pwdHelper.IsPwdValid(user, pwd) || user == null)
            {
                return false;
            }

            for (int i = 0; i < book.Length; i++)
            {
                if (book[i].Stock > 0)
                {
                    book[i].Stock--;
                }
            }

            user.BoughtBooks = user.BoughtBooks.Concat(book);

            return await UpdateUser(user, pwd);
        }


        // ================================================
        // Do not use the two below methods outside of CRUD
        // ================================================

        /// <summary>
        /// Gets a user by id.
        /// </summary>
        /// <param name="id">The id of the user to be found.</param>
        /// <returns>The first user with matching id.</returns>
        protected private async Task<UserModel> GetUserById(string id)
        {
            var collection = Connect<UserModel>(userCollection);
            return (await collection.FindAsync(u => u.Id == id)).FirstOrDefault();
        }

        /// <summary>
        /// Finds the id of a specific user.
        /// </summary>
        /// <param name="name">The name of the user whose id is to be found.</param>
        /// <returns>The id of a matching user</returns>
        protected private async Task<string> UserNameToId(string name)
        {
            var collection = Connect<UserModel>(userCollection);
            return (await collection.FindAsync(u => u.Name == name)).FirstOrDefault().Id;
        }
    }
}
