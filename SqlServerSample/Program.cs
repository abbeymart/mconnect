using System;
using System.Text;
using System.Data.SqlClient;

namespace SqlServerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Build connection string
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = @"MCONNECTPC\SQLEXPRESS",
                    UserID = "sa",
                    Password = "ab12admin",
                    InitialCatalog = "master"
                };

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using var connection = new SqlConnection(builder.ConnectionString);
                connection.Open();
                Console.WriteLine("Done.");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }
    }
}