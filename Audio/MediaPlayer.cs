using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace Audio
{
    class media
    {
        public MediaPlayer mdp;
        public bool isPlay = false;
        public media(MediaPlayer mdp)
        {
            this.mdp = mdp;
        }
        public delegate void refr(Song s);
        public event refr refresh;
        public void mediaPlay(Song s)
        {

            mdp.Open(new Uri(s.path, UriKind.Absolute));
            mdp.Play();
            isPlay = true;
            refresh(s);
            
        }
        
        public void playStop(object s, EventArgs e)
        {
            if(isPlay)
            {
                mdp.Pause();
                isPlay = false;
            }
            else
            {
                mdp.Play();
                isPlay = true;
            }
        } 
    }
}
