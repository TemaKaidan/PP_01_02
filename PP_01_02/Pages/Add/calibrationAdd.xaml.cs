using Microsoft.VisualBasic;
using PP_01_02.Context;
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

namespace PP_01_02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для calibrationAdd.xaml
    /// </summary>
    public partial class calibrationAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.list.calibration Maincalibration;
        public Models.calibration calibration;

        Context.employeesContext employeesContext = new Context.employeesContext();
        Context.equipmentContext equipmentContext = new Context.equipmentContext();

        public calibrationAdd(list.calibration Maincalibration, Models.calibration calibration = null)
        {
            InitializeComponent();
            this.Maincalibration = Maincalibration;
            this.calibration = calibration;

            cb_equipment_id.Items.Clear();
            cb_equipment_id.ItemsSource = equipmentContext.equipment.ToList();
            cb_equipment_id.DisplayMemberPath = "name";
            cb_equipment_id.SelectedValuePath = "equipment_id";

            cb_calibrated_by.Items.Clear();
            cb_calibrated_by.ItemsSource = employeesContext.employees.ToList();
            cb_calibrated_by.DisplayMemberPath = "last_name";
            cb_calibrated_by.SelectedValuePath = "employee_id";
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (calibration == null)
                {
                    calibration = new Models.calibration
                    {
                        equipment_id = (cb_equipment_id.SelectedItem as Models.equipment).equipment_id,
                        calibration_date = db_calibration_date.SelectedDate ?? DateTime.MinValue,
                        calibrated_by = (cb_calibrated_by.SelectedItem as Models.employees).employee_id,
                        calibration_result = cb_calibration_result.Text,
                        notes = tb_notes.Text
                    };

                    Maincalibration._calibrationContext.calibration.Add(calibration);
                }

                Maincalibration._calibrationContext.SaveChanges();

                MainWindow.init.OpenPages(MainWindow.pages.calibration);
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