using File;
using File.JsonModels;
using Lombardelli.Nathan.Poo.Tracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombardelli.Nathan.Poo.Tracker.Presentation
{

    public class Storage
    {

        private IDictionary<string, User> _users;
        private IDictionary<string,Chantier> _chantiers;
        private JsonModel _jsonModel;

        public Storage() {

            _users = new Dictionary<string, User>();
            _chantiers = new Dictionary<string,Chantier>();


            //load from file.
            _jsonModel = ImportDatas();

            //brute Data
            /* InitUser();
             InitChantier1();
             InitChantier2();
            
             Save();*/

        }

        private JsonModel ImportDatas() 
        {
            _chantiers = Import.ImportChantier();
            _users = Import.ImportChef();
            if (!isEmpty())
            {
                return Import.JsonModel;
            }
            else 
            {
                return null;
            }
            
        }


        public void Save() 
        {
            Export.ExportChantier(_chantiers,_jsonModel);
            Export.ExportChef(_users);
        }

       private void InitUser() {
            //charger le fichier de donnée
            
            _users.Add("D007", new User("DaniBond", "Bond", "Dani"));
            _users.Add("G021", new User("GretaRulez", "Thumberg", "Greta"));
            _users.Add("H042", new User("H2G2", "Hendrikx", "Nicolas"));
            _users.Add("F004", new User("OktoberFest", "Muller", "Gerard"));
            _users.Add("S010", new User("HELMo", "Lamy", "Simon"));

        }

        public bool isEmpty()
        {
            if (this._chantiers.Count == 0) 
            {
                return true;
            }
            return false;
        }

        internal DateTime FindLeastDate()
        {
            DateTime date = DateTime.Now;

            foreach (Chantier c in _chantiers.Values) 
            {
                foreach (Tache t in c.EnumTache()) 
                {
                    if (t.DateDebutPrevu < date) 
                    {
                        date = t.DateDebutPrevu;
                    }
                }
            }

            return date;
        }

        internal DateTime FindTallestDate()
        {
            DateTime date = DateTime.MinValue;

            foreach (Chantier c in _chantiers.Values)
            {
                foreach (Tache t in c.EnumTache())
                {
                    if (t.DateFinPrevu > date)
                    {
                        date = t.DateFinPrevu;
                    }
                    if (t.End > date)
                    {
                        date = t.End;
                    }

                }
            }

            return date;
        }

        private void InitChantier1() {

            //charger le fichier de donnée  

            IList<Tache> taches1 = new List<Tache>
            {
                new Tache("HELMo - Garden Party","A", "Installer electricite", new DateTime(2021, 12, 1), new DateTime(2021, 12, 6), "D007", new DateTime(2021, 12, 1), new DateTime(2021, 12, 6),new List<Commentaire>()),
                new Tache("HELMo - Garden Party","B","Installer eau",new DateTime(2021,12,1),new DateTime(2021,12,5),"G021",new DateTime(2021,12,1),new DateTime(2021,12,6),new List<Commentaire>()),
                new Tache("HELMo - Garden Party","C","Monter scene",new DateTime(2021,12,7),new DateTime(2021,12,10),"H042",new DateTime(),new DateTime(),new List<Commentaire>()),
                new Tache("HELMo - Garden Party","D","Monter bar",new DateTime(2021,12,6),new DateTime(2021,12,11),"H042",new DateTime(),new DateTime(),new List<Commentaire>()),
                new Tache("HELMo - Garden Party","E","Installer eclairage",new DateTime(2021,12,11),new DateTime(2021,12,15),"D007",new DateTime(),new DateTime(),new List<Commentaire>()),
                new Tache("HELMo - Garden Party","F","Valider electricite et hygiene",new DateTime(2021,12,12),new DateTime(2021,12,17),"F004",new DateTime(),new DateTime(),new List<Commentaire>()),
                new Tache("HELMo - Garden Party","G","Repeter les concerts",new DateTime(2021,12,18),new DateTime(2021,12,21),"S010",new DateTime(),new DateTime(),new List<Commentaire>())
            };

            _chantiers.Add("HELMo - Garden Party",new Chantier("HELMo - Garden Party", taches1, new DateTime(2021, 9, 1)));
        }

        internal IList<string> GetRetard()
        {
            IList<string> datas = new List<string>();

            foreach (Chantier c in _chantiers.Values) 
            {
                datas.Add(c.Nom);
                double retard = 0;
                foreach (Tache t in c.EnumTache())
                {

                    String retardTache = t.Retard();

                    retard += int.Parse(retardTache);
                }
                datas.Add(retard.ToString());
            }
            return datas;
        }

        internal IList<double[]> GetDatasGraph(IList<string> periode)
        {
            IList<double[]> datas = new List<double[]>();

            DateTime dateDebut = DateTime.ParseExact(periode[0], "dd-MM-yy", null);
            DateTime dateFin = DateTime.ParseExact(periode[1], "dd-MM-yy", null);

            string resolution = periode[2]; // par Jour, par Semaine, par Mois
           
            int Scale = CalculerScale(dateDebut, dateFin, resolution);//calculer le nbr de jour,semaine,mois entre les 2 dates.
            if (Scale > 0)
            {
                DefinirPosTaches(datas, dateDebut, dateFin, resolution, Scale);
            }
            else
            {
                DefaultData(datas);
            }

            return datas;
            
        }

        private static void DefaultData(IList<double[]> datas)
        {
            datas.Add(new double[1] { 0 }); // nbr de jour,semaine,mois
            datas.Add(new double[1] { 0 });// valeur du jour,de la semaine,du mois
        }

        private void DefinirPosTaches(IList<double[]> datas, DateTime dateDebut, DateTime dateFin, string resolution, int Scale)
        {
            double[] position = new double[Scale];
            double[] valeur = new double[Scale];

            //assignation des positions et valeur de base : 
            InitPosVal(position, valeur);

            foreach (Chantier c in _chantiers.Values) //parcourir tt les chantiers
            {
                IEnumerable<Tache> enumT = c.EnumTache();
                foreach (Tache t in enumT)  //parcourir les taches des chantiers
                {
                    DefinirPosTache(dateDebut, dateFin, resolution, valeur, t);
                }
            }

            datas.Add(position); // nbr de jour,semaine,mois
            datas.Add(valeur);// valeur du jour,de la semaine,du mois
        }

        private static void DefinirPosTache(DateTime dateDebut, DateTime dateFin, string resolution, double[] valeur, Tache t)
        {
            if (t.Statut.Equals("Terminée")) //si la tache est Terminer.(end nn null)
            {
                if (t.End >= dateDebut && t.End <= dateFin)  //si la date de fin est comprise dans la periode demander.
                {
                    TimeSpan difference = t.End - dateDebut;
                    double days = difference.TotalDays;

                    AddToPos(resolution, valeur, days);

                }

            }
        }

        private static void AddToPos(string resolution, double[] valeur, double days)
        {
            
            switch (resolution) //incrémenter a la bonne position de veleur.
            {

                case "par Jour":
                    ByDays(valeur, days);
                    break;
                case "par Semaine":
                    ByWeek(valeur, days);
                    break;
                case "par Mois":
                    ByMonth(valeur, days);
                    break;
            };
        }

        private static void ByMonth(double[] valeur, double days)
        {
            int pos = (int)Math.Ceiling((days / 30));
            if (pos == 0) { pos = 1; }
            valeur[pos - 1]++;
        }

        private static void ByWeek(double[] valeur, double days)
        {
            int pos = (int)Math.Ceiling((days / 7));
            if (pos == 0) { pos = 1; }
            valeur[pos - 1]++;
        }

        private static void ByDays(double[] valeur, double days)
        {
            int pos = (int)days;
            if (pos == 0) { pos = 1; }
            valeur[pos - 1]++;
        }

        private static void InitPosVal(double[] position, double[] valeur)
        {
            for (int i = 0; i < position.Length; i++)
            {
                position[i] = i + 1;
                valeur[i] = 0;
            }
        }

        private static int CalculerScale(DateTime dateDebut, DateTime dateFin, string resolution)
        {

            TimeSpan difference = dateFin - dateDebut;

            double days = difference.TotalDays;

            return resolution switch
            {
                "par Jour" => Convert.ToInt32(days),
                "par Semaine" => Convert.ToInt32(days) / 7,
                "par Mois" => Convert.ToInt32(days) / 30,
                _ => Convert.ToInt32(days),
            };
        }


        internal void AddCommentToTask(string nomChantier, string nomTache, string Commentaire, string user)
        {
            _chantiers[nomChantier].AddCommentToTask(nomTache, Commentaire, user);
        }

        internal IList<string> GetDataComentaire(string nomChantier, string nomTache)
        {
            IList<string> list = new List<string>();

            if (_chantiers.ContainsKey(nomChantier)) 
            {
                IEnumerable<Tache> enumTaches = _chantiers[nomChantier].EnumTache();
                foreach (Tache t in enumTaches) 
                {
                    if (t.Libelle.Equals(nomTache)) 
                    {
                        list = t.getDataCommentaires();
                    }
                }
            }

            return list;
        }

       private void InitChantier2()
        {

            IList<Tache> taches2 = new List<Tache>
            {

                new Tache("Mariage Claudy et Claudette","A","Monter tonnelle",new DateTime(2021,12,5),new DateTime(2021,12,6),"H042",new DateTime(2021,12,6),new DateTime(2021,12,8),new List<Commentaire>()),
                new Tache("Mariage Claudy et Claudette","B","Installer electricite",new DateTime(2021,12,5),new DateTime(2021,12,7),"D007",new DateTime(2021,12,5),new DateTime(2021,12,6),new List<Commentaire>()),
                new Tache("Mariage Claudy et Claudette","C","Installer electricite",new DateTime(2021,12,8),new DateTime(2021,12,9),"H042",new DateTime(),new DateTime(),new List<Commentaire>()),
                new Tache("Mariage Claudy et Claudette","D","Installer eclairage",new DateTime(2021,12,8),new DateTime(2021,12,9),"D007",new DateTime(),new DateTime(),new List<Commentaire>()),
                new Tache("Mariage Claudy et Claudette","E","Tester avec DJ",new DateTime(2021,12,10),new DateTime(2021,12,10),"S010",new DateTime(),new DateTime(),new List<Commentaire>())

            };

            _chantiers.Add("Mariage Claudy et Claudette", new Chantier("Mariage Claudy et Claudette", taches2, new DateTime(2021, 12, 5)));

        }

        public void SetDebutTacheToday(string nomChantier, string nomTache) 
        {
            IEnumerable<Chantier> enumChantier = EnumChantier();

            foreach (Chantier c in enumChantier) 
            {
                if(c.Nom.Equals(nomChantier)) {
                    c.SetDebutTacheToday(nomTache);
                }
            }

        }


        internal void SetFinTacheToday(string nomChantier, string nomTache)
        {
            IEnumerable<Chantier> enumChantier = EnumChantier();

            foreach (Chantier c in enumChantier)
            {
                if (c.Nom.Equals(nomChantier))
                {
                    c.SetFinTacheToday(nomTache);
                }
            }
        }

        public void AddUser(string code, User utilisateur)=> _users.Add(code, utilisateur);

        public bool CodeExist(string code) =>_users.ContainsKey(code);


        public bool CheckUserMdp(string code, string mdp)
        {
            if (_users.ContainsKey(code.ToUpper())) //authorise maj et minuscule pour l'identifiant
            {
                return _users[code.ToUpper()].CheckMdp(mdp);
            }
            else 
            {
                return false;
            }
           
        }


        public IEnumerable<Chantier> EnumChantier()
        {
            foreach (Chantier c in this._chantiers.Values) 
            {

                yield return c;

            }
        }



        public IList<string> GetData(string code, string codeFiltre,string codeTri, string filtreData)
        {

            IList<Tache> taches = new List<Tache>();

            ///////////recuperer les taches lier a l'utilisateur//////////////
            GetTacheUtilisateur(code, taches);

            //////////////Filter la liste////////////

            taches = FilterList(codeFiltre, filtreData, taches);

            /////////////////trier la liste de taches////////////////////////

            taches = codeTri switch
            {
                "Chantier ^" => TriCChantier(taches),
                "Chantier v" => TriDChantier(taches),
                "Statut ^" => TriCStatut(taches),
                "Statut v" => TriDStatut(taches),
                _ => TriCDateDebut(taches),
            };

            return GetDataToDisplay(taches);

        }

        private IList<Tache> FilterList(string codeFiltre, string filtreData, IList<Tache> taches)
        {
            if (filtreData != null && !filtreData.Equals(""))
            {
                taches = FindFilter(codeFiltre, filtreData, taches);

            }

            return taches;
        }

        private IList<Tache> FindFilter(string codeFiltre, string filtreData, IList<Tache> taches)
        {
            switch (codeFiltre)
            {
                case "Chantier":
                    taches = FiChantier(taches, filtreData);
                    break;

                case "Date":
                    taches = FiDate(taches, filtreData);
                    break;

                case "Statut":
                    taches = FiStatut(taches, filtreData); ;
                    break;

            }

            return taches;
        }

        private void GetTacheUtilisateur(string code, IList<Tache> taches)
        {
            IEnumerable<Chantier> EChantier = EnumChantier();
            //parcourir les Chantier 
            foreach (Chantier chantier in EChantier)
            {
                //parcourir les Tâches
                IEnumerable<Tache> ETache = chantier.EnumTache();

                foreach (Tache tache in ETache)
                {

                    //si IsChef
                    if (tache.IsChef(code))
                    {
                        //récupère toute les taches.
                        taches.Add(tache);
                    }


                }

            }
        }

        private static IList<string> GetDataToDisplay(IList<Tache> taches)
        {

            //récupères les données a afficher.

            IList<string> datas = new List<string>();

            for (int i = 0; i < taches.Count; i++)
            {
                //get datas
                GetDatas(taches, datas, i);

            }

            return datas;
        }

        private static void GetDatas(IList<Tache> taches, IList<string> datas, int i)
        {
            datas.Add(taches[i].NomChantier);
            datas.Add(taches[i].Libelle);
            datas.Add(taches[i].Description);
            datas.Add(taches[i].DateDebutPrevu.ToString("yyyy-MM-dd"));
            datas.Add(taches[i].DateFinPrevu.ToString("yyyy-MM-dd"));

            datas.Add(taches[i].Statut);//Status

            AddStart(taches, datas, i);

            AddEnd(taches, datas, i);

            datas.Add(taches[i].Retard()); //retard
        }

        private static void AddEnd(IList<Tache> taches, IList<string> datas, int i)
        {
            if (!taches[i].End.Equals(new DateTime()))
            {
                datas.Add(taches[i].End.ToString("yyyy-MM-dd"));
            }
            else
            {
                datas.Add("Indéfinies");
            }
        }

        private static void AddStart(IList<Tache> taches, IList<string> datas, int i)
        {
            if (!taches[i].Start.Equals(new DateTime()))
            {
                datas.Add(taches[i].Start.ToString("yyyy-MM-dd"));
            }
            else
            {
                datas.Add("Indéfinies");
            }
        }

        private IList<Tache> FiStatut(IList<Tache> taches, string filtreData)
        {
            IList<Tache> result = new List<Tache>();

            for (int i = 0;i<taches.Count;i++) 
            {

                if (taches[i].Statut.Equals(filtreData)) 
                {

                    result.Add(taches[i]);
                
                }
            
            }
            

            return result;
        }

        private IList<Tache> FiDate(IList<Tache> taches, string filtreData)
        {
            IList<Tache> result = new List<Tache>();

            DateTime dateDone = DateTime.Parse(filtreData);
            dateDone = new DateTime(dateDone.Year,dateDone.Month,dateDone.Day);

            for (int i = 0; i < taches.Count; i++)
            {

                if (taches[i].DateDebutPrevu <= dateDone && taches[i].DateFinPrevu >= dateDone)
                {

                    result.Add(taches[i]);

                }

            }


            return result;
        }

        private IList<Tache> FiChantier(IList<Tache> taches, string filtreData)
        {
            IList<Tache> result = new List<Tache>();

            for (int i = 0; i < taches.Count; i++)
            {

                if (taches[i].NomChantier.Equals(filtreData))
                {

                    result.Add(taches[i]);

                }

            }


            return result;
        }



        private static IList<Tache> TriDStatut(IList<Tache> taches) => taches.OrderByDescending(x => x.Statut).ToList();

        private static IList<Tache> TriCStatut(IList<Tache> taches) => taches.OrderBy(x => x.Statut).ToList();

        private static IList<Tache> TriDChantier(IList<Tache> taches) => taches.OrderByDescending(x => x.NomChantier).ToList();

        private static IList<Tache> TriCChantier(IList<Tache> taches) => taches.OrderBy(x => x.NomChantier).ToList();

        private static IList<Tache> TriCDateDebut(IList<Tache> taches) => taches.OrderBy(x => x.DateDebutPrevu).ToList();


    }
}
