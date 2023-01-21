
create table Icon
(
	Id int primary key identity (1, 1),
	InsertTime datetime not null default (getdate()),
	DeleteTime datetime,
	Name varchar(50) not null,
	CssClass varchar(100),
	Image varchar(1)
)
go


