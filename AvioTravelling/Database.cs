using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AvioTravelling
{
    public static class Database
    {
        public static IDocumentStore NewConnection()
        {
            return new DocumentStore
            {
                Url = ConfigurationManager.ConnectionStrings["RavenDB"].ConnectionString,
                DefaultDatabase = ConfigurationManager.AppSettings["DefaultDatabase"]
            }.Initialize();
        }

        public static void AddQueryParams(this UriBuilder uriBuilder, params KeyValuePair<string, string>[] pairs)
        {
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (var pair in pairs)
            {
                query.Add(pair.Key, pair.Value);
            }

            uriBuilder.Query = query.ToString();
        }

        public static KeyValuePair<string, string> AddValue(this string key, object value)
        {
            return new KeyValuePair<string, string>(key, value.ToString());
        }
    }
}