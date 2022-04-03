create schema [Catalog];
Go
create table [Catalog].Products (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) Not Null
);

create table [Catalog].ProductAttributes (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[ProductId] int FOREIGN KEY REFERENCES [Catalog].Products(Id),
	[Name] varchar(255) Not Null,
	[Default] varchar(255) Not Null
);