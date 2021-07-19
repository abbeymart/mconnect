using System;
using System.IO;
using SQLite;

namespace MConnect.AuditLog.Sys
{
    /// <summary>
    /// System audit log types
    /// </summary>
    public static class AuditLogType
    {
        public const string App = "app";
        public const string Data = "data";
        public const string Env = "env";
        public const string Other = "other";
        public const string Unknown = "unknown";
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Table("audits")]
    public class Audit
    {
        [PrimaryKey] [Column("id")] public Guid Id { get; set; }

        [Column("log_type")] public string LogType { get; set; }
        [Column("log_message")] public string LogMessage { get; set; }
        [Column("log_by")] public string LogBy { get; set; }
        [Column("log_code")] public string LogCode { get; set; }
        [Column("log_desc")] public object LogDesc { get; set; }
    }


    /// <summary>
    /// System Audit log class
    /// </summary>
    public class SqLiteAudit
    {
        private static SQLiteConnection DbCon()
        {
            // set the user-data-path
            var dataSource = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "mcSysAudit.db");
            // compose the connection string
            var options = new SQLiteConnectionString(dataSource, false);
            // activate/establish db-connection
            var conn = new SQLiteConnection(options);
            // create table, if not exist
            conn.CreateTable<Audit>();
            return conn;
        }

        public string AuditLog(string logType, string logMessage,
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
            Audit record;

            switch (logType.ToLower())
            {
                case AuditLogType.App:
                case AuditLogType.Env:
                case AuditLogType.Data:
                case AuditLogType.Other:
                case AuditLogType.Unknown:
                    record = new Audit
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
                    return $"Non-supported system-audit-log-type: {logType}";
            }
            // perform crud-task
            using var conn = DbCon();
            var result = conn.Insert(record);
            return result < 1 ? "error" : "success";
        }
    }
}