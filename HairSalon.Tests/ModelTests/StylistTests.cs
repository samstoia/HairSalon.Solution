using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest
  {
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
  }
}