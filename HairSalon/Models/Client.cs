//THIS IS THE CHILD OBJECT
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace HairSalon.Models
{
  public class Client
  {
      private int _id;
      private string _clientName;
      public Client(int id, string clientName)
      {
          _id = id;
          _clientName = clientName;
      }

      public string GetClientName()
      {
          return _clientName;
      }

      
  }

}