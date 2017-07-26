using FWCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace FWApp.Services
{
    public class AppFileService : IAppFileService
    {
        public async Task CreateOrReplaceFile(string Name, string Content)
        {
            var Folder = ApplicationData.Current.LocalFolder;
            var File = await Folder.CreateFileAsync(Name, CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(File, Content);
        }

        public async Task DeleteFile(string Name)
        {
            var Folder = ApplicationData.Current.LocalFolder;
            var File = await Folder.TryGetItemAsync(Name);
            File?.DeleteAsync();
        }

        public async Task<bool> FileExists(string Name)
        {
            var Folder = ApplicationData.Current.LocalFolder;
            var File = await Folder.TryGetItemAsync(Name);
            return File != null;
        }

        public async Task<string> ReadFile(string Name)
        {
            var Folder = ApplicationData.Current.LocalFolder;
            var Item = await Folder.TryGetItemAsync(Name);
            var File = Item as StorageFile;
            if(File != null)
                return await FileIO.ReadTextAsync(File);
            return null;
        }
    }
}
