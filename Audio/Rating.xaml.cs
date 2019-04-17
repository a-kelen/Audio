using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Audio
{
    /// <summary>
    /// Interaction logic for Rating.xaml
    /// </summary>
    public partial class Rating : Page
    {
        public Rating()
        {
            InitializeComponent();
            songs = new ObservableCollection<Song>();
        }
        public ObservableCollection<Song> songs;
        public delegate void player(Song s);
        public event player PlayS;
        int indexActive = 0;


        bool showed_description = true;
        private void show_description(object sender, RoutedEventArgs e)
        {
            if (showed_description)
            {
                description_column.Width = new GridLength(0, GridUnitType.Pixel);
                showed_description = false;
            }
            else
            {
                description_column.Width = new GridLength(270, GridUnitType.Pixel);
                showed_description = true;
            }

        }
        private void playingSong(object s, RoutedEventArgs e)
        {
            Button bt = (Button)s;
            int ID = (int)bt.DataContext;

            var res = from a in songs where a.Id == ID select a;
            Song ss = res.ToList<Song>()[0];
            indexActive = songs.IndexOf(ss);
            PlayS(ss);
        }
        private void descriptionSong(object s, RoutedEventArgs e)
        {
            Button bt = (Button)s;
            var res = from a in songs where a.Id == (int)bt.DataContext select a;
            Song so = res.ToList<Song>()[0];
            var file = TagLib.File.Create(so.Path);
            TagLib.IPicture pic = file.Tag.Pictures[0];
            MemoryStream ms = new MemoryStream(pic.Data.Data);
            ms.Seek(0, SeekOrigin.Begin);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = ms;
            bitmap.EndInit();

            des_title.Text = file.Tag.Title;
            des_Artist.Text = file.Tag.FirstPerformer;
            des_duration.Text = file.Properties.Duration.Minutes + ":" + file.Properties.Duration.Seconds;
            des_genre.Text = file.Tag.Genres[0];
            des_album.Text = file.Tag.Album;
            des_year.Text = "" + file.Tag.Year;
            des_image.Source = bitmap;

        }
    }
    
}
