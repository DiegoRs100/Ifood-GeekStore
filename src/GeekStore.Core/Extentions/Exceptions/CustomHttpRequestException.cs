using System;
using System.Net;

namespace GeekStore.Core.Extentions.Exceptions
{
    public class CustomHttpRequestException : Exception
    {
        #region Properties

        public HttpStatusCode StatusCode;

        #endregion

        public CustomHttpRequestException() : base()
        { }

        public CustomHttpRequestException(string message) : base(message)
        { }

        public CustomHttpRequestException(string message, Exception innerException) : base(message, innerException)
        { }

        public CustomHttpRequestException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}