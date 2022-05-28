create schema [Catalog];
Go
create table [Catalog].ProductClasses (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) Not Null
);
create table [Catalog].Products (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) Not Null,
	[Class] int FOREIGN KEY REFERENCES [Catalog].ProductClasses(Id),
	[Attributes] varchar(MAX)
);