using System;
using System.IO;
using System.Threading.Tasks;
using PoC_Thomas.iOS.Service;
using PoC_Thomas.Services.Interface;
using Xamarin.Essentials;
using Xamarin.Forms;


[assembly: Dependency(typeof(PoC_Thomas.iOS.Service.CameraService))]
namespace PoC_Thomas.iOS.Service
{
    public class CameraService : ICameraService
    {


        public async Task<object> TakePhotoAsync()
        {
            Console.WriteLine("Camera not available on simulator");
            return null;
        }


    }
}
