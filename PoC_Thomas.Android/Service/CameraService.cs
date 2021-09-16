using System;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using PoC_Thomas.Services.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(PoC_Thomas.Droid.Service.CameraService))]
namespace PoC_Thomas.Droid.Service
{
    public class CameraService : ICameraService
    {
        public CameraService()
        {
        }


        public async Task<Task<MediaFile>> TakePictureAsync()
        {
            var photo = Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            Console.WriteLine("Android");


            return photo;

        }
    }
}
