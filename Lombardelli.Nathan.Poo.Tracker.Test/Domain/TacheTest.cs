using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System;
using Lombardelli.Nathan.Poo.Tracker.Domain;

namespace Lombardelli.Nathan.Poo.Test.Domains.TacheTest
{
    public class Tests
    {

        Tache tache1;
        Tache tache2;
        Tache tache3;

        [SetUp]
        public void Setup()
        {

            tache1 = new Tache("HELMo - Garden Party", "A", "Installer electricité", new DateTime(2021, 9, 1), new DateTime(2021, 9, 6), "D007", new DateTime(2021, 9, 1), new DateTime(2021, 9, 6), new List<Commentaire>());
            tache2 = new Tache("HELMo - Garden Party", "B", "Installer Scene", new DateTime(2021, 9, 1), DateTime.Today.AddDays(-1), "D007", new DateTime(), new DateTime(), new List<Commentaire>());
            tache3 = new Tache("HELMo - Garden Party", "B", "Installer Scene", new DateTime(2021, 9, 1), DateTime.Today.AddDays(1), "D007", new DateTime(), new DateTime(), new List<Commentaire>());
        }

        
        public void TestNomChantier()
        {
            
            Assert.AreEqual("HELMo - Garden Party", tache1.NomChantier);
        }

        
        public void TestLibelle()
        {

            Assert.AreEqual("A", tache1.Libelle);
        }

        
        public void TestDescription()
        {

            Assert.AreEqual("Installer electricité", tache1.Description);
        }


        
        public void TestChefProj()
        {

            Assert.AreEqual("D007", tache1.ChefProj);
        }

        
        public void TestStatut()
        {

            Assert.AreEqual("Terminée", tache1.Statut);
            Assert.AreEqual("À faire", tache2.Statut);
        }

        
        public void TestEnd()
        {
            tache1.End = DateTime.Today;
            Assert.AreEqual(DateTime.Today, tache1.End);
        }

        
        public void TestStart()
        {
            tache1.Start = DateTime.Today;
            Assert.AreEqual(DateTime.Today, tache1.Start);
        }

        
        public void TestIsChef()
        {
            Assert.AreEqual(true, tache1.IsChef("D007"));
            Assert.AreEqual(false, tache2.IsChef("H2G2"));
        }

        
        public void TestDateDebutPrevu()
        {
            Assert.AreEqual(new DateTime(2021, 9, 1), tache1.DateDebutPrevu);
        }

        
        public void TestDateFinPrevu()
        {
            Assert.AreEqual(new DateTime(2021, 9, 6), tache1.DateFinPrevu);
        }

        
        public void TestRetard()
        {
            Assert.AreEqual("0", tache1.Retard());
            Assert.AreEqual("1", tache2.Retard());
            Assert.AreEqual("0", tache3.Retard());
        }

    }
}