using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    class DevSong : iDevSong
    {
        public string Title { get; set; }
        public int AlbumId { get; set; }
        public string Artist { get; set; }
        public string Path { get; set; }
        public string Genre { get; set; }
        public double Duration { get; set; }
        public int Likes { get; set; }
        public DevSong()
        {
           
        }
        public Song Create(string title, string artist, string path, double duration, Album album, string Genre)
        {
            this.Title = title;
            this.AlbumId = album.Id;
            this.Artist = artist;
            this.Path = path;
            this.Duration = duration;
            this.Genre = Genre;
            this.Likes = 0;
            return new Song
            {
                Title = this.Title,
                AlbumId = this.AlbumId,
                Artist = this.Artist,
                Path = this.Path,
                Duration = this.Duration,
                Genre = this.Genre,
                Likes = this.Likes
            };
        }
    }
}
