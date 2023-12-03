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
    public partial class ConnectionWindow : Window,IConnectionView
    {

        private TextBox _UserTB;
        private TextBox _PasswordTB;
        private TextBlock _ErrorMessageTB;

        public event EventHandler<IList<string>> ConnectionRequest;
        public event EventHandler QuitRequested;

        public ConnectionWindow()
        {
            
            InitializeComponent();
            LocateControls(); //initialize les composant utiles de la fenêtre
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void LocateControls()
        {
            _UserTB = this.FindControl<TextBox>("UserTB"); //récupère l'élément avec le name UserTB
            _PasswordTB = this.FindControl<TextBox>("PasswordTB");
            _ErrorMessageTB = this.FindControl<TextBlock>("ErrorMessageTB");
        }

        public Boolean ErrorMessage 
        {
            set => _ErrorMessageTB.IsVisible = value;
        }

        private void Quit_Click(object? sender, RoutedEventArgs args) 
        {
            QuitRequested(this, EventArgs.Empty); //(emetteur, Objet a donner dans l'event(Empty car rien ici)).
        }

        private void Connection_Click(object? sender, RoutedEventArgs args) 
        {
            IList<string> identifiant = new List<string>
            {
                _UserTB.Text,
                _PasswordTB.Text
            };

            ConnectionRequest(this, identifiant); 
        }
        

    }
}
