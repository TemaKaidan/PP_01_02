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
    /// Логика взаимодействия для calibration_historyAdd.xaml
    /// </summary>
    public partial class calibration_historyAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.list.calibration_history Maincalibration_history;
        public Models.calibration_history calibration_history;

        Context.employeesContext employeesContext = new Context.employeesContext();
        Context.equipmentContext equipmentContext = new Context.equipmentContext();

        public calibration_historyAdd(list.calibration_history Maincalibration_history, Models.calibration_history calibration_history = null)
        {
            InitializeComponent();
            this.Maincalibration_history = Maincalibration_history;
            this.calibration_history = calibration_history;

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
                if (calibration_history == null)
                {
                    calibration_history = new Models.calibration_history
                    {
                        calibration_id = (cb_equipment_id.SelectedItem as Models.equipment).equipment_id,
                        change_date = db_calibration_date.SelectedDate ?? DateTime.MinValue,
                        updated_by = (cb_calibrated_by.SelectedItem as Models.employees).employee_id,
                        description = tb_notes.Text
                    };

                    Maincalibration_history._calibration_historyContext.calibration_history.Add(calibration_history);
                }

                Maincalibration_history._calibration_historyContext.SaveChanges();

                MainWindow.init.OpenPages(MainWindow.pages.calibration_history);
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
