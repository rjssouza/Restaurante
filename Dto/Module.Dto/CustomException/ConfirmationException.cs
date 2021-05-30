using System;
using System.Net;

namespace Module.Dto.CustomException
{
    public class ConfirmationException : ApiException
    {
        public ConfirmationException(string message) 
            : base(HttpStatusCode.Conflict , message)
        {
        }
    }
}