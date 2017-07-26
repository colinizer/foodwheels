using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWViewModels.Interfaces
{
    public interface IDialogService
    {
        Task<int> ShowDialogAsync(string title, string caption, string button1, string button2);
    }
}
