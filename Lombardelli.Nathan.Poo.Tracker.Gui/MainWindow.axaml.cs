using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Lombardelli.Nathan.Poo.Tracker.Presentation;
using ScottPlot;
using ScottPlot.Avalonia;
using System;
using System.Collections.Generic;
#nullable disable

namespace Lombardelli.Nathan.Poo.Tracker
{
    public partial class MainWindow : Window, IMainWindowView
    {
        private Storage _datas;

        private TextBlock _UserTB;
        private ListBox _listTaches;
        private ComboBox _TrisCB;
        private ComboBox _FiltreCB;
        private TextBox _ValFiltreTB;
        private ListBox _listCommentaires;
        private TextBox _Commentaire;
        private AvaPlot _graph;
        private DatePicker _debutDP;
        private DatePicker _finDP;
        private ComboBox _resolutionCB;
        private TextBlock _totalL;
        private ListBox _listRetard;

        public event EventHandler<IList<string>> UpdateListTask;
        public event EventHandler<IList<string>> UpdateListCommentaires;
        public event EventHandler<IList<string>> AddCommentToTask;
        public event EventHandler<IList<string>> UpdateGraph;
        public event Action UpdateListRetard;

        public List<string> _tacheSelected;//[0] = chantier, [1] = Tache

        public MainWindow()
        {
            _tacheSelected = new List<string>();
            InitializeComponent();
            LocateControls(); //initialize les composant utiles de la fenêtre
            
#if DEBUG   
            this.AttachDevTools();
#endif
           
        }

        private void LocateControls()
        {
            LocateTextBlock();

            LocateListBox();

            LocateComboBox();

            LocateTextBox();

            LocateDatePicker();

            _graph = this.Find<AvaPlot>("AvaPlot1");

            InitComboBox();

        }

        private void LocateTextBlock()
        {
            _UserTB = this.Find<TextBlock>("UserTB");
            _totalL = this.Find<TextBlock>("TotalL");
        }

        private void LocateDatePicker()
        {
            _debutDP = this.Find<DatePicker>("DebutDP");
            _finDP = this.Find<DatePicker>("FinDP");
        }

        private void LocateTextBox()
        {
            _ValFiltreTB = this.Find<TextBox>("ValFiltreTB");
            _Commentaire = this.Find<TextBox>("Commentaire");
        }

        private void LocateComboBox()
        {
            _TrisCB = this.Find<ComboBox>("TrisCB");
            _FiltreCB = this.Find<ComboBox>("FiltreCB");
            _resolutionCB = this.Find<ComboBox>("ResolutionCB");
        }

        private void LocateListBox()
        {
            _listTaches = this.Find<ListBox>("ListTaches");
            _listCommentaires = this.Find<ListBox>("ListCommentaires");
            _listRetard = this.Find<ListBox>("ListRetard");
        }

        public DateTime DPDebut 
        {

            set 
            {
                _debutDP.SelectedDate = new DateTimeOffset(value);
            }
        
        }

        public DateTime DPFin
        {

            set
            {
                _finDP.SelectedDate = new DateTimeOffset(value);
            }

        }

        private void Initdata()
        {
            IList<string> listFiltre = new List<string> { _UserTB.Text, "", "", "" };
            UpdateListTask?.Invoke(this, listFiltre); //lancement de l'event qui dans MainSuperviser initialise la liste de tâches.
            UpdateListRetard();
        }


        public IList<TacheSuperviser> ListTaches
        {
            set 
            {
                IList<TacheSuperviser> ListTacheSup = value;
                IList<TacheView> ListTacheView = new List<TacheView>();

                foreach (TacheSuperviser Ts in ListTacheSup)
                {
                    AddTacheView(ListTacheView, Ts);
                }

                _listTaches.Items = ListTacheView;
                _listTaches.SelectionChanged += ListTaches_SelectionChanged;

            }
        }

        private void AddTacheView(IList<TacheView> ListTacheView, TacheSuperviser Ts)
        {
            Ts.UpdateTache += UpdateTaskList; //abonement a l'évenement.
            Ts.Data = _datas; //Ajout des données.
            TacheView Tv = new TacheView();
            Ts.TacheView = Tv;
            ListTacheView.Add(Tv);
        }

        public IList<double[]> DatasGraph 
        {

            set 
            {
                var mainPlot = new Plot();

                var bars = mainPlot.AddBar(value[1], value[0]); //valeur, position.

                bars.ShowValuesAboveBars = true;
                
                mainPlot.Title("Tâches Terminées");
                mainPlot.SetAxisLimits(yMin: 0);
                mainPlot.SetAxisLimits(xMin: 0);

                _graph.Reset(mainPlot);
                _graph.Refresh();
            }
        
        }

