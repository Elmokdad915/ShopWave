using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request you have made",
                401 => "Access denied. You are not authorized to view this resource.",
                404 => "The page or resource you are looking for could not be found. Check the URL for any errors, or go back to the homepage and try again.",
                500 => "We are sorry, but something went wrong on our end. Please try again later.",
                _ => null
            };
        }

    }
}