create table Companies (
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

create table Orders (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) NOT NULL,
	[Number] varchar(255) NOT NULL,
	[CustomerId] int,
	[VendorId] int,
	[SupplierId] int,
	[PlacedDate] datetime DEFAULT CURRENT_TIMESTAMP,
	[ConfirmedDate] datetime,
	[ReleasedDate] datetime,
	[CompletedDate] datetime,
	[LastModifiedDate] datetime DEFAULT CURRENT_TIMESTAMP,
	[Status] varchar(255) NOT NULL,
	[Info] TEXT,
	FOREIGN KEY(CustomerId) REFERENCES Companies(Id),
	FOREIGN KEY(VendorId) REFERENCES Companies(Id),
	FOREIGN KEY(SupplierId) REFERENCES Companies(Id)
);

create table OrderedItems (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[OrderId] int,
	[ProductId] int NOT NULL,
	[ProductName] varchar(255) NOT NULL,
	[ProductClass] int NOT NULL,
	[Qty] varchar(255) NOT NULL,
	[Options] TEXT,
	FOREIGN KEY(OrderId) REFERENCES Orders(Id)
);

create table Contacts (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[CompanyId] int,
	[Name] varchar(255) NOT NULL,
	[Email] varchar(255),
	[Phone] varchar(255),
	FOREIGN KEY(CompanyId) REFERENCES Companies(Id)
);

go

/* This trigger will update the LastModifiedDate of an Order whenever it is updated */
create trigger trg_Orders_UpdateModifiedDate
on Orders
begin
	update Sales.Orders
	set [LastModifiedDate] = CURRENT_TIMESTAMP
	where [Id] in (select distinct Id from inserted)
end;