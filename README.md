# OrderManager
An order managment desktop application.

# Projects

### ApplicationCore
Contains domain entities and main applicationlogic 

#### Dependencies:
  MediatR - for cqrs pattern
  FluentValidation - to validate requests
  ClosedXML.Report - for creating excel reports
  Dapper - for reading and writing to sql database
  System.Data.OleDb - for accessing Access database
  
### ExcelToPDFConverter
.Net 4.8 console application that uses Microsoft.Office.Interop.Excel to print excel files (cannot be done with ClosedXML)

#### Dependencies:
  Microsoft.Office.Interop.Excel - for interacting with excel files
