using System;
using System.Threading.Tasks;

namespace PoC_Thomas.Services.Interface
{
    public interface ICameraService
    {
        Task<Task<Plugin.Media.Abstractions.MediaFile>> TakePictureAsync();
    }
}
