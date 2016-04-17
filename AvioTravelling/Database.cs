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
    }
}