using FWCore.Models;
using FWCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWViewModels.ViewModels
{
    public class TruckVM : NotificationBase
    {
        public TruckVM()
        {
            RatingChangedCommand = new Command((o) =>
            {
                var rating = (int)o;
                this.Rating = rating;
                ViewModelLocator.MainVM.SendTruckRatingExecutor.ExecuteAsync(this);
            }, () => true);
        }

        private bool _IsFavorite;

        public bool IsFavorite
        {
            get { return _IsFavorite; }
            set { NotifySet(ref _IsFavorite, value); }
        }

        private bool _IsPinned;

        public bool IsPinned
        {
            get { return _IsPinned; }
            set { NotifySet(ref _IsPinned, value); }
        }

        public Truck Truck { get; set; }

        public Command RatingChangedCommand { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public string FoodType { get; set; }

        public string ImageUrl { get; set; }

        private double _Rating;

        public double Rating
        {
            get { return _Rating; }
            set { NotifySet(ref _Rating, value); }
        }


        public List<OpeningVM> Openings { get; set; }

        public List<FoodItemVM> FoodItems { get; set; }

    }
}
