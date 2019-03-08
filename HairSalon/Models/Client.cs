//THIS IS THE CHILD OBJECT
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace HairSalon.Models
{
  public class Client
  {
		private int _id;
		private string _clientName;
	
		
		public Client(string clientName, int id = 0)
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

		public void SetClientName(string newClient_name)
    {
      _clientName = newClient_name;
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
					string clientName = rdr.GetString(1);
					Client newClient = new Client(clientName, clientId);
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
				string clientName = "";
				while (rdr.Read())
				{
					clientId = rdr.GetInt32(0);
					clientName = rdr.GetString(1);
				}
				Client newClient = new Client(clientName, clientId);
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
					return (idEquality && nameEquality);
				}

			}

			public override int GetHashCode()
			{
				return this.GetId().GetHashCode();
			}
			
			public void Save()
			{
				MySqlConnection conn = DB.Connection();
				conn.Open();
				var cmd = conn.CreateCommand() as MySqlCommand;
				cmd.CommandText = @"INSERT INTO clients (name) VALUES (@name);";

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

			public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists_clients WHERE client_id = @clientId; DELETE FROM clients WHERE id = @clientId;";
      MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@clientId";
      clientIdParameter.Value = this._id;
      cmd.Parameters.Add(clientIdParameter);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

			public void Edit(string newClientName)
    	{
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @clientName WHERE id = @clientId;";
      MySqlParameter clientNameParameter = new MySqlParameter();
      clientNameParameter.ParameterName = "@clientName";
      clientNameParameter.Value = newClientName;
      cmd.Parameters.Add(clientNameParameter);
      MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@clientId";
      clientIdParameter.Value = this._id;
      cmd.Parameters.Add(clientIdParameter);
      cmd.ExecuteNonQuery();
      _clientName = newClientName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

		public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM clients
        JOIN stylists_clients ON (clients.id = stylists_clients.client_id)
        JOIN stylists ON (stylists_clients.stylist_id = stylists.id)
        WHERE clients.id = @clientId;";
      MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@clientId";
      clientIdParameter.Value = _id;
      cmd.Parameters.Add(clientIdParameter);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Stylist> stylistList = new List<Stylist>{};
      while(rdr.Read())
      {
				int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        
        Stylist newStylist = new Stylist(stylistName, stylistId);
        stylistList.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylistList;
    }

		
		public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_clients (client_id, stylist_id,) VALUES (@clientId, @stylistId);";
			MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@clientId";
      clientIdParameter.Value = this._id;
      cmd.Parameters.Add(clientIdParameter);
      cmd.ExecuteNonQuery();
      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = newStylist.GetId();
      cmd.Parameters.Add(stylistIdParameter);
      

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }







  }

}



			// public static void ClearAllWithin(int stylistId)
			// {
			// 	MySqlConnection conn = DB.Connection();
			// 	conn.Open();
			// 	var cmd = conn.CreateCommand() as MySqlCommand;
			// 	cmd.CommandText = @"DELETE FROM clients WHERE stylist_id = "+stylistId+";";
			// 	cmd.ExecuteNonQuery();
			// 	conn.Close();
			// 	if (conn != null)
			// 	{
			// 	conn.Dispose();
			// 	}
			// }