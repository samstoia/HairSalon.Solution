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
          Stylist newStylist = new Stylist(1, "test");
          Assert.AreEqual(typeof(Stylist), newStylist.GetType());
      }

      [TestMethod]
      public void GetStylistName_GetsStylistName_String()
      {
          string name = "Paula Abdul";
          Stylist newStylist = new Stylist(1, name);

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
        Stylist testStylist = new Stylist(1, "Hair Cutter");
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
            Stylist newStylist1 = new Stylist(id, name01);
            newStylist1.Save();
            Stylist newStylist2 = new Stylist(id, name02);
            newStylist2.Save();
            List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };

            //Act
            List<Stylist> result = Stylist.GetAll();

            //Assert
            CollectionAssert.AreEqual(newList, result);
        }
  }
}