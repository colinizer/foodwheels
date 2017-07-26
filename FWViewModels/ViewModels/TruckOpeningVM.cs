using FWCore.Models;
using FWCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWViewModels.ViewModels
{
    public class TruckOpeningVM : NotificationBase
    {
        public string Id { get; set; }
        public string TruckId { get; set; }
        public string Title { get; set; }
        public string FoodType { get; set; }
        public string ImageUrl { get; set; }
        public int OpeningTime { get; set; }
        public int ClosingTime { get; set; }
        public double Rating { get; set; }        

        public string OpenHoursText
        {
            get
            {
                var ohours = OpeningTime / 60;
                var ominutes = OpeningTime % 60;
                var chours = ClosingTime / 60;
                var cminutes = ClosingTime % 60;
                return string.Format("{0:00}:{1:00} - {2:00}:{3:00}", ohours, ominutes, chours, cminutes);
            }
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
