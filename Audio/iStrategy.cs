using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
namespace Audio
{
    public interface iStrategy
    {
        void eventPlayPrev(ObservableCollection<Song> songs ,int indexActive,bool isRepeter, player PlayS);
    }
}
