create schema Sales;
Go
create table Sales.Orders (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) NOT NULL,
	[Number] varchar(255) NOT NULL,
	[CustomerId] int FOREIGN KEY REFERENCES Companies(Id),
	[VendorId] int FOREIGN KEY REFERENCES Companies(Id),
	[SupplierId] int FOREIGN KEY REFERENCES Companies(Id),
	[PlacedDate] datetime,
	[ConfirmationDate] datetime,
	[CompletionDate] datetime,
	[Status] varchar(255) NOT NULL,
	[Fields] varchar(MAX)
);

create table Sales.OrderedItems (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[OrderId] int FOREIGN KEY REFERENCES Orders(Id),
	[ProductId] int NOT NULL,
	[ProductName] varchar(255) NOT NULL,
	[Qty] varchar(255) NOT NULL,
	[Options] varchar(MAX)
);

create table Sales.Companies (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) NOT NULL,
	[Roles] varchar(255),
	[Line1] varchar(255),
	[Line2] varchar(255),
	[Line3] varchar(255),
	[City] varchar(255),
	[State] varchar(255),
	[Zip] varchar(255)
);

create table Sales.Contacts (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[CompanyId] int FOREIGN KEY REFERENCES Companies(Id),
	[Name] varchar(255) NOT NULL,
	[Email] varchar(255),
	[Phone] varchar(255)
);