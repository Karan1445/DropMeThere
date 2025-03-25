using dropmethereapi.Models;
using System.Data.SqlClient;

namespace dropmethereapi.Repos.SeekerRequestHandler
{
    public class SeekerRequestHandler
    {
        private readonly IConfiguration _configuration;
        public SeekerRequestHandler(IConfiguration icn) {
        _configuration = icn;
        }
        public bool MakeANewRequest_Inset(SeekerRequestHandlerModel srm) {
            try
            {
                String ConnectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Pr_Api_SeekerRequestTable_Insert";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", srm.UserID);
                cmd.Parameters.AddWithValue("@UserName", srm.UserName);
                cmd.Parameters.AddWithValue("@PhoneNumber", srm.PhoneNumber);
                cmd.Parameters.AddWithValue("@SeekerCity", srm.SeekerCity);
                cmd.Parameters.AddWithValue("@SeekerArea", srm.SeekerArea);
                cmd.Parameters.AddWithValue("@SeekerState", srm.SeekerState);
                cmd.Parameters.AddWithValue("@CurrentLocationLatLong", srm.CurrentLocationLatLong);
                cmd.Parameters.AddWithValue("@RideStartPointLatLong", srm.RideStartPointLatLong);
                cmd.Parameters.AddWithValue("@RideEndPointLatLong", srm.RideEndPointLatLong);
                cmd.Parameters.AddWithValue("@ReqTime", srm.ReqTime);
                cmd.Parameters.AddWithValue("@StartPointName", srm.StartPointName);
                cmd.Parameters.AddWithValue("@EndPointName", srm.EndPointName);
                cmd.Parameters.AddWithValue("@Distance", srm.Distance);
                cmd.Parameters.AddWithValue("@ReachableTime", srm.ReachableTime);

                int k = cmd.ExecuteNonQuery();
                return k > 0;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
          
        }
        public List<SeekerRequestHandlerModel> GetAllRequestOfPerticularUser(int UserID) {
            try
            {
                String ConnectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Pr_Api_SeekerRequestTable_SelectAll_fro_perticularUser";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID",UserID);
                SqlDataReader rd = cmd.ExecuteReader();
                List<SeekerRequestHandlerModel> seekerreq = new List<SeekerRequestHandlerModel>();
                int count = 0;
                while (rd.Read()) {
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
                else {
                    return seekerreq;
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                List<SeekerRequestHandlerModel> seekerreq = new List<SeekerRequestHandlerModel>();
                return seekerreq;
                
            }
            }
        public List<SeekerRequestHandlerModel> GetAllData()
        {
            try
            {
                String ConnectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "pr_get_all_Requests_fromtable";
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
        public bool DeleteRequestByRequestID(int RequesID) {
            try{
                String ConnectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Pr_Api_SeekerRequestTable_Delete";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestID", RequesID);
                

                int k = cmd.ExecuteNonQuery();
                return k > 0;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        public bool UpdateRequetByRequestID(SeekerRequestHandlerModel srm)
        {
            try
            {
                String ConnectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Pr_Api_SeekerRequestTable_Update";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestID", srm.RequestID);
                cmd.Parameters.AddWithValue("@UserID", srm.UserID);
                cmd.Parameters.AddWithValue("@UserName", srm.UserName);
                cmd.Parameters.AddWithValue("@PhoneNumber", srm.PhoneNumber);
                cmd.Parameters.AddWithValue("@SeekerCity", srm.SeekerCity);
                cmd.Parameters.AddWithValue("@SeekerArea", srm.SeekerArea);
                cmd.Parameters.AddWithValue("@SeekerState", srm.SeekerState);
                cmd.Parameters.AddWithValue("@CurrentLocationLatLong", srm.CurrentLocationLatLong);
                cmd.Parameters.AddWithValue("@RideStartPointLatLong", srm.RideStartPointLatLong);
                cmd.Parameters.AddWithValue("@RideEndPointLatLong", srm.RideEndPointLatLong);
                cmd.Parameters.AddWithValue("@ReqTime", srm.ReqTime);
                cmd.Parameters.AddWithValue("@StartPointName", srm.StartPointName);
                cmd.Parameters.AddWithValue("@EndPointName", srm.EndPointName);
                cmd.Parameters.AddWithValue("@Distance", srm.Distance);
                cmd.Parameters.AddWithValue("@ReachableTime", srm.ReachableTime);

                int k = cmd.ExecuteNonQuery();
                Console.Write(k);
                return k > 0;
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
                return false;
            }

        }
        public SeekerRequestHandlerModel GetARequestByID(int RequestID)
        {
            try
            {
                String ConnectionString = _configuration.GetConnectionString("ConnectionString");
                SqlConnection connection = new SqlConnection(ConnectionString);
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Pr_Api_SeekerRequestTable_SelectByID";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RequestID", RequestID);
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
                    return seekerreq[0];
                }
                else
                {
                    return seekerreq[0];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                List<SeekerRequestHandlerModel> seekerreq = new List<SeekerRequestHandlerModel>();
                return seekerreq[0];

            }
        }
    }
}
