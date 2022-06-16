create table WorkCells (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Alias] varchar(255),
	[ProductClass] int
)

create table Jobs (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[OrderId] int NOT NULL,
	[Name] varchar(255) NOT NULL,
	[Number] varchar(255) NOT NULL,
	[CustomerName] varchar(255),
	[ScheduledDate] datetime,
	[ReleasedDate] datetime,
	[CompletedDate] datetime,
	[ShippedDate] datetime,
	[Status] varchar(255) NOT NULL,
	[ProductClass] int,
	[ProductQty] int,
	[WorkCellId] int,
	FOREIGN KEY(WorkCellId) REFERENCES WorkCells(Id)
);