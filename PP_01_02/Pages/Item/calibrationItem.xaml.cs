using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using PP_01_02.Context;
using System.IO;
using System.Windows;
using System.Windows.Controls;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using System.IO;
using iText.IO.Font;

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
            lb_calibration_date.Content = "Дата калибровки: " + calibration.calibration_date.ToString("dd.MM.yyyy");

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

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            List<Models.calibration> calibrations;
            using (var context = new calibrationContext())
            {
                calibrations = context.calibration.ToList();
            }
            GeneratePdfWithTable(calibrations);
        }

        public void GeneratePdfWithTable(List<Models.calibration> calibrations)
        {
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Отчет по калибровке.pdf");

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                PdfFont font = PdfFontFactory.CreateFont("c:/windows/fonts/arial.ttf", PdfEncodings.IDENTITY_H);

                document.Add(new iText.Layout.Element.Paragraph("Отчет по калибровке оборудования")
                    .SetFont(font)
                    .SetFontSize(18)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                iText.Layout.Element.Table table = new iText.Layout.Element.Table(5);
                table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("№").SetFont(font)));
                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Оборудование").SetFont(font)));
                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Сотрудник").SetFont(font)));
                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Результат калибровки").SetFont(font)));
                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Дата").SetFont(font)));

                int rowNumber = 1;
                foreach (var calibration in calibrations)
                {
                    var equipmentName = _equipmentContext.equipment.FirstOrDefault(x => x.equipment_id == calibration.equipment_id)?.name ?? "Неизвестно";

                    var employeeName = "Неизвестно";
                    employeesContext _employeesContext = new employeesContext();
                    var employee = _employeesContext.employees.FirstOrDefault(x => x.employee_id == calibration.calibrated_by);
                    if (employee != null)
                    {
                        employeeName = $"{employee.last_name} {employee.name} {employee.sur_name}";
                    }

                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(rowNumber.ToString()).SetFont(font)));
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(equipmentName).SetFont(font)));
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(employeeName).SetFont(font)));
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(calibration.calibration_result).SetFont(font)));
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(calibration.calibration_date.ToString("dd.MM.yyyy")).SetFont(font)));

                    rowNumber++;
                }
                document.Add(table);
                document.Close();
            }
            MessageBox.Show($"PDF файл был успешно создан: {filePath}");
        }
    }
}