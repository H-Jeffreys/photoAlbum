namespace PhotoAlbum.Core.Models.Interfaces
{
    public interface IAlbumModel: IUserIdModel
    {
        public int Id { get; set; }
        public new int UserId { get; set; }
        public string Title { get; set; }
    }
}