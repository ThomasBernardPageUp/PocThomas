using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using PoC_Thomas.Services.Interface;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(PoC_Thomas.Droid.Service.CameraService))]
namespace PoC_Thomas.Droid.Service
{
    public class CameraService : ICameraService
    {


        public async Task<string> TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                
                return ImageSource.FromFile(photo.FullPath).ToString().Remove(0, 6);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }

            
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo != null)
            {
                // save the file into local storage
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            }
        }
    }
}
