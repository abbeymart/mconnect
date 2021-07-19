using System;
using System.Data.SQLite;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        private static void Main(string[] args)
        {
            // Console.WriteLine($"Main arguments: {args}");
            var userDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "mctest.db");
            var cs = $@"URI=file:{userDataPath}";

            using var con = new SQLiteConnection(cs);
            con.Open();
            
            var stm = $"SELECT * FROM cars LIMIT 5";
            
            using var cmd = new SQLiteCommand(stm, con);
            
            using var rdr = cmd.ExecuteReader();
            Console.WriteLine($"{rdr.GetName(0), -3} {rdr.GetName(1), -8} {rdr.GetName(2), 8}");
            
            while (rdr.Read())
            {
                Console.WriteLine($@"{rdr.GetInt32(0), -3} {rdr.GetString(1), -8} {rdr.GetInt32(2), 8}");
            }
        }
    }
}