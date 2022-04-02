using System;
using System.IO;
using Newtonsoft.Json;
using SQLite;
using ToJSON;

namespace MConnect.AuditLog
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuditLogType
    {
        public const string Create = "create";
        public const string Update = "update";
        public const string Read = "read";
        public const string Delete = "delete";
        public const string Login = "login";
        public const string Logout = "logout";
    }

    /// <summary>
    /// 
    /// </summary>
    [Table("audits")]
    public class Audit
    {
        [PrimaryKey] [Column("id")] public Guid Id { get; set; }

        [Column("log_type")] public string LogType { get; set; }
        [Column("log_table")] public string LogTable { get; set; }
        [Column("table_records")] public string LogRecords { get; set; }
        [Column("log_by")] public string LogBy { get; set; }
        [Column("new_table_records")] public string NewLogRecords { get; set; }
    }

    public class SqLiteAudit
    {
        private static SQLiteConnection DbCon()
        {
            // set the user-data-path
            var dataSource = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "mcAudit.db");
            // compose the connection string
            var options = new SQLiteConnectionString(dataSource, false);
            // activate/establish db-connection
            var conn = new SQLiteConnection(options);
            // create table, if not exist
            conn.CreateTable<Audit>();
            return conn;
        }

        public string AuditLog(string logType, string logTable, object tableRecords,
            string logBy = "", object newTableRecords = default)
        {
            var errorMessage = "";
            // validate params/values
            if (string.IsNullOrEmpty(logTable))
            {
                errorMessage = !string.IsNullOrEmpty(errorMessage)
                    ? errorMessage + " | Table or Collection name is required."
                    : "Table or Collection name is required.";
            }

            if (string.IsNullOrEmpty(logBy))
            {
                errorMessage = !string.IsNullOrEmpty(errorMessage)
                    ? errorMessage + " | userId is required."
                    : "userId is required.";
            }

            if (tableRecords is null)
            {
                errorMessage = !string.IsNullOrEmpty(errorMessage)
                    ? errorMessage + " | Crud-Task record(s) information is required."
                    : "Crud-Task record(s) information is required.";
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return errorMessage;
            }

            Audit record;
            switch (logType.ToLower())
            {
                case AuditLogType.Create:
                case AuditLogType.Read:
                case AuditLogType.Delete:
                case AuditLogType.Login:
                case AuditLogType.Logout:
                    // compose Audit model-object
                    record = new Audit
                    {
                        Id = new Guid(),
                        LogType = logType,
                        LogTable = logTable,
                        LogRecords = tableRecords.ToJSON(),
                        LogBy = logBy
                    };
                    break;
                case AuditLogType.Update:
                    if (newTableRecords is null)
                    {
                        errorMessage = !string.IsNullOrEmpty(errorMessage)
                            ? errorMessage + " | Update record(s) information is required."
                            : "Update record(s) information is required.";
                    }

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        return errorMessage;
                    }

                    // compose Audit model-object
                    record = new Audit
                    {
                        Id = new Guid(),
                        LogType = logType,
                        LogTable = logTable,
                        LogRecords = tableRecords.ToJSON(),
                        LogBy = logBy,
                        NewLogRecords = newTableRecords.ToJSON()
                    };
                    var fromJson = JsonConvert.DeserializeObject(record.LogRecords);
                    Console.WriteLine($"{fromJson}");
                    break;
                default:
                    return $"Non-supported crud-audit-log-type: {logType}";
            }

            // perform crud-task
            using var conn = DbCon();
            var result = conn.Insert(record);
            return result < 1 ? "error" : "success";
        }
    }
}