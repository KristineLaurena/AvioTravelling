using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using AvioTravelling.Validation;
using AvioTravelling.Models;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Configuration;

namespace AvioTravelling.Api
{
    public partial class GeoController : ApiController
    {
        [HttpGet]
        public async Task<IEnumerable<Book>> Books(string cityId)
        {
            var client = new ApiClient();
            var config = new Parameters();

            UriBuilder uriBuilder = new UriBuilder(config.Url);
            uriBuilder.AddQueryParams(config.ParamaterName.AddValue(cityId), config.PageSize.AddValue(config.PageSizeParam));

            //expected result: http://104.131.35.197/basic/web/index.php?r=book/getbyparam&cityId=cities/14721&pageSize=2
            //ParameterName: "cityId"
            //PageSize: "pageSize"
            //PageSizeParam: 10
            
            var url = uriBuilder.ToString();

            var result = await client.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var responseString = result.Content.ReadAsStringAsync().Result;
                var bookResult = responseString.ConvertJSonToObject<BookResponse>();

                return bookResult.Books;
            }
            else
            {
                return new List<Book>();
            }
        }
    }

    public class Parameters
    {
        public string PageSize { get; set; }
        public string PageSizeParam { get; set; }
        public string ParamaterName { get; set; }
        public string Url { get; set; }

        //read data from Web.config
        public Parameters()
        {
            this.PageSizeParam = ConfigurationManager.AppSettings["integration:books:pageSizeParam"];
            this.PageSize = ConfigurationManager.AppSettings["integration:books:pageSize"];
            this.ParamaterName = ConfigurationManager.AppSettings["integration:books:parameterName"];
            this.Url = ConfigurationManager.AppSettings["integration:books:get"];
        }
    }

    [DataContract]
    public class BookResponse
    {
        [DataMember(Name = "data")]
        public Book[] Books { get; set; }

    }
}