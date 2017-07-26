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
    public class TruckOpeningsController : ApiController
    {
        // GET: api/TruckOpenings
        public IEnumerable<TruckOpening> Get()
        {
            var trucks = TruckDataService.Instance.GetTrucks();

            var truckopenings = trucks.SelectMany(t => t.Openings.Select((o, idx) => new TruckOpening()
            {
                Id = idx.ToString(),
                TruckId = t.Id,
                Title = t.Title,
                FoodType = t.FoodType,
                ImageUrl = t.ImageUrl,
                Rating = t.Rating,
                OpeningTime = o.OpeningTime,
                ClosingTime = o.ClosingTime,
                Latitude = o.Latitude,
                Longitude = o.Longitude,
            })).OrderBy(o => o.OpeningTime);
            return truckopenings;
        }
    }
}
