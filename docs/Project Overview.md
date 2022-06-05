# Project Overview

## Objectives
In an effort to increase the volume and efficiency of our manufacturing processes this project will improve insight into the current status of orders as they move through out the ordering and manufacturing processes. By streamlining the procedure for receiving orders  and creating a unified pipeline for all different product offerings we can minimize complexity and increase the throughput on the front end. By providing the ability to view jobs both from a big-picture-view as well as more fine grained work-cell-view job scheduling can be done more efficiently and bottlenecks can found and eliminated more easily.

 This consists of two main components a backend web api which reads and writes data to a database, and a desktop application used to view and edit data as well as other features listed bellow. Secondary applications include a barcode scanning desktop application used in the workshop to keep track of order statuses and an online vendor portal webpage used by vendors to see up to date information on their orders.

## Web API Features
The web API consists of three 'contexts', Sales, Catalog and Manufacturing.
- The __Sales__ context is responsible for maintaining data on <u>Orders</u> and <u>Companies</u>, exposing endpoints to create, read, and update the entities. <u>Orders</u> are made up of <u>Ordered Items</u>, a customer, a vendor, a supplier and any number of additional fields. <u>Companies</u> represent a company which takes part in some part of the ordering process. A <u>Company</u> may have one or more role; _customer_, _vendor_, _supplier_.
- The __Catalog__ context is responsible for maintaining data on <u>Products</u>. Products can have any number of attributes, may include attribute validation, and a pricing formula.
- The __Manufacturing__ context is responsible for tracking <u>Jobs</u> as they move through the production process. <u>Jobs</u> can be scheduled to be manufactured on a specific date.
### Technologies
Azure Functions, SQL Server, MediatR, Dapper, FluentValidation

## Desktop Features
The main purpose of the desktop application is to act as an interface for the web api and to allow users to manage and create orders. Orders can be viewed and edited in the desktop application and then updates can be sent to the backend to be saved. The desktop application is responsible for most of the significant operations involved in releasing an order into production. An order release can consist of any combination of three main actions:
- __Sending Emails__ - Can be configured to be sent to the customer, vendor, supplier, and/or a static email. The email confirmation utilizes string interpolation and the email body uses razor html templating engine.
- __Printing Labels__ - There are two available label types, _Order Labels_, which are printed once per each label, and _Item Labels_ which are printed once per each item in an order (if an ordered item has a quantity of 2, then two labels will be printed). Each field in a label utilizes string interpolation to create the desired field values. The label templates are created from Dymo label files, and must be printed with a Dymo label printer.
- __Executing a Release Plugin__ - The plugin will be given an instance of the released order and will run any arbitrary code.
### Technologies
[Avalonia](https://avaloniaui.net/), [AvaloniaUIRibbon](https://github.com/Splitwirez/AvaloniaRibbon), Dapper, [McMaster.NETCore.Plugins](https://github.com/natemcmaster/DotNetCorePlugins), Sqlite, [Refit](https://github.com/reactiveui/refit), CSharp.Scripting, [RazorEngineCore](https://github.com/adoconnection/RazorEngineCore)

## Plugins
Plugins are compiled class libraries which implement the IPlugin interface and can be loaded and swapped out during runtime. A release plugin will be passed an instance of the order object when the user releases the order.

## Vendor Portal Features
- View live information on current orders
- Add notes to orders
- Assign priority levels to orders
- Export order data to .xlsx files
### Technologies
Blazor

## Projects
- __Backend__ - Each of the backend contexts consists of two projects; a contracts project and an implementations project. The contracts plugin includes classes, interfaces, notifications, etc. that can be referenced by other backend projects.
	- __Catalog__  - Catalog.Contracts / Catalog.Infrastructure
	- __Sales__  - Sales.Contracts / Sales.Infrastructure
	- __Manufacturing__  - Manufacturing.Contracts / Manufacturing.Infrastructure
	- __Functions__
- __Desktop__
	- UI
	- Domain
	- Infrastructure
	- Application Core
	- Plugin Contracts
- __Web__

## Key Words
- __Order__
- __Ordered Item__
- __Customer__ 
- __Vendor__
- __Supplier__  
- __Product__
- __Job__  
- __Product Label__ 
- __Order Label__
