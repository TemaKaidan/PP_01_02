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
    /// Логика взаимодействия для equipmentAdd.xaml
    /// </summary>
    public partial class equipmentAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.list.equipment Mainequipment;
        public Models.equipment equipment;

        Context.equipment_typeContext equipment_typeContext = new Context.equipment_typeContext();

        public equipmentAdd(list.equipment Mainequipment, Models.equipment equipment = null)
        {
            InitializeComponent();
            this.Mainequipment = Mainequipment;
            this.equipment = equipment;

            cb_type_id.Items.Clear();
            cb_type_id.ItemsSource = equipment_typeContext.equipment_type.ToList(); 
            cb_type_id.DisplayMemberPath = "type_name";
            cb_type_id.SelectedValuePath = "id";
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            try
            {
                if (equipment == null)
                {
                    equipment = new Models.equipment
                    {
                        name = tb_Name.Text,
                        type_id = (cb_type_id.SelectedItem as Models.equipment_type).type_id,
                        serial_number = tb_serial_number.Text,
                        manufacturer = tb_manufacturer.Text,
                        installation_date = db_date.SelectedDate ?? DateTime.MinValue
                    };

                    Mainequipment._equipmentContext.equipment.Add(equipment);
                }

                Mainequipment._equipmentContext.SaveChanges();

                MainWindow.init.OpenPages(MainWindow.pages.equipment);
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