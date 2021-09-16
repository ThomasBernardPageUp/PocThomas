using System;
using System.Threading.Tasks;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using PoC_Thomas.Services.Interface;

namespace PoC_Thomas.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        public async Task<bool> Authentification()
        {
            bool retunedResultBool = false;

            var avaibility = await CrossFingerprint.Current.IsAvailableAsync();

            if (avaibility)
            {
                var authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Face Id", "We need to unlocked the app"));

                if (authResult.Authenticated)
                {
                    retunedResultBool = true;
                }
                else
                {
                    retunedResultBool = false;

                }

            }

            return retunedResultBool;
        }
    }
}
