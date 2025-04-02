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
    /// Логика взаимодействия для equipmentEdit.xaml
    /// </summary>
    public partial class equipmentEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.list.equipment Mainequipment;
        public Models.equipment equipment;

        Context.equipment_typeContext equipment_typeContext = new Context.equipment_typeContext();
        public equipmentEdit(Pages.list.equipment Mainequipment, Models.equipment equipment = null)
        {
            InitializeComponent();
            this.Mainequipment = Mainequipment;
            this.equipment = equipment;

            tb_Name.Text = equipment.name;
            foreach (Models.equipment_type equipment_type in equipment_typeContext.equipment_type)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = equipment_type.type_name;
                item.Tag = equipment_type.type_id;
                if (equipment_type.type_id == equipment.type_id)
                {
                    item.IsSelected = true;
                }
                cb_type_id.Items.Add(item);
            }
            tb_serial_number.Text = equipment.serial_number;
            tb_manufacturer.Text = equipment.manufacturer;
            db_date.Text = equipment.installation_date.ToString();
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.equipment editequipment = Mainequipment._equipmentContext.equipment.
                    FirstOrDefault(x => x.equipment_id == equipment.equipment_id);
                if (editequipment != null)
                {
                    editequipment.name = tb_Name.Text;
                    editequipment.type_id = (int)(cb_type_id.SelectedItem as ComboBoxItem).Tag;
                    editequipment.serial_number = tb_serial_number.Text;
                    editequipment.manufacturer = tb_manufacturer.Text;
                    editequipment.installation_date = DateTime.Parse(db_date.Text);

                    // Сохранение изменений в базе данных
                    Mainequipment._equipmentContext.SaveChanges();
                    MainWindow.init.OpenPages(MainWindow.pages.equipment);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка!");
                    MainWindow.init.OpenPages(MainWindow.pages.equipment);
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