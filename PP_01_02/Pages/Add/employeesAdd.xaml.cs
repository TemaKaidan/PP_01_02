﻿using System;
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

namespace PP_01_02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для employeesAdd.xaml
    /// </summary>
    public partial class employeesAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.list.employees Mainemployees;
        public Models.employees employees;

        public employeesAdd(Pages.list.employees Mainemployees, Models.employees employees = null)
        {
            InitializeComponent();
            this.Mainemployees = Mainemployees;
            this.employees = employees;
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (employees == null)
                {
                    employees = new Models.employees
                    {
                        last_name = tb_last_name.Text,
                        name = tb_name.Text,
                        sur_name = tb_sur_name.Text,
                        position = tb_position.Text
                    };

                    Mainemployees._employeesContext.employees.Add(employees);
                }

                Mainemployees._employeesContext.SaveChanges();

                MainWindow.init.OpenPages(MainWindow.pages.employees);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}