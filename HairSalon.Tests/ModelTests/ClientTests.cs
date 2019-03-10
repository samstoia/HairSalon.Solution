using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
    DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=sam_stoia_test;";
    }

    public void Dispose()
    {
      Client.ClearAll();
    }

      [TestMethod]
      public void ClientConstructor_CreatesInstanceOfClient_Client()
      {
          Client newClient = new Client("test", "test", "test");
          Assert.AreEqual(typeof(Client), newClient.GetType());
      }

      [TestMethod]
      public void GetClientName_GetsClientName_String()
      {
          string name = "The Rock";
          Client newClient = new Client(name, "test", "test");

          string result = newClient.GetClientName();

          Assert.AreEqual(name, result);
      }

			[TestMethod]
			public void GetAll_ReturnsClients_ClientList()
			{
				//Arrange
				string name01 = "Michael Jackson";
				string name02 = "John Snow";
				Client newClient1 = new Client(name01, "test", "test");
				newClient1.Save();
				Client newClient2 = new Client(name02, "test", "test");
				newClient2.Save();
				List<Client> newList = new List<Client> { newClient1, newClient2 };

				//Act
				List<Client> result = Client.GetAll();

				//Assert
				CollectionAssert.AreEqual(newList, result);
			}


      
  }
}