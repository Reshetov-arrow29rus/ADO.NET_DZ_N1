using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET_DZ_N1
{
    public class AddObject
    {
        public string TitleObject { get; set; }
        public string TypeObject { get; set; }
        public string ColourObject { get; set; }
        public float CaloriesObject { get; set; }

        private SqlConnection connection;
        public AddObject(SqlConnection conn)
        {
            this.connection = conn;
        }

        public void InsertData()
        {
        string query = "INSERT INTO Table_Vegetables_and_Fruits (Title, Type, Сolour, Calories) VALUES (@Value1, @Value2, @Value3, @Value4)";

            using (SqlCommand comm = new SqlCommand(query, connection))
                {
                    comm.Parameters.AddWithValue("@Value1", TitleObject);
                    comm.Parameters.AddWithValue("@Value2", TypeObject);
                    comm.Parameters.AddWithValue("@Value3", ColourObject);
                    comm.Parameters.AddWithValue("@Value4", CaloriesObject);

                    comm.ExecuteNonQuery();
                }
        }

    }
}
