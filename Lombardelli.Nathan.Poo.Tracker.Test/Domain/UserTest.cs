using NUnit.Framework;
using Moq;
using Lombardelli.Nathan.Poo.Tracker.Domain;

namespace Lombardelli.Nathan.Poo.Test.Domains.UserTest
{
    public class Tests
    {
        User user1;
        User user2;

        [SetUp]
        public void Setup()
        {
           user1 = new User("H2G2", "Nathan", "Lombardelli");
           user2 = new User("Baptou", "Baptiste", "Lebon");

        }

        
        public void TestName()
        {
            Assert.AreEqual("Nathan", user1.Name);
            Assert.AreEqual("Baptiste", user2.Name);
            
            User user3 = new User("H2G2", "Nathan", "Lombardelli");
            user1.Name = "Romain";
            Assert.AreEqual("Romain", user1.Name);
        }

        
        public void TestSurname()
        {
            Assert.AreEqual("Lombardelli", user1.Surname);
            Assert.AreEqual("Lebon", user2.Surname);
            
            user1.Surname = "Smet";
            Assert.AreEqual("Smet", user1.Surname);
        }

        
        public void TestCheckMdp()
        {
            Assert.AreEqual(true, user1.CheckMdp("H2G2"));
            Assert.AreEqual(false, user1.CheckMdp("motdepasse"));
            Assert.AreEqual(true, user2.CheckMdp("Baptou"));
        }


    }
}