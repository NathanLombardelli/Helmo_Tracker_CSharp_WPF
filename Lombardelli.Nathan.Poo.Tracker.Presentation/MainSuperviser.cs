using File;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombardelli.Nathan.Poo.Tracker.Presentation
{
    public class MainSuperviser
    {
        private IMainWindowView _mainWindowView;
        private Storage _datas;

        public ObservableCollection<string> Taches { get; } //taches a afficher (lier à l'user)

        public MainSuperviser(IMainWindowView mainWindow, Storage storage)
        {
            _mainWindowView = mainWindow;
            _datas = storage; //création du présentation model + récupération des données du fichier
            SubscribeToViewsEvents();

            SetDataToView();//donner les données a la vue pour pouvoir les donner a la vue des taches.
            InitDatePicker();
        }

        private void InitDatePicker()
        {
            _mainWindowView.DPDebut = FindDPDebut();
            _mainWindowView.DPFin = FindDPFin();
        }

        private DateTime FindDPDebut()
        {
            if (!_datas.isEmpty()) 
            { 
                return _datas.FindLeastDate(); 
            } 
            else 
            { 
                return DateTime.Today; 
            }
           
        }

        private DateTime FindDPFin()
        {
            if (!_datas.isEmpty()) 
            { 
                return _datas.FindTallestDate(); 
            } 
            else 
            { 
                return DateTime.Today; 
            }
            
        }

        private void SetDataToView() => _mainWindowView.Data = _datas;

        private void SubscribeToViewsEvents()
        {
            //abonnement aux évènements de main
            _mainWindowView.UpdateListTask += UpdateListTask;
            _mainWindowView.UpdateListCommentaires += UpdateListCommentaire;
            _mainWindowView.AddCommentToTask += AddCommentToTask;
            _mainWindowView.UpdateGraph += UpdateGraph;
            _mainWindowView.UpdateListRetard += UpdateListRetard;
           
        }

        private void UpdateListRetard()
        {
            IList<string> retards = _datas.GetRetard();
            _mainWindowView.ListRetard = retards;

            int retardTot = 0;

            int nbrRetard = retards.Count / 2; // 2 donnée par Retard (chantier/retard)


            for (int i = 0; i < nbrRetard; ++i)
            {
                int pos = i+1;
                if ( i != 0) {
                    pos = i + 2;
                }

                retardTot += int.Parse(retards[pos]);

            }

                _mainWindowView.RetardTotalTxt = retardTot.ToString();
        }

        private void AddCommentToTask(object sender, IList<string> e) => _datas.AddCommentToTask(e[0],e[1],e[2],e[3]);

        private void UpdateListCommentaire(object sender, IList<string> infosTaskSelected)
        {
            if (infosTaskSelected.Count != 0) {
                IList<string> commentairesDatas = _datas.GetDataComentaire(infosTaskSelected[0], infosTaskSelected[1]);

                if (commentairesDatas != null)
                {
                    _mainWindowView.ListCommentaires = commentairesDatas;

                }
                else
                {
                    _mainWindowView.ListCommentaires = new List<string>();
                }
            }
        }


        private void UpdateListTask(object sender, IList<string> paramData)
        {
            IList<string> listDatas = _datas.GetData(paramData[0], paramData[1], paramData[2], paramData[3]);

            IList<TacheSuperviser> listTacheSuperviser = new List<TacheSuperviser>(); // création de la list des TachesSuperviser

            int nbrTaches = listDatas.Count / 9; //une tache = 9 données

            InitDataSuperviser(listDatas, listTacheSuperviser, nbrTaches); //initialise les données des superviser.

            _mainWindowView.ListTaches = listTacheSuperviser;

        }

        private void UpdateGraph(object sender, IList<string> paramData)
        {
            _mainWindowView.DatasGraph = _datas.GetDatasGraph(paramData);
        }

        private static void InitDataSuperviser(IList<string> listDatas, IList<TacheSuperviser> listTacheSuperviser, int nbrTaches)
        {
            for (int i = 0; i < nbrTaches; i++)
            {
                TacheSuperviser superviser = new(); // Création du superviser de la Tâche.
                int pos = i * 9;

                InitSuperviser(listDatas, superviser, pos);

                listTacheSuperviser.Add(superviser);
            }
        }

        private static void InitSuperviser(IList<string> listDatas, TacheSuperviser superviser, int pos)
        {
            superviser.ChantierT = listDatas[pos + 0];
            superviser.TacheT = listDatas[pos + 1];
            superviser.DescriptionT = listDatas[pos + 2];
            superviser.DatePrevueT = "Du " + listDatas[pos + 3] + " au " + listDatas[pos + 4];

            DateDebut(listDatas, superviser, pos);

            DateFin(listDatas, superviser, pos);

            superviser.NbJourRetardT = listDatas[pos + 8];
        }

        private static void DateFin(IList<string> listDatas, TacheSuperviser superviser, int pos)
        {
            if (!listDatas[pos + 7].Equals("Indéfinies"))
            {
                superviser.TerminerT = "Terminer le " + listDatas[pos + 7];
            }
            else
            {
                superviser.TerminerT = "Terminer";
            }
        }

        private static void DateDebut(IList<string> listDatas, TacheSuperviser superviser, int pos)
        {
            if (!listDatas[pos + 6].Equals("Indéfinies"))
            {
                superviser.CommencerT = "Commencer le " + listDatas[pos + 6];
            }
            else
            {
                superviser.CommencerT = "Commencer";
            }
        }
    }
}
