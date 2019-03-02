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
          Client newClient = new Client(1, "test", 1);
          Assert.AreEqual(typeof(Client), newClient.GetType());
      }

      [TestMethod]
      public void GetClientName_GetsClientName_String()
      {
          string name = "The Rock";
          Client newClient = new Client(1, name, 1);

          string result = newClient.GetClientName();

          Assert.AreEqual(name, result);
      }

			[TestMethod]
			public void GetAll_ReturnsClients_ClientList()
			{
				//Arrange
				string name01 = "Michael Jackson";
				string name02 = "John Snow";
				Client newClient1 = new Client(1, name01, 1);
				newClient1.Save();
				Client newClient2 = new Client(1, name02, 1);
				newClient2.Save();
				List<Client> newList = new List<Client> { newClient1, newClient2 };

				//Act
				List<Client> result = Client.GetAll();

				//Assert
				CollectionAssert.AreEqual(newList, result);
			}


      
  }
}