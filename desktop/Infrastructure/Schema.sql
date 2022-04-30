create table LabelFieldMaps (
	[Id] INTEGER,
	[Name] varchar(255) NOT NULL,
	[TemplatePath] varchar(255),
	[PrintQty] int,
	[Type] varchar(255),
	[Fields] varchar(255),
	PRIMARY KEY([Id] ASC)
);

create table EmailTemplates (
	[Id] INTEGER,
	[Name] varchar(255) NOT NULL,
	[Sender] varchar(255),
	[Password] varchar(255),
	[Subject] varchar(255),
	[Body] varchar(2048),
	[To] varchar(255),
	[Cc] varchar(255),
	[Bcc] varchar(255),
	PRIMARY KEY([Id] ASC)
);

create table ReleaseProfiles (
	[Id] INTEGER,
	[Name] varchar(255) NOT NULL,
	PRIMARY KEY([Id] ASC)
);

create table Profiles_Plugins (
	[Id] INTEGER,
	[PluginName] varchar(255) NOT NULL,
	[ProfileId] int NOT NULL,
	PRIMARY KEY([Id] ASC),
	foreign key([ProfileId]) references ReleaseProfiles(Id)
);

create table Profiles_Labels (
	[Id] INTEGER,
	[LabelId] int NOT NULL,
	[ProfileId] int NOT NULL,
	PRIMARY KEY([Id] ASC),
	foreign key([LabelId]) references LabelFieldMaps(Id),
	foreign key([ProfileId]) references ReleaseProfiles(Id)
);

create table Profiles_Emails (
	[Id] INTEGER,
	[EmailId] int NOT NULL,
	[ProfileId] int NOT NULL,
	PRIMARY KEY([Id] ASC),
	foreign key([EmailId]) references EmailTemplates(Id),
	foreign key([ProfileId]) references ReleaseProfiles(Id)
);