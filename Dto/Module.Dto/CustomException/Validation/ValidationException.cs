using System;
using System.Net;

namespace Module.Dto.CustomException.Validation
{
    public class ValidationException : ApiException
    {
        public ValidationException(Summary summary)
            : base(HttpStatusCode.BadRequest, summary.ToString())
        {
        }

        public ValidationException(string message)
            : base(HttpStatusCode.BadRequest, message)
        {
        }
    }
}