using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using PoC_Thomas.Models.Business;

namespace PoC_Thomas.Helpers.Interface
{
    public interface IDataTransferHelper
    {
        Task<DataTransferHandlerResult<TResult>> SendClientAsync<TResult>
            (string route, HttpMethod httpMethod, object jsonContent = null) where TResult : class;
    }
}
