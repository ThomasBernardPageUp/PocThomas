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
            db = new SQLiteAsyncConnection(_databasePath);
            Console.WriteLine(_databasePath);


            Query("CREATE TABLE IF NOT EXISTS 'UserEntity'('Id' INTEGER NOT NULL, 'Username' TEXT,'Password' TEXT, 'Picture' TEXT,PRIMARY KEY(\"Id\" AUTOINCREMENT));");
            Query("CREATE TABLE IF NOT EXISTS 'CharacterEntity' ('Id' INTEGER NOT NULL, 'IdCreator' INTEGER NOT NULL, 'Name' TEXT, 'Image' TEXT, 'Species' TEXT, 'Origin' TEXT, PRIMARY KEY(\"Id\",\"IdCreator\") );");
        }

        public string GetDataBasePath()
        {
            return _databasePath;
        }


        public async void Query(string query)
        {
            await db.ExecuteAsync(query);
        }


        // Return one user or null
        public async Task<UserEntity> UserConnection(string username, string password)
        {
            UserEntity user = await db.Table<UserEntity>().Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();

            if(user != null)
            {
                Console.WriteLine("User found -> ID = " + user.Id);
            }
            else
            {
                Console.WriteLine("User not found");
            }

            return user;
        }

        // return true if the username is takken
        // return false if the username is free
        public async Task<bool> UsernameExist(string username)
        {
            var result = await db.Table<UserEntity>().Where(u => u.Username == username).FirstOrDefaultAsync();

            if (result == null)
            {
                Console.WriteLine("This username is free");
                return false;
            }
            else
            {
                Console.WriteLine("This username is already takken");
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


        // Return one user with the id in parameter or null
        public async Task<UserEntity> GetUser(long id)
        {
            // var user = await db.QueryAsync<UserEntity>("SELECT * FROM UserEntity WHERE Id =" + id);
            var user = await db.Table<UserEntity>().Where(u => u.Id == id).FirstOrDefaultAsync();

            return user;
        }



        // Delete one character
        // Return true if sucess
        // Else return false
        public async Task<bool> DeleteCharacter(long idCharacter, long idCreator)
        {
            try
            {
                await db.Table<CharacterEntity>().Where(c => c.Id == idCharacter && c.IdCreator == idCreator).DeleteAsync();
                Console.WriteLine("Deleting Success");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deleting error");
                return false;
            }
        }

        // Return true if the user has already save this character
        // Else return false
        public async Task<bool> CheckCharacter(long id, long idCreator)
        {
            var result = await db.Table<CharacterEntity>().Where(c => c.Id == id && c.IdCreator == idCreator).FirstOrDefaultAsync();

            if(result == null)
            {
                return false;
            }
            else
            {
                Console.WriteLine("This character already exist");
                return true;
            }
        }


        // return the list of all character of the user 
        public async Task<List<CharacterEntity>> GetCharacters(long id)
        {
            var result = await db.Table<CharacterEntity>().Where(c => c.IdCreator == id).ToListAsync();

            return result;
        }


    }
}
