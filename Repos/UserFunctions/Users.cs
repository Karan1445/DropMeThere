using System.Data.SqlClient;
using dropmethereapi.Models;
namespace dropmethereapi.Repos.UserFunctions
{
    public class Users
    {
            private readonly IConfiguration _configuration;

            public Users(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            #region Get All Users
            public List<User> GetAllUsers()
            {
                try
                {
                    string connectionString = _configuration.GetConnectionString("ConnectionString");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "admin_pr_get_all_user";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();
                        List<User> users = new List<User>();

                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                PassWord = reader["PassWord"].ToString(),
                                IsVehicalRegistered = reader["IsVehicalRegistered"].ToString()
                            });
                        }
                    Console.WriteLine(users);
                        return users;
                    }
                }
                catch (Exception e)
                {
              
                    Console.WriteLine(e.Message);
                    return new List<User>();
                }
            }
            #endregion

            #region Get User by ID
            public User GetUserById(int userID)
            {
                try
                {
                    string connectionString = _configuration.GetConnectionString("ConnectionString");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "admin_pr_get_user_by_id";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserID = Convert.ToInt32(reader["UserID"]),
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                PassWord = reader["PassWord"].ToString(),
                                IsVehicalRegistered = reader["IsVehicalRegistered"].ToString()
                            };
                        }
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            #endregion

            #region Update User
            public bool UpdateUser(User user)
            {
                try
                {
                    string connectionString = _configuration.GetConnectionString("ConnectionString");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "Pr_User_update_user";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", user.UserID);
                        cmd.Parameters.AddWithValue("@UserName", user.UserName);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                        cmd.Parameters.AddWithValue("@PassWord", user.PassWord);
                        cmd.Parameters.AddWithValue("@IsVehicalRegistered", user.IsVehicalRegistered);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            #endregion

            #region Delete User
            public bool DeleteUser(int userID)
            {
                try
                {
                    string connectionString = _configuration.GetConnectionString("ConnectionString");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandText = "pr_User_delte_User";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", userID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            #endregion
        }


    }


