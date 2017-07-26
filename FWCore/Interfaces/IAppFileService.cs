using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWCore.Interfaces
{
    public interface IAppFileService
    {
        Task CreateOrReplaceFile(string Name, string Content);
        Task<string> ReadFile(string Name);
        Task<bool> FileExists(string Name);
        Task DeleteFile(string Name);
    }
}
