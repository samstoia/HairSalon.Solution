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
  }
}