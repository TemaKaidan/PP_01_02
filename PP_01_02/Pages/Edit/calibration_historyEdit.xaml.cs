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
    /// Логика взаимодействия для calibration_historyEdit.xaml
    /// </summary>
    public partial class calibration_historyEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.list.calibration_history Maincalibration_history;
        public Models.calibration_history calibration_history;

        Context.equipmentContext equipmentContext = new Context.equipmentContext();
        Context.employeesContext employeesContext = new Context.employeesContext();

        public calibration_historyEdit(list.calibration_history Maincalibration_history, Models.calibration_history calibration_history = null )
        {
            InitializeComponent();
            this.Maincalibration_history = Maincalibration_history;
            this.calibration_history = calibration_history;

            foreach (Models.equipment equipment in equipmentContext.equipment)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = equipment.name;
                item.Tag = equipment.equipment_id;
                if (equipment.equipment_id == calibration_history.calibration_id)
                {
                    item.IsSelected = true;
                }
                cb_equipment_id.Items.Add(item);
            }

            db_calibration_date.Text = calibration_history.change_date.ToString();

            foreach (Models.employees employees in employeesContext.employees)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = employees.last_name + " " + employees.name + " " + employees.sur_name;
                item.Tag = employees.employee_id;
                if (employees.employee_id == calibration_history.updated_by)
                {
                    item.IsSelected = true;
                }
                cb_calibrated_by.Items.Add(item);
            }

            tb_notes.Text = calibration_history.description;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.calibration_history editcalibration_history = Maincalibration_history._calibration_historyContext.calibration_history.FirstOrDefault(x => x.history_id == calibration_history.history_id);
                if (editcalibration_history != null)
                {
                    editcalibration_history.calibration_id = (int)(cb_equipment_id.SelectedItem as ComboBoxItem).Tag;
                    editcalibration_history.updated_by = (int)(cb_calibrated_by.SelectedItem as ComboBoxItem).Tag;
                    editcalibration_history.change_date = DateTime.Parse(db_calibration_date.Text);
                    editcalibration_history.description = tb_notes.Text;

                    Maincalibration_history._calibration_historyContext.SaveChanges();
                    MainWindow.init.OpenPages(MainWindow.pages.calibration_history);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка!");
                    MainWindow.init.OpenPages(MainWindow.pages.calibration_history);
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
