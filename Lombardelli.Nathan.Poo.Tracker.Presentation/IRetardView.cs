using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombardelli.Nathan.Poo.Tracker.Presentation
{
    public interface IRetardView
    {

        public string Chantier { set; }
        public string Retard { set; get; }

    }
}