        private void ListTaches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)//si la selection de dèselectionne alors il n'y a plus de selection.(sinon erreur lors du tris/filtre ou Commencer/Terminer des taches.)
            { 
                _tacheSelected.Clear();//suppression ancienne taches selected.
                TacheView tacheSelected = (TacheView)e.AddedItems[0]; //get le 1er élément sélectionner.
                _tacheSelected.Add(tacheSelected.Chantier);
                _tacheSelected.Add(tacheSelected.Tache);
                IList<string> dataTacheSelected = new List<string> { tacheSelected.Chantier, tacheSelected.Tache }; //création de la list de donnée a donnée lors de l'event.
                UpdateListCommentaires(this, dataTacheSelected); //lance l'event qui dans mainSuperviser update la liste des commentaires.
            }
            else 
            {
                _listCommentaires.Items = new List<CommentaireView>(); // clear la liste si pas de selection.
            }
        }

        private void UpdateTaskList(object? sender, EventArgs e)
        {
            IList<string> listFiltre = new List<string>();

            listFiltre.Add(UserTxt);

            VerifFiltreNull(listFiltre);

            VerifTriNull(listFiltre);

            listFiltre.Add(_ValFiltreTB.Text);

            UpdateListTask(this, listFiltre); // lancer l'event pour lancer un get data.
            RefreshGraph(); //Rafréchir le Graph
            UpdateListRetard(); // mettre a jour la list des retard
        }

        private void VerifTriNull(IList<string> listFiltre)
        {
            if (_TrisCB.SelectedItem != null)
            {
                listFiltre.Add(_TrisCB.SelectedItem.ToString());
            }
            else
            {
                listFiltre.Add("");
            }
        }

        private void VerifFiltreNull(IList<string> listFiltre)
        {
            if (_FiltreCB.SelectedItem != null)
            {
                listFiltre.Add(_FiltreCB.SelectedItem.ToString());
            }
            else
            {
                listFiltre.Add("");
            }
        }

        public string UserTxt
        {
            set
            {
                _UserTB.Text = value.ToUpper();
                Initdata(); //quand ont connait l'utilisateur, initialiser ses données
            }

            get => _UserTB.Text;
        }

        public string RetardTotalTxt
        {
            set
            {
                _totalL.Text = value.ToUpper();
                
            }
        }

        public Storage Data { set => _datas = value; }

        public IList<string> ListRetard
        {
            set
            {
                IList<string> datasRetard = value;
                int nbrRetard = datasRetard.Count / 2; // 2 donnée par Retard (chantier/retard)
                IList<RetardView> ListRetardView = new List<RetardView>();

                for (int i = 0; i < nbrRetard; ++i)
                {
                    AddRetardView(datasRetard, ListRetardView, i);
                }
                _listRetard.Items = ListRetardView;
            }
        }

        private static void AddRetardView(IList<string> datasRetard, IList<RetardView> ListRetardView, int i)
        {
            int pos = i * 2;

            RetardView rv = new RetardView
            {
                Chantier = datasRetard[pos + 0],
                Retard = datasRetard[pos + 1]
            };


            ListRetardView.Add(rv);
        }

        public IList<string> ListCommentaires 
        { 
            set 
            {
                IList<string> datasCommentaires = value;
                int nbrCommentaire = datasCommentaires.Count/3; // 3 donnée par commentaires
                IList<CommentaireView> ListComView = new List<CommentaireView>();
                for (int i = 0;i< nbrCommentaire;++i)
                {
                    AddCommentaireView(datasCommentaires, ListComView, i);
                }
                _listCommentaires.Items = ListComView;
            } 
        }

        private static void AddCommentaireView(IList<string> datasCommentaires, IList<CommentaireView> ListComView, int i)
        {
            int pos = i * 3;
            CommentaireView cm = new CommentaireView
            {
                Utilisateur = datasCommentaires[pos + 0],
                Commentaire = datasCommentaires[pos + 1],
                Date = datasCommentaires[pos + 2]
            };

            ListComView.Add(cm);
        }

        private void InitComboBox()
        {
            _TrisCB.Items = new List<string> {"","Chantier ^","Chantier v","Statut ^","Statut v"};
            _FiltreCB.Items = new List<string> {"","Chantier", "Date", "Statut" };
            _resolutionCB.Items = new List<string> { "par Jour","par Semaine","par Mois"};
        }


        private void Filtrer_Click(object? sender, RoutedEventArgs args) {
            UpdateTaskList(this,EventArgs.Empty);
        }

        
        private void Refresh_Click(object? sender, RoutedEventArgs args)
        {
            RefreshGraph();
        }

        private void RefreshGraph()
        { 
            string debut = _debutDP.SelectedDate.ToString().Substring(0, 8);
            string fin = _finDP.SelectedDate.ToString().Substring(0, 8);
            string resolution = (string)_resolutionCB.SelectedItem;

            IList<string> datas = new List<string>
            {
                debut,fin,resolution,
            };

            UpdateGraph(this, datas);
        }

        private void Publier_Click(object? sender, RoutedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(_Commentaire.Text)) {
                IList<string> TaskAndComment = new List<string>
                {
                    _tacheSelected[0],//ajout chantier.
                    _tacheSelected[1],//ajout Tache.
                    _Commentaire.Text, //ajout Commentaire.
                    _UserTB.Text//ajout utilisateur.
                };

                AddCommentToTask(this, TaskAndComment);
                UpdateListCommentaires(this, _tacheSelected);
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
    }
}
