using FWViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FWCore.Utils;

namespace FWApp.Services
{
    public class KnownExceptionHandler : IKnownExceptionHandler
    {
        public async Task HandleException(KnownException Exception)
        {
            var DS = Repository.GetObject<IDialogService>();
            await DS.ShowDialogAsync("Error", Exception.Message, "OK", null);
        }
    }
}
