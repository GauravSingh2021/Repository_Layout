using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository.Models;
using System.Data.SqlClient;

namespace Repository.Models
{
    public class UsersRepository : IUsers
    {
        private SqlConnection con = new SqlConnection("Data Source=CHETUIWK0101;Initial Catalog=RepositoryDB;User ID=sa;Password=Chetu@123");
        private SqlCommand com = new SqlCommand();
        private SqlDataReader dr;
        private string str;

        public UsersRepository(string str)
        {
            this.str = str;
        }

        public bool FindDuplicate(string Email)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from Repositorytbl where Email='" + Email + "'";
            dr = com.ExecuteReader();
            
            List<Users> u = new List<Users>();

            while(dr.Read())
            {
                var x = new Users
                {
                    Id = dr.GetInt32 (0),
                    FullName = dr.GetString(1),
                    Email = dr.GetString(2),
                    Password = dr.GetString(3),
                    ConfirmPassword = dr.GetString(4)
                };
                u.Add(x);
            }

            con.Close();

            if(u.Count>=1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Register(Users u)
        {
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "Insert into Repositorytbl values ('" + u.FullName + "', '" + u.Email + "', '" + u.Password + "', '" + u.ConfirmPassword + "')";
                com.ExecuteNonQuery();
                con.Close();

                return true;
            }
            catch(Exception)
            {
                if(con.State==System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
                return false;
            }
        }

        public bool Verify(string Email, string Password)
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "Select * from Repositorytbl where Email= '" + Email + "' and Password = '" + Password + "'";
            dr = com.ExecuteReader();

            List<Users> u = new List<Users>();

            while(dr.Read())
            {
                var x = new Users()
                {
                    FullName = dr.GetString(1),
                    Email = dr.GetString(2),
                    Password = dr.GetString(3),
                    ConfirmPassword = dr.GetString(4)
                };

                u.Add(x);
            }

            con.Close();
            if(u.Count==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
