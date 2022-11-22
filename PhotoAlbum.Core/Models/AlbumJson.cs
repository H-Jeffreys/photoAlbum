using Newtonsoft.Json;
using PhotoAlbum.Core.Models.Interfaces;

namespace PhotoAlbum.Core.Models
{
    public class AlbumJson: IEntityWithId
    {
        [JsonProperty(PropertyName = "userId")]
        public int UserId { get; set; }
        
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        
       
    }
}