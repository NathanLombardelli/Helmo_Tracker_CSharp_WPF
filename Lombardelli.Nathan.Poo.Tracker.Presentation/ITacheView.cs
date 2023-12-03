using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombardelli.Nathan.Poo.Tracker.Presentation
{
    public interface ITacheView
    {

        public event EventHandler<IList<string>> EndRequest; //notifie que la tâche prend fin. (string = nom de la tache)
        public event EventHandler<IList<string>> StartRequest; //notifie que la tâche commence. (string = nom de la tache)

        public string Chantier { set; get; }
        public string Tache { set; get; }
        public string DatePrevue { set; }
        public string Commencer { set; }
        public string Terminer { set; }
        public string NbJourRetard { set; }
        public string Description { set; }

        public Boolean TerminerEnabled { set; }
        public Boolean CommencerEnabled { set; }

    }
}
