using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombardelli.Nathan.Poo.Tracker.Domain
{
    public interface IUser
    {


        public string Name
        {
            get;
            set;
        }

        public string Surname
        {
            get;
            set;
        }

         public string  mdp // pour serialization.
        {
            get;
        }

        public bool CheckMdp(string mdp);



    }
}
