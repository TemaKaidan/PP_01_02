using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static PP_01_02.MainWindow;

namespace PP_01_02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;

        public Pages.list.equipment_type Mainequipment_Type = new Pages.list.equipment_type();
        public Pages.list.equipment Mainequipment = new Pages.list.equipment();
        public Pages.list.employees Mainemployeest = new Pages.list.employees();

        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPages(pages.main);
        }

        public enum pages
        {
            main,
            equipment_type, equipment_typeAdd, equipment_typeEdit,
            equipment, equipmentAdd, equipmentEdit,
            employees, employeesAdd, employeesEdit
        }

        public void OpenPages(pages _pages, 
            Models.equipment_type met = null,
            Models.equipment me = null,
            Models.employees memp = null)
        {
            this.MinHeight = 800;
            this.MinWidth = 950;
            this.Height = 750;
            this.Width = 900;

            switch (_pages)
            {
                case pages.main:
                    frame.Navigate(new Pages.Main());
                    break;

                case pages.equipment_type:
                    frame.Navigate(new Pages.list.equipment_type());
                    break;

                case pages.equipment_typeAdd:
                    frame.Navigate(new Pages.Add.equipment_typeAdd(Mainequipment_Type));
                    break;

                case pages.equipment_typeEdit:
                    frame.Navigate(new Pages.Edit.equipment_typeEdit(Mainequipment_Type, met));
                    break;

                case pages.equipment:
                    frame.Navigate(new Pages.list.equipment());
                    break;

                case pages.equipmentAdd:
                    frame.Navigate(new Pages.Add.equipmentAdd(Mainequipment));
                    break;

                case pages.equipmentEdit:
                    frame.Navigate(new Pages.Edit.equipmentEdit(Mainequipment, me));
                    break;

                case pages.employees:
                    frame.Navigate(new Pages.list.employees());
                    break;

                case pages.employeesAdd:
                    frame.Navigate(new Pages.Add.employeesAdd(Mainemployeest));
                    break;

                case pages.employeesEdit:
                    frame.Navigate(new Pages.Edit.employeesEdit(Mainemployeest, memp));
                    break;
            }
        }
    }
}