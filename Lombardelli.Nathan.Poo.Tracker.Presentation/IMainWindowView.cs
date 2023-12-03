using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombardelli.Nathan.Poo.Tracker.Presentation
{
    public interface IMainWindowView
    {
        public string UserTxt { set; get; }

        public event EventHandler<IList<string>> UpdateListTask; //[0] = user, [1] = filtre, [2] = tris,[3] = valeur filtre.
        public event EventHandler<IList<string>> UpdateListCommentaires; //[0] = Chantier, [1] = Tache
        public event EventHandler<IList<string>> AddCommentToTask;//[0] = chantier, [1] = Tache, [2] = Commentaire,[3] = utilisateur.
        public event EventHandler<IList<string>> UpdateGraph;
        public event Action UpdateListRetard;

        public IList<TacheSuperviser> ListTaches { set; }
        public Storage Data { set; }
        IList<string> ListCommentaires { set; }
        public IList<double[]> DatasGraph { set; }
        public DateTime DPDebut { set; }
        public DateTime DPFin { set; }
        public string RetardTotalTxt { set; }
        public IList<string> ListRetard { set; }
       
    }

}
