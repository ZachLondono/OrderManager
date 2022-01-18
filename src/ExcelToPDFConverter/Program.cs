using System;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelToPDFConverter {

    internal class Program {

        struct PrintParameters {
            public string FilePath;
            public string ExportPath;
            public string SheetName;
        };

        static void Main(string[] args) {

            if (args.Length != 6) {
                Console.Error.WriteLine("Invalid number of arguments");
                return;
            }

            PrintParameters settings = new PrintParameters();
            for (int i = 0; i < args.Length - 1; i++) {
                switch (args[i]) {
                    case "--FilePath":
                        settings.FilePath = args[++i];
                        break;
                    case "--ExportPath":
                        settings.ExportPath = args[++i];
                        break;
                    case "--SheetName":
                        settings.SheetName = args[++i];
                        break;
                    default:
                        Console.Error.WriteLine($"Unexpected token near {args[i]}");
                        return;
                }
            }

            if (string.IsNullOrEmpty(settings.FilePath)
                || string.IsNullOrEmpty(settings.SheetName)
                || string.IsNullOrEmpty(settings.ExportPath)) {
                Console.Error.WriteLine("Unexpected blank parameter");
                return;
            }

            File.Exists(settings.FilePath);

            Excel.Application app = new Excel.Application {
                Visible = false
            };

            var wb = app.Workbooks
                .Open(settings.FilePath);

            Excel.Worksheet ws;
            try { 
                ws = wb.Worksheets[settings.SheetName];
            } catch {
                Console.Error.WriteLine($"Sheet '{settings.SheetName}' not found in workbook");
                return;
            }

            ws.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, Filename: settings.ExportPath);

            wb.Close(SaveChanges: false);

        }

    }

}
