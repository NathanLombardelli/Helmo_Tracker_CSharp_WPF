using System;
using System.Collections.Generic;
using System.Linq;

namespace Lombardelli.Nathan.Poo.Tracker.Domain
{
    public class Chantier : IChantier
    {

        private string _nom;
        private IList<Tache> _taches;
        private DateTime _dateDebut;

        public Chantier(string nom,IList<Tache> taches,DateTime dateDebut) {

            _nom = nom;
            _taches = taches;
            _dateDebut = dateDebut;
        
        }

        public IEnumerable<Tache> EnumTache() 
        {

            for (int index = 0; index < this._taches.Count; index++)
            {

                yield return this._taches[index];

            }

        }

        public string Nom { get =>_nom;  }
        public DateTime DateDebut { get => _dateDebut; }

        public IList<Tache> taches => _taches;

        public void TriCDateDebut()
        {
            _taches = _taches.OrderBy(x => x.DateDebutPrevu).ToList();
           
        }

        public void SetDebutTacheToday(string nomTache)
        {
            IEnumerable<Tache> enumTache = EnumTache();

            foreach (Tache t in enumTache) 
            {
                if (t.Libelle.Equals(nomTache)) 
                {
                    t.Start = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd'T'hh:mm:ss"));
                }
            }

        }

        public void SetFinTacheToday(string nomTache)
        {
            IEnumerable<Tache> enumTache = EnumTache();

            foreach (Tache t in enumTache)
            {
                if (t.Libelle.Equals(nomTache))
                {
                    t.End = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd'T'hh:mm:ss"));
                }
            }
        }

        public void AddCommentToTask(string nomTache, string commentaire, string user)
        {
            IEnumerable<Tache> enumTache = EnumTache();

            foreach (Tache t in enumTache)
            {
                if (t.Libelle.Equals(nomTache)) 
                {
                    t.AddComment(commentaire, user);
                    return;
                }
            }

        }
    }
}
