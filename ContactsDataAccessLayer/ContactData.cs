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

        static public int AddNewContact(string FirstName,string LastName, string Email,string Phone, 
                                  string Address, DateTime DateOfBirth,int CountryID, string ImagePath)
        {
            int ContactID = -1;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.stringConnection);
            string query = @"insert into Contacts (FirstName,LastName,Email,Phone,Address,DateOfBirth,CountryID,ImagePath)
                                 values (@FirstName,@LastName,@Email,@Phone,@Address,@DateOfBirth,@CountryID,@ImagePath)
                                 select scope_identity()";
            
            SqlCommand cmd  = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImagePath != "")
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                cmd.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

            try
            {
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                    ContactID = ID;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                ContactID = -1;
            }
            finally
            {
                conn.Close();
            }
            return ContactID;
        }
    
        static public bool UpdateContact(int ID, string FirstName, string LastName, string Email, string Phone,
                                  string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            bool isUpdated = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.stringConnection);
            string query = @"update Contacts
                            set FirstName=@FirstName,
                                LastName=@LastName,
                                Email=@Email,
                                Phone=@Phone,
                                Address=@Address,
                                DateOfBirth=@DateOfBirth,
                                CountryID=@CountryID,
                                ImagePath=@ImagePath
                            where ContactID=@ID";

            SqlCommand cmd = new SqlCommand(query,conn);

            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@CountryID", CountryID);
            if (ImagePath != "")
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            cmd.Parameters.AddWithValue("@ID", ID);


            try
            {
                conn.Open();
                int RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                    isUpdated = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);   
                isUpdated = false;
            }
            finally
            {
                conn.Close();
            }

            return isUpdated;
        }
    }
}
