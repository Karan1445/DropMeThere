using dropmethereapi.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace dropmethereapi.Repos.History
{
    public class HistoryAll
    {
        private IConfiguration configuration;
        public HistoryAll(IConfiguration icn) {
            configuration = icn;
        }
        public String DeleteAndStoreRecord(int confirmID) {
            try
            {
                String ConnectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "pr_delete_at_the_confimation_page_with_data_store_in_history";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ConfrimationID", confirmID);
                int k= cmd.ExecuteNonQuery();
                if (k == 0) {
                    return "nO--data-found";
                }
                else {
                    return "Yes";
                }
               
            }
            catch (Exception e) {
                return e.Message;
            }
        }
        public List<RideHistoryModel> GetAllRideHistoryRequests(int id)
        {
            try
            {
                // Connection string from configuration
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        // Call stored procedure to fetch ride history
                        cmd.CommandText = "Pr_get_all_from_History"; // Adjust with your stored procedure name
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserID", id);

                        // Execute the reader to get the result set
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            List<RideHistoryModel> rideHistoryList = new List<RideHistoryModel>();
                            int count = 0;

                            while (rd.Read())
                            {
                                count++;
                                rideHistoryList.Add(new RideHistoryModel()
                                {
                                    HistoryId = Convert.ToInt32(rd["HistoryId"]),
                                    HelperUserName = Convert.ToString(rd["HelperUserName"]),
                                    SeekerUserID = Convert.ToInt32(rd["SeekerUserID"]),
                                    HelperUserID = Convert.ToInt32(rd["HelperUserID"]),
                                    RequestID = Convert.ToInt32(rd["RequestID"]),
                                    RideFinishedAt = Convert.ToDateTime(rd["RideFinishedAt"]),
                                    RideRegardsFromSeeker = rd["RideRegardsFromSeeker"] as string, // NULL can be handled with 'as'
                                    HelpFromSeeker = rd["HelpFromSeeker"] as string, // NULL can be handled with 'as'
                                    IsEnable = Convert.ToString(rd["isEnable"]),
                                    RideStartedAt = Convert.ToDateTime(rd["RideStartedAt"]),
                                    SeekerUserName = Convert.ToString(rd["UserName"]),
                                    SeekerPhoneNumber = Convert.ToString(rd["PhoneNumber"]),
                                    SeekerCity = Convert.ToString(rd["SeekerCity"]),
                                    SeekerState = Convert.ToString(rd["SeekerState"]),
                                    SeekerArea = Convert.ToString(rd["SeekerArea"]),
                                    CurrentLocationLatLong = Convert.ToString(rd["CurrentLocationLatLong"]),
                                    RideStartPointLatLong = Convert.ToString(rd["RideStartPointLatLong"]),
                                    RideEndPointLatLong = Convert.ToString(rd["RideEndPointLatLong"]),
                                    ReqTime = Convert.ToDateTime(rd["ReqTime"]),
                                    StartPointName = Convert.ToString(rd["StartPointName"]),
                                    EndPointName = Convert.ToString(rd["EndPointName"]),
                                    Distance = Convert.ToString(rd["Distance"]),
                                    ReachableTime = Convert.ToString(rd["ReachableTime"])
                                });
                            }

                            // If no data found, return null
                            if (count == 0)
                            {
                                return null;
                            }
                            else
                            {
                                return rideHistoryList;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Log exception if needed
                Console.WriteLine(e.Message);
                // Return an empty list on error
                return new List<RideHistoryModel>();
            }
        }
        public String sendGift(int id, string rrfd, string HelpFromSeeker) {
            try
            {
                String ConnectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "pr_update_value_at_history";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@HistoryId", id);
                cmd.Parameters.AddWithValue("@rrfd", rrfd);
                cmd.Parameters.AddWithValue("@HelpFromSeeker", HelpFromSeeker);
                int k = cmd.ExecuteNonQuery();
                if (k == 0)
                {
                    return "No Data";
                }
                else {
                    return k.ToString();
                }
            }
            catch (Exception e)
            {
                return "9999";
            }
        }
        public List<RideHistoryAdmin> GetAlldataAdmin()
        {
            try
            {
                // Connection string from configuration
                string connectionString = configuration.GetConnectionString("ConnectionString");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        // Call stored procedure to fetch ride history
                        cmd.CommandText = "Pr_get_All_History"; // Adjust with your stored procedure name
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   

                        // Execute the reader to get the result set
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            List<RideHistoryAdmin> rideHistoryList = new List<RideHistoryAdmin>();
                            int count = 0;

                            while (rd.Read())
                            {
                                count++;
                                rideHistoryList.Add(new RideHistoryAdmin()
                                {
                                    HistoryId = Convert.ToInt32(rd["HistoryId"]),
                                    SeekerUserID = Convert.ToInt32(rd["SeekerUserID"]),
                                    HelperUserID = Convert.ToInt32(rd["HelperUserID"]),
                                    RequestID = Convert.ToInt32(rd["RequestID"]),
                                    RideFinishedAt = Convert.ToDateTime(rd["RideFinishedAt"]),
                                    RideRegardsFromSeeker = rd["RideRegardsFromSeeker"] as string, // Handles NULL values
                                    HelpFromSeeker = rd["HelpFromSeeker"] as string,
                                    IsEnable = Convert.ToString(rd["isEnable"]) ?? "all", // Default to "all" if NULL
                                    RideStartedAt = Convert.ToDateTime(rd["RideStartedAt"])
                                });
                            }

                            // If no data found, return null
                            if (count == 0)
                            {
                                return null;
                            }
                            else
                            {
                                return rideHistoryList;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // Log exception if needed
                Console.WriteLine(e.Message);
                // Return an empty list on error
                return new List<RideHistoryAdmin>();
            }
        }
        public String delteHistoryByID(int id)
        {
            try
            {
                String ConnectionString = configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "pr_delete_history_by_admin";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                int k = cmd.ExecuteNonQuery();
                if (k == 0)
                {
                    return "nO--data-found";
                }
                else
                {
                    return "Yes";
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

    }
  
}
public class RideHistoryAdmin
{
    public int HistoryId { get; set; }
    public int SeekerUserID { get; set; }
    public int HelperUserID { get; set; }
    public int RequestID { get; set; }
    public DateTime RideFinishedAt { get; set; }
    public string? RideRegardsFromSeeker { get; set; }
    public string? HelpFromSeeker { get; set; }
    public string IsEnable { get; set; }
    public DateTime RideStartedAt { get; set; }
}