using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace IIS_503_Error.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubTest : ControllerBase
    {
        [HttpGet("AllRoutes")]
        public List<CurrentRoutes> Routes()
        {

            List<CurrentRoutes> routes = new List<CurrentRoutes>();
            List<Groups> groups = new List<Groups>();
            List<Routes> RouteList = new List<Routes>();
            //     CurrentRoutes routes = new CurrentRoutes();
            var builder = WebApplication.CreateBuilder();
            var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (var conn = new SqlConnection(connetionString))
                using (var cmd = new SqlCommand("SELECT * FROM Groups", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Groups Group = new Groups();
                        Group.GroupID = (int)reader["GroupID"];
                        Group.GroupName = (string)reader["GroupName"];
                        groups.Add(Group);

                    }
                    conn.Close();

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            Routes routes2 = new Routes();
            routes2.RouteID = 0;
            routes2.Name = "Phone lines open";
            RouteList.Add(routes2);
            try
            {
                using (var conn = new SqlConnection(connetionString))
                using (var cmd = new SqlCommand("SELECT * FROM AdhocRoutes", conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Routes routes1 = new Routes();
                        routes1.RouteID = (int)reader["routeid"]; ;
                        routes1.Name = (string)reader["RouteName"];
                        RouteList.Add(routes1);

                    }
                    conn.Close();

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }


            foreach (Groups group in groups)
            {
                try
                {
                    using (var conn = new SqlConnection(connetionString))
                    using (var command = new SqlCommand("GetCurrentRouteGroup", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@groupid", SqlDbType.Int).Value = group.GroupID;
                        conn.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            CurrentRoutes currentRoute = new CurrentRoutes();

                            currentRoute.GroupName = group.GroupName;
                            currentRoute.RouteName = RouteList[(int)reader["route"]].Name;
                            routes.Add(currentRoute);
                        }
                        conn.Close();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            return routes;
        }
    }
}
