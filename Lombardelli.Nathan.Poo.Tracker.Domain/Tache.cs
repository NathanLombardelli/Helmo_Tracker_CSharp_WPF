using System;
using System.Collections.Generic;

namespace Lombardelli.Nathan.Poo.Tracker.Domain
{
    public class Tache : ITache
    {
        private string _libelle;
        private string _description;
        private IList<DateTime> _datesPrevues;
        private string _chefProj;
        private IList<DateTime> _datesEffectives;
        private string _nomChantier; //si tache ne possaide pas le nom du chantier, problème lors du tris car par possible de connaitre le chantier de la tache.
        private IList<Commentaire> _commentaires;

        public Tache(string nomChantier,string libelle,string description, DateTime dateDebutPrevu, DateTime dateFinPrevu,string chefProj, DateTime start, DateTime end ,IList<Commentaire> commentaires) {
            _datesPrevues = new List<DateTime>();
            _datesEffectives = new List<DateTime>();
            _commentaires = commentaires;

            _nomChantier = nomChantier;
            _libelle = libelle;
            _description = description;
            _datesPrevues.Add(dateDebutPrevu);
            _datesPrevues.Add(dateFinPrevu);
            _chefProj = chefProj;
            _datesEffectives.Add(start);
            _datesEffectives.Add(end);

        }


        public bool IsChef(string codeChef)
        {

            return codeChef.Equals(_chefProj);

        }

        public string Libelle { get =>_libelle;  }
        public string Description { get =>_description;  }
        public DateTime DateDebutPrevu { get => _datesPrevues[0];  } 
        public DateTime DateFinPrevu { get => _datesPrevues[1];  }
        public string ChefProj { get =>_chefProj;  }

        public void AddComment(string commentaire, string user) => _commentaires.Add(new Commentaire(user, commentaire));
        

        public DateTime Start { get => _datesEffectives[0]; set => _datesEffectives[0] = value; }
        public DateTime End { get => _datesEffectives[1]; set => _datesEffectives[1] = value; }
        public string NomChantier { get =>_nomChantier;  }

        public string Statut
        {
            get
            {
                if (End.Equals(new DateTime()))
                {

                    return "À faire";

                }
                else
                {
                    return "Terminée";
                }
            }

        }

        public IList<Commentaire> Commentaires => _commentaires;

        public string Retard()
        {
            

            if (Statut.Equals("Terminée"))
            {
                return TaskTerminee();

            }
            else
            {
                return TaskNonTerminer();

            }

        }

        private string TaskNonTerminer()
        {
            DateTime actualDate = DateTime.Now;

            TimeSpan difference = actualDate.Subtract(DateFinPrevu);

            if (difference.Days < 0)
            {
                return 0 + "";
            }
            else
            {
                return difference.Days + "";
            }
        }

        private string TaskTerminee()
        {
            TimeSpan difference = End.Subtract(DateFinPrevu);

            if (difference.Days < 0)
            {
                return 0 + "";
            }
            else
            {
                return difference.Days + "";
            }
        }

        public IList<string> getDataCommentaires() 
        {
            if (_commentaires.Count > 0)
            {
                IList<string> list = new List<string>();
                foreach (Commentaire c in _commentaires) 
                {
                    list.Add(c.Auteur);
                    list.Add(c.Text);
                    list.Add(c.Date.ToShortDateString());

                }
                return list;
            }
            else 
            {
                return null;
            }
        }

    }
}
