using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    class AlbumBuilder
    {
        Album album;
        public AlbumBuilder()
        {
            album = new Album();
           
        }
        public AlbumBuilder setName(string name)
        {
            album.Name = name;
            return this;
        }
        public AlbumBuilder setStatus(int status)
        {
            album.Status = status;
            return this;
        }
        public AlbumBuilder setDuration(double duration)
        {
            album.Duration = duration;
            return this;
        }
        public AlbumBuilder setUser(int id)
        {
            album.UserId = id;
            Album a = new AlbumBuilder();
            return this;
        }
        public static implicit operator Album(AlbumBuilder albumBuilder)
        {
            return albumBuilder.album;
        }
    }
}
