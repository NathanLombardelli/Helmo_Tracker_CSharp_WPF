using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombardelli.Nathan.Poo.Tracker.Presentation
{
    public interface IConnectionView
    {

        public event EventHandler<IList<string>> ConnectionRequest; //[0] = user, [1] = Password.
        public event EventHandler QuitRequested; //notifie le fermeture du programme.
        public Boolean ErrorMessage { set; }

        void Close();
    }
}
