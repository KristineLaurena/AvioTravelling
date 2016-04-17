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
        public IEnumerable<Showplace> Showplaces()
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    return session.Query<Showplace>()
                        .OrderBy(x => x.Id).ToList();
                }
            }
        }

        [HttpGet]
        public Showplace Showplace(string id)
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    return session.Load<Showplace>(id);
                }
            }
        }

        [HttpGet]
        public string NewShowplaceModel()
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    var showplace = new Showplace();
                    session.Store(showplace);
                    session.SaveChanges();

                    return showplace.Id;
                }
            }
        }

        [HttpPost]
        public void UpdateShowplace(Showplace showplace)
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    var showplaceDB = session.Load<Showplace>(showplace.Id);

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Showplace, Showplace>());
                    var mapper = config.CreateMapper();
                    mapper.Map(showplace, showplaceDB);

                    showplaceDB.Validate(new Validators.ShowplaceValidator());

                    session.SaveChanges();
                }
            }
        }

        [HttpPost]
        public void DeleteShowPlace(Showplace sp)
        {
            using (var documentStore = Database.NewConnection())
            {
                using (var session = documentStore.OpenSession())
                {
                    var showplaceDb = session.Load<Showplace>(sp.Id);

                    session.Delete(showplaceDb);

                    session.SaveChanges();
                }
            }
        }
    }

}
