using dropmethereapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace dropmethereapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDataController : ControllerBase
    {
        IConfiguration icn;
        public AdminDataController(IConfiguration icu) {
            icn = icu;
        }

        [HttpGet]
        public IActionResult GetAllTables(String Data) {

            try
            {
                
                SqlConnection conn = new SqlConnection(icn.GetConnectionString("ConnectionString"));
                conn.Open();
                SqlCommand cm = conn.CreateCommand();
                cm.CommandText = Data.ToString();
                if (Data.Contains("Delete")) {
                    Console.Write("Delete");
                    return Ok(cm.ExecuteNonQuery());
                }
                if (Data.Contains("UPDATE")) {
                    return Ok(cm.ExecuteNonQuery());
                }
                SqlDataReader rd = cm.ExecuteReader();
                List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

                while (rd.Read())
                {
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    for (int i = 0; i < rd.FieldCount; i++)
                    {
                        row[rd.GetName(i)] = rd.GetValue(i);
                    }
                    results.Add(row);
                }
                return Ok(results);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
