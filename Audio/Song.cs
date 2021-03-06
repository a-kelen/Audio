﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Audio
{
    
    public class Song 
    {
        [Key]
      public  int Id { get; set; }
      public  string Title { get; set; }
      public  string Artist { get; set; }
      public  string Path { get; set; }
      public  double Duration { get; set; }
      public int Likes { get; set; }
      public string Genre { get; set; }
        [NotMapped]
        public string fullName { get
            {
                return Title + " - " + Artist;
            }
        }
        [NotMapped]
        public string FullDuration
        {
            get
            {
                int min = 0, sec;
                string s1 = "", s2 = "";
                min = (int)(Duration / 60);
                sec = (int)(Duration - min * 60);
                if (min < 10)
                    s1 = "0";
                if (sec < 10)
                    s2 = "0";
                return s1 + min + ":" + s2 + sec;
            }
        }
        public  int AlbumId { get; set; }
        public delegate void _added(Song s);
        static public event _added addedSong; 

        public Song(string title,string artist,string path,double duration,Album album , string Genre)
        {
            this.Title = title;
            this.AlbumId = album.Id;
            this.Artist = Artist;
            this.Path = path;
            this.Duration = duration;
            //this.Genre = Genre;
          //  this.Likes = 0;
        }
        public Song()
        {
            
        }
        
        public static void AddSong(Song song)
        {
            using (Db db = new Db())
            {
                db.Songs.Add(song);
                Album a = db.Albums.FirstOrDefault(x => x.Id == song.AlbumId);
                a.Duration += song.Duration;
                db.Albums.Update(a);
                db.SaveChangesAsync();

            }
        }
        public static bool find(Song sms)
        {
            using (Db db = new Db())
            {
                var res = db.Songs.Where(x => x.Title == sms.Title && x.Artist == sms.Artist && x.AlbumId == x.AlbumId).FirstOrDefault();
                if (res != null)
                    return true;
                else
                    return false;
            }
        }

        public static List<Song> GetSongs(int us)
        {
            using (Db db = new Db())
            {
                var res = db.Songs.Where(x => x.AlbumId == us);

                    return res.ToList();
            }

        }
        public static List<Song> GetFavoritesSongs(int us)
        {
            using (Db db = new Db())
            {
                var ids = db.Favorites.Where(x=>x.UserId==us).Select(x=>x.SongId);
                var res = db.Songs.Where(x => ids.Any(t => t == x.Id));
                return res.ToList();
            }
        }
        public static List<Song> GetPopularSongs(int us)
        {
            using (Db db = new Db())
            {
                var num =   db.Favorites.Count() < 20 ? db.Favorites.Count() : 20;
                var ids = db.Favorites.GroupBy(x => x.SongId).OrderByDescending(x=>x.Count()).Take(num).Select(x=>x.Key);
                List<Song> s = new List<Song>();
                foreach (var a in ids)
                    s.Add(db.Songs.FirstOrDefault(x => x.Id == a));
                return s;
            }

        }
    }
}
