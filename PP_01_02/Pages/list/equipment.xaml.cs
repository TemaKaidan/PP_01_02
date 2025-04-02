using Microsoft.EntityFrameworkCore.Query.Internal;
using PP_01_02.Context;
using PP_01_02.Pages.Item;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PP_01_02.Pages.list
{
    /// <summary>
    /// Логика взаимодействия для equipment.xaml
    /// </summary>
    public partial class equipment : Page
    {
        private bool isMenuCollapsed = false;
        public equipmentContext _equipmentContext = new equipmentContext();

        public equipment()
        {
            InitializeComponent();
            CreateUI();
        }

        private void CreateUI()
        {
            parrent.Children.Clear();
            foreach (var x in _equipmentContext.equipment.ToList())
            {
                parrent.Children.Add(new equipmentItem(x, this));
            }
        }

        private void Click_equipment(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.equipment);
        }

        private void Click_equipment_type(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.equipment_type);
        }

        private void Click_employees(object sender, RoutedEventArgs e)
        {

        }

        private void Click_calibration(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.equipmentAdd);
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