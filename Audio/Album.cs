using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace Audio
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public double Duration { get; set; }
        public string FullDuration { get
            {
                int min = 0,sec;
                string s1="",s2="";
                min = (int) ( Duration / 60);
                sec = (int)(Duration - min * 60);
                if (min < 10)
                    s1 = "0";
                if (sec < 10)
                    s2 = "0";
                return s1+ min + ":" +s2+sec ;
            }
        }
        public Album(string name,User user,int status,double duration)
        {
            this.Name = name;
            this.UserId = user.Id;
            this.Status = status;
            this.Duration = duration;
        }
        public Album()
        {

        }
        public static bool ExistAlbum(Album alb)
        {
            using (Db db = new Db())
            {
                var res = db.Albums.FirstOrDefault(x => x.Name == alb.Name);
                return true ? res == null : false;
            }
            
        }
      
        public static void AddAlbum(Album alb)
        {
            using (Db db = new Db())
            {
               
                db.Albums.Add(alb);
                db.SaveChanges();
             //   MessageBox.Show(db.Albums.FirstOrDefault().Name);
            }
        }
        public static Album find(string sms)
        {
            using (Db db = new Db())
            {
                var res = db.Albums.FirstOrDefault(x => x.Name == sms);
                return res;
            }
        }
        public static Album Find(int a)
        {
            using (Db db = new Db())
            {
                return db.Albums.FirstOrDefault(x => x.Id==a);
               
            }
        }
        public static List<Album> GetAlbums(int us)
        {
            using (Db db = new Db())
            {
                var res = db.Albums.Where(x => x.UserId == us);
                return res.ToList();
            }

        }
    }
}
