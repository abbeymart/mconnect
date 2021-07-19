using System;
using System.IO;
using Newtonsoft.Json;
using SQLite;
using ToJSON;

namespace MConnect.AuditLog
{
    public enum AuditLog1Type
    {
        Create,
        Update,
        Read,
        Delete,
        Login,
        Logout,
    }

    public enum SysAuditLog1Type
    {
        App,
        Data,
        Env,
        Other,
        Unknown,
    }

    public static class AuditLogType
    {
        public const string Create = "create";
        public const string Update = "update";
        public const string Read = "read";
        public const string Delete = "delete";
        public const string Login = "login";
        public const string Logout = "logout";
    }

    public static class SysAuditLogType
    {
        public const string App = "app";
        public const string Data = "data";
        public const string Env = "env";
        public const string Other = "other";
        public const string Unknown = "unknown";
    }

    [Table("audits")]
    public class Audit
    {
        [PrimaryKey] [Column("id")] public Guid Id { get; set; }

        [Column("log_type")] public string LogType { get; set; }
        [Column("log_table")] public string LogTable { get; set; }
        [Column("table_records")] public string TableRecords { get; set; }
        [Column("log_by")] public string LogBy { get; set; }
        [Column("new_table_records")] public string NewTableRecords { get; set; }
    }

    [Table("sys_audits")]
    public class SysAudit
    {
        [PrimaryKey] [Column("id")] public Guid Id { get; set; }

        [Column("log_type")] public string LogType { get; set; }
        [Column("log_message")] public string LogMessage { get; set; }
        [Column("log_by")] public string LogBy { get; set; }
        [Column("log_code")] public string LogCode { get; set; }
        [Column("log_desc")] public object LogDesc { get; set; }
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
            return conn;
        }

        private static SQLiteConnection SysDbCon()
        {
            // set the user-data-path
            var dataSource = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "mcSysAudit.db");
            // compose the connection string
            var options = new SQLiteConnectionString(dataSource, false);
            // activate/establish db-connection
            var conn = new SQLiteConnection(options);
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
                        TableRecords = tableRecords.ToJSON(),
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
                        TableRecords = tableRecords.ToJSON(),
                        LogBy = logBy,
                        NewTableRecords = newTableRecords.ToJSON()
                    };
                    var fromJson = JsonConvert.DeserializeObject(record.TableRecords);
                    Console.WriteLine($"{fromJson}");
                    break;
                default:
                    return $"Non-supported crud-audit-log-type: {logType.ToString()}";
            }

            // perform crud-task
            using var conn = DbCon();
            var result = conn.Insert(record);
            return result < 1 ? "error" : "success";
        }

        public string SystemAuditLog(string logType, string logMessage,
            string logBy = "", string logCode = "SYS100", string logDesc = "")
        {
            var errorMessage = "";
            // validate params/values
            if (string.IsNullOrEmpty(logMessage))
            {
                errorMessage = !string.IsNullOrEmpty(errorMessage)
                    ? errorMessage + " | logMessage is required."
                    : "logMessage is required.";
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return errorMessage;
            }

            // compose SysAudit model-object
            SysAudit record;

            switch (logType.ToLower())
            {
                case SysAuditLogType.App:
                case SysAuditLogType.Env:
                case SysAuditLogType.Data:
                case SysAuditLogType.Other:
                case SysAuditLogType.Unknown:
                    record = new SysAudit
                    {
                        Id = new Guid(),
                        LogType = logType,
                        LogMessage = logMessage,
                        LogBy = logBy,
                        LogCode = logCode,
                        LogDesc = logDesc,
                    };
                    break;
                default:
                    return $"Non-supported crud-audit-log-type: {logType.ToString()}";
            }
            // perform crud-task
            using var conn = DbCon();
            var result = conn.Insert(record);
            return result < 1 ? "error" : "success";
        }
    }
}