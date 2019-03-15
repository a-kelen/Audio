using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Windows;

namespace Audio
{
    public class Song 
    {
      public  int id { get; set; }
      public  string title { get; set; }
      public  string artist { get; set; }
      public  string path { get; set; }
      public  double duration { get; set; }
      public string fullName { get
            {
                return title + " - " + artist;
            }
        }
        public string Duration
        {
            get
            {
                int min = 0, sec;
                string s1 = "", s2 = "";
                min = (int)(duration / 60);
                sec = (int)(duration - min * 60);
                if (min < 10)
                    s1 = "0";
                if (sec < 10)
                    s2 = "0";
                return s1 + min + ":" + s2 + sec;
            }
        }
        public  int album_id { get; set; }
        public delegate void _added(Song s);
        static public event _added addedSong; 

        public Song(string title,string artist,string path,double duration,int album_id)
        {
            this.title = title;
            this.album_id = album_id;
            this.artist = artist;
            this.path = path;
            this.duration = duration;
        }
        public Song()
        {
            
        }
        private static string Load(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        
        public static void AddSong(Song song)
        {
            using (IDbConnection cnn = new SQLiteConnection(Load()))
            {
                if(!find(song))
                {
                    cnn.Execute("insert into Songs (title,artist,path,duration,album_id) values(@title,@artist,@path,@duration,@album_id)", song);
                    cnn.Execute("update Albums set duration = duration+@duration where id = @album_id", song);
                    var res = cnn.QueryFirstOrDefault<Song>("select * from Songs where title=@title and artist=@artist and album_id = @album_id", song);
                    addedSong(res);
                }

            }
        }
        public static bool find(Song sms)
        {
            using (IDbConnection cnn = new SQLiteConnection(Load()))
            {
                var res = cnn.QueryFirstOrDefault<Song>("select * from Songs where title=@title and artist=@artist and album_id = @album_id",sms);
                if (res != null)
                    return true;
                else
                    return false;
            }
        }

        public static List<Song> GetSongs(int us)
        {
            using (IDbConnection cnn = new SQLiteConnection(Load()))
            {
                var res = cnn.Query<Song>("select * from Songs where album_id=@album_id ", new { album_id = us });

                    return res.ToList<Song>();
            }

        }
    }
}
