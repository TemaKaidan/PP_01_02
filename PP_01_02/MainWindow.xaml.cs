using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static PP_01_02.MainWindow;

namespace PP_01_02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPages(pages.main);
        }

        public enum pages
        {
            main
        }

        public void OpenPages(pages _pages)
        {
            this.MinHeight = 800;
            this.MinWidth = 950;
            this.Height = 750;
            this.Width = 900;

            switch (_pages)
            {
                case pages.main:
                    frame.Navigate(new Pages.Main());
                    break;
            }
        }
    }
}