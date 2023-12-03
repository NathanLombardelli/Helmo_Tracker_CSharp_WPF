using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lombardelli.Nathan.Poo.Tracker.Presentation
{
    public class ConnectionSuperviser
    {
        public  IConnectionView _view;
        private Storage _data;

        public event EventHandler<string> UserConnected;

        public event EventHandler AboutToQuit;

        public ConnectionSuperviser(IConnectionView view) 
        {
            _view = view;
            SubscribeToViewEvents(); //s'abonner aux évènement de la vue.
            _data = new Storage();  //lire les données pour la connection.
        }

        private void SubscribeToViewEvents()
        {
            _view.ConnectionRequest += this.CheckConnection; //quand l'event ConnectionRequest survient, appeler CheckConnection.
            _view.QuitRequested += this.OnQuitRequested;    //quand l'event QuitRequested survient, appeler OnQuitRequested.
        }

        private void OnQuitRequested(object sender, EventArgs e)
        {
            CloseView();    //fermer la vue.
            NotifyAboutQuitting(); //notifier aux abonner que l'applicationse ferme.
        }

        private void CheckConnection(object sender, IList<string> identifiant)
        {
            if (identifiant.Count == 0) return; // si liste vide => rien.
            if (string.IsNullOrWhiteSpace(identifiant[0]) || string.IsNullOrWhiteSpace(identifiant[1]))
            {
                NotifyUserNotConnected(); //si text vide ou espace => message erreur + stoper connection.
                return;
            } 
            
            if (_data.CheckUserMdp(identifiant[0],identifiant[1]))  
            {
                NotifyUserConnected(identifiant[0]); //si connection ok.
            }
            else
            {
                NotifyUserNotConnected(); //si connection not ok.
            }
            
        }

        private void NotifyUserNotConnected()
        {
            _view.ErrorMessage = true;
        }

        private void NotifyUserConnected(string user)
        {
            Console.WriteLine("Connected");
            UserConnected?.Invoke(this, user);
            _view.Close();
        }

        private void CloseView() => _view.Close();
        private void NotifyAboutQuitting() => AboutToQuit?.Invoke(this, EventArgs.Empty); //notifier les abonnées de AboutToQuit.
    }
}
