create database OneToManyDb
GO
use OneToManyDb
GO

create table Groups(
GroupId int primary key identity(1,1) not null,
[Title] nvarchar(30) not null
)

GO



create table Students(
StudentId int primary key identity(1,1) not null,
[Firstname] nvarchar(30) not null,
[Age] int not null,
GroupId int foreign key references Groups(GroupId)
)

GO
insert into Groups([Title])
values('2201_az'),('12012_az')
GO

insert into Students([Firstname],[Age],[GroupId])
values('Kamran',23,1),('Lale',25,1),
('Murad',18,2),('Mehrac',28,2)

GO

select * from Students
inner join Groups
on Groups.GroupId=Students.GroupId


