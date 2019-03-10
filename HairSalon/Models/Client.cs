//THIS IS THE CHILD OBJECT
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace HairSalon.Models
{
  public class Client
  {
		private int _id;
		private string _clientName;
		private string _clientPhone;
		private string _clientEmail;
		
		public Client(string clientName, string clientPhone, string clientEmail, int id = 0)
		{
			_id = id;
			_clientName = clientName;
			_clientPhone = clientPhone;
			_clientEmail = clientEmail;
		}

		public string GetClientName()
		{
			return _clientName;
		}

		public string GetClientPhone()
		{
			return _clientPhone;
		}

		public string GetClientEmail()
		{
			return _clientEmail;
		}

		public int GetId()
		{
			return _id;
		}

		public void SetClientName(string newClient_name)
    {
      _clientName = newClient_name;
    }

		public void SetClientPhone(string newClientPhone)
    {
      _clientPhone = newClientPhone;
    }

		public void SetClientEmail(string newClientEmail)
    {
      _clientEmail = newClientEmail;
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
					string clientPhone = rdr.GetString(2);
					string clientEmail = rdr.GetString(3);
					Client newClient = new Client(clientName, clientPhone, clientEmail, clientId);
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
				string clientPhone = "";
				string clientEmail = "";
				while (rdr.Read())
				{
					clientId = rdr.GetInt32(0);
					clientName = rdr.GetString(1);
					clientPhone = rdr.GetString(2);
					clientEmail = rdr.GetString(3);
				}
					Client newClient = new Client(clientName, clientPhone, clientEmail, clientId);

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
					bool phoneEquality = this.GetClientPhone() == newClient.GetClientPhone();
					bool emailEquality = this.GetClientEmail() == newClient.GetClientEmail();
					return (idEquality && nameEquality && phoneEquality && emailEquality);
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
				cmd.CommandText = @"INSERT INTO clients (name, phone, email) VALUES (@name, @phone, @email);";

				MySqlParameter name = new MySqlParameter();
				name.ParameterName = "@name";
				name.Value = this._clientName;
				cmd.Parameters.Add(name);

				MySqlParameter phone = new MySqlParameter();
				phone.ParameterName = "@phone";
				phone.Value = this._clientPhone;
				cmd.Parameters.Add(phone);

				MySqlParameter email = new MySqlParameter();
				email.ParameterName = "@email";
				email.Value = this._clientEmail;
				cmd.Parameters.Add(email);

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

			public void Edit(string newClientName, string newClientPhone, string newClientEmail)
    	{
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = (@clientName), phone = (@clientPhone), email = (@clientEmail) WHERE id = @clientId;";

      MySqlParameter clientNameParameter = new MySqlParameter();
      clientNameParameter.ParameterName = "@clientName";
      clientNameParameter.Value = newClientName;
      cmd.Parameters.Add(clientNameParameter);

			MySqlParameter clientPhoneParameter = new MySqlParameter();
      clientPhoneParameter.ParameterName = "@clientPhone";
      clientPhoneParameter.Value = newClientPhone;
      cmd.Parameters.Add(clientPhoneParameter);

			MySqlParameter clientEmailParameter = new MySqlParameter();
      clientEmailParameter.ParameterName = "@clientEmail";
      clientEmailParameter.Value = newClientEmail;
      cmd.Parameters.Add(clientEmailParameter);

			MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@clientId";
      clientIdParameter.Value = this._id;
      cmd.Parameters.Add(clientIdParameter);



      cmd.ExecuteNonQuery();
      _clientName = newClientName;
			_clientPhone = newClientPhone;
			_clientEmail = newClientEmail;
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
      cmd.CommandText = @"INSERT INTO stylists_clients (client_id, stylist_id) VALUES (@clientId, @stylistId);";
			MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = newStylist.GetId();
      cmd.Parameters.Add(stylistIdParameter);
			MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@clientId";
      clientIdParameter.Value = _id;
      cmd.Parameters.Add(clientIdParameter);
      
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

		public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients; DELETE FROM specialties_stylists;";
      cmd.ExecuteNonQuery();
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