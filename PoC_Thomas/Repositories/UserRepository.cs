using System;
using PageUpX.DataAccess.DataAccessor;
using PoC_Thomas.Models.Entities;
using PoC_Thomas.Repositories.Interface;

namespace PoC_Thomas.Repositories
{

    // https://bitbucket.org/pageup/pageupx/src/develop/PageUpX.Sample/PageUpX.Samples.Core/Repositories/AddressRepository.cs
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(IPuxSimpleDataAccessor<UserEntity> puxDataAccess) : base(puxDataAccess)
        {
        }
    }
}
