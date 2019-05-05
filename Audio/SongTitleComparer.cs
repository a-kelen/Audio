using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    class SongTitleComparer : IComparer<Song>
    {
        public int Compare(Song s1,Song s2)
        {
            return s1.Title.CompareTo(s2.Title);
        }
    }
}
