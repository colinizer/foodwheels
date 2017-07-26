using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Models
{
    public class Truck
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("foodType")]
        public string FoodType { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("openings")]
        public List<Opening> Openings { get; set; }

        [JsonProperty("foodItems")]
        public List<FoodItem> FoodItems { get; set; }
    }
}
