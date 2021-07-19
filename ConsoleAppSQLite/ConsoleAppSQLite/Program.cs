using System;
using System.Data.SQLite;
using System.IO;

namespace ConsoleAppSQLite
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("C#-SQLite App \n");

            // const string cs = @"URI=file:C:\Users\abbey\Documents\test.db";
            // set the user-data-path
            var userDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "mctest.db");
            Console.WriteLine($"User Data Path: {userDataPath} \n");
            // User env-value: C:\Users\abbey\OneDrive\Documents\mctest.db
            var cs = $@"URI=file:{userDataPath}";
            
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con) {CommandText = $"DROP TABLE IF EXISTS cars"};

            cmd.ExecuteNonQuery();

            cmd.CommandText = $@"CREATE TABLE cars(id INTEGER PRIMARY KEY, name TEXT, price INT)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $"INSERT INTO cars(name, price) VALUES('Audi',52642)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $"INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $"INSERT INTO cars(name, price) VALUES('Skoda',9000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $"INSERT INTO cars(name, price) VALUES('Volvo',29000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $"INSERT INTO cars(name, price) VALUES('Bentley',350000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $"INSERT INTO cars(name, price) VALUES('Citroen',21000)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $"INSERT INTO cars(name, price) VALUES('Hummer',41400)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $"INSERT INTO cars(name, price) VALUES('Volkswagen',21600)";
            cmd.ExecuteNonQuery();

            Console.WriteLine("Table cars created");
        }
    }
}