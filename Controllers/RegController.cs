using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Store_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegController : ControllerBase
    {
        private IConfiguration _config;
        public RegController(IConfiguration configuration) //Must be same name as controller
        {
            _config = configuration;
        }

        [HttpPost]
        [Route("RegisterUser")]

        public JsonResult RegisterUser(Register reg)
        {
            string query = @"Insert into Users (FirstName, LastName, UserName, Password)
                 Values ('" + reg.FirstName + @"',
                         '" + reg.LastName + @"',
                         '" + reg.UserName + @"',
                         '" + reg.Password + @"')";

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



        [HttpGet]
        [Route("MyCart")]

        public JsonResult MyCart(int id, int ProdID)
        {
            string query = @"Select ProductID, ProductName, Price 
                             Into #" + id + "Cart " +
                             "Where ProductID = " + ProdID;

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
