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

namespace Audio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            mainframe.NavigationService.Navigate(new Page1());
           
        }
        void  move( object sender,RoutedEventArgs e)
        {
            DragMove();
        }
        void close_window(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        bool fulled_creen = false;
        void full_screen(object sender, RoutedEventArgs e)
        {
            if(fulled_creen)
            {
                WindowState = WindowState.Normal;
                fulled_creen = false;
            }
            else
            {
                WindowState = WindowState.Maximized;
                fulled_creen = true;
            }

        }
        void resizeWin(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (this.Width + e.HorizontalChange > 4)
                this.Width += e.HorizontalChange;
            if (this.Height + e.VerticalChange > 4)
                this.Height += e.VerticalChange;

        }

    }
}
