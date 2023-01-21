
create table Role
(
	[Id] int primary key identity (1, 1),
	[InsertTime] datetime not null default (getdate()),
	[DeleteTime] datetime,
	[Name] varchar(100) not null
)
go


