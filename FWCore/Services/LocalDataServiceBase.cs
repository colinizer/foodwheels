using FWCore.Interfaces;
using FWCore.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Services
{
    public abstract class LocalDataServiceBase<T>
    {

        protected async static Task SaveData(string DataFileName, T Favourites)
        {
            var AFS = Repository.GetObject<IAppFileService>();

            var Text = JsonConvert.SerializeObject(Favourites);
            await AFS.CreateOrReplaceFile(DataFileName, Text);
        }

        protected async static Task<T> LoadData(string DataFileName)
        {
            var AFS = Repository.GetObject<IAppFileService>();
            var Text = await AFS.ReadFile(DataFileName);
            if (Text == null)
                return default(T);
            return JsonConvert.DeserializeObject<T>(Text);
        }

        protected async static Task ClearData(string DataFileName)
        {
            var AFS = Repository.GetObject<IAppFileService>();
            await AFS.DeleteFile(DataFileName);
        }
    }
}
