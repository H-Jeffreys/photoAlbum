using System;
using Newtonsoft.Json;
using PhotoAlbum.Core.Models.Interfaces;

namespace PhotoAlbum.Core.Models
{
    public class PhotoJson: IEntityWithId
    {
        
        [JsonProperty(PropertyName = "albumId")]
        public int AlbumId { get; set; }
        
        
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        
        [JsonProperty(PropertyName = "url")]
        public Uri URl { get; set; }
        
        
        [JsonProperty(PropertyName = "thumbnailUrl")]
        public Uri ThumbnailUrl { get; set; }
    }
}