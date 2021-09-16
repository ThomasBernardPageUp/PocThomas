using System;
using System.IO;
using System.Threading.Tasks;

namespace PoC_Thomas.Services.Interface
{
    public interface IStorageService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
