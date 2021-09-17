using System;
using System.Threading.Tasks;

namespace PoC_Thomas.Services.Interface
{
    public interface IAuthentificationService
    {
        Task<bool> Authentification();
    }
}
