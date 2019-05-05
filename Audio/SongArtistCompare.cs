using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    class SongArtistCompare : IComparer<Song>
    {
        public int Compare(Song s1, Song s2)
        {
            return s1.Artist.CompareTo(s2.Artist);
        }
    }

}
