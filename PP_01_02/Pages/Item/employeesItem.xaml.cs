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
    /// Логика взаимодействия для employeesItem.xaml
    /// </summary>
    public partial class employeesItem : UserControl
    {
        public Pages.list.employees Maineemployees;
        private Models.employees employees;

        public employeesItem(Models.employees employees, Pages.list.employees Maineemployees)
        {
            InitializeComponent();
            this.employees = employees;
            this.Maineemployees = Maineemployees;

            lb_last_name.Content = "Фамилия: " + employees.last_name;
            lb_name.Content = "Имя: " + employees.name;
            lb_sur_name.Content = "Отчество: " + employees.sur_name;
            lb_position.Content = "Должность: " + employees.position;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.employeesEdit, null, null, employees);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Maineemployees._employeesContext.employees.Remove(employees);
                    Maineemployees._employeesContext.SaveChanges();

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
