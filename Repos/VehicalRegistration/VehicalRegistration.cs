using dropmethereapi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Data.SqlClient;

namespace dropmethereapi.Repos.VehicalRegistration
{
    public class VehicalRegistration
    {
        private readonly IConfiguration _configuration;
        public VehicalRegistration(IConfiguration Iconfiguration) { 
            _configuration = Iconfiguration;
        }

        public User RegisterVehical(VehicalRegistrationModel vehicalData) {
           
                String ConnectinString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectinString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "Pr_vehical_registration_new_Vehical_Registration_with_user_Feilldupdation_with_Select_by_id_in_user";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", vehicalData.UserID);
                command.Parameters.AddWithValue("@VehicalNumber", vehicalData.VehicalNumber);
                command.Parameters.AddWithValue("@HelperDL", vehicalData.HelperDL);
                command.Parameters.AddWithValue("@HelperLocality", vehicalData.HelperLocality);
                command.Parameters.AddWithValue("@VehicalName", vehicalData.VehicalName);
                command.Parameters.AddWithValue("@VehicalType", vehicalData.VehicalType);
                command.Parameters.AddWithValue("@VehicalColor", vehicalData.VehicalColor);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                User user = new User();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                    user.UserID = Convert.ToInt32(reader["UserID"]);
                    user.UserName = Convert.ToString(reader["UserName"]);
                    user.Email = Convert.ToString(reader["Email"]);
                    user.PhoneNumber = Convert.ToString(reader["PhoneNumber"]);
                    user.PassWord = Convert.ToString(reader["PassWord"]);
                    user.IsDriver = Convert.ToString(reader["IsDriver"]);
                    user.IsVehicalRegistered = Convert.ToString(reader["IsVehicalRegistered"]);
                    Console.WriteLine(user);
                }

                User isError = new User();
                isError.UserName = _configuration["VehicalRegisrationErrordu:userNotFound"];
                return (count > 0 ? user : isError);
            }
            catch (SqlException e)
            {   if (e.ErrorCode == -2146232060)
                {
                    Console.WriteLine(e.ErrorCode);
                    User isError = new User();
                    isError.UserName = _configuration["VehicalRegisrationErrordu:duplicateEntry"];
                    return isError;
                }
                else {
                    User isError = new User();
                    isError.UserName ="Some---Error--occured!";
                    return isError;
                }
               
                
            }
        }
        public VehicalRegistrationModel GetAVehicalByUserID(int UserID) {
            try
            {
                String ConnectinString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectinString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "pr_select_vehicalinfofromUserID";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                SqlDataReader rd = command.ExecuteReader();
                VehicalRegistrationModel result = new VehicalRegistrationModel();
                while (rd.Read())
                {
                    result.UserID = Convert.ToInt32(rd["UserID"]);
                    result.VehicalID = Convert.ToInt32(rd["VehicalID"]);
                    result.VehicalColor = Convert.ToString(rd["VehicalColor"]);
                    result.HelperDL = Convert.ToString(rd["HelperDL"]);
                    result.HelperLocality = Convert.ToString(rd["HelperLocality"]);
                    result.VehicalNumber = Convert.ToString(rd["VehicalNumber"]);
                    result.VehicalName = Convert.ToString(rd["VehicalName"]);
                    result.VehicalType = Convert.ToString(rd["VehicalType"]);
                    Console.WriteLine(result);
                }
                return result;
            }
            catch (Exception e) {
                VehicalRegistrationModel result = new VehicalRegistrationModel();
                result.VehicalName = e.Message;
                result.VehicalID = -999;
                return result;

            }
        }
        public bool UpdateVehicalFromViewVehicalPage(VehicalRegistrationModel vehicalData) {
            try
            {
                String ConnectinString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectinString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "pr_update_VehicalAtView";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", vehicalData.UserID);
                command.Parameters.AddWithValue("@VehicalNumber", vehicalData.VehicalNumber);
                command.Parameters.AddWithValue("@HelperDL", vehicalData.HelperDL);
                command.Parameters.AddWithValue("@HelperLocality", vehicalData.HelperLocality);
                command.Parameters.AddWithValue("@VehicalName", vehicalData.VehicalName);
                command.Parameters.AddWithValue("@VehicalType", vehicalData.VehicalType);
                command.Parameters.AddWithValue("@VehicalColor", vehicalData.VehicalColor);
                command.Parameters.AddWithValue("@VehicalID", vehicalData.VehicalID);
                
                int k = command.ExecuteNonQuery();
                return k > 0;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        public List<VehicalRegistrationModel> GetAlldataFromTable (int UserID)
        {
            try
            {
                String ConnectinString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectinString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "pr_get_all_vehical_Data";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                SqlDataReader rd = command.ExecuteReader();
                List<VehicalRegistrationModel> result = new List<VehicalRegistrationModel>();
                while (rd.Read())
                {
                    result.Add(new VehicalRegistrationModel {
                        UserID = Convert.ToInt32(rd["UserID"]),
                        VehicalID = Convert.ToInt32(rd["VehicalID"]),
                        VehicalColor = Convert.ToString(rd["VehicalColor"]),
                        HelperDL = Convert.ToString(rd["HelperDL"]),
                        HelperLocality = Convert.ToString(rd["HelperLocality"]),
                        VehicalNumber = Convert.ToString(rd["VehicalNumber"]),
                        VehicalName = Convert.ToString(rd["VehicalName"]),
                        VehicalType = Convert.ToString(rd["VehicalType"])
                      //  Console.WriteLine(result);
                    });
                }
                return result;
            }
            catch (Exception e)
            {
                List<VehicalRegistrationModel> result = new List<VehicalRegistrationModel>();
                result[0].VehicalName = e.Message;
                result[0].VehicalID = -999;
                return result;

            }
        }

        public int DeleteDataWithVehicalID(int VehicalID)
        {
            try
            {
                String ConnectinString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectinString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "pr_delete_vehical_data";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@VehicalID", VehicalID);
                int k = command.ExecuteNonQuery();
              
                return k;
            }
            catch (Exception e)
            {
                VehicalRegistrationModel result = new VehicalRegistrationModel();
                result.VehicalName = e.Message;
                int k = -999;
                return k;

            }
        }

    }
    
}
