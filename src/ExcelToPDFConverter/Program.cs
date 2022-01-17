using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelToPDFConverter {

    internal class Program {
        static void Main(string[] args) {

            var start = DateTime.Now;

            const string filePath = @"C:\Users\Zachary Londono\Desktop\report.xlsx";
            const string exportPath = @"C:\Users\Zachary Londono\Desktop\report.pdf";
            const string sheetName = "Sheet1";

            Excel.Application app = new Excel.Application {
                Visible = false
            };

            Excel.Workbook wb = app.Workbooks.Open(filePath);

            Excel.Worksheet ws = wb.Worksheets[sheetName];

            ws.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, Filename: exportPath);
            wb.Close(SaveChanges: false);

            var end = DateTime.Now;

            Console.ReadLine();

        }
    }

}
