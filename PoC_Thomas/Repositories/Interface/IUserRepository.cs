using System;
using PageUpX.DataAccess.Repository;
using PoC_Thomas.Models.Entities;

namespace PoC_Thomas.Repositories.Interface
{

     //https://bitbucket.org/pageup/pageupx/src/develop/PageUpX.Sample/PageUpX.Samples.Core/Repositories/Interfaces/IAddressRepository.cs
    public interface IUserRepository : IPuxSimpleRepository<UserEntity>
    {

    }
}
