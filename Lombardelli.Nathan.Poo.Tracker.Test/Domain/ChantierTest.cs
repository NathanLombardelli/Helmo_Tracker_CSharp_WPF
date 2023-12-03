using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System;
using Lombardelli.Nathan.Poo.Tracker.Domain;

namespace Lombardelli.Nathan.Poo.Test.Domains.ChantierTest
{
    public class Tests
    {

        private Chantier _chantier1;
        private List<ITache> _tacheListD;
        private List<ITache> _tacheListC;

        [SetUp]
        public void Setup()
        {
            var tache1 = new Mock<ITache>();
            var tache2 = new Mock<ITache>();

            tache1.SetupGet(x => x.DateDebutPrevu).Returns(new DateTime(2021,1,1));
            tache2.SetupGet(y => y.DateDebutPrevu).Returns(new DateTime(2021, 2, 2));

            _tacheListD = new();

            _tacheListD.Add(tache2.Object);
            _tacheListD.Add(tache1.Object);

            _tacheListC = new();

          //  _tacheListC.Add(tache1.Object);
          //  _tacheListC.Add(tache2.Object);


            _chantier1 = new Chantier("chantier1", _tacheListD, new DateTime(2021, 6, 9));
        }

        
        public void TestNom()
        {

            Assert.AreEqual("chantier1", _chantier1.Nom);
        }

        
        public void TestDateDebut()
        {

            Assert.AreEqual(new DateTime(2021, 6, 9), _chantier1.DateDebut);
        }

       
        public void TestEnumTache()
        {
            IEnumerable<ITache> EChantier =  _chantier1.EnumTache();
            List<ITache> testTacheList = new();

            foreach (ITache tache in EChantier) 
            {

                testTacheList.Add(tache);
            
            }

            Assert.AreEqual(testTacheList,_tacheListD);
        }

        
        public void TestTriCDateDebut()
        {

            _chantier1.TriCDateDebut();

            IEnumerable<ITache> EChantier = _chantier1.EnumTache();
            List<ITache> testTacheList = new();

            foreach (ITache tache in EChantier)
            {

                testTacheList.Add(tache);

            }

            Assert.AreEqual(testTacheList, _tacheListC);
        }


    }
}