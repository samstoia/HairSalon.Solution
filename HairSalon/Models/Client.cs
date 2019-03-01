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

      public int GetId()
      {
          return _id;
      }

      public static void ClearAll()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"DELETE FROM clients;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
      }

      public static List<Client> GetAll()
      {
          List<Client> allClients = new List<Client> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM clients;";
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
              int ClientId = rdr.GetInt32(0);
              string ClientName = rdr.GetString(1);
              Client newClient = new Client(ClientId, ClientName);
              allClients.Add(newClient);

          }
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        return allClients;
      }
        
  }

}