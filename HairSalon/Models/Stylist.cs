//THIS IS THE PARENT OBJECT
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace HairSalon.Models
{
  public class Stylist
  {
		private int _id;
		private string _stylistName;
		public Stylist(string stylistName, int id = 0)
		{
			_id = id;
			_stylistName = stylistName;
		}

		public string GetStylistName()
		{
			return _stylistName;
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
			cmd.CommandText = @"DELETE FROM stylists;";
			cmd.ExecuteNonQuery();
			conn.Close();
			if (conn != null)
			{
				conn.Dispose();
			}
		}

		public static List<Stylist> GetAll()
		{
			List<Stylist> allStylists = new List<Stylist> {};
			MySqlConnection conn = DB.Connection();
			conn.Open();
			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"SELECT * FROM Stylists;";
			var rdr = cmd.ExecuteReader() as MySqlDataReader;
			while(rdr.Read())
			{
				int StylistId = rdr.GetInt32(0);
				string StylistName = rdr.GetString(1);
				Stylist newStylist = new Stylist(StylistName, StylistId);
				allStylists.Add(newStylist);
			}

			conn.Close();
			if (conn != null)
			{
				conn.Dispose();
			}

			return allStylists;
		}

		public override bool Equals(System.Object otherStylist)
		{
			if (!(otherStylist is Stylist))
			{
					return false;
			}
			else
			{
				Stylist newStylist = (Stylist) otherStylist;
				bool idEquality = this.GetId().Equals(newStylist.GetId());
				bool nameEquality = this.GetStylistName().Equals(newStylist.GetStylistName());
				return nameEquality;
			}
		}

		public void Save()
		{
			MySqlConnection conn = DB.Connection();
			conn.Open();
			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@name);";
			MySqlParameter name = new MySqlParameter();
			name.ParameterName = "@name";
			name.Value = this._stylistName;
			cmd.Parameters.Add(name);
			cmd.ExecuteNonQuery();
			_id = (int) cmd.LastInsertedId;
			conn.Close();
			if (conn != null)
			{
					conn.Dispose();
			}
		}

		public static Stylist Find(int id)
		{
			MySqlConnection conn = DB.Connection();
			conn.Open();
			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";
			MySqlParameter searchId = new MySqlParameter();
			searchId.ParameterName = "@searchId";
			searchId.Value = id;
			cmd.Parameters.Add(searchId);
			var rdr = cmd.ExecuteReader() as MySqlDataReader;
			int StylistId = 0;
			string StylistName = "";
			while(rdr.Read())
			{
				StylistId = rdr.GetInt32(0);
				StylistName = rdr.GetString(1);
			}
			Stylist newStylist = new Stylist(StylistName, StylistId);
			conn.Close();
			if (conn != null)
			{
				conn.Dispose();
			}
				
			return newStylist;
		}

		public List<Client> GetClients()
		{
			MySqlConnection conn = DB.Connection();
			conn.Open();
			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"SELECT clients.* FROM stylists
        JOIN stylists_clients ON (stylists.id = stylists_clients.stylist_id)
        JOIN clients ON (stylists_clients.client_id = clients.id)
        WHERE stylists.id = @stylistId;";
			MySqlParameter stylistIdParameter = new MySqlParameter();
			stylistIdParameter.ParameterName = "@stylistId";
			stylistIdParameter.Value = this._id;
			cmd.Parameters.Add(stylistIdParameter);
			var rdr = cmd.ExecuteReader() as MySqlDataReader;
			List<Client> clientList = new List<Client> {};
			while(rdr.Read())
			{
				int clientId = rdr.GetInt32(0);
				string clientName = rdr.GetString(1);
				Client newClient = new Client(clientName, clientId);
				clientList.Add(newClient);
			}
			conn.Close();
			if (conn != null)
			{
				conn.Dispose();
			}
			return clientList;
		}

		public override int GetHashCode()
		{
			return this.GetId().GetHashCode();
		}

		public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists_clients WHERE stylist_id = @stylistId; DELETE FROM stylists WHERE id = @stylistId;";
      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = this._id;
      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

		public void AddClient(Client newClient)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_clients (stylist_id, client_id) VALUES (@stylistId, @clientId);";
      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = _id;
      cmd.Parameters.Add(stylistIdParameter);
      MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@clientId";
      clientIdParameter.Value = newClient.GetId();
      cmd.Parameters.Add(clientIdParameter);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

		public void Edit(string newStylistName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @stylistName WHERE id = @stylistId;";
      MySqlParameter stylistNameParameter = new MySqlParameter();
      stylistNameParameter.ParameterName = "@stylistName";
      stylistNameParameter.Value = newStylistName;
      cmd.Parameters.Add(stylistNameParameter);
      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = this._id;
      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();
      _stylistName = newStylistName;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

		public List<Specialty> GetSpecialties()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT specialties.* FROM stylists
        JOIN specialties_stylists ON (stylists.id = specialties_stylists.stylist_id)
        JOIN specialties ON (specialties_stylists.specialty_id = specialties.id)
        WHERE stylists.id = @stylistId;";
      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = _id;
      cmd.Parameters.Add(stylistIdParameter);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Specialty> specialtyList = new List<Specialty>{};
      while(rdr.Read())
      {
				int specialtyId = rdr.GetInt32(0);
        string specialtyName = rdr.GetString(1);
        
        Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
        specialtyList.Add(newSpecialty);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return specialtyList;
    }

    public void AddSpecialty(Specialty newSpecialty)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialties_stylists (specialty_id, stylist_id) VALUES (@specialtyId, @stylistId);";
			MySqlParameter specialtyIdParameter = new MySqlParameter();
      specialtyIdParameter.ParameterName = "@specialtyId";
      specialtyIdParameter.Value = this._id;
      cmd.Parameters.Add(specialtyIdParameter);
      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = newSpecialty.GetId();
      cmd.Parameters.Add(stylistIdParameter);
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
      cmd.CommandText = @"DELETE FROM stylists; DELETE FROM specialties_stylists;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}