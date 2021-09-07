using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLite;
using System.IO;
using SQLite.Net.Async;
using System.Threading.Tasks;

namespace PoC_Thomas.Models.Entities
{

    [Table("UserEntity")]
    public class UserEntity
    {
        [PrimaryKey, AutoIncrement, NotNull]
        [Column("Id")]
        public long Id { get; set; }

        [Column("Username")]
        public string Username { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Picture")]
        public string Picture { get; set; }


        public UserEntity()
        {
        }
    }
}
