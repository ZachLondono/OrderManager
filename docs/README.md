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


## Features
1. Load orders from a number of different sources (sould be easily extendible) and store the data in a database
      * IOrderDataStrategy - classes which implement this provide some mechanism for loading order data (not the order entity)
      * IOrderService - uses an order data strategy to load order data and then creates an order from the data
3. Execute a number of operations to 'Release' an order
      * Public OrderReleaseNotification when user pushes the 'Release' button
      * Multiple handlers will then do some operation on the Order to prepare it for release
4. Create production reports for a given time period
      * ClosedXML.Report for generating reports from excel templates
      * Razor for generating reports from HTML tempaltes
