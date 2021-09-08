using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PoC_Thomas.Models.Entities;
using SQLite;

namespace PoC_Thomas.Helpers.Interface
{
        public interface ISqliteNetHelper
        {

        #region properties
        SQLiteAsyncConnection db { get; set; }
        #endregion

            string GetDataBasePath();
            void Query(string query);
            Task<UserEntity> UserConnection(string username, string password);
            Task<bool> UsernameExist(string username);
            Task<bool> CreateUser(string username, string password, string picture);
            Task<bool> DeleteCharacter(long IdCharacter, long IdCreator);
            Task<CharacterEntity> GetCharacter(long id, long idCreator);
            Task<List<CharacterEntity>> GetCharacters(long id);
        }

}
