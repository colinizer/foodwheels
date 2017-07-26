using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Models
{
    public class Opening
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("openingTime")]
        public int OpeningTime { get; set; }

        [JsonProperty("closingTime")]
        public int ClosingTime { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }
}
