using System;
using System.IO;
using LiteDB;

namespace MConnect.AuditLog
{
    public class LiteDbAudit
    {
        private LiteDatabase _db;
        private string _auditTable;

        public LiteDbAudit()
        {
            _db = DbCon();
            _auditTable = "audits";
        }

        private static LiteDatabase DbCon()
        {
            // set the user-data-path
            var userDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "mcAuditLiteDB.db");
            // Open database (or create if doesn't exist)
            using var db = new LiteDatabase(userDataPath);
            return db;
        }

        public string AuditLog(AuditLogType auditLogType, string logTable, string logBy, object tableRecords,
            object newTableRecords)
        {
            var errorMessage = "";
            switch (auditLogType)
            {
                case AuditLogType.Create:
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
                            ? errorMessage + " | Created record(s) information is required."
                            : "Created record(s) information is required.";
                    }

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        return errorMessage;
                    }

                    // TODO: compose Audit model-object
                    break;
                case AuditLogType.Update:
                    break;
                case AuditLogType.Read:
                    break;
                case AuditLogType.Delete:
                    break;
                default:
                    break;
            }
            
            // set the user-data-path
            var userDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "mcAuditLiteDB.db");
            // Open database (or create if doesn't exist)
            using var db = new LiteDatabase(userDataPath);
            // Get a collection (or create, if doesn't exist)
            var col = db.GetCollection<Audit>("audits");

            return "";
        }
    }
}