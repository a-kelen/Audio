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
using System.Data.SQLite;
using System.Threading;
using System.Data;

namespace Audio
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            
        }
        bool btn_login = false;
        bool btn_register = false;
        
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!btn_login)
            {
                btn_log.Visibility = Visibility.Visible;
                lblock_1.Visibility = Visibility.Visible;
                lblock_2.Visibility = Visibility.Visible;
                text_l.Visibility = Visibility.Visible;
                l_text_1.Visibility = Visibility.Visible;
                l_text_2.Visibility = Visibility.Visible;
                card_register.Visibility = Visibility.Collapsed;
                var animation = new ThicknessAnimation();
                animation.IsAdditive = true;
                animation.To = new Thickness(100, 60, 100, 100);
                animation.Duration = TimeSpan.FromMilliseconds(200);
                card_login.BeginAnimation(MarginProperty, animation);
                Storyboard.SetTarget(animation, card_login);
                Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
                var animation1 = new ThicknessAnimation();
                animation1.IsAdditive = true;
                animation1.To = new Thickness(-400, -180, 100, 100);
                animation1.Duration = TimeSpan.FromMilliseconds(200);
                btn_l.BeginAnimation(MarginProperty, animation1);
                Storyboard.SetTarget(animation1, btn_l);
                Storyboard.SetTargetProperty(animation1, new PropertyPath(MarginProperty));
                Storyboard.SetTarget(animation, card_login);
                Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
                var anim = new DoubleAnimation();
                anim.From = 100;
                anim.To = 350;
                anim.Duration = TimeSpan.FromMilliseconds(200);
                Storyboard.SetTarget(anim, card_login);
                Storyboard.SetTargetProperty(anim, new PropertyPath(HeightProperty));

                var animbtnH = new DoubleAnimation();
                animbtnH.To = 40;
                animbtnH.Duration = TimeSpan.FromMilliseconds(200);
                Storyboard.SetTarget(animbtnH, btn_l);
                Storyboard.SetTargetProperty(animbtnH, new PropertyPath(HeightProperty));


                var animbtnW = new DoubleAnimation();
                animbtnW.To = 40;
                animbtnW.Duration = TimeSpan.FromMilliseconds(200);
                Storyboard.SetTarget(animbtnW, btn_l);
                Storyboard.SetTargetProperty(animbtnW, new PropertyPath(WidthProperty));

                var s = new Storyboard();
                s.Children = new TimelineCollection { animation, anim, animation1, animbtnH, animbtnW };

                s.Begin();
                SolidColorBrush myBrush = new SolidColorBrush();
                ColorAnimation myColorAnimation = new ColorAnimation();
                myColorAnimation.To = Colors.White;
                myColorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                card_login.Background = myBrush;

                btn_l_icon.Height = 20;
                btn_l_icon.Width = 20;
                btn_l_icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowBack;
                btn_login = true;

            }
            else
            {
                var animation = new ThicknessAnimation();
                animation.IsAdditive = true;
                animation.To = new Thickness(10, 69, 689, 0);
                animation.Duration = TimeSpan.FromMilliseconds(300);
                card_login.BeginAnimation(MarginProperty, animation);
                Storyboard.SetTarget(animation, card_login);
                Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
                var animation1 = new ThicknessAnimation();
                animation1.IsAdditive = true;
                animation1.To = new Thickness(0, 0, 0, 0);
                animation1.Duration = TimeSpan.FromMilliseconds(300);
                btn_l.BeginAnimation(MarginProperty, animation1);
                Storyboard.SetTarget(animation1, btn_l);
                Storyboard.SetTargetProperty(animation1, new PropertyPath(MarginProperty));
                Storyboard.SetTarget(animation, card_login);
                Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
                var anim = new DoubleAnimation();
                anim.From = 350;
                anim.To = 100;
                anim.Duration = TimeSpan.FromMilliseconds(300);
                Storyboard.SetTarget(anim, card_login);
                Storyboard.SetTargetProperty(anim, new PropertyPath(HeightProperty));

                var animbtnH = new DoubleAnimation();
                animbtnH.To = 80;
                animbtnH.Duration = TimeSpan.FromMilliseconds(300);
                Storyboard.SetTarget(animbtnH, btn_l);
                Storyboard.SetTargetProperty(animbtnH, new PropertyPath(HeightProperty));


                var animbtnW = new DoubleAnimation();
                animbtnW.To = 80;
                animbtnW.Duration = TimeSpan.FromMilliseconds(300);
                Storyboard.SetTarget(animbtnW, btn_l);
                Storyboard.SetTargetProperty(animbtnW, new PropertyPath(WidthProperty));

                var s = new Storyboard();
                s.Children = new TimelineCollection { animation, anim, animation1, animbtnH, animbtnW };

                s.Begin();
                SolidColorBrush myBrush = new SolidColorBrush();
                ColorAnimation myColorAnimation = new ColorAnimation();
                myColorAnimation.To = Color.FromRgb(23, 97, 112);
                myColorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                card_login.Background = myBrush;

                btn_l_icon.Height = 30;
                btn_l_icon.Width = 30;
                btn_l_icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.LoginVariant;
                btn_login = false;

                card_register.Visibility = Visibility.Visible;
                l_text_1.Visibility = Visibility.Collapsed;
                l_text_2.Visibility = Visibility.Collapsed;
                text_l.Visibility = Visibility.Collapsed;
                btn_log.Visibility = Visibility.Collapsed;
                lblock_1.Visibility = Visibility.Collapsed;
                lblock_2.Visibility = Visibility.Collapsed;

            }
        }

        private void btn_r_Click(object sender, RoutedEventArgs e)
        {
            if (!btn_register)
            {

                btn_reg.Visibility = Visibility.Visible;
                block_1.Visibility = Visibility.Visible;
                block_2.Visibility = Visibility.Visible;
                block_3.Visibility = Visibility.Visible;
                text_r.Visibility = Visibility.Visible;
                r_text_1.Visibility = Visibility.Visible;
                r_text_2.Visibility = Visibility.Visible;
                r_text_3.Visibility = Visibility.Visible;
                card_login.Visibility = Visibility.Collapsed;
                var animation = new ThicknessAnimation();
                animation.IsAdditive = true;
                animation.To = new Thickness(100, 60, 100, 100);
                animation.Duration = TimeSpan.FromMilliseconds(200);
                card_register.BeginAnimation(MarginProperty, animation);
                Storyboard.SetTarget(animation, card_register);
                Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
                var animation1 = new ThicknessAnimation();
                animation1.IsAdditive = true;
                animation1.To = new Thickness(-400, -180, 100, 100);
                animation1.Duration = TimeSpan.FromMilliseconds(200);
                btn_r.BeginAnimation(MarginProperty, animation1);
                Storyboard.SetTarget(animation1, btn_r);
                Storyboard.SetTargetProperty(animation1, new PropertyPath(MarginProperty));
                Storyboard.SetTarget(animation, card_register);
                Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
                var anim = new DoubleAnimation();
                anim.From = 100;
                anim.To = 350;
                anim.Duration = TimeSpan.FromMilliseconds(200);
                Storyboard.SetTarget(anim, card_register);
                Storyboard.SetTargetProperty(anim, new PropertyPath(HeightProperty));

                var animbtnH = new DoubleAnimation();
                animbtnH.To = 40;
                animbtnH.Duration = TimeSpan.FromMilliseconds(200);
                Storyboard.SetTarget(animbtnH, btn_r);
                Storyboard.SetTargetProperty(animbtnH, new PropertyPath(HeightProperty));


                var animbtnW = new DoubleAnimation();
                animbtnW.To = 40;
                animbtnW.Duration = TimeSpan.FromMilliseconds(200);
                Storyboard.SetTarget(animbtnW, btn_r);
                Storyboard.SetTargetProperty(animbtnW, new PropertyPath(WidthProperty));

                var s = new Storyboard();
                s.Children = new TimelineCollection { animation, anim, animation1, animbtnH, animbtnW };

                s.Begin();
                SolidColorBrush myBrush = new SolidColorBrush();
                ColorAnimation myColorAnimation = new ColorAnimation();
                myColorAnimation.To = Colors.White;
                myColorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(200));
                myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                card_register.Background = myBrush;

                btn_r_icon.Height = 20;
                btn_r_icon.Width = 20;
                btn_r_icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.ArrowRight;
                btn_register = true;

            }
            else
            {
                var animation = new ThicknessAnimation();
                animation.IsAdditive = true;
                animation.To = new Thickness(689, 69, 10, 0);
                animation.Duration = TimeSpan.FromMilliseconds(300);
                card_register.BeginAnimation(MarginProperty, animation);
                Storyboard.SetTarget(animation, card_register);
                Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
                var animation1 = new ThicknessAnimation();
                animation1.IsAdditive = true;
                animation1.To = new Thickness(0, 0, 0, 0);
                animation1.Duration = TimeSpan.FromMilliseconds(300);
                btn_r.BeginAnimation(MarginProperty, animation1);
                Storyboard.SetTarget(animation1, btn_r);
                Storyboard.SetTargetProperty(animation1, new PropertyPath(MarginProperty));
                Storyboard.SetTarget(animation, card_register);
                Storyboard.SetTargetProperty(animation, new PropertyPath(MarginProperty));
                var anim = new DoubleAnimation();
                anim.From = 350;
                anim.To = 100;
                anim.Duration = TimeSpan.FromMilliseconds(300);
                Storyboard.SetTarget(anim, card_register);
                Storyboard.SetTargetProperty(anim, new PropertyPath(HeightProperty));

                var animbtnH = new DoubleAnimation();
                animbtnH.To = 80;
                animbtnH.Duration = TimeSpan.FromMilliseconds(300);
                Storyboard.SetTarget(animbtnH, btn_r);
                Storyboard.SetTargetProperty(animbtnH, new PropertyPath(HeightProperty));


                var animbtnW = new DoubleAnimation();
                animbtnW.To = 80;
                animbtnW.Duration = TimeSpan.FromMilliseconds(300);
                Storyboard.SetTarget(animbtnW, btn_r);
                Storyboard.SetTargetProperty(animbtnW, new PropertyPath(WidthProperty));

                var s = new Storyboard();
                s.Children = new TimelineCollection { animation, anim, animation1, animbtnH, animbtnW };

                s.Begin();
                SolidColorBrush myBrush = new SolidColorBrush();
                ColorAnimation myColorAnimation = new ColorAnimation();
                myColorAnimation.To = Color.FromRgb(23, 97, 112);
                myColorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));
                myBrush.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                card_register.Background = myBrush;

                btn_r_icon.Height = 30;
                btn_r_icon.Width = 30;
                btn_r_icon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Register;
                btn_register = false;
                card_login.Visibility = Visibility.Visible;
                r_text_1.Visibility = Visibility.Collapsed;
                r_text_2.Visibility = Visibility.Collapsed;
                r_text_3.Visibility = Visibility.Collapsed;
                text_r.Visibility = Visibility.Collapsed;
                btn_reg.Visibility = Visibility.Collapsed;
                block_1.Visibility = Visibility.Collapsed;
                block_2.Visibility = Visibility.Collapsed;
                block_3.Visibility = Visibility.Collapsed;
                
            }

            }

       

        private void register(object s, RoutedEventArgs e)
        {
            if (r_text_1.Text != "" && r_text_2.Password != "" && r_text_2.Password == r_text_3.Password)
            {
                if (User.find(r_text_1.Text) == null) {
                    User u = new User(r_text_1.Text, r_text_2.Password);
                    User.AddUser(u);
                    Main m = new Main();
                    
                    m.setUser(User.find(u.Login));
                    this.NavigationService.Navigate(m);
                    
                }
                else btn_reg.Background = Brushes.Red;
            }
            else btn_reg.Background = Brushes.Red;
        }

        private void login(object s,RoutedEventArgs e)
        {
            User user = new User(l_text_1.Text, l_text_2.Password);
            if (User.ExistUser(user))
            {
                    Main m = new Main();
                    m.setUser(User.find(user.Login));

                this.NavigationService.Navigate(m);   
            }
            else
                btn_log.Background = Brushes.Red;
               
        }
       
    }
}
