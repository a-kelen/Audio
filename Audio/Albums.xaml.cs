using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using TagLib;

namespace Audio
{
    /// <summary>
    /// Interaction logic for Albums.xaml
    /// </summary>
    public partial class Albums : Page
    {
        public Albums()
        {
            InitializeComponent();
            albums = new ObservableCollection<Album>();
            foreach (Album a in Album.GetAlbums(1))
                albums.Add(a);
            list.ItemsSource = albums ;
            comboAlbum.ItemsSource = albums;
            
        }
        ObservableCollection<Album> albums;
        User _user = new User { Id = 3};
        public delegate void _refreshAlbum(Album a);
        public event _refreshAlbum refreshAlbum;

        public void setUser(User U)
        {
            this._user = U;
            
        }
        private void alb_Click(object sender,RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            int n = (int)bt.DataContext;
            var res = from a in albums where a.Id == n select a;
            Album alb1 = res.ToList<Album>()[0];
            refreshAlbum(alb1);
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
        bool showed_search = false;
     

        private void addAlbum(object sender,RoutedEventArgs e)
        {
            int st = 1;
            if (albumPrivate.IsChecked == true) st = 0;
            if(create_abl_name.Text != "" && create_abl_name.Text.Length > 1)
            {
                Album a = new Album(create_abl_name.Text,_user,st,0);
                if (Album.find(create_abl_name.Text) == null)
                {
                    Album.AddAlbum(a);
                    albums.Add(a);
                }
                else
                    AddAlb.Background = Brushes.Red;
                create_abl_name.Text = "";
                albumPrivate.IsChecked = false;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (comboAlbum.SelectedItem != null)
            {
                Album alb = (Album)comboAlbum.SelectedValue;
                
                List<string> temp = new List<string>();
                OpenFileDialog of = new OpenFileDialog();
                of.Multiselect = true;
                of.Filter = "Music (.mp3)|*.mp3";
                if (of.ShowDialog() == true)
                {
                    foreach (string filename in of.FileNames)
                        temp.Add(filename);

                }

                foreach (string f in temp)
                {
                    var tags = TagLib.File.Create(f);
                    string title = tags.Tag.Title;
                    string artist = tags.Tag.FirstPerformer;
                    string path = f;
                    double duration = tags.Properties.Duration.TotalSeconds;
                    Song s = new Song(title, artist, path, duration, alb);
                    
                    Song.AddSong(s);
                }
            }
                    
        }

        private void show_search(object sender, RoutedEventArgs e)
        {
            if (showed_search)
            {
                var animation1 = new ThicknessAnimation();
                animation1.IsAdditive = true;
                animation1.To = new Thickness(0, 0, -310, 0);
                animation1.Duration = TimeSpan.FromMilliseconds(100);
                search_card.BeginAnimation(MarginProperty, animation1);
                showed_search = false;
            }
            else
            {
                var animation1 = new ThicknessAnimation();
                animation1.IsAdditive = true;
                animation1.To = new Thickness(0, 0, 10, 0);
                animation1.Duration = TimeSpan.FromMilliseconds(100);
                search_card.BeginAnimation(MarginProperty, animation1);
                showed_search = true;
            }

        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchBox.Text.Trim() != "" && showed_search)
            {
                list.ItemsSource = from a in albums where a.Name.ToLower().Contains(searchBox.Text.ToLower()) select a;
            }
            else
                list.ItemsSource = albums;
        }
    }
}
