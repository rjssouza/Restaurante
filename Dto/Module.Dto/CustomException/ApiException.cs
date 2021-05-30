using System;
using System.Net;

namespace Module.Dto.CustomException
{
    public class ApiException : Exception
    {
        private readonly HttpStatusCode _statusCode;

        public ApiException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            this._statusCode = statusCode;
        }

        public HttpStatusCode StatusCode => this._statusCode;
    }
}