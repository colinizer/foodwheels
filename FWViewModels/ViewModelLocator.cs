using FWViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {

        }

        private static MainVM _MainVM;

        public static MainVM MainVM
        {
            get
            {
                if (_MainVM == null)
                    _MainVM = new MainVM();
                return _MainVM;
            }
        }

    }
}
