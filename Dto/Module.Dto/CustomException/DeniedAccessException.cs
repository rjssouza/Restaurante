using System;
using System.Net;

namespace Module.Dto.CustomException
{
    public class DeniedAccessException : ApiException
    {
        public DeniedAccessException(string message) 
            : base(HttpStatusCode.Unauthorized, message)
        {
        }
    }
}