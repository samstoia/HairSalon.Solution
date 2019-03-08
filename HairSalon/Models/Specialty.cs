using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace HairSalon.Models
{
  public class Specialty
  {
    private int _id;
		private string _specialtyName;
		public Specialty(string specialtyName, int id = 0)
		{
			_id = id;
			_specialtyName = specialtyName;
		}

		public string GetSpecialtyName()
		{
			return _specialtyName;
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
			cmd.CommandText = @"DELETE FROM specialties;";
			cmd.ExecuteNonQuery();
			conn.Close();
			if (conn != null)
			{
				conn.Dispose();
			}
		}

    public static List<Specialty> GetAll()
		{
			List<Specialty> allSpecialties = new List<Specialty> {};
			MySqlConnection conn = DB.Connection();
			conn.Open();
			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"SELECT * FROM specialties;";
			var rdr = cmd.ExecuteReader() as MySqlDataReader;
			while(rdr.Read())
			{
				int specialtyId = rdr.GetInt32(0);
				string specialtyName = rdr.GetString(1);
				Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
				allSpecialties.Add(newSpecialty);
			}

			conn.Close();
			if (conn != null)
			{
				conn.Dispose();
			}

			return allSpecialties;
		}

    public override bool Equals(System.Object otherSpecialty)
		{
			if (!(otherSpecialty is Specialty))
			{
					return false;
			}
			else
			{
				Specialty newSpecialty = (Specialty) otherSpecialty;
				bool idEquality = this.GetId().Equals(newSpecialty.GetId());
				bool nameEquality = this.GetSpecialtyName().Equals(newSpecialty.GetSpecialtyName());
				return nameEquality;
			}
		}

    public void Save()
		{
			MySqlConnection conn = DB.Connection();
			conn.Open();
			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"INSERT INTO specialties (name) VALUES (@name);";
			MySqlParameter name = new MySqlParameter();
			name.ParameterName = "@name";
			name.Value = this._specialtyName;
			cmd.Parameters.Add(name);
			cmd.ExecuteNonQuery();
			_id = (int) cmd.LastInsertedId;
			conn.Close();
			if (conn != null)
			{
					conn.Dispose();
			}
		}

    public static Specialty Find(int id)
		{
			MySqlConnection conn = DB.Connection();
			conn.Open();
			var cmd = conn.CreateCommand() as MySqlCommand;
			cmd.CommandText = @"SELECT * FROM specialties WHERE id = (@searchId);";
			MySqlParameter searchId = new MySqlParameter();
			searchId.ParameterName = "@searchId";
			searchId.Value = id;
			cmd.Parameters.Add(searchId);
			var rdr = cmd.ExecuteReader() as MySqlDataReader;
			int specialtyId = 0;
			string specialtyName = "";
			while(rdr.Read())
			{
				specialtyId = rdr.GetInt32(0);
				specialtyName = rdr.GetString(1);
			}
			Specialty newSpecialty = new Specialty(specialtyName, specialtyId);
			conn.Close();
			if (conn != null)
			{
				conn.Dispose();
			}
				
			return newSpecialty;
		}

    public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM stylists
        JOIN specialties_stylists ON (stylists.id = specialties_stylists.stylist_id)
        JOIN specialties ON (specialties_stylists.specialty_id = specialties.id)
        WHERE specialties.id = @specialtyId;";
      MySqlParameter specialtyIdParameter = new MySqlParameter();
      specialtyIdParameter.ParameterName = "@specialtyId";
      specialtyIdParameter.Value = _id;
      cmd.Parameters.Add(specialtyIdParameter);
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
      cmd.CommandText = @"INSERT INTO specialties_stylists (specialty_id, stylist_id) VALUES (@specialtyId, @stylistId);";
			MySqlParameter specialtyIdParameter = new MySqlParameter();
      specialtyIdParameter.ParameterName = "@specialtyId";
      specialtyIdParameter.Value = this._id;
      cmd.Parameters.Add(specialtyIdParameter);
      MySqlParameter stylistIdParameter = new MySqlParameter();
      stylistIdParameter.ParameterName = "@stylistId";
      stylistIdParameter.Value = newStylist.GetId();
      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}