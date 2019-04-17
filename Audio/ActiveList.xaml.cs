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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using System.IO;

namespace Audio
{
    /// <summary>
    /// Interaction logic for ActiveList.xaml
    /// </summary>
    public partial class ActiveList : Page
    {
        public ActiveList()
        {
            InitializeComponent();
            Song.addedSong += addInList;
            songs = new ObservableCollection<Song>();
            foreach(Song a in Song.GetSongs(2))
            {
                songs.Add(a);
                
            }
            list.ItemsSource = songs;
            if(songs.Count!=0)
            {
                Album al = Album.Find(songs[0].AlbumId);
                title1.Text = al.Name;
                title2.Text = al.Duration+"";
                User u1 = User.findID(al.UserId);
                title3.Text = u1.Login;
            }
        }
        public ObservableCollection<Song> songs;
        public delegate void player(Song s);
        public event player PlayS;
        int indexActive = 0;

        bool showed_search = false;
        public void refreshAlbum(Album al)
        {
            songs.Clear();
            foreach (Song a in Song.GetSongs(al.Id))
            {
                songs.Add(a);

            }
            list.ItemsSource = songs;
            if (songs.Count != 0)
            {
               
                title1.Text = al.Name;
                title2.Text = al.Duration+"";
                User u1 = User.findID(al.UserId);
                title3.Text = u1.Login;
            }
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
            des_Artist.Text = file.Tag.Artists[0];
            des_duration.Text = file.Properties.Duration.Minutes+":"+ file.Properties.Duration.Seconds;
            des_genre.Text = file.Tag.Genres[0];
            des_album.Text = file.Tag.Album;
            des_year.Text = ""+file.Tag.Year;
            des_image.Source = bitmap;

        }
        private void playingSong(object s,RoutedEventArgs e)
        {
            Button bt = (Button)s;
            int ID = (int)bt.DataContext;
            
            var res = from a in songs where a.Id == ID select a;
            Song ss = res.ToList<Song>()[0];
            indexActive = songs.IndexOf(ss);
            PlayS(ss);
        }
        public void eventPlayPrev(object s, EventArgs e)
        {
            if(indexActive>0)
            {
                indexActive--;
                PlayS(songs[indexActive]);
            }
        }
        public bool isRepeter = false;
        public bool isRandom = false;
        public void eventPlayNext(object s, EventArgs e)
        {
            if (isRandom)
            {
                Random rand = new Random();
                indexActive = rand.Next(0, songs.Count);
                PlayS(songs[indexActive]);

            }else if (indexActive < songs.Count-1)
            {
                if(isRandom)
                {
                    Random rand = new Random();
                    indexActive = rand.Next(0, songs.Count);
                    PlayS(songs[indexActive]);

                }
                else
                {
                    if (isRepeter)
                        indexActive--;
                    indexActive++;
                    PlayS(songs[indexActive]);
                }
            }
        }
        private void show_search(object sender, RoutedEventArgs e)
        {
            if (showed_search)
            {
                var animation1 = new ThicknessAnimation();
                animation1.IsAdditive = true;
                animation1.To = new Thickness(0, 40, -310, 0);
                animation1.Duration = TimeSpan.FromMilliseconds(100);
                search_card.BeginAnimation(MarginProperty, animation1);
                showed_search = false;
            }
            else
            {
                var animation1 = new ThicknessAnimation();
                animation1.IsAdditive = true;
                animation1.To = new Thickness(0, 40, 10, 0);
                animation1.Duration = TimeSpan.FromMilliseconds(100);
                search_card.BeginAnimation(MarginProperty, animation1);
                showed_search = true;
            }

        }
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
        public void addInList(Song s)
        {
            if(s.AlbumId==songs[0].AlbumId)
            songs.Add(s);
        }
        bool sortTag = false; 
        public void sortList(object s, RoutedEventArgs e)
        {
            if(sortTag)
            {
                SortTitle(songs);
                sortTag = false;
            }
            else {
                SortArtist(songs);
                sortTag = true;
            }
        }
        public  void SortTitle(ObservableCollection<Song> collection)
        {
            var sortableList = new List<Song>(collection);
            sortableList.Sort(new SongTitleComparer());

            for (int i = 0; i < sortableList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }
        public void SortArtist(ObservableCollection<Song> collection)
        {
            var sortableList = new List<Song>(collection);
            sortableList.Sort(new SongArtistCompare());

            for (int i = 0; i < sortableList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBox.Text.Trim() != "" && showed_search)
            {
                list.ItemsSource = from a in songs where a.fullName.ToLower().Contains(searchBox.Text.ToLower()) select a;
            }
            else
                list.ItemsSource = songs;
        }
    }
}
