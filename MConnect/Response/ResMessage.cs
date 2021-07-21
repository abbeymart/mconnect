namespace MConnect.Response
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct ResponseMessage<T>
    {
        public string Code;
        public int ResCode;
        public string ResMessage;
        public string Message;
        public T Value;
    }

    public struct ResponseMessageOptions<T>
    {
        public string Code;
        public int ResCode;
        public string ResMessage;
        public string Message;
        public T Value;
    }

    public static class MessageCodes
    {
        public const string ParamsError = "paramsError";
        public const string CheckError = "checkError";
        public const string ValidationError = "validationError";
        public const string ConnectionError = "connectionError";
        public const string TokenExpired = "tokenExpired";
        public const string UnAuthorized = "unAuthorized";
        public const string NotFound = "notFound";
        public const string Success = "success";
        public const string RemoveDenied = "removeDenied";
        public const string RemoveError = "removeError";
        public const string Removed = "removed";
        public const string SubItems = "subItems";
        public const string Duplicate = "duplicate";
        public const string UpdateError = "updateError";
        public const string Updated = "updated";
        public const string UpdateDenied = "updateDenied";
        public const string InsertError = "insertError";
        public const string Inserted = "inserted";
        public const string Exists = "exists";
        public const string Unknown = "unknown";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResMessage<T>
    {
        public static ResponseMessage<T> StandardMessage(string msgCode)
        {
            return msgCode switch
            {
                "paramsError" => new ResponseMessage<T>
                {
                    Code = "paramsError",
                    ResCode = (int) NetStatus.Status.NotAcceptable,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.NotAcceptable),
                    Message = "Parameters checking error",
                    Value = default
                },
                "checkError" => new ResponseMessage<T>()
                {
                    Code = "paramsError",
                    ResCode = (int) NetStatus.Status.NotAcceptable,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.NotAcceptable),
                    Message = "Parameters checking error",
                    Value = default
                },
                "validationError" => new ResponseMessage<T>
                {
                    Code = "validationError",
                    ResCode = (int) NetStatus.Status.NotAcceptable,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.NotAcceptable),
                    Message = "Validation error for input parameters",
                    Value = default
                },
                "connectionError" => new ResponseMessage<T>
                {
                    Code = "connectionError",
                    ResCode = (int) NetStatus.Status.NetworkAuthenticationRequired,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.NetworkAuthenticationRequired),
                    Message = "Connection error",
                    Value = default
                },
                "tokenExpired" => new ResponseMessage<T>
                {
                    Code = "tokenExpired",
                    ResCode = (int) NetStatus.Status.Unauthorized,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                    Message = "Unauthorized. Token / Access-key has expired. Please login again",
                    Value = default
                },
                "unAuthorized" => new ResponseMessage<T>
                {
                    Code = "unAuthorized",
                    ResCode = (int) NetStatus.Status.Unauthorized,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                    Message = "Unauthorized Action or Task",
                    Value = default
                },
                "notFound" => new ResponseMessage<T>
                {
                    Code = "notFound",
                    ResCode = (int) NetStatus.Status.NotFound,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.NotFound),
                    Message = "Requested information not found",
                    Value = default
                },
                "success" => new ResponseMessage<T>
                {
                    Code = "success",
                    ResCode = (int) NetStatus.Status.Ok,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.Ok),
                    Message = "Request completed successfully",
                    Value = default
                },
                "removeDenied" => new ResponseMessage<T>
                {
                    Code = "removeDenied",
                    ResCode = (int) NetStatus.Status.Unauthorized,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                    Message = "Remove action/task denied/unauthorised",
                    Value = default
                },
                "removeError" => new ResponseMessage<T>
                {
                    Code = "removeError",
                    ResCode = (int) NetStatus.Status.NotModified,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.NotModified),
                    Message = "Error removing/deleting information, retry or contact system-admin",
                    Value = default
                },
                "removed" => new ResponseMessage<T>
                {
                    Code = "removed",
                    ResCode = (int) NetStatus.Status.Ok,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.Ok),
                    Message = "Record(s) removed/deleted successfully",
                    Value = default
                },
                "subItems" => new ResponseMessage<T>
                {
                    Code = "subItems",
                    ResCode = (int) NetStatus.Status.Unauthorized,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                    Message = "Record includes sub-items, which must be removed first",
                    Value = default
                },
                "duplicate" => new ResponseMessage<T>
                {
                    Code = "duplicate",
                    ResCode = (int) NetStatus.Status.Unauthorized,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                    Message = "Duplicate record exists",
                    Value = default
                },
                "updateError" => new ResponseMessage<T>
                {
                    Code = "updateError",
                    ResCode = (int) NetStatus.Status.NotModified,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.NotModified),
                    Message = "Error updating information/record(s)",
                    Value = default
                },
                "updated" => new ResponseMessage<T>
                {
                    Code = "updated",
                    ResCode = (int) NetStatus.Status.Ok,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.Ok),
                    Message = "Information/Record(s) updated successfully",
                    Value = default
                },
                "updateDenied" => new ResponseMessage<T>
                {
                    Code = "updateDenied",
                    ResCode = (int) NetStatus.Status.Unauthorized,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                    Message = "Update task/action not authorized",
                    Value = default
                },
                "insertError" => new ResponseMessage<T>
                {
                    Code = "insertError",
                    ResCode = (int) NetStatus.Status.NotModified,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.NotModified),
                    Message = "Error creating/inserting new information/record(s)",
                    Value = default
                },
                "inserted" => new ResponseMessage<T>
                {
                    Code = "inserted",
                    ResCode = (int) NetStatus.Status.Ok,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.Ok),
                    Message = "New information/record(s) created successfully",
                    Value = default
                },
                "exists" => new ResponseMessage<T>
                {
                    Code = "exists",
                    ResCode = (int) NetStatus.Status.NotModified,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.NotModified),
                    Message = "Information/record exists",
                    Value = default
                },
                "unknown" => new ResponseMessage<T>
                {
                    Code = "unknown",
                    ResCode = (int) NetStatus.Status.NotModified,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.NotModified),
                    Message = "Unspecified response/error message",
                    Value = default
                },
                _ => new ResponseMessage<T>
                {
                    Code = "unknown",
                    ResCode = (int) NetStatus.Status.NotModified,
                    ResMessage = NetStatus.StatusText(NetStatus.Status.NotModified),
                    Message = "Unspecified response/error message",
                    Value = default
                }
            };
        }

        public static ResponseMessage<T> ComputeMessage(ResponseMessage<T> msgObj)
        {
            return new()
            {
                Code = msgObj.Code,
                ResCode = msgObj.ResCode,
                ResMessage = msgObj.ResMessage,
                Message = msgObj.Message,
                Value = msgObj.Value
            };
        }
        public static ResponseMessage<T> GetResMessage()
        {
            return new ResponseMessage<T>
            {
                
            };
        }
    }
}