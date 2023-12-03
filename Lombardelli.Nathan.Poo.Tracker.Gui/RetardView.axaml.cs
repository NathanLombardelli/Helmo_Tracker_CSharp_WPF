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
    public partial class RetardView : UserControl,IRetardView
    {

        private TextBlock _chantierTB;
        private TextBlock _retardTB;


        public RetardView()
        {
            
            InitializeComponent();
            LocateControls(); //initialize les composant utiles de la fenêtre

        }

        public string Chantier { set => _chantierTB.Text = value; }
        public string Retard { get => _retardTB.Text; set => _retardTB.Text = value; }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void LocateControls()
        {
            
            _chantierTB = this.FindControl<TextBlock>("ChantierTB");
            _retardTB = this.FindControl<TextBlock>("RetardChantierTB");

        }

     

    }
}
