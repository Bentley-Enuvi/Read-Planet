using System.Net;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Read_Planet.Models.DTOs
{
    public class ResponseObjectDto
    {
        public object? Result { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public int StatusCode { get; set; }
        public string Message { get; set; } = "";

    }
}


