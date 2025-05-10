using System;
using System.Data.SqlClient; 

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        static public bool FindContactWithID(int ID, ref string FirstName, ref string LastName, ref string Email,
          ref string Phone, ref string Address, ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.stringConnection);
            string query = "select * from Contacts where ContactID = @ID";

            SqlCommand cmd = new SqlCommand(query,conn);
            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    FirstName = reader["FirstName"].ToString();
                    LastName = reader["LastName"].ToString();
                    Email = reader["Email"].ToString();
                    Phone = reader["Phone"].ToString();
                    Address = reader["Address"].ToString();
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)(reader["CountryID"]);
                    ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "Empty";
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isFound;
        }
    }
}
