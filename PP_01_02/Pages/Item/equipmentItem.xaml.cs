using PP_01_02.Context;
using System.Windows;
using System.Windows.Controls;
using PP_01_02.Models;
using PP_01_02.Pages.list;

namespace PP_01_02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для equipmentItem.xaml
    /// </summary>
    public partial class equipmentItem : UserControl
    {
        public Pages.list.equipment Mainequipment;
        private Models.equipment equipment;

        private readonly equipment_typeContext _equipment_TypeContext = new equipment_typeContext();

        public equipmentItem(Models.equipment equipment, Pages.list.equipment Mainequipment)
        {
            InitializeComponent();
            this.Mainequipment = Mainequipment;
            this.equipment = equipment;

            lb_name.Content = "Название: " + equipment.name;
            lb_type_id.Content = "Тип: " + _equipment_TypeContext.equipment_type.FirstOrDefault(x => x.type_id == equipment.type_id).type_name;
            lb_serial_number.Content = "Серийный номер: " + equipment.serial_number;
            lb_explanatoryNote.Content = "Производитель: " + equipment.manufacturer;
            lb_installation_date.Content = "Дата установки: " + equipment.installation_date;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.equipmentEdit, null, equipment);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Mainequipment._equipmentContext.equipment.Remove(equipment);
                    Mainequipment._equipmentContext.SaveChanges();

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