using System;
using System.Net.Http;
using PoC_Thomas.Helpers.Interface;

namespace PoC_Thomas.Helpers
{
    public class HttpClientHelper : IHttpClientHelper
    {

        //This service is user to create and return the HttpClient

        //You can use the handler to configure server certificates.


        private readonly HttpClient _client;

        public HttpClientHelper()
        {
            HttpClientHandler handler = new HttpClientHandler();
            _client = new HttpClient(handler);
        }

        public HttpClient GetHttpClient()
        {
            return _client;
        }
    }
}
