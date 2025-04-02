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
    /// Логика взаимодействия для equipment_typeEdit.xaml
    /// </summary>
    public partial class equipment_typeEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.list.equipment_type Mainequipment_type;
        public Models.equipment_type equipment_Type;

        public equipment_typeEdit(list.equipment_type Mainequipment_type, Models.equipment_type equipment_Type = null)
        {
            InitializeComponent();
            this.Mainequipment_type = Mainequipment_type;
            this.equipment_Type = equipment_Type;

            tb_typeName.Text = equipment_Type.type_name;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.equipment_type met = Mainequipment_type._equipment_TypeContext.equipment_type.FirstOrDefault(x => x.type_id == equipment_Type.type_id);
                met.type_name = tb_typeName.Text;

                Mainequipment_type._equipment_TypeContext.SaveChanges();

                MainWindow.init.OpenPages(MainWindow.pages.equipment_type);
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
