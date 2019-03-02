//THIS IS THE CHILD OBJECT
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace HairSalon.Models
{
  public class Client
  {
			private int _id;
			private int _stylistId;
      private string _clientName;
		
			
      public Client(int stylistId, string clientName, int id = 0)
      {
					_id = id;
					_stylistId = stylistId;
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
              int clientId = rdr.GetInt32(0);
							int clientStylistId = rdr.GetInt32(1);
              string clientName = rdr.GetString(2);
              Client newClient = new Client(clientStylistId, clientName, clientId);
              allClients.Add(newClient);

          }
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        return allClients;
      }

      public static Client Find(int id)
    	{
				MySqlConnection conn = DB.Connection();
				conn.Open();
				var cmd = conn.CreateCommand() as MySqlCommand;
				cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";
				MySqlParameter searchId = new MySqlParameter();
				searchId.ParameterName = "@searchId";
				searchId.Value = id;
				cmd.Parameters.Add(searchId);
				var rdr = cmd.ExecuteReader() as MySqlDataReader;
				int clientId = 0;
				int clientStylistId = 0;
				string clientName = "";
				while (rdr.Read())
				{
					clientId = rdr.GetInt32(0);
					clientStylistId = rdr.GetInt32(1);
					clientName = rdr.GetString(2);
				}
				Client newClient = new Client(clientStylistId, clientName, clientId);
				conn.Close();
				if (conn != null)
				{
					conn.Dispose();
				}
      return newClient;
    	}

			public override bool Equals(System.Object otherClient)
			{
				if (!(otherClient is Client))
				{
					return false;
				}
				else
				{
					Client newClient = (Client) otherClient;
					bool idEquality = this.GetId() == newClient.GetId();
					bool nameEquality = this.GetClientName() == newClient.GetClientName();
					bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
					return (idEquality && nameEquality && stylistEquality);
				}

			}

			public override int GetHashCode()
			{
				return this.GetId().GetHashCode();
			}

			public int GetStylistId()
			{
				return _stylistId;
			}
			
			public void Save()
			{
				MySqlConnection conn = DB.Connection();
				conn.Open();
				var cmd = conn.CreateCommand() as MySqlCommand;
				cmd.CommandText = @"INSERT INTO clients (stylist_id, name) VALUES (@stylist_id, @name);";

				MySqlParameter stylistId = new MySqlParameter();
				stylistId.ParameterName = "@stylist_id";
				stylistId.Value = this._stylistId;
				cmd.Parameters.Add(stylistId);
				MySqlParameter name = new MySqlParameter();
				name.ParameterName = "@name";
				name.Value = this._clientName;
				cmd.Parameters.Add(name);
				
				cmd.ExecuteNonQuery();
				_id = (int) cmd.LastInsertedId;
				conn.Close();
				if (conn != null)
				{
					conn.Dispose();
				}
    	}
  }

}