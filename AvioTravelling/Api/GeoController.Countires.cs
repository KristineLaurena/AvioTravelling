using AutoMapper;
using AvioTravelling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AvioTravelling.Validation;

namespace AvioTravelling.Api
{
    public partial class GeoController : ApiController
    {
        [HttpGet]
        public IEnumerable<Country> Countries()
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    return session.Query<Country>()
                        .OrderBy(x => x.Id).ToList();
                }
            }
        }

        [Obsolete]
        [HttpGet]
        public Country CountryModel()
        {
            return new Country();
        }

        [HttpPost]
        public Country InsertCountry(Country country)
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    country.Id = String.Empty;
                    session.Store(country);
                    session.SaveChanges();

                    return country;
                }
            }
        }

        [HttpPost]
        public Country UpdateCountry(Country country)
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    var countryDb = session.Load<Country>(country.Id);

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Country, Country>());
                    var mapper = config.CreateMapper();
                    mapper.Map(country, countryDb);

                    countryDb.Validate(new Validators.CountryValidator());

                    session.SaveChanges();

                    return country;
                }
            }
        }

        [HttpGet]
        public Country Country(string id)
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    var country = session.Load<Country>(id);

                    return country;
                }
            }
        }

        [HttpPost]
        public void DeleteCountry(RecordIds records)
        {
            using (var documentStore = Database.NewConnection())
            {
                foreach (var Id in records.Ids)
                {
                    using (var session = documentStore.OpenSession())
                    {
                        var countryDb = session.Load<Country>(Id);

                        session.Delete(countryDb);

                        session.SaveChanges();
                    }
                }

            }
        }


    }
}
