using System;
using System.Collections.Generic;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using PoC_Thomas.Models.Entities.Interface;

namespace PoC_Thomas.Models.Entities
{

    // https://bitbucket.org/pageup/pageupx/src/develop/PageUpX.Samples.Model/Entities/AddressEntity.cs
    public class UserEntity : IUserEntity
    {
        [PrimaryKey, AutoIncrement, NotNull]
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Picture { get; set; }




        public UserEntity()
        {
        }

        public UserEntity(string username, string password, string picture):this()
        {
            this.Username = username;
            this.Password = password;
            this.Picture = picture;
        }
    }
}
