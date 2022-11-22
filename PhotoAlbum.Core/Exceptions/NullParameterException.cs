namespace PhotoAlbum.Core.Exceptions
{
    public class NullParameterException : DomainException
    {
        public NullParameterException(string paramName = "userId") : base(
            $"The lookup parameter: {paramName} was undefined.")
        {
        }
    }
}