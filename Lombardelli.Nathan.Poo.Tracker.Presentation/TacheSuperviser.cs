using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombardelli.Nathan.Poo.Tracker.Presentation
{
    public class TacheSuperviser
    {

        private Storage _datas;
        private ITacheView _tacheView;

        //j'enregistres les données de la vue car la vue est ajouter après la création du présenter dans le main qui ne connait pas les données.
        private string _chantierT;
        private string _tacheT;
        private string _descriptionT;
        private string _datePrevueT;
        private string _commencerT;
        private string _terminerT;
        private string _nbJourRetardT;


        private void SubscribeToViewsEvents()
        {
            //abonnement aux évènements de _tacheView
            _tacheView.StartRequest += SetStartDateToTask;
            _tacheView.EndRequest += SetEndDateToTask;

        }



        private void SetEndDateToTask(object sender, IList<string> tache)
        {
            _datas.SetFinTacheToday(tache[0], tache[1]);
            _commencerT = DateTime.Today.ToString();
            UpdateTache(this, EventArgs.Empty); // notifier la mise a jour des tache a la mainWindow
        }

        private void SetStartDateToTask(object sender, IList<string> tache)
        {
            _datas.SetDebutTacheToday(tache[0],tache[1]);
            _terminerT = DateTime.Today.ToString();
            UpdateTache(this, EventArgs.Empty); // notifier la mise a jour des tache a la mainWindow
        }

        public event EventHandler UpdateTache;
       
        public ITacheView TacheView
        {
            set
            {
                InitDataView(value); //donne a la vue ses données.
                SubscribeToViewsEvents();
            }

            get => _tacheView;


        }

        private void InitDataView(ITacheView value)
        {
            _tacheView = value;
            _tacheView.Chantier = _chantierT;
            _tacheView.Tache = _tacheT;
            _tacheView.Description = _descriptionT;
            _tacheView.DatePrevue = _datePrevueT;
            _tacheView.Commencer = _commencerT;
            _tacheView.Terminer = _terminerT;
            _tacheView.NbJourRetard = _nbJourRetardT;
        }

        public string ChantierT
        {
            set => _chantierT = value;
        }

        public string TacheT
        {
            set => _tacheT = value;
        }

        public string DescriptionT
        {
            set => _descriptionT = value;
        }

        public string DatePrevueT
        {
            set => _datePrevueT = value;
        }

        public string CommencerT
        {
            set => _commencerT = value;
            get => _commencerT;
        }

        public string TerminerT
        {
            set => _terminerT = value;
            get => _terminerT;
        }

        public string NbJourRetardT
        {
            set => _nbJourRetardT = value;
        }

        public Storage Data
        {
            set
            {
                _datas = value;
            }
        }
    }
}
