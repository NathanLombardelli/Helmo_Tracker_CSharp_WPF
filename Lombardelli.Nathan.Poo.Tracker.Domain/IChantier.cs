using System;
using System.Collections.Generic;

namespace Lombardelli.Nathan.Poo.Tracker.Domain
{
    public interface IChantier
    {

        public IList<Tache> taches { get; }

        public IEnumerable<Tache> EnumTache();

        public string Nom{ get; }

        public DateTime DateDebut { get; }

        public void TriCDateDebut();
        void AddCommentToTask(string nomTache, string commentaire, string user);
    }
}
