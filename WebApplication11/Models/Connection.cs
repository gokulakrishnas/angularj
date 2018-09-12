using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace WebApplication11.Models
{
    public class Connection
    {
       
        public string GetConnectionString()
        {

            return System.Configuration.ConfigurationManager.ConnectionStrings["User"].ConnectionString;

        }

        public IEnumerable<Register> Get()
        {
            List<Register> Reg = new List<Register>();
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                using(SqlCommand cmd =new SqlCommand("spGetAllEmployees ", connection))
            {
                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Register employee = new Register();
                    employee.username = rdr["Username"].ToString();
                    employee.password = rdr["Passwords"].ToString();
                   
                    employee.EmailAddress = rdr["EmailAddress"].ToString();

                    Reg.Add(employee);
                }
                connection.Close();
            }
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //return Reg=(js.Serialize(Reg));
            return Reg;
        }
        public int AddRegister(Register Reg)
        {
            String query = "Insert into Register (Username,EmailAddress,Passwords,Confirmpassword) VALUES (@Username,@EmailAddress, @Passwords, @Confirmpassword)";
            string encryptpassword1 = encryptpass(Reg.password);
            string encryptpassword2 = encryptpass(Reg.confirmPassword);
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@EmailAddress", SqlDbType.VarChar).Value = Reg.EmailAddress;
                command.Parameters.Add("@Username", SqlDbType.VarChar).Value = Reg.username;
                command.Parameters.Add("@Passwords", SqlDbType.VarChar).Value = encryptpassword1;
                command.Parameters.Add("@Confirmpassword", SqlDbType.VarChar).Value = encryptpassword2;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            return 1;
        }
        public bool GetData(Register Reg)
        {
        bool message = true;
            SqlConnection con = new SqlConnection(GetConnectionString());
            //Register op = new Register();

            string encryptpassword1 = encryptpass(Reg.password);
            con.Open();

            SqlCommand cmd = new SqlCommand("LoginUser", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter p1 = new SqlParameter("Username", Reg.username);

            SqlParameter p2 = new SqlParameter("Password", encryptpassword1);

            cmd.Parameters.Add(p1);

            cmd.Parameters.Add(p2);

            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)

            {

                rd.Read();



                //FormsAuthentication.RedirectFromLoginPage(Reg.username, true);

                return message;

            }
            else
            {
                return message = false;
            }
           
        }
        public string encryptpass(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }
    }



}
   