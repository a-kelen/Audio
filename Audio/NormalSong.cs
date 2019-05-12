using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    class NormalSong : iStrategy
    {
        public void eventPlayPrev(ObservableCollection<Song> songs, int indexActive, bool isRepeter, player PlayS)
        {
            if (indexActive < songs.Count - 1)
            {
                    if (isRepeter)
                        indexActive--;
                    indexActive++;
                    PlayS(songs[indexActive]);
                
            }
        }
    }
}
