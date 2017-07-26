using FWCore.Models;
using FWWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FWWebService.Controllers
{
    public class TrucksController : ApiController
    {
        // GET: api/Truck
        public IEnumerable<Truck> Get()
        {
            return TruckDataService.Instance.GetTrucks();            
        }

        // GET: api/Truck/5
        public Truck Get(string id)
        {
            var truck = TruckDataService.Instance.GetTrucks().Where(t => t.Id.Equals(id)).FirstOrDefault();
            return truck;
        }
    }
}
