using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWViewModels.Interfaces
{
    public interface ITilePinService
    {
        bool IsPinned(string Id);
        Task<bool> PinTile(string Id, string Title, string ImageUri);
        Task UnPinTile(string id);
    }
}
