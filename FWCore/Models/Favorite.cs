using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Models
{
    public class Favorite
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
    }
}
