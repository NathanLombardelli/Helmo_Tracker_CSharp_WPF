using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.JsonModels
{
    public class JsonChantier
    {

        private string _nom;
        private IList<JsonTache> _taches;
        private DateTime _dateDebut;

        public JsonChantier(string nom, IList<JsonTache> taches, DateTime dateDebut)
        {
            Nom = nom;
            Taches = taches;
            DateDebut = dateDebut;
        }

        public string Nom { get => _nom; set => _nom = value; }
        public DateTime DateDebut { get => _dateDebut; set => _dateDebut = value; }
        public IList<JsonTache> Taches { get => _taches; set => _taches = value; }
    }
}
