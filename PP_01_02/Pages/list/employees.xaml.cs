using PP_01_02.Context;
using PP_01_02.Pages.Item;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PP_01_02.Pages.list
{
    /// <summary>
    /// Логика взаимодействия для employees.xaml
    /// </summary>
    public partial class employees : Page
    {
        private bool isMenuCollapsed = false;
        public employeesContext _employeesContext = new employeesContext();

        public employees()
        {
            InitializeComponent();
            CreateUI();
        }

        private void CreateUI()
        {
            parrent.Children.Clear();
            foreach (var x in _employeesContext.employees.ToList())
            {
                parrent.Children.Add(new employeesItem(x, this));
            }
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.employeesAdd);
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
            MainWindow.init.OpenPages(MainWindow.pages.employees);
        }

        private void Click_calibration(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.calibration);
        }

        private void Click_calibration_history(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.calibration_history);
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

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();

            var resultsurname = _employeesContext.employees
                .Where(x => x.sur_name.ToLower().Contains(searchText));

            var resultname = _employeesContext.employees
                .Where(x => x.name.ToLower().Contains(searchText));

            var resultlastname = _employeesContext.employees
                .Where(x => x.last_name.ToLower().Contains(searchText));

            var combinedResults = resultsurname
                .Union(resultname)
                .Union(resultlastname);

            parrent.Children.Clear();
            foreach (var item in combinedResults)
            {
                parrent.Children.Add(new Pages.Item.employeesItem(item, this));
            }
        }
    }
}