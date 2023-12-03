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
    public partial class CommentaireView : UserControl
    {
        Label _utilisateurL;
        TextBlock _commentaireL;
        Label _dateL;

        public CommentaireView()
        {
            
            InitializeComponent();
            LocateControls(); //initialize les composant utiles de la fenêtre

        }

        public string Utilisateur 
        {
            set => _utilisateurL.Content = value;
        }

        public string Commentaire
        {
            set => _commentaireL.Text = value;
        }

        public string Date
        {
            set => _dateL.Content = value;
        }


        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void LocateControls()
        {
            _utilisateurL = this.FindControl<Label>("UtilisateurL"); 
            _commentaireL = this.FindControl<TextBlock>("CommentaireL");
            _dateL = this.FindControl<Label>("DateL");
        }


    }
}
