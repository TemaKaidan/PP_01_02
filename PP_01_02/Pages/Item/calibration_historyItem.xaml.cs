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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PP_01_02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для calibration_historyItem.xaml
    /// </summary>
    public partial class calibration_historyItem : UserControl
    {
        public Pages.list.calibration_history Maincalibration_history;
        private Models.calibration_history calibration_history;

        private readonly equipmentContext _equipmentContext = new equipmentContext();

        public calibration_historyItem(Models.calibration_history calibration_history , Pages.list.calibration_history Maincalibration_history)
        {
            InitializeComponent();
            this.Maincalibration_history = Maincalibration_history;
            this.calibration_history = calibration_history;

            lb_equipment_id.Content = "Оборудование: " + _equipmentContext.equipment.FirstOrDefault(x => x.equipment_id == calibration_history.calibration_id).name;
            employeesContext _employeesContext = new employeesContext();
            var employeesContext = _employeesContext.employees.FirstOrDefault(x => x.employee_id == calibration_history.updated_by);
            lb_calibrated_by.Content = "Сотрудник: " + (employeesContext != null ? employeesContext.last_name : "Неизвестно") + " " + (employeesContext != null ? employeesContext.name : "Неизвестно") + " " + (employeesContext != null ? employeesContext.sur_name : "Неизвестно");
            lb_calibration_date.Content = "Дата: " + calibration_history.change_date.ToString("dd.MM.yyyy");
            lb_notes.Content = "Описание: " + calibration_history.description;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.calibration_historyEdit, null, null, null, null, calibration_history);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Maincalibration_history._calibration_historyContext.calibration_history.Remove(calibration_history);
                    Maincalibration_history._calibration_historyContext.SaveChanges();

                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}