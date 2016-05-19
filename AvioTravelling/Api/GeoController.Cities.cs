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
        //api/Geo/Cities
        [HttpGet]
        public IEnumerable<City> Cities()
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    return session.Query<City>()
                        .OrderBy(x => x.Id).ToList();
                }
            }
        }


        [HttpGet]
        public City City(string id)
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    return session.Load<City>(id);
                }
            }
        }

        [HttpGet]
        public string NewCityModel()
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    var city = new City();
                    session.Store(city);
                    session.SaveChanges();

                    return city.Id;
                }
            }
        }

        [HttpGet]
        public IEnumerable<City> CitiesByCode(string code)
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    return session.Query<City>()
                        .Where(x => x.Country != null && x.Country.CountryCode.Equals(code))
                        .OrderBy(x => x.Id).ToList();
                }
            }
        }

        [HttpPost]
        public void UpdateCity(City city) //city Klasses objekts, no javaScript///   City - class, city - maingais
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    var cityDb = session.Load<City>(city.Id);

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<City, City>());
                    var mapper = config.CreateMapper();

                    mapper.Map(city, cityDb);
                   
                    if (!string.IsNullOrEmpty(cityDb?.Country?.Id))
                    {
                        cityDb.Country = session.Load<Country>(cityDb.Country.Id);
                    }
                    else
                    {
                        cityDb.Country = null;
                    }

                    cityDb.Validate(new Validators.CityValidator());

                    session.SaveChanges();
                }
            }
        }

        [Obsolete]
        //Empty model, JSON source for Client Side
        public City NewCity()
        {
            return new City();
        }

        [HttpPost]
        public void DeleteCity(City city)
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    var cityDb = session.Load<City>(city.Id);

                    session.Delete(cityDb);

                    session.SaveChanges();
                }

            }
        }
    }
}
