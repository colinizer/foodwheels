using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWViewModels.Interfaces
{
    public interface INavigationService
    {
        string CurrentPageName { get; }
        void GotoPage(string PageType);
        void GoBack();
        Task SaveFrame();
        Task RestoreFrame();
    }
}
