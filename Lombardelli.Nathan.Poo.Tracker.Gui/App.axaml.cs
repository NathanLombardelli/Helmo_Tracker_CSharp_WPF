using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Lombardelli.Nathan.Poo.Tracker.Presentation;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using MessageBox.Avalonia.Models;
using System;


namespace Lombardelli.Nathan.Poo.Tracker
{
    public class App : Application
    {

        private MainWindow _mainWindow;
        private Storage storage;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                _mainWindow = new MainWindow(); // création de la fenêtre avec les données
                desktop.MainWindow = _mainWindow;
                storage = CreateMainSuperviser(_mainWindow);
                desktop.MainWindow.Opened += MainWindow_Opended; //quand MainWindow S'ouvre, executer la méthode MainWindow_Opended.
                desktop.MainWindow.Closed += MainWindow_Closed; //quadn la fenêtre se ferme,save les données dans un fichier.
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
            storage.Save();
        }

        private Storage CreateMainSuperviser(MainWindow mainWindow)
        {
            Storage store = new Storage();
            if (store.isEmpty())
            {
                ErreurLoadFichier();
            }
            else
            {
                var superviser = new MainSuperviser(mainWindow, store);
            }
            return store;
        }

        private void ErreurLoadFichier()
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
              .GetMessageBoxStandardWindow("Error", "Erreur lors du chargement du ou des fichiers");

            messageBoxStandardWindow.Show();

        }

        private void MainWindow_Opended(object? sender, EventArgs e)
        {
            var connectionWindow = new ConnectionWindow
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,//elle doit occuper le centre de sa fenêtre parent.
                SystemDecorations = SystemDecorations.BorderOnly//pas de bouton agrandire,retrecir,fermer
            };

            CreateConnectionSuperviser(connectionWindow); // créer le superviser de connection et de main.

            connectionWindow.ShowDialog(sender as Window);// afficher la fenêtre (sender = au parent)
        }

        private void CreateConnectionSuperviser(IConnectionView connectionWindow)
        {
            var superviser = new ConnectionSuperviser(connectionWindow);
            superviser.UserConnected += SetUserToMain;//methode get user. puis env setter au main.
            superviser.AboutToQuit += Superviser_AboutToQuit;
        }

        private void SetUserToMain(object? sender, string user)
        {
            _mainWindow.UserTxt = user;
        }

        private void Superviser_AboutToQuit(object? sender, EventArgs e)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown(0);//deallouer les ressources du superviser.
            }
            
        }
    }
}
