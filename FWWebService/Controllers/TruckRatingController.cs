using FWWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FWWebService.Controllers
{
    public class TruckRatingController : ApiController
    {
        // GET: api/TruckRating
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TruckRating/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TruckRating
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TruckRating/5
        public void Put(string id, [FromBody]string value)
        {
            var truck = TruckDataService.Instance.GetTrucks().Where(t => t.Id.Equals(id)).FirstOrDefault();
            truck.Rating = int.Parse(value);
        }

        // DELETE: api/TruckRating/5
        public void Delete(int id)
        {
        }
    }
}
