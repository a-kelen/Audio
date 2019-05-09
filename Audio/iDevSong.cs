using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    interface iDevSong
    {
        string Title { get; set; }
        int AlbumId { get; set; }
        string Artist { get; set; }
        string Path { get; set; }
        string Genre { get; set; }
        double Duration { get; set; }
        int Likes { get; set; }
        Song Create(string title, string artist, string path, double duration, Album album, string Genre);
    }
}
