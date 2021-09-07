using System;
using SQLite.Net.Attributes;

namespace PoC_Thomas.Models.Entities
{

    [Table("CharacterEntity")]
    public class CharacterEntity
    {
        [PrimaryKey, NotNull]
        public long Id { get; set; }
        public long? IdCreator { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Species { get; set; }
        public string Origin { get; set; }

        public CharacterEntity()
        {
        }
    }
}
