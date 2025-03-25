using dropmethereapi.Models;
using dropmethereapi.ValidationLogic;
using System.Data.SqlClient;

namespace dropmethereapi.Repos.HelperSideViewRides
{
    public class HelperSideViewRidesRepo
    {
        private readonly IConfiguration _configuration;
        public HelperSideViewRidesRepo(IConfiguration Iconfiguration)
        {
            _configuration = Iconfiguration;
        }
        public bool InsertANewRideConfimation(HelperSideViewRidesModel data) {
            try
            {
                String ConnectinString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectinString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "pr_insert_a_new_help";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SeekerUserID", data.SeekerUserID);
                command.Parameters.AddWithValue("@HelperUserID", data.HelperUserID);
                command.Parameters.AddWithValue("@RequestID", data.RequestID);
                command.Parameters.AddWithValue("@HelpersCurrentLocationLatLong", data.HelpersCurrentLocationLatLong);
                command.Parameters.AddWithValue("@HelperRechabletimetoStartPoint", data.HelperRechabletimetoStartPoint);
                command.Parameters.AddWithValue("@HelperDistanceFromStartPoint", data.HelperDistanceFromStartPoint);
                int k = command.ExecuteNonQuery();
                return k > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        public DisplayConfirmationRequestModel DisplayActiveRideForboth(int UserID) {
            try
            {
                String ConnectinString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectinString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "Pr_Select_Confirmation_wiht_allPossibledata_BySeekerorHelpersUserID";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                SqlDataReader rd = command.ExecuteReader();
                DisplayConfirmationRequestModel result = new DisplayConfirmationRequestModel();
                int count = 0;
                while (rd.Read())
                {
                    count++;
                    result.ConfrimationID = Convert.ToInt32(rd["ConfrimationID"]);
                    result.SeekerUserID = Convert.ToInt32(rd["SeekerUserID"]);
                    result.HelperUserID = Convert.ToInt32(rd["HelperUserID"]);
                    result.RequestID = Convert.ToInt32(rd["RequestID"]);
                    result.HelpersCurrentLocationLatLong = Convert.ToString(rd["HelpersCurrentLocationLatLong"]);
                    result.ConfirmationTime = Convert.ToDateTime(rd["ConfirmationTime"]);
                    result.HelperRechabletimetoStartPoint = Convert.ToString(rd["HelperRechabletimetoStartPoint"]);
                    result.HelperDistanceFromStartPoint = Convert.ToString(rd["HelperDistanceFromStartPoint"]);
                    result.HelperUserName = Convert.ToString(rd["HelperUserName"]);
                    result.HelperPhoneNumber = Convert.ToString(rd["HelperPhoneNumber"]);
                    result.SeekerUserName = Convert.ToString(rd["SeekerUserName"]);
                    result.SeekerPhoneNumber = Convert.ToString(rd["SeekerPhoneNumber"]);
                    result.StartPointName = Convert.ToString(rd["StartPointName"]);
                    result.EndPointName = Convert.ToString(rd["EndPointName"]);
                    result.RidedataReachableTime = Convert.ToString(rd["RidedataReachableTime"]);
                    result.RidedataDistance = Convert.ToString(rd["RidedataDistance"]);
                    Console.WriteLine(result);
                }
                if (count > 0)
                {
                    return result;
                }
                else
                {   
                    result.HelperUserName = "Not----------------data------------------found----123";
                    return result;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("hey there!");
                DisplayConfirmationRequestModel result = new DisplayConfirmationRequestModel();
                result.HelperUserName = "sql----------------Error------------------found----123";
                return result;
            }
        }

        public List<SeekerRequestHandlerModel> GetAllrequestTemporay() {
            try
            {
                String ConnectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "pr_temporary_select_request_fro_helper";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
               
                SqlDataReader rd = cmd.ExecuteReader();
                List<SeekerRequestHandlerModel> seekerreq = new List<SeekerRequestHandlerModel>();
                int count = 0;
                while (rd.Read())
                {
                    count++;
                    seekerreq.Add(

                        new SeekerRequestHandlerModel()
                        {

                            RequestID = Convert.ToInt32(rd["RequestID"]),
                            UserID = Convert.ToInt32(rd["UserID"]),
                            UserName = Convert.ToString(rd["UserName"]),
                            PhoneNumber = Convert.ToString(rd["PhoneNumber"]),
                            SeekerCity = Convert.ToString(rd["SeekerCity"]),
                            SeekerArea = Convert.ToString(rd["SeekerArea"]),
                            SeekerState = Convert.ToString(rd["SeekerState"]),
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
                if (count == 0)
                {
                    seekerreq = null;
                    return seekerreq;
                }
                else
                {
                    return seekerreq;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                List<SeekerRequestHandlerModel> seekerreq = new List<SeekerRequestHandlerModel>();
                return seekerreq;

            }
        }
        public DisplayConfirmationRequestModel GetAllData()
        {
            try
            {
                String ConnectinString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectinString);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "pr_get_all_data_for_helper_side_View";
                command.CommandType = System.Data.CommandType.StoredProcedure;
               
                SqlDataReader rd = command.ExecuteReader();
                DisplayConfirmationRequestModel result = new DisplayConfirmationRequestModel();
                int count = 0;
                while (rd.Read())
                {
                    count++;
                    result.ConfrimationID = Convert.ToInt32(rd["ConfrimationID"]);
                    result.SeekerUserID = Convert.ToInt32(rd["SeekerUserID"]);
                    result.HelperUserID = Convert.ToInt32(rd["HelperUserID"]);
                    result.RequestID = Convert.ToInt32(rd["RequestID"]);
                    result.HelpersCurrentLocationLatLong = Convert.ToString(rd["HelpersCurrentLocationLatLong"]);
                    result.ConfirmationTime = Convert.ToDateTime(rd["ConfirmationTime"]);
                    result.HelperRechabletimetoStartPoint = Convert.ToString(rd["HelperRechabletimetoStartPoint"]);
                    result.HelperDistanceFromStartPoint = Convert.ToString(rd["HelperDistanceFromStartPoint"]);
                    result.HelperUserName = Convert.ToString(rd["HelperUserName"]);
                    result.HelperPhoneNumber = Convert.ToString(rd["HelperPhoneNumber"]);
                    result.SeekerUserName = Convert.ToString(rd["SeekerUserName"]);
                    result.SeekerPhoneNumber = Convert.ToString(rd["SeekerPhoneNumber"]);
                    result.StartPointName = Convert.ToString(rd["StartPointName"]);
                    result.EndPointName = Convert.ToString(rd["EndPointName"]);
                    result.RidedataReachableTime = Convert.ToString(rd["RidedataReachableTime"]);
                    result.RidedataDistance = Convert.ToString(rd["RidedataDistance"]);
                    Console.WriteLine(result);
                }
                if (count > 0)
                {
                    return result;
                }
                else
                {
                    result.HelperUserName = "Not----------------data------------------found----123";
                    return result;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("hey there!");
                DisplayConfirmationRequestModel result = new DisplayConfirmationRequestModel();
                result.HelperUserName = "sql----------------Error------------------found----123";
                return result;
            }
        }
        public int DelteDataWIthID(int id) {
            try
            {
                String ConnectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "pr_delete_request_admin";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                int k = cmd.ExecuteNonQuery();
                return k;
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //List<SeekerRequestHandlerModel> seekerreq = new List<SeekerRequestHandlerModel>();
                return -999;

            }
        }
    }
    }

