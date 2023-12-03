using Lombardelli.Nathan.Poo.Tracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.JsonModels
{
    public class JsonTache
    {

        private string _libelle;
        private string _description;
        private DateTime _dateDebutPrevu;
        private DateTime _dateFinPrevu;
        private string _chefProj;
        private DateTime _start;
        private DateTime _end;
        private string _nomChantier;
        private string _statut;
        private IList<Commentaire> _commentaires;
        private int _dureeExecution;
        private IList<string> _requiredTasks;



        public JsonTache(string libelle, string description, DateTime dateDebutPrevu, DateTime dateFinPrevu, string chefProj, DateTime start, DateTime end, string nomChantier, string statut, IList<Commentaire> commentaires, int dureeExecution, IList<string> requiredTasks)
        {
            Libelle = libelle;
            Description = description;
            DateDebutPrevu = dateDebutPrevu;
            DateFinPrevu = dateFinPrevu;
            ChefProj = chefProj;
            Start = start;
            End = end;
            NomChantier = nomChantier;
            Statut = statut;
            Commentaires = commentaires;
            DureeExecution = dureeExecution;
            RequiredTasks = requiredTasks;
        }


        public string Libelle { get => _libelle; set => _libelle = value; }
        public string Description { get => _description; set => _description = value; }
        public DateTime DateDebutPrevu { get => _dateDebutPrevu; set => _dateDebutPrevu = value; }
        public DateTime DateFinPrevu { get => _dateFinPrevu; set => _dateFinPrevu = value; }
        public string ChefProj { get => _chefProj; set => _chefProj = value; }
        public DateTime Start { get => _start; set => _start = value; }
        public DateTime End { get => _end; set => _end = value; }
        public string NomChantier { get => _nomChantier; set => _nomChantier = value; }
        public string Statut { get => _statut; set => _statut = value; }
        public IList<Commentaire> Commentaires { get => _commentaires; set => _commentaires = value; }
        public int DureeExecution { get => _dureeExecution; set => _dureeExecution = value; }
        public IList<string> RequiredTasks { get => _requiredTasks; set => _requiredTasks = value; }

    }
}
