using FWCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWViewModels.Interfaces
{
    public interface IKnownExceptionHandler
    {
        Task HandleException(KnownException Exception);
    }
}
