using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Models
{
    public class TruckOpening
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("truckId")]
        public string TruckId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("foodType")]
        public string FoodType { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

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
