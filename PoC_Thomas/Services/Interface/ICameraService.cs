using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PoC_Thomas.Services.Interface
{
    public interface ICameraService
    {
        Task<object> TakePhotoAsync();
    }
}
