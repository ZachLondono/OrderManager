create table ProductClasses (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) Not Null
);
create table Products (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) Not Null,
	[Class] int,
	[Attributes] TEXT,
	FOREIGN KEY(Class) REFERENCES ProductClasses(Id)
);