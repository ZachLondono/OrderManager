create schema Manufacturing;
Go
create table Manufacturing.Job (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) NOT NULL,
	[Number] varchar(255) NOT NULL,
	[CustomerId] int,
	[VendorId] int,
	[ItemCount] int,
	[ReleasedDate] datetime,
	[CompletedDate] datetime,
	[ShippedDate] datetime,
	[Status] varchar(255) NOT NULL
);