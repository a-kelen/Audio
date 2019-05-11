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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TagLib;
using System.Windows.Threading;
using System.Drawing;

namespace Audio
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
           
            InitializeComponent();
             md = new media(new MediaPlayer());
            md.refresh += refresh;
            playStopSong.Click += md.playStop;
            
            playStopSong.Click += changeIcoPlayStop;
            activeList = new ActiveList();
            activeList.PlayS += md.mediaPlay;
            
            albums = new Albums();
            rating = new Rating();
            
            albums.refreshAlbum += activeList.refreshAlbum;
            TwoFrame.NavigationService.Navigate(activeList);
            prevSong.Click += activeList.eventPlayPrev;
            nextSong.Click += activeList.eventPlayNext;
            md.mdp.MediaEnded += activeList.eventPlayNext;
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(400);
            dt.Tick += changePos;
            volume.Value = 1;
            
        }

        
        ActiveList activeList;
        Albums albums;
        Rating rating;
        Favorites favorites;
        User _user;
        media md;
        ObservableCollection<Song> songs;
        DispatcherTimer dt;
        
        private void changePos(object s, EventArgs e) {
            position.Value = md.mdp.Position.TotalSeconds;
        }
        public void refresh(Song s)
        {
            musicTilte.Text = s.Title;
            musicArtist.Text = s.Artist;
            like.DataContext = s.Id;
            position.Minimum = 0;
            position.Maximum = s.Duration-1;
            position.Delay = 1;
            var file = TagLib.File.Create(s.Path);
            TagLib.IPicture pic = file.Tag.Pictures[0];
            MemoryStream ms = new MemoryStream(pic.Data.Data);
            ms.Seek(0, SeekOrigin.Begin);
            using (Db db = new Db())
            {
                if (db.Favorites.FirstOrDefault(x => x.UserId == _user.Id && x.SongId == s.Id) == null)
                {
                    like.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
                }
                else
                {
                    like.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(30, 255, 255, 255));
                }
            }
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = ms;
            bitmap.EndInit();
            imageSong.Source = bitmap;
            Bitmap btm = new Bitmap(ms);
            var cl = getColor(btm);
            main_panel.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, cl.R, cl.G, cl.B));
            dt.Start();
            if (md.isPlay)
                icoPlayStop.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
            else
                icoPlayStop.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
        }
        private void changeIcoPlayStop(object sender, RoutedEventArgs e)
        {
            if(md.isPlay)
            {
                icoPlayStop.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
                dt.Start();
            }
            else
            {
                icoPlayStop.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
                dt.Stop();
            }

        }
        public void setUser(User U)
        {
            this._user = U;
        }
        private System.Drawing.Color getColor(Bitmap btm)
        {
            Random rnd = new Random();
            int x = rnd.Next(0, btm.Height);
            int y = rnd.Next(0, btm.Width);
            var cl = btm.GetPixel(x, y);
            if (cl.R > 245 || cl.G > 245 || cl.B > 245)
                return getColor(btm);
            else return cl;
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            md.mdp.Volume= e.NewValue;
        }
        private void Position_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            md.mdp.Position = TimeSpan.FromSeconds(e.NewValue);
        }
        bool showed_menu = true;
        private void show_menu(object sender, RoutedEventArgs e)
        {
            if(showed_menu)
            {
                var animation1 = new ThicknessAnimation();
                animation1.IsAdditive = true;
                animation1.To = new Thickness(20, -130, 0, 20);
                animation1.Duration = TimeSpan.FromMilliseconds(200);
                menu1.BeginAnimation(MarginProperty, animation1);
                Storyboard.SetTarget(animation1, menu1);
                Storyboard.SetTargetProperty(animation1, new PropertyPath(MarginProperty));
                var animation2 = new ThicknessAnimation();
                animation2.IsAdditive = true;
                animation2.To = new Thickness(20, 0, 0, 20);
                animation2.Duration = TimeSpan.FromMilliseconds(200);
                menu2.BeginAnimation(MarginProperty, animation2);
                Storyboard.SetTarget(animation2, menu2);
                Storyboard.SetTargetProperty(animation2, new PropertyPath(MarginProperty));
                var animation3 = new ThicknessAnimation();
                animation3.IsAdditive = true;
                animation3.To = new Thickness(20, 130, 0, 20);
                animation3.Duration = TimeSpan.FromMilliseconds(200);
                menu3.BeginAnimation(MarginProperty, animation3);
                Storyboard.SetTarget(animation3, menu3);
                Storyboard.SetTargetProperty(animation3, new PropertyPath(MarginProperty));

                var animation4 = new ThicknessAnimation();
                animation4.IsAdditive = true;
                animation4.To = new Thickness(20, 260, 0, 20);
                animation4.Duration = TimeSpan.FromMilliseconds(200);
                menu4.BeginAnimation(MarginProperty, animation4);
                Storyboard.SetTarget(animation4, menu4);
                Storyboard.SetTargetProperty(animation4, new PropertyPath(MarginProperty));
                var s = new Storyboard();
                s.Children = new TimelineCollection { animation1, animation2, animation3 , animation4};
                showed_menu = false;
            }
                else { 
            var animation1 = new ThicknessAnimation();
            animation1.IsAdditive = true;
            animation1.To = new Thickness(-190, -130, 0, 20);
            animation1.Duration = TimeSpan.FromMilliseconds(200);
            menu1.BeginAnimation(MarginProperty, animation1);
            Storyboard.SetTarget(animation1, menu1);
            Storyboard.SetTargetProperty(animation1, new PropertyPath(MarginProperty));
            var animation2 = new ThicknessAnimation();
            animation2.IsAdditive = true;
            animation2.To = new Thickness(-190, -130, 0, 20);
            animation2.Duration = TimeSpan.FromMilliseconds(200);
            menu2.BeginAnimation(MarginProperty, animation2);
            Storyboard.SetTarget(animation2, menu2);
            Storyboard.SetTargetProperty(animation2, new PropertyPath(MarginProperty));
            var animation3 = new ThicknessAnimation();
            animation3.IsAdditive = true;
            animation3.To = new Thickness(-190, -130, 0, 20);
            animation3.Duration = TimeSpan.FromMilliseconds(200);
            menu3.BeginAnimation(MarginProperty, animation3);
            Storyboard.SetTarget(animation3, menu3);
            Storyboard.SetTargetProperty(animation3, new PropertyPath(MarginProperty));

                var animation4 = new ThicknessAnimation();
                animation4.IsAdditive = true;
                animation4.To = new Thickness(-190, -130, 0, 20);
                animation4.Duration = TimeSpan.FromMilliseconds(200);
                menu4.BeginAnimation(MarginProperty, animation4);
                Storyboard.SetTarget(animation4, menu4);
                Storyboard.SetTargetProperty(animation4, new PropertyPath(MarginProperty));

                var s = new Storyboard();
            s.Children = new TimelineCollection { animation1 ,animation2,animation3};

            s.Begin();
                showed_menu = true;
                 }
        }

        private void menu1_Click(object sender, RoutedEventArgs e)
        {
            
            TwoFrame.NavigationService.Navigate(activeList);
        }

        private void menu2_Click(object sender, RoutedEventArgs e)
        {
            albums.setUser(this._user);
            TwoFrame.NavigationService.Navigate(albums);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void repeatSong(object sender, RoutedEventArgs e)
        {
            activeList.isRepeter = !activeList.isRepeter;
            
            if(activeList.isRepeter)
            {
                repeat.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(30,255,255,255));
                
            }
            else
            {
                repeat.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
                
            }

        }
        private void RandomSong(object sender, RoutedEventArgs e)
        {
            activeList.isRandom = !activeList.isRandom;

            if (activeList.isRandom)
            {
                random.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(30, 255, 255, 255));
            }
            else
            {
                random.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
            }

        }

        private void menu3_Click(object sender, RoutedEventArgs e)
        {
            favorites = new Favorites();
            favorites.PlayS += md.mediaPlay;
            prevSong.Click += favorites.eventPlayPrev;
            nextSong.Click += favorites.eventPlayNext;
            md.mdp.MediaEnded += favorites.eventPlayNext;
            TwoFrame.NavigationService.Navigate(favorites);
            favorites.setSongs(this._user);
            
        }

        private void like_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if(button.DataContext!=null)
            {
                int Id = (int)button.DataContext;
                using (Db db = new Db())
                {
                    if (db.Favorites.FirstOrDefault(x => x.UserId == _user.Id && x.SongId == Id) == null)
                    {
                        like.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(30, 255, 255, 255));
                        db.Favorites.Add(new Favorite { SongId = Id, UserId = _user.Id });
                        db.SaveChangesAsync();
                    }
                    else
                    {
                        db.Favorites.Remove(db.Favorites.FirstOrDefault(x => x.UserId == _user.Id && x.SongId == Id));
                        db.SaveChangesAsync();
                        like.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 255, 255, 255));
                    }
                }
            }
            
            
        }

        private void menu4_Click(object sender, RoutedEventArgs e)
        {
            rating = new Rating();
            rating.PlayS += md.mediaPlay;
            prevSong.Click += rating.eventPlayPrev;
            nextSong.Click += rating.eventPlayNext;
            md.mdp.MediaEnded += rating.eventPlayNext;
            TwoFrame.NavigationService.Navigate(rating);
            rating.setSongs(this._user);
        }
    }
}
