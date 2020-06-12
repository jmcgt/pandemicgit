using System;
using System.Data.SqlClient;

namespace PandemicWeb.Data
{
    public abstract class EntityData : IDisposable
    {
        public SqlConnection connection;
        public SqlDataReader reader;
        public SqlCommand command;

        public EntityData()
        {
            connection = new SqlConnection("Data Source=localhost;Initial Catalog=BDPub2;Integrated Security=true");
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}