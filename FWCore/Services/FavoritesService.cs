using FWCore.Interfaces;
using FWCore.Models;
using FWCore.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Services
{
    public class FavoritesService : LocalDataServiceBase<IEnumerable<Favorite>>
    {
        public static Task Save(IEnumerable<Favorite> Favorites)
        {
            return SaveData(FavoritesFileName, Favorites);
        }

        public static Task<IEnumerable<Favorite>> Load()
        {
            return LoadData(FavoritesFileName);
        }

        public static Task Clear()
        {
            return ClearData(FavoritesFileName);
        }

        private static string FavoritesFileName = "favs.json";
    }

}
