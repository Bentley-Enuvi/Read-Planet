using System.Net;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Read_Planet.Models.DTOs
{
    public class ResponseObjectDto<T>
    {
        public bool IsSuccessful { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }
        public IEnumerable<Error> Errors { get; set; }

        public ResponseObjectDto()
        {
        }

        

        public ResponseObjectDto(T? data, string message, bool isSuccessful, int statusCode, IEnumerable<Error> errors)
        {
            IsSuccessful = isSuccessful;
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Errors = errors;
        }

        public static ResponseObjectDto<T> Failure(IEnumerable<Error> errors, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            return new ResponseObjectDto<T>(default, string.Empty, false, statusCode, errors);
        }

        public static ResponseObjectDto<T> Success(T data, string successMessage = "", int statusCode = (int)HttpStatusCode.OK)
        {
            return new ResponseObjectDto<T>(data, successMessage, true, statusCode, Array.Empty<Error>());
        }

        public static ResponseObjectDto<T> Success(string successMessage = "", int statusCode = (int)HttpStatusCode.OK)
        {
            return new ResponseObjectDto<T>(default, successMessage, true, statusCode, Array.Empty<Error>());
        }

        public static ResponseObjectDto<T> Fail(IEnumerable<string> errors, int statusCode = (int)HttpStatusCode.OK)
        {
            return new ResponseObjectDto<T>
            {
                IsSuccessful = false,
                //Errors = errors,
                StatusCode = statusCode
            };
        }
    }

    public class Error
    {
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Message { get; }
        public string Code { get; }

        public static readonly IEnumerable<Error> None = Array.Empty<Error>();
        public static readonly Error NullValue = new("Error.NullValue", "the specified result value is null.");
    }
}


