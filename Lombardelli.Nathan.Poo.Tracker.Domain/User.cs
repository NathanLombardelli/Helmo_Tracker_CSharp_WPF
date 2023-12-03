using System;

namespace Lombardelli.Nathan.Poo.Tracker.Domain
{
    public class User : IUser
    {

        private string _motDePasse;
        private string _name;
        private string _surname;

        
        public User(string mdp,string name, string surname) {
            
            _motDePasse = mdp;
            _name = name;
            _surname = surname;
        
        }

        public string Name
        {
            get => _name; 
            set => _name = value;
        }

        public string Surname
        {
            get => _surname; 
            set => _surname = value;
        }

        public string mdp => _motDePasse;

        public bool CheckMdp(string mdp) {

            return mdp.Equals(_motDePasse);
        
        }

    }
}
