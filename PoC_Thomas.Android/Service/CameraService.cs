using System;
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

        public void TakePictureAsync()
        {
            Console.WriteLine("android");
        }

    }
}
