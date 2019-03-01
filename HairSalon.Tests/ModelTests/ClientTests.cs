using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest
  {
      [TestMethod]
      public void ClientConstructor_CreatesInstanceOfClient_Client()
      {
          Client newClient = new Client(1, "test");
          Assert.AreEqual(typeof(Client), newClient.GetType());
      }

      [TestMethod]
      public void GetClientName_GetsClientName_String()
      {
          string name = "Paula Abdul";
          Client newClient = new Client(1, name);

          string result = newClient.GetClientName();

          Assert.AreEqual(name, result);
      }
  }
}