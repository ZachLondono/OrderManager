create schema [Catalog];
Go
create table [Catalog].Products (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) Not Null,
	[Attributes] varchar(MAX)
);