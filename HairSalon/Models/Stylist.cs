//THIS IS THE PARENT OBJECT
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace HairSalon.Models
{
  public class Stylist
  {
      private int _id;
      private string _stylistName;
      public Stylist(int id, string stylistName)
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
              Stylist newStylist = new Stylist(StylistId, StylistName);
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
  }
}