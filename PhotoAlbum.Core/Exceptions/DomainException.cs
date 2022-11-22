using System;

namespace PhotoAlbum.Core.Exceptions
{
    public class DomainException : Exception
    {
        protected DomainException(string message = null) : base(message)
        {
        }
    }
}