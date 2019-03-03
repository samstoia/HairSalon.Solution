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
			List<Client> allStylistClients = new List<Client> {};
			MySqlConnection conn = DB.Connection();
			conn.Open();
			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";
			MySqlParameter stylistId = new MySqlParameter();
			stylistId.ParameterName = "@stylist_id";
			stylistId.Value = this._id;
			cmd.Parameters.Add(stylistId);
			var rdr = cmd.ExecuteReader() as MySqlDataReader;
			while(rdr.Read())
			{
				int clientId = rdr.GetInt32(0);
				string clientName = rdr.GetString(2);
				int clientStylistId = rdr.GetInt32(1);
				Client newClient = new Client(clientId, clientName, clientStylistId);
				allStylistClients.Add(newClient);
			}
			conn.Close();
			if (conn != null)
			{
				conn.Dispose();
			}
			return allStylistClients;
		}

		public override int GetHashCode()
		{
			return this.GetId().GetHashCode();
		}

		public static void Delete(int id)
		{
			MySqlConnection conn = DB.Connection();
			conn.Open();
			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText =@"DELETE FROM stylists WHERE id = "+id+";";

			cmd.ExecuteNonQuery();

			conn.Close();
			{
				if (conn != null)
				{
					conn.Dispose();
				}
			}

		}

  }
}