using System;
using PageUpX.DataAccess.DataAccessor;
using PoC_Thomas.Models.Entities;
using PoC_Thomas.Repositories.Interface;

namespace PoC_Thomas.Repositories
{

    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(IPuxSimpleDataAccessor<UserEntity> puxDataAccess) : base(puxDataAccess)
        {
        }
    }
}
