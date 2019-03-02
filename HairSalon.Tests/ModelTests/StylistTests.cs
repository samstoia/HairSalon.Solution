using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
      public StylistTest()
      {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=sam_stoia_test;";
      }

      public void Dispose()
      {
        Stylist.ClearAll();
        Client.ClearAll();
      }

      [TestMethod]
      public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
      {
          Stylist newStylist = new Stylist("test", 1);
          Assert.AreEqual(typeof(Stylist), newStylist.GetType());
      }

      [TestMethod]
      public void GetStylistName_GetsStylistName_String()
      {
          string name = "The Rock";
          Stylist newStylist = new Stylist(name, 1);

          string result = newStylist.GetStylistName();

          Assert.AreEqual(name, result);
      }

      [TestMethod]
      public void GetAll_ClientsEmptyAtFirst_List()
      {
          int result = Client.GetAll().Count;
          Assert.AreEqual(0, result);
      }

      [TestMethod]
      public void Save_SavesStylistToDatabase_StylistList()
      {
            //Arrange
        Stylist testStylist = new Stylist("Hair Cutter", 1);
        testStylist.Save();

        //Act
        List<Stylist> result = Stylist.GetAll();
        List<Stylist> testList = new List<Stylist>{testStylist};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }

        [TestMethod]
        public void GetAll_ReturnsAllStylistObjects_StylistList()
        {
            //Arrange
            string name01 = "Paula Abdul";
            string name02 = "Liza Minelli";
            int id = 0;
            Stylist newStylist1 = new Stylist(name01, id);
            newStylist1.Save();
            Stylist newStylist2 = new Stylist(name02, id);
            newStylist2.Save();
            List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };

            //Act
            List<Stylist> result = Stylist.GetAll();

            //Assert
            CollectionAssert.AreEqual(newList, result);
        }

        [TestMethod]
        public void Find_ReturnsCorrectStylist_Stylist()
        {
            Stylist testStylist = new Stylist("The Rock");
            testStylist.Save();

            Stylist foundStylist = Stylist.Find(testStylist.GetId());

            Assert.AreEqual(testStylist, foundStylist);
        }

        // [TestMethod]
        // public void GetClients_RetrievesAllClientsWithStylist_ClientList()
        // {
        //     //Arrange, Act
        //     Stylist testStylist = new Stylist("The Queen");
        //     testStylist.Save();
        //     Client firstClient = new Client(1, "Paula Abdul", testStylist.GetId());
        //     firstClient.Save();
        //     Client secondClient = new Client(1, "Liza Minelli", testStylist.GetId());
        //     secondClient.Save();
        //     List<Client> testClientList = new List<Client> {firstClient, secondClient};
        //     List<Client> resultClientList = testStylist.GetClients();

        //     //Assert
        //     CollectionAssert.AreEqual(testClientList, resultClientList);
        // }

  }
}