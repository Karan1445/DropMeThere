using dropmethereapi.Models;
using System.Data.SqlClient;

namespace dropmethereapi.Repos.admin_dashboard
{
    public class AdminUserRepo
    {
        private readonly IConfiguration _configuration;

        public AdminUserRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Get All Admin Users
        public List<AdminUserModel> GetAllAdminUsers()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "admin_pr_get_all_adminUsers";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<AdminUserModel> adminUsers = new List<AdminUserModel>();

                    while (reader.Read())
                    {
                        adminUsers.Add(new AdminUserModel
                        {
                            AdminID = Convert.ToInt32(reader["adminID"]),
                            AdminName = reader["adminName"].ToString(),
                            RoleName = reader["roleName"].ToString(),
                            CreatedBy = reader["createdBy"].ToString(),
                            CreatedOn = reader["createdOn"].ToString()
                        });
                    }
                    return adminUsers;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<AdminUserModel>();
            }
        }
        #endregion

        #region Get Admin User by ID
        public AdminUserModel GetAdminUserById(int adminID)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "admin_pr_get_adminUser_by_id";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@adminID", adminID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new AdminUserModel
                        {
                            AdminID = Convert.ToInt32(reader["adminID"]),
                            AdminName = reader["adminName"].ToString(),
                            RoleName = reader["roleName"].ToString(),
                            CreatedBy = reader["createdBy"].ToString(),
                            CreatedOn = reader["createdOn"].ToString()
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

        #region Update Admin User
        public bool UpdateAdminUser(AdminUserModel adminUser)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "pr_admin_update_adminUser";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@adminID", adminUser.AdminID);
                    cmd.Parameters.AddWithValue("@adminName", adminUser.AdminName);
                    cmd.Parameters.AddWithValue("@roleName", adminUser.RoleName);
                   


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

        #region Delete Admin User
        public bool DeleteAdminUser(int adminID)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "pr_admin_delete_adminUser";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@adminID", adminID);

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
     
        #region Insert Admin User
        public bool InsertAdminUser(AdminUserModel adminUser)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "pr_admin_insert_adminUser";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@adminName", adminUser.AdminName);
                    cmd.Parameters.AddWithValue("@roleName", adminUser.RoleName);
                    cmd.Parameters.AddWithValue("@createdBy", adminUser.CreatedBy);
                  
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
        #region dashboard values
        public List<DashBoardValuesNumber> GetDashbordNumber()
        {
           
                string connectionString = _configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "pr_dashboard_count_Data";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                 List<DashBoardValuesNumber> data = new List<DashBoardValuesNumber>();
    
                    while (reader.Read())
                    {
                    data.Add(new DashBoardValuesNumber {
                        TableName = Convert.ToString(reader["TableName"]),
                        TotalRows= Convert.ToInt32(reader["TotalRows"])
                    });
                        
                    }
                    return data;
                }

        }
        #endregion
    }
}
