using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub
{
    class DataBase
    {
        SqlConnection con = new SqlConnection(@"workstation id=BooksClub.mssql.somee.com;packet size=4096;user id=Malikov_SQLLogin_1;pwd=9fer4gmd28;data source=BooksClub.mssql.somee.com;persist security info=False;initial catalog=BooksClub;TrustServerCertificate=True");
        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }
        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
        public SqlConnection getConnection()
        {
            return con;
        }
    }
}
