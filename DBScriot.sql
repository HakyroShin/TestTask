create database CountTime

use CountTime

create table Employees
(
	Id int identity(1,1) primary key not null,
	Name nvarchar(max) not null,
	Phone nvarchar(max) not null,
	Email nvarchar(max) not null,
);

create table Tasks
(
	Id int identity(1,1) primary key not null,
	Name nvarchar(max) not null, 
	Comment nvarchar(max) not null,
);

create table Times
(
	Id int identity(1,1) primary key not null,
	EmployeeId int not null,
	TaskId int not null,
	Date datetime not null,
	Minutes int not null,
);