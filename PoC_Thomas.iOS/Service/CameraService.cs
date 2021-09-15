using System;
using System.Threading.Tasks;
using PoC_Thomas.iOS.Service;
using PoC_Thomas.Services.Interface;
using Xamarin.Forms;


[assembly: Dependency(typeof(PoC_Thomas.iOS.Service.CameraService))]
namespace PoC_Thomas.iOS.Service
{
    public class CameraService : ICameraService
    {
        public CameraService()
        {
        }

        public async Task<Task<Plugin.Media.Abstractions.MediaFile>> TakePictureAsync()
        {
            var photo = Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            Console.WriteLine("IOS");

            return photo;
        }

    }
}
