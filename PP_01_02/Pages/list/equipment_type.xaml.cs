using Microsoft.EntityFrameworkCore.Query.Internal;
using PP_01_02.Context;
using PP_01_02.Pages.Item;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PP_01_02.Pages.list
{
    /// <summary>
    /// Логика взаимодействия для equipment_type.xaml
    /// </summary>
    public partial class equipment_type : Page
    {
        private bool isMenuCollapsed = false;
        public equipment_typeContext _equipment_TypeContext = new equipment_typeContext();

        public equipment_type()
        {
            InitializeComponent();
            CreateUI();
        }

        private void CreateUI()
        {
            parrent.Children.Clear();
            foreach (var x in _equipment_TypeContext.equipment_type.ToList())
            {
                parrent.Children.Add(new equipment_typeItem(x, this));
            }
        }

        private void Click_equipment(object sender, RoutedEventArgs e)
        {
            
        }

        private void Click_equipment_type(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.equipment_type);
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.equipment_typeAdd);
        }

        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                widthAnimation.From = 200;
                widthAnimation.To = 50;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);
            isMenuCollapsed = !isMenuCollapsed;
        }
    }
}
