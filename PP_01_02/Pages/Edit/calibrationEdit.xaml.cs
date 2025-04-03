using Microsoft.VisualBasic;
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

namespace PP_01_02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для calibrationEdit.xaml
    /// </summary>
    public partial class calibrationEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.list.calibration Maincalibration;
        public Models.calibration calibration;

        Context.equipmentContext equipmentContext = new Context.equipmentContext();
        Context.employeesContext employeesContext = new Context.employeesContext();

        public calibrationEdit(list.calibration Maincalibration, Models.calibration calibration = null)
        {
            InitializeComponent();
            this.Maincalibration = Maincalibration;
            this.calibration = calibration;

            foreach (Models.equipment equipment in equipmentContext.equipment)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = equipment.name;
                item.Tag = equipment.equipment_id;
                if (equipment.equipment_id == calibration.equipment_id)
                {
                    item.IsSelected = true;
                }
                cb_equipment_id.Items.Add(item);
            }

            db_calibration_date.Text = calibration.calibration_date.ToString();

            foreach (Models.employees employees in employeesContext.employees)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = employees.last_name + " " + employees.name + " " + employees.sur_name;
                item.Tag = employees.employee_id;
                if (employees.employee_id == calibration.calibrated_by)
                {
                    item.IsSelected = true;
                }
                cb_calibrated_by.Items.Add(item);
            }

            if (calibration.calibration_result == "Успешно")
            {
                cb_calibration_result.SelectedItem = cb_calibration_result.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == "Успешно");
            }
            else if (calibration.calibration_result == "Неудачно")
            {
                cb_calibration_result.SelectedItem = cb_calibration_result.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Content.ToString() == "Неудачно");
            }

            tb_notes.Text = calibration.notes;
        }

        private void ExplanatoryNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)cb_calibration_result.SelectedItem;
            if (selectedItem != null)
            {
                calibration.calibration_result = selectedItem.Content.ToString();
            }
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = (ComboBoxItem)cb_calibration_result.SelectedItem;
                if (selectedItem != null)
                {
                    calibration.calibration_result = selectedItem.Content.ToString();
                }

                Models.calibration editcalibration = Maincalibration._calibrationContext.calibration.FirstOrDefault(x => x.calibration_id == calibration.calibration_id);
                if (editcalibration != null)
                {
                    editcalibration.equipment_id = (int)(cb_equipment_id.SelectedItem as ComboBoxItem).Tag;
                    editcalibration.calibration_date = DateTime.Parse(db_calibration_date.Text);
                    editcalibration.calibrated_by = (int)(cb_calibrated_by.SelectedItem as ComboBoxItem).Tag;
                    editcalibration.calibration_result = calibration.calibration_result;

                    editcalibration.notes = tb_notes.Text;

                    Maincalibration._calibrationContext.SaveChanges();
                    MainWindow.init.OpenPages(MainWindow.pages.calibration);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка!");
                    MainWindow.init.OpenPages(MainWindow.pages.calibration);
                }
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