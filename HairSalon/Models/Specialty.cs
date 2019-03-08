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
    
  }
}