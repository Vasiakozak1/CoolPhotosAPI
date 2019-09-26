using System;
using System.Net;

namespace CoolPhotosAPI.BL.Exceptions
{
    public abstract class BaseException: Exception
    {
        public HttpStatusCode HttpStatusCode { get; private set; }
        protected BaseException(string message, HttpStatusCode statusCode)
            : base($"{message}, status code: {statusCode}")
        {
            HttpStatusCode = statusCode;
        }
    }
}
