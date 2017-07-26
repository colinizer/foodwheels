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

    public class SessionDataService : LocalDataServiceBase<SessionData>
    {
        public static Task Save(SessionData Data)
        {
            return SaveData(FavoritesFileName, Data);
        }

        public static Task<SessionData> Load()
        {
            return LoadData(FavoritesFileName);
        }

        private static string FavoritesFileName = "session.json";
    }

}
