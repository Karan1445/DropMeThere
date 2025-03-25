using dropmethereapi.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace dropmethereapi.Repos.UserLogin
{
    public class LoginRegistrations
    {
        private readonly IConfiguration _configuration;
        public LoginRegistrations(IConfiguration configuration) { 
            _configuration = configuration;
        }
        public User LoginIntoUserAccount(String Email,String PassWord)
        {
            
            String sqlconn = _configuration.GetConnectionString("ConnectionString");
            SqlConnection con = new SqlConnection(sqlconn);
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            User user = new User();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "LoginInUserWithEmail";
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@PassWord", PassWord);
            SqlDataReader rd = cmd.ExecuteReader();
            int count = 0;
            while (rd.Read())
            {
                count++;
                user.UserID = Convert.ToInt32(rd["UserID"]);
                user.UserName = Convert.ToString(rd["UserName"]);
                user.Email = Convert.ToString(rd["Email"]);
                user.PhoneNumber = Convert.ToString(rd["PhoneNumber"]);
                user.PassWord = Convert.ToString(rd["PassWord"]);
                user.IsDriver = Convert.ToString(rd["IsDriver"]);
                user.IsVehicalRegistered = Convert.ToString(rd["IsVehicalRegistered"]);
            }
            User isError = new User();
            isError.UserName = _configuration["LoginMetaData:UserNamePharse"];
            return (count > 0?user :isError);
        }
        public User RegisterForNewUser(userRegsitermodel user) {
            string sqlConn = _configuration.GetConnectionString("ConnectionString");
            using (SqlConnection con = new SqlConnection(sqlConn))
            {
              
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Insert_at_Api_Users";
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@PassWord", user.PassWord);
                cmd.Parameters.AddWithValue("@IsDriver", user.IsDriver);
                cmd.Parameters.AddWithValue("@IsVehicalRegistered", "No");
                try
                {
                    int result = cmd.ExecuteNonQuery();
               
                if (result > 0)
                {
                    User userData = LoginIntoUserAccount(user.Email, user.PassWord);
                    return userData;
                }
                else {
                    User isError = new User();
                    isError.UserName = _configuration["LoginMetaData:UserRegisterationError"];
                    return isError;
                    }
                }
                catch (SqlException e)
                {
                   
                    if (e.ErrorCode == -2146232060)
                    {
                        Console.WriteLine(e.ErrorCode);
                        User isError = new User();
                        isError.UserName = _configuration["LoginMetaData:SameUserEmailError"];
                        return isError;
                    }
                    else {
                        User isError = new User();
                        isError.IsDriver = _configuration["LoginMetaData:UserRegisterationError"];
                        isError.UserName = e.Message;
                        return isError;
                    }
                }
            }
        }
    }
}
