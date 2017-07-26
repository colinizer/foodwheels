using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWViewModels.Interfaces
{
    public interface IToastService
    {
        void DoBasicToast(string Id, string Title, string Message, string ImageUrl);
    }
}
