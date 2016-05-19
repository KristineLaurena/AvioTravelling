using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AvioTravelling
{
    public class ApiClient
    {
        public HttpClient _http { get; set; }
        public ApiClient()
        {
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _http.Timeout = new TimeSpan(0, 10, 0);

        }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return _http.GetAsync(requestUri);
        }
    }
}