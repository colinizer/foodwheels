using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWViewModels.ViewModels
{
    public class OpeningVM
    {
        public int OpeningTime { get; set; }
        public int ClosingTime { get; set; }
        public string OpenHoursText
        {
            get
            {
                var ohours = OpeningTime / 60;
                var ominutes = OpeningTime % 60;
                var chours = ClosingTime / 60;
                var cminutes = ClosingTime % 60;
                return string.Format("{0:00}:{1:00} - {2:00}:{3:00}", ohours, ominutes, chours, cminutes);
            }
        }
    }
}
