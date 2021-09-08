using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PoC_Thomas.Models.Entities;
using SQLite;

namespace PoC_Thomas.Helpers.Interface
{
    public class SqliteNetHelper : ISqliteNetHelper
    {
        private readonly string _databasePath;
        public SQLiteAsyncConnection db { get; set; }


        public SqliteNetHelper()
        {
            _databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");
            Console.WriteLine(_databasePath);
            db = new SQLiteAsyncConnection(_databasePath);
        }

        public string GetDataBasePath()
        {
            return _databasePath;
        }


        public async void Query(string query)
        {
            await db.ExecuteAsync(query);
        }

        public async Task<UserEntity> UserConnection(string username, string password)
        {
            UserEntity user = await db.Table<UserEntity>().Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();

            return user;
        }

        // return true if the username is takken
        // return false if the username is free
        public async Task<bool> UsernameExist(string username)
        {
            var result = await db.QueryAsync<UserEntity>("SELECT Id FROM UserEntity WHERE Username = '" + username + "';");

            if (result == null || result.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // return true if the user is created
        // else return false
        public async Task<bool> CreateUser(string username, string password, string picture)
        {
            int res = await db.ExecuteAsync("INSERT INTO UserEntity (Username, Password, Picture) VALUES ('" + username + "', '" + password + "', '" + picture + "')");

            if (res == 0)
            {
                Console.WriteLine("Error, can't create this user");
                return false;
            }
            else
            {
                Console.WriteLine("User " + username + " is created");
                return true;
            }
        }


        public async Task<UserEntity> GetUser(long id)
        {
            var user = await db.QueryAsync<UserEntity>("SELECT * FROM UserEntity WHERE Id =" + id);

            return user[0];
        }


        public async Task<bool> DeleteCharacter(long IdCharacter, long IdCreator)
        {
            int result = await db.ExecuteAsync("DELETE FROM CharacterEntity WHERE Id =" + IdCharacter + " AND IdCreator = " + IdCreator);

            if(result == 0)
            {
                Console.WriteLine("Deleting Error, " + result + " row(s) modified");
                return false;

            }
            else
            {
                Console.WriteLine("Deleting Success, " + result + " row(s) modified, character deleted");
                return true;
            }
        }

        public async Task<CharacterEntity> GetCharacter(long id, long idCreator)
        {
            string query = "SELECT * FROM CharacterEntity WHERE CharacterEntity.Id =" + id + " AND CharacterEntity.IdCreator = " + idCreator;
            var result = await db.FindWithQueryAsync<CharacterEntity>(query);

            return result;
        }

        public async Task<List<CharacterEntity>> GetCharacters(long id)
        {
            string query = "SELECT CharacterEntity.Id, CharacterEntity.IdCreator, CharacterEntity.Image, CharacterEntity.Name, CharacterEntity.Origin, CharacterEntity.Species FROM CharacterEntity INNER JOIN UserEntity ON CharacterEntity.IdCreator = UserEntity.Id WHERE UserEntity.Id = " + id;
            var result = await db.QueryAsync<CharacterEntity>(query);

            return result;
        }


    }
}
