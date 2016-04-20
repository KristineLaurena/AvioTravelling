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

        [HttpPost]
        public void UpdateCity(City city) //city Klasses objekts, no javaScript///   City - class, city - maingais
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    // cityDb - no datubazes
                    var cityDb = session.Load<City>(city.Id);


                    //tika izmantota biblioteka AutoMapper lai automatiski veiktu ipashibas update.
                    //piem. :   cityDB.Name = city.Name => tas ir jaizpilda uz visam ipashibas,
                    //ar rokam tas ir gruti, tapec tiek izmantots AutoMapper
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<City, City>());
                    var mapper = config.CreateMapper();

                    //mapper.Map ( no kur niemt dates,    kura objeta tos ierakstit)
                    mapper.Map(city, cityDb);
                   
                    //Tapec ka select (Web Lappuse dropdown) var stradat tikai ar Id, bet Name ipashibas nemainas,
                    // Vajag iznemt no datubazes entity, ar jauno Id, kura glabasises pareeizie dati ( Name u.t.t.) 
                    if (!string.IsNullOrEmpty(cityDb?.Country?.Id))
                    {
                        cityDb.Country = session.Load<Country>(cityDb.Country.Id);
                    }

                    //FluentValidation
                    // Izveido jaunus likumus, lai lamatos ka kads lauks ir tukshs
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
