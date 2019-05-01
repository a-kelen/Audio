using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
namespace Audio
{
    public class Album
    {
        public int id { get; set; }
        public string name { get; set; }
        public int user_id { get; set; }
        public int status { get; set; }
        public double duration { get; set; }
        public string Duration { get
            {
                int min = 0,sec;
                string s1="",s2="";
                min = (int) ( duration / 60);
                sec = (int)(duration - min * 60);
                if (min < 10)
                    s1 = "0";
                if (sec < 10)
                    s2 = "0";
                return s1+ min + ":" +s2+sec ;
            }
        }
        public Album(string name,int user_id,int status,double duration)
        {
            this.name = name;
            this.user_id = user_id;
            this.status = status;
            this.duration = duration;
        }
        public Album()
        {

        }
        public static bool ExistAlbum(Album alb)
        {
            using (IDbConnection cnn = new SQLiteConnection(Load()))
            {

                var res = cnn.QueryFirstOrDefault<Album>("select * from Albums where name = @name ", new { login = alb.name});
                if (res != null)
                {
                    Album u = (Album)res;
                    if (u != null)
                        return true;
                    else return false;
                }
                else return false;
            }
            
        }
        private static string Load(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        public static void AddAlbum(Album alb)
        {
            using (IDbConnection cnn = new SQLiteConnection(Load()))
            {
                cnn.Execute("insert into Albums (name,user_id,status,duration) values(@name,@user_id,@status,@duration)", alb);
            }
        }
        public static Album find(string sms)
        {
            using (IDbConnection cnn = new SQLiteConnection(Load()))
            {
                var res = cnn.QueryFirstOrDefault<Album>("select * from Albums where name=@name", new { name = sms });
                if (res != null)
                    return (Album)res;
                else
                    return null;
            }
        }
        public static Album Find(int a)
        {
            using (IDbConnection cnn = new SQLiteConnection(Load()))
            {
                var res = cnn.QueryFirstOrDefault<Album>("select * from Albums where id=@id", new { id = a });
                if (res != null)
                    return (Album)res;
                else
                    return null;
            }
        }
        public static List<Album> GetAlbums(int us)
        {
             using (IDbConnection cnn = new SQLiteConnection(Load()))
            {
                var res = cnn.Query<Album>("select * from Albums where user_id=@user_id or status = 1", new { user_id = us });
                return res.ToList<Album>();
            }
            
        }
    }
}
