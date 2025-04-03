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
    /// Логика взаимодействия для calibrationItem.xaml
    /// </summary>
    public partial class calibrationItem : UserControl
    {
        public Pages.list.calibration Maincalibration;
        private Models.calibration calibration;

        private readonly equipmentContext _equipmentContext = new equipmentContext();

        public calibrationItem(Models.calibration calibration, list.calibration Maincalibration)
        {
            InitializeComponent();
            this.calibration = calibration;
            this.Maincalibration = Maincalibration;

            lb_equipment_id.Content = "Оборудование: " + _equipmentContext.equipment.FirstOrDefault(x => x.equipment_id == calibration.equipment_id).name;
            lb_calibration_date.Content = "Дата калибровки: " + calibration.calibration_date;

            employeesContext _employeesContext = new employeesContext();
            var employeesContext = _employeesContext.employees.FirstOrDefault(x => x.employee_id == calibration.calibrated_by);
            lb_calibrated_by.Content = "Сотрудник: " + (employeesContext != null ? employeesContext.last_name : "Неизвестно") + " " + (employeesContext != null ? employeesContext.name : "Неизвестно") + " " + (employeesContext != null ? employeesContext.sur_name : "Неизвестно");
            lb_calibration_result.Content = "Результат калибровки: " + calibration.calibration_result;
            lb_notes.Content = "Примечание: " + calibration.notes;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.calibrationEdit, null, null,null, calibration);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Maincalibration._calibrationContext.calibration.Remove(calibration);
                    Maincalibration._calibrationContext.SaveChanges();

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