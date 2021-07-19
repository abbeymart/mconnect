using System;
using System.IO;
using Newtonsoft.Json;
using SQLite;
using ToJSON;

namespace MConnect.AuditLog
{
    public enum AuditLogType
    {
        Create,
        Update,
        Read,
        Delete,
        Login,
        Logout,
    }

    public enum SysAuditLogType
    {
        App,
        Data,
        Env,
        Other,
        Unknown,
    }

    [Table("audits")]
    public class Audit
    {
        [PrimaryKey]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("log_type")] public AuditLogType LogType { get; set; }
        [Column("log_table")] public string LogTable { get; set; }
        [Column("table_records")] public string TableRecords { get; set; }
        [Column("log_by")] public string LogBy { get; set; }
        [Column("new_table_records")] public string NewTableRecords { get; set; }
    }

    [Table("sys_audits")]
    public class SysAudit
    {
        [PrimaryKey]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("log_type")] public SysAuditLogType LogType { get; set; }
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

        public string AuditLog(AuditLogType logType, string logTable, object tableRecords,
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

            var record = new Audit();
            switch (logType)
            {
                case AuditLogType.Create:
                case AuditLogType.Read:
                case AuditLogType.Delete:
                case AuditLogType.Login:
                case AuditLogType.Logout:
                    // TODO: compose Audit model-object
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

                    // TODO: compose Audit model-object
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
                    return $"Non-support crud-audit-log-type: {logType.ToString()}";
            }

            // perform crud-task
            using var conn = DbCon();
            var result = conn.Insert(record);
            return result < 1 ? "error" : "success";
        }

        public string SystemAuditLog(SysAuditLogType logType, string logMessage,
            string logBy = "", string logCode= "SYS100", string logDesc = "")
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
            
            // TODO: compose Audit model-object
            var record = new SysAudit
            {
                Id = new Guid(),
                LogType = logType,
                LogMessage = logMessage,
                LogBy = logBy,
                LogCode = logCode,
                LogDesc = logDesc,
            };

            // perform crud-task
            using var conn = DbCon();
            var result = conn.Insert(record);
            return result < 1 ? "error" : "success";
        }
    }
}