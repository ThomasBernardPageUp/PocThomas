using System;
using PageUpX.Core.Entities;

namespace PoC_Thomas.Models.Entities.Interface
{
    public interface IUserEntity : IPuxEntity
    {
        // All differents property of user exept Id

        string Username { get; set; }
        string Password { get; set; }
        string Picture { get; set; }

    }
}
