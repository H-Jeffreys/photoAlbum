using System;
using System.Net;

namespace PhotoAlbum.Core.Exceptions
{
    public class HttpDependencyException: Exception
    {
        private string Name { get; }
        public HttpStatusCode StatusCode { get; }
        
        public HttpDependencyException(string name, HttpStatusCode statusCode, string message) : base(message)
        {
            this.Name = name;
            this.StatusCode = statusCode;

        }
    }
}