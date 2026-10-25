using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Store_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GodzillaController : ControllerBase
    {
        private IConfiguration _config;
        public GodzillaController(IConfiguration configuration) //Must be same name as controller
        {
            _config = configuration;
        }

        [HttpGet]
        [Route("GetGodzilla")]

        //Adding quick note here too
        public JsonResult GetGodzilla()
        {
            string query = "Select ProductID,ProductName, Price,In_Stock,Pictures" +
                " from Products where ToyBrand = 'Godz'";
            DataTable dt = new DataTable();
            string sqlDatasource = _config.GetConnectionString("products");
            SqlDataReader myreader;
            using (SqlConnection myconn = new SqlConnection(sqlDatasource))
            {
                myconn.Open();
                using (SqlCommand mycmd = new SqlCommand(query, myconn))
                {
                    myreader = mycmd.ExecuteReader();
                    dt.Load(myreader);
                    myreader.Close();
                    myconn.Close();
                }
            }


            return new JsonResult(dt);
        }
    }
}
