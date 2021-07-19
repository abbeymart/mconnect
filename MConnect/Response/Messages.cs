using System.Collections.Generic;

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

    public struct ResponseMessageOptions
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Messages<T>
    {
        public readonly Dictionary<string, ResponseMessage<T>> MessageParam = default;

        public Messages()
        {
            MessageParam.Add("paramsError", new ResponseMessage<T>
            {
                Code = "paramsError",
                ResCode = (int)NetStatus.Status.NotAcceptable,
                ResMessage = NetStatus.StatusText(NetStatus.Status.NotAcceptable),
                Message = "Parameters checking error",
                Value = default
            });
            MessageParam.Add("checkError", new ResponseMessage<T>
            {
                Code = "paramsError",
                ResCode = (int)NetStatus.Status.NotAcceptable,
                ResMessage = NetStatus.StatusText(NetStatus.Status.NotAcceptable),
                Message = "Parameters checking error",
                Value = default
            });
            MessageParam.Add("validationError", new ResponseMessage<T>
            {
                Code = "validationError",
                ResCode = (int)NetStatus.Status.NotAcceptable,
                ResMessage = NetStatus.StatusText(NetStatus.Status.NotAcceptable),
                Message = "Validation error for input parameters",
                Value = default
            });
            MessageParam.Add("connectionError", new ResponseMessage<T>
            {
                Code = "connectionError",
                ResCode = (int)NetStatus.Status.NetworkAuthenticationRequired,
                ResMessage = NetStatus.StatusText(NetStatus.Status.NetworkAuthenticationRequired),
                Message = "Connection error",
                Value = default
            });
            MessageParam.Add("tokenExpired", new ResponseMessage<T>
            {
                Code = "tokenExpired",
                ResCode = (int)NetStatus.Status.Unauthorized,
                ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                Message = "Unauthorized. Token / Access-key has expired. Please login again",
                Value = default
            });
            MessageParam.Add("unAuthorized", new ResponseMessage<T>
            {
                Code = "unAuthorized",
                ResCode = (int)NetStatus.Status.Unauthorized,
                ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                Message = "Unauthorized Action or Task",
                Value = default
            });
            MessageParam.Add("notFound", new ResponseMessage<T>
            {
                Code = "notFound",
                ResCode = (int)NetStatus.Status.NotFound,
                ResMessage = NetStatus.StatusText(NetStatus.Status.NotFound),
                Message = "Requested information not found",
                Value = default
            });
            MessageParam.Add("success", new ResponseMessage<T>
            {
                Code = "success",
                ResCode = (int)NetStatus.Status.Ok,
                ResMessage = NetStatus.StatusText(NetStatus.Status.Ok),
                Message = "Request completed successfully",
                Value = default
            });
            MessageParam.Add("removeDenied", new ResponseMessage<T>
            {
                Code = "removeDenied",
                ResCode = (int)NetStatus.Status.Unauthorized,
                ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                Message = "Remove action/task denied/unauthorised",
                Value = default
            });
            MessageParam.Add("removeError", new ResponseMessage<T>
            {
                Code = "removeError",
                ResCode = (int)NetStatus.Status.NotModified,
                ResMessage = NetStatus.StatusText(NetStatus.Status.NotModified),
                Message = "Error removing/deleting information, retry or contact system-admin",
                Value = default
            });
            MessageParam.Add("removed", new ResponseMessage<T>
            {
                Code = "removed",
                ResCode = (int)NetStatus.Status.Ok,
                ResMessage = NetStatus.StatusText(NetStatus.Status.Ok),
                Message = "Record(s) removed/deleted successfully",
                Value = default
            });
            MessageParam.Add("subItems", new ResponseMessage<T>
            {
                Code = "subItems",
                ResCode = (int)NetStatus.Status.Unauthorized,
                ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                Message = "Record includes sub-items, which must be removed first",
                Value = default
            });
            MessageParam.Add("duplicate", new ResponseMessage<T>
            {
                Code = "duplicate",
                ResCode = (int)NetStatus.Status.Unauthorized,
                ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                Message = "Duplicate record exists",
                Value = default
            });
            MessageParam.Add("updateError", new ResponseMessage<T>
            {
                Code = "updateError",
                ResCode = (int)NetStatus.Status.NotModified,
                ResMessage = NetStatus.StatusText(NetStatus.Status.NotModified),
                Message = "Error updating information/record(s)",
                Value = default
            });
            MessageParam.Add("updated", new ResponseMessage<T>
            {
                Code = "updated",
                ResCode = (int)NetStatus.Status.Ok,
                ResMessage = NetStatus.StatusText(NetStatus.Status.Ok),
                Message = "Information/Record(s) updated successfully",
                Value = default
            });
            MessageParam.Add("updateDenied", new ResponseMessage<T>
            {
                Code = "updateDenied",
                ResCode = (int)NetStatus.Status.Unauthorized,
                ResMessage = NetStatus.StatusText(NetStatus.Status.Unauthorized),
                Message = "Update task/action not authorized",
                Value = default
            });
            MessageParam.Add("insertError", new ResponseMessage<T>
            {
                Code = "insertError",
                ResCode = (int)NetStatus.Status.NotModified,
                ResMessage = NetStatus.StatusText(NetStatus.Status.NotModified),
                Message = "Error creating/inserting new information/record(s)",
                Value = default
            });
            MessageParam.Add("inserted", new ResponseMessage<T>
            {
                Code = "inserted",
                ResCode = (int)NetStatus.Status.Ok,
                ResMessage = NetStatus.StatusText(NetStatus.Status.Ok),
                Message = "New information/record(s) created successfully",
                Value = default
            });
            MessageParam.Add("exists", new ResponseMessage<T>
            {
                Code = "exists",
                ResCode = (int)NetStatus.Status.NotModified,
                ResMessage = NetStatus.StatusText(NetStatus.Status.NotModified),
                Message = "Information/record exists",
                Value = default
            });
            MessageParam.Add("unknown", new ResponseMessage<T>
            {
                Code = "unknown",
                ResCode = (int)NetStatus.Status.NotModified,
                ResMessage = NetStatus.StatusText(NetStatus.Status.NotModified),
                Message = "Unspecified response/error message",
                Value = default
            });
        }
    }
}