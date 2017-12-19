using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.Data.Sql;
using System;
using System.Data.SqlClient;
using System.Text;
using IntermountainHealth.DAL;

namespace IntermountainHealth.Models
{
    public class PatientListModel
    {
        private DataContext db = new DataContext();
        public IEnumerable<PatientModel> Items;

        public string results { get; set; }

        public void LoadItems()
        {
            if (Items == null)
            {
                Items = db.Database.SqlQuery<PatientModel>("SELECT * FROM PATIENT").ToList();
            }
            //Items = context.PatientModel.ToList();
        }

        public void Main()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "intermountainhealthcare.database.windows.net";
                builder.UserID = "datafactory415";
                builder.Password = "Password!";
                builder.InitialCatalog = "IntermountainHealthcare415";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT * ");
                    sb.Append("FROM Patient ");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                                results = reader.GetString(0) + reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }
    }
}