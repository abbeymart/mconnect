using System;
using System.Data.SQLite;
using System.IO;

namespace ConsoleApp1
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // const string cs = @"URI=file:C:\Users\abbey\Documents\test.db";
            // var userDocFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var userDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "mctest.db");
            var cs = $@"URI=file:{userDataPath}";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con)
            {
                CommandText = $"INSERT INTO cars(name, price) VALUES(@name, @price)"
            };

            cmd.Parameters.AddWithValue("@name", "BMW");
            cmd.Parameters.AddWithValue("@price", 36600);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Console.WriteLine("row inserted");
        }
    }
}