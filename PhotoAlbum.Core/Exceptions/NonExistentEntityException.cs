namespace PhotoAlbum.Core.Exceptions
{
    public class NonExistentEntityException<TEntity> : DomainException

    {
        public NonExistentEntityException(object paramValue, string paramName = "userId") : base(
            $"No {typeof(TEntity).Name} exists for given {paramName} {paramValue}.")
        {
        }
    }
}