using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Audio
{
    /// <summary>
    /// Interaction logic for WinFormater.xaml
    /// </summary>
    public partial class WinFormater : Window
    {
        public WinFormater(Song song)
        {
            InitializeComponent();
            this.song = song;
            var file = TagLib.File.Create(song.Path);
            name.Text = song.Title;
            author.Text = song.Artist;
            year.Text = file.Tag.Year.ToString();
            album.Text = file.Tag.Album;
            genre.Text = song.Genre;
        }
        private Song song;
        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            var file = TagLib.File.Create(song.Path);

            if (name.Text.Trim() != "")
            {
                song.Title = name.Text;
                file.Tag.Title = song.Title;
            }
            if (album.Text.Trim() != "")
            {
                file.Tag.Album = album.Text;
            }
            if (author.Text.Trim() != "")
            {
                song.Artist = author.Text;
                file.Tag.Artists[0] = author.Text;
            }
            if (genre.Text.Trim() != "")
            {
                song.Genre = genre.Text;
                file.Tag.Genres[0] = genre.Text;
            }
            if (year.Text.Trim() != "" && uint.TryParse(year.Text.Trim() , out uint y))
            {   
                if(y>1900)
                file.Tag.Year = y;
            }
            file.Save();
            using(Db db = new Db())
            {
                var t = db.Songs.Where(x => x.Path == song.Path);
                foreach(var a in t)
                {
                    a.Title = song.Title;
                    a.Genre = song.Genre;
                    a.Artist = song.Artist;
                }
                db.Songs.UpdateRange(t);
                db.SaveChangesAsync();
            }

        }
    }
}
