using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    class RandomSong : iStrategy
    {
        public void eventPlayPrev(ObservableCollection<Song> songs, int indexActive, bool isRepeter, player PlayS)
        {
            Random rand = new Random();
            indexActive = rand.Next(0, songs.Count);
            PlayS(songs[indexActive]);
        }
    }
}
