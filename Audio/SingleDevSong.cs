using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    class SingleDevSong
    {
        private static iDevSong dev { get; set; }

        public static iDevSong GetDev()
        {
            if (dev == null)
                dev = new DevSong();
            return dev;

        }

    }
}
