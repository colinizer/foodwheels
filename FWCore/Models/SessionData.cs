using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Models
{
    public class SessionData
    {
        [JsonProperty("truckOpenings")]
        public IEnumerable<TruckOpening> TruckOpenings { get; set; }

        [JsonProperty("currentTruck")]
        public Truck CurrentTruck { get; set; }
    }
}
