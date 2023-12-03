using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombardelli.Nathan.Poo.Tracker.Domain
{
    public class Commentaire
    {
        private string _titre;
        private string _text;
        private string _auteur;
        private DateTime _date;

        public Commentaire(string auteur,string text) 
        {
            _auteur = auteur;
            _text = text;
            _date = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd'T'hh:mm:ss"));
        }


        public string Titre
        {
            get => _titre;
        }

        public string Text 
        {
            get => _text;
        }

        public string Auteur
        {
            get => _auteur;
        }

        public DateTime Date
        {
            get => _date;
        }


    }
}
