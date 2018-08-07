using CDModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceXDAL
{
    public class DALAccount
    {
        string connectionString = ConfigurationManager.ConnectionStrings["CDConnection"].ConnectionString;
        public loggedinUser Login(LoginBindingModel obj)
        {
            loggedinUser result = new loggedinUser();


            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(StoreProcedure.SPLogin, con);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@UserName", obj.Email));
            command.Parameters.Add(new SqlParameter("@Password", obj.Password));
            using (con)
            {
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        result.userID = Convert.ToInt32(reader["userid"]);
                        result.HomeId = Convert.ToString(reader["Homeid"]);
                    }
                }
            }
            if (result != null)
                return result;
            else
                return null;
        }

    }
}
