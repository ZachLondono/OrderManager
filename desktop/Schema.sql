create table LabelFieldMaps (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) NOT NULL,
	[TemplatePath] varchar(255),
	[PrintQty] int,
	[Type] varchar(255),
	[Fields] varchar(255)
);

create table EmailTemplates (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) NOT NULL,
	[Sender] varchar(255),
	[Password] varchar(255),
	[Subject] varchar(255),
	[Body] varchar(MAX),
	[To] varchar(255),
	[Cc] varchar(255),
	[Bcc] varchar(255)
);

create table ReleaseProfiles (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[Name] varchar(255) NOT NULL
);

create table Profiles_Plugins (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[PluginName] varchar(255) NOT NULL,
	[ProfileId] int NOT NULL,
	foreign key([ProfileId]) references ReleaseProfiles(Id)
);

create table Profiles_Labels (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[LabelId] int NOT NULL,
	[ProfileId] int NOT NULL,
	foreign key([LabelId]) references LabelFieldMaps(Id),
	foreign key([ProfileId]) references ReleaseProfiles(Id)
);

create table Profiles_Emails (
	[Id] int IDENTITY(1,1) PRIMARY KEY,
	[EmailId] int NOT NULL,
	[ProfileId] int NOT NULL,
	foreign key([EmailId]) references EmailTemplates(Id),
	foreign key([ProfileId]) references ReleaseProfiles(Id)
);