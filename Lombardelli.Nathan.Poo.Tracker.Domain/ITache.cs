using System;
using System.Collections.Generic;

namespace Lombardelli.Nathan.Poo.Tracker.Domain
{
    public interface ITache
    {

        public string Libelle { get; }
        public string Description { get; }
        public DateTime DateDebutPrevu { get; }
        public DateTime DateFinPrevu { get; }
        public string ChefProj { get; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string NomChantier { get; }
        public string Statut { get; }
        public IList<Commentaire> Commentaires { get; } // pour serialization

        public bool IsChef(string codeChef);
        public IList<string> getDataCommentaires();
        public void AddComment(string commentaire, string user);
        

            public string Retard();


    }
}
