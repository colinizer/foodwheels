using FWCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FWWebService.Models
{
    public class TruckDataService
    {
        public TruckDataService()
        {

        }

        static TruckDataService()
        {
            InitData();
        }

        private static void InitData()
        {
            var RNG = new Random(DateTime.Now.Millisecond);
            int id = 0;
            // Manhattan
            var Latitude = 40.760333;
            var Longitude = -73.981167;
            foreach (var t in _TruckInfo)
            {
                t.Rating = RNG.NextDouble() * 5;
                var NumOpenings = RNG.Next(3) + 1;
                int StartTime = 8 * 60;
                for (int i = 0; i < NumOpenings; i++)
                {
                    StartTime += RNG.Next(7) * 30;
                    var Closing = StartTime + RNG.Next(7) * 30;

                    t.Openings.Add(new Opening()
                    {
                        Id = (id++).ToString(),
                        OpeningTime = StartTime,
                        ClosingTime = Closing,  
                        Latitude = Latitude + RNG.NextDouble() * .02 - .01,
                        Longitude = Longitude + RNG.NextDouble() * .02 - .01,
                    });

                    StartTime = Closing;
                }
            }

            // Comment out this loop code if you have updated the array of data with your own images
            foreach(var t in _TruckInfo)
            {
                foreach (var fi in t.FoodItems)
                    fi.ImageUrl = "Content/TruckImages/Generic/Dish.jpg";
            }
        }

        public IEnumerable<Truck> GetTrucks()
        {
            return _TruckInfo;
        }

        static Truck[] _TruckInfo = new Truck[]
        {
            new Truck()
            {
                Id = "1",
                Title = "Burger Bar",
                FoodType = "American",
                ImageUrl = "Content/TruckImages/American/trailer_fast_food_vector_burger_van/Fotolia_80310587.jpg",
                Rating = 0.0,
                FoodItems = new List<FoodItem>()
                {
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Freedom Fries",
                        ImageUrl = "Content/TruckImages/American/freedom_fries/Fotolia_43182843.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "A Better Burger",
                        ImageUrl = "Content/TruckImages/American/hamburger/Fotolia_72366541.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Homemade Donuts",
                        ImageUrl = "Content/TruckImages/American/homemade_donuts/Fotolia_66218267.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Piggies",
                        ImageUrl = "Content/TruckImages/American/Homemade_Pigs/Fotolia_71283334.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Classic Dog",
                        ImageUrl = "Content/TruckImages/American/hot_dog/Fotolia_14077875.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Pulled Pork Pile",
                        ImageUrl = "Content/TruckImages/American/pulled_pork_sandwich/Fotolia_64583572.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "The Quarter Master",
                        ImageUrl = "Content/TruckImages/American/Quarter_Pounder/Fotolia_6882692.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Oh So Sweet Fries",
                        ImageUrl = "Content/TruckImages/American/sweet_potato_fries/Fotolia_54581538.jpg",
                    },
                },
                Openings = new List<Opening>()
                {
                }
            },
            new Truck()
            {
                Id = "2",
                Title = "Pete's Pizza",
                FoodType = "Italian",
                ImageUrl = "Content/TruckImages/Italian/trailer_fast_food_vector(pizza)/Fotolia_80310827.jpg",
                Rating = 0.0,
                FoodItems = new List<FoodItem>()
                {
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Garlic Bread",
                        ImageUrl = "Content/TruckImages/Italian/Garlic_bread/Fotolia_68975610.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Gluten-free Pasta",
                        ImageUrl = "Content/TruckImages/Italian/Gluten_free_pasta/Fotolia_77760012.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Meatballs",
                        ImageUrl = "Content/TruckImages/Italian/meatballs/Fotolia_77185968.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Quattro Pizza",
                        ImageUrl = "Content/TruckImages/Italian/Pizza_quattro/Fotolia_62782252.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Mediterranean",
                        ImageUrl = "Content/TruckImages/Italian/Plate_of_Mediterranean/Fotolia_91446021.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Peppy Pizza",
                        ImageUrl = "Content/TruckImages/Italian/slice_of_pepperoni_pizza/Fotolia_1479577.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Tiramismooth",
                        ImageUrl = "Content/TruckImages/Italian/tiramisu/Fotolia_98950543.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Cheese Bread",
                        ImageUrl = "Content/TruckImages/Italian/Toasted_Cheese/Fotolia_63972064.jpg",
                    },
                },
                Openings = new List<Opening>()
                {
                }
            },
            new Truck()
            {
                Id = "3",
                Title = "Dessert Deli",
                FoodType = "Dessert",
                ImageUrl = "Content/TruckImages/Dessert/trailer_fast_food_vector/Fotolia_80310791_Subscription_Yearly_XXL.jpg",
                Rating = 0.0,
                FoodItems = new List<FoodItem>()
                {
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Cookies",
                        ImageUrl = "Content/TruckImages/Dessert/cookies/Fotolia_65804912_Subscription_Yearly_XXL.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Gourmet Cheery Sundae",
                        ImageUrl = "Content/TruckImages/Dessert/Gourmet_cherry/Fotolia_86305687_Subscription_Yearly_XXL.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "The Cone",
                        ImageUrl = "Content/TruckImages/Dessert/Ice_cream_isolated/Fotolia_57842538_Subscription_Yearly_L.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Velvet Cups",
                        ImageUrl = "Content/TruckImages/Dessert/Red_velvet_cupcakes/Fotolia_83431598_Subscription_Yearly_XL.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Chocolate Truffles",
                        ImageUrl = "Content/TruckImages/Dessert/Stack_of_Chocolate/Fotolia_62883667_Subscription_Yearly_XXL.jpg",
                    },
                },
                Openings = new List<Opening>()
                {
                }
            },
            new Truck()
            {
                Id = "4",
                Title = "Japan Jack's",
                FoodType = "Japanese",
                ImageUrl = "Content/TruckImages/Japanese/trailer_fast_food_vector(sushi_van)/Fotolia_80310946.jpg",
                Rating = 0.0,
                FoodItems = new List<FoodItem>()
                {
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Rice Pad",
                        ImageUrl = "Content/TruckImages/Japanese/rice_fast_food/Fotolia_38239568.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Sushi Chocs",
                        ImageUrl = "Content/TruckImages/Japanese/sushi/Fotolia_19002612.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "The Lunch Box",
                        ImageUrl = "Content/TruckImages/Japanese/Sushi_lunch_box-1/Fotolia_29452462.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Tuna Bowl",
                        ImageUrl = "Content/TruckImages/Japanese/Tuna_bowl/Fotolia_96372137.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Inside Out Rolls",
                        ImageUrl = "Content/TruckImages/Japanese/Inside-out_rolls/Fotolia_79640367.jpg",
                    },
                },
                Openings = new List<Opening>()
                {
                }
            },
            new Truck()
            {
                Id = "5",
                Title = "Bread Bonanza",
                FoodType = "Sandwiches",
                ImageUrl = "Content/TruckImages/Sandwiches/trailer_fast_food/Fotolia_80310577.jpg",
                Rating = 0.0,
                FoodItems = new List<FoodItem>()
                {
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Devon Cream Scone",
                        ImageUrl = "Content/TruckImages/Sandwiches/Devonshire/Fotolia_58799742_Subscription_Yearly_XXL.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Grilled Steak",
                        ImageUrl = "Content/TruckImages/Sandwiches/grilled_steak_sandwich/Fotolia_40587929_Subscription_Yearly_XXL.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "The Lean Club",
                        ImageUrl = "Content/TruckImages/Sandwiches/healthy_club/Fotolia_52349340_Subscription_Yearly_XL.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "The Reuben",
                        ImageUrl = "Content/TruckImages/Sandwiches/reuben_sandwich/Fotolia_65814735.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Tomato Soup",
                        ImageUrl = "Content/TruckImages/Sandwiches/Soup_tomato_in_white_bowl/Fotolia_72145255.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Ham Toasty",
                        ImageUrl = "Content/TruckImages/Sandwiches/Toasted_ham_a/Fotolia_80085453.jpg",
                    },
                },
                Openings = new List<Opening>()
                {
                }
            },
            new Truck()
            {
                Id = "6",
                Title = "Pie Party",
                FoodType = "Pie",
                ImageUrl = "Content/TruckImages/Pies/trailer_fast_food_vector_(pie_van)/Fotolia_80310821_Subscription_Yearly_XXL.jpg",
                Rating = 0.0,
                FoodItems = new List<FoodItem>()
                {
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Cornish Pasty",
                        ImageUrl = "Content/TruckImages/Pies/Cornish_pasty/Fotolia_56446673_Subscription_Yearly_XL.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Lime Moose Pie",
                        ImageUrl = "Content/TruckImages/Pies/Dolce_al_c/Fotolia_87880122_Subscription_Yearly_XXL.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Fresh Apple Pie",
                        ImageUrl = "Content/TruckImages/Pies/Fresh_piece/Fotolia_78494783_Subscription_Yearly_XXL.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Home Pie",
                        ImageUrl = "Content/TruckImages/Pies/Homemade_pie/Fotolia_98072786_Subscription_Yearly_XL.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Cheese PI",
                        ImageUrl = "Content/TruckImages/Pies/Homemade_Pie_with_Cheese/Fotolia_79633970_Subscription_Yearly_XXL.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Mini Quiche",
                        ImageUrl = "Content/TruckImages/Pies/Savory_mini_quiche/Fotolia_74108640_Subscription_Yearly_XXL.jpg",
                    },
                },
                Openings = new List<Opening>()
                {
                }
            },
            new Truck()
            {
                Id = "7",
                Title = "Maracas",
                FoodType = "Mexican",
                ImageUrl = "Content/TruckImages/Mexican/trailer_fast_food_vector_(taco_van)/Fotolia_80310954.jpg",
                Rating = 0.0,
                FoodItems = new List<FoodItem>()
                {
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Burrito Man",
                        ImageUrl = "Content/TruckImages/Mexican/chicken_burrito/Fotolia_74128500.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "The Carne",
                        ImageUrl = "Content/TruckImages/Mexican/chile_con_carne/Fotolia_92652982.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Churros",
                        ImageUrl = "Content/TruckImages/Mexican/Churros_on/Fotolia_90259278.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Oy Tamales",
                        ImageUrl = "Content/TruckImages/Mexican/hot_tamales/Fotolia_7208189.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "The Beef",
                        ImageUrl = "Content/TruckImages/Mexican/Mexican_beef/Fotolia_51830140.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Quesadillas",
                        ImageUrl = "Content/TruckImages/Mexican/Quesadillas/Fotolia_71920954.jpg",
                    },
                    new FoodItem()
                    {
                        Id = "1",
                        Title = "Tacos",
                        ImageUrl = "Content/TruckImages/Mexican/Tacos-yellow_c/Fotolia_58459085.jpg",
                    },
                },
                Openings = new List<Opening>()
                {
                }
            },
        };

        public static TruckDataService _Instance;
        public static TruckDataService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TruckDataService();
                }
                return _Instance;
            }
        }

    }
}