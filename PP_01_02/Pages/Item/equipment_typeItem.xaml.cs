using System.Windows;
using System.Windows.Controls;
using PP_01_02.Models;
using PP_01_02.Pages.list;

namespace PP_01_02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для equipment_typeItem.xaml
    /// </summary>
    public partial class equipment_typeItem : UserControl
    {
        public Pages.list.equipment_type Mainequipment_type;
        private Models.equipment_type equipment_Type;

        public equipment_typeItem(Models.equipment_type equipment_Type, Pages.list.equipment_type Mainequipment_type)
        {
            InitializeComponent();
            this.equipment_Type = equipment_Type;
            this.Mainequipment_type = Mainequipment_type;

            lb_equipment_type.Content = "Тип оборудования: " + equipment_Type.type_name;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.equipment_typeEdit, equipment_Type);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Mainequipment_type._equipment_TypeContext.equipment_type.Remove(equipment_Type);
                    Mainequipment_type._equipment_TypeContext.SaveChanges();

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