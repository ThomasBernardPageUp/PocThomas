using System;
using System.Net.Http;

namespace PoC_Thomas.Helpers.Interface
{
    public interface IHttpClientHelper
    {
        HttpClient GetHttpClient();
    }
}
