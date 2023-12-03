using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Lombardelli.Nathan.Poo.Tracker.Presentation;
using System;
using System.Collections.Generic;
#nullable disable

namespace Lombardelli.Nathan.Poo.Tracker
{
    public partial class TacheView : UserControl,ITacheView
    {

        private TextBlock _ChantierTB;
        private TextBlock _TacheTB;
        private TextBlock _DatePrevueTB;
        private Button _CommencerBt;
        private Button _TerminerBt;
        private TextBlock _NbJourTB;
        private TextBlock _DescriptionTB;

        public TacheView()
        {
            
            InitializeComponent();
            LocateControls(); //initialize les composant utiles de la fenêtre

        }

        public string Chantier { set => _ChantierTB.Text = value; get => _ChantierTB.Text; }
        public string Tache { set => _TacheTB.Text = value; get => _TacheTB.Text; }
        public string DatePrevue { set => _DatePrevueTB.Text = value; }
        public string Commencer { 
            set 
            { 
                _CommencerBt.Content = value;
                if (!value.Equals("Commencer")) { //si set de date.
                    _CommencerBt.IsEnabled = false; //grisser Commencer.
                    _TerminerBt.IsEnabled = true; // dégriser Terminer.
                }
            } 
        }
        public string Terminer
        {
            set
            {
                _TerminerBt.Content = value;
                if (!value.Equals("Terminer")) //si set de date.
                {
                    _TerminerBt.IsEnabled = false; //grisser Terminer.
                }
                
            }
        }

        public string NbJourRetard { set => _NbJourTB.Text = value; }
        public string Description { set => _DescriptionTB.Text = value; }
        public bool TerminerEnabled { set => _TerminerBt.IsEnabled = value; }
        public bool CommencerEnabled { set => _CommencerBt.IsEnabled = value; }

        public event EventHandler<IList<string>> EndRequest;    //[0] = nomChantier , [1] = nom tache car un meme nomde tache peut apparêtre dans plusieurs chantiers
        public event EventHandler<IList<string>> StartRequest; //[0] = nomChantier , [1] = nom tache car un meme nomde tache peut apparêtre dans plusieurs chantiers

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void LocateControls()
        {
            //faire setter pour chaque.
            _ChantierTB = this.FindControl<TextBlock>("ChantierTB");
            _TacheTB = this.FindControl<TextBlock>("TacheTB");
            _DatePrevueTB = this.FindControl<TextBlock>("DatePrevueTB");
            _CommencerBt = this.FindControl<Button>("CommencerBt");
            _TerminerBt = this.FindControl<Button>("TerminerBt");
            _TerminerBt.IsEnabled = false; // griser Terminer de base.
            _NbJourTB = this.FindControl<TextBlock>("NbJourTB");
            _DescriptionTB = this.FindControl<TextBlock>("DescriptionTB");
        }

        private void Commencer_Click(object? sender, RoutedEventArgs args)
        {
            IList<string> infos = new List<string>{_ChantierTB.Text,_TacheTB.Text};
            StartRequest(this, infos);
        }

        private void Terminer_Click(object? sender, RoutedEventArgs args)
        {
            IList<string> infos = new List<string>{ _ChantierTB.Text,_TacheTB.Text};
            EndRequest(this, infos);
        }


    }
}
