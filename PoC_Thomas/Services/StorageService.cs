using System;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PoC_Thomas.Services.Interface;

namespace PoC_Thomas.Services
{
    public class StorageService : IStorageService
    {
        public async Task<string> PickImagesAsync()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                var mediaOptions = new PickMediaOptions() { PhotoSize = PhotoSize.Large };
                var selectedPicture = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                return selectedPicture.Path;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            
        }
    }
}
