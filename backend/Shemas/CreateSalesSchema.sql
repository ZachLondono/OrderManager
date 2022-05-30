create schema Sales;
Go

create table Sales.Companies (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) NOT NULL,
	[Email] varchar(255) NOT NULL,
	[Roles] varchar(255),
	[Line1] varchar(255),
	[Line2] varchar(255),
	[Line3] varchar(255),
	[City] varchar(255),
	[State] varchar(255),
	[Zip] varchar(255)
);

create table Sales.Orders (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) NOT NULL,
	[Number] varchar(255) NOT NULL,
	[CustomerId] int FOREIGN KEY REFERENCES Sales.Companies(Id),
	[VendorId] int FOREIGN KEY REFERENCES Sales.Companies(Id),
	[SupplierId] int FOREIGN KEY REFERENCES Sales.Companies(Id),
	[PlacedDate] datetime DEFAULT CURRENT_TIMESTAMP,
	[ConfirmedDate] datetime,
	[ReleasedDate] datetime,
	[CompletedDate] datetime,
	[LastModifiedDate] datetime DEFAULT CURRENT_TIMESTAMP,
	[Status] varchar(255) NOT NULL,
	[Info] varchar(MAX)
);

create table Sales.OrderedItems (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[OrderId] int FOREIGN KEY REFERENCES Sales.Orders(Id),
	[ProductId] int NOT NULL,
	[ProductName] varchar(255) NOT NULL,
	[ProductClass] int NOT NULL,
	[Qty] varchar(255) NOT NULL,
	[Options] varchar(MAX)
);

create table Sales.Contacts (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[CompanyId] int FOREIGN KEY REFERENCES Sales.Companies(Id),
	[Name] varchar(255) NOT NULL,
	[Email] varchar(255),
	[Phone] varchar(255)
);

go

/* This trigger will update the LastModifiedDate of an Order whenever it is updated */
create trigger trg_Orders_UpdateModifiedDate
on Sales.Orders
after update
as
update Sales.Orders
set [LastModifiedDate] = CURRENT_TIMESTAMP
where [Id] in (select distinct Id from inserted);