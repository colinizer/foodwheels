using FWCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Services
{
    public class TruckService
    {
        static string _UrlRoot = "http://localhost:18552/";
        public static string UrlRoot {  get { return _UrlRoot; } }
        public async static Task<IEnumerable<TruckOpening>> GetTruckOpenings()
        {
            var HC = new HttpClient();
            var Text = await HC.GetStringAsync(_UrlRoot + "api/truckopenings");
            var Openings = JsonConvert.DeserializeObject<IEnumerable<TruckOpening>>(Text);
            await Task.Delay(2000);
            return Openings;


        }

        public async static Task<Truck> GetTruck(string Id)
        {
            var HC = new HttpClient();
            var Text = await HC.GetStringAsync(_UrlRoot + "api/trucks/" + Id);
            var Truck = JsonConvert.DeserializeObject<Truck>(Text);
            await Task.Delay(1500);
            return Truck;
        }

        public async static Task SendRating(string TruckId, int Rating)
        {
            var HC = new HttpClient();
            var Url = _UrlRoot + "api/truckrating/" + TruckId;

            var Response = await HC.PutAsync(Url, new StringContent("\"" + Rating.ToString() + "\"", Encoding.UTF8, "application/json"));

            await Task.Delay(1000);
        }
    }
}
