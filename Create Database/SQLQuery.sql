create database DbLibrary

use DbLibrary

create table Category(
	categoryId tinyint primary key identity(1,1),
	categoryName varchar(20)
)

create table Author(
	authorId int primary key identity(1,1),
	firstName varchar(30),
	lasName varchar(30),
	detail varchar(1000)
)

create table Book(
	bookId int primary key identity(1,1),
	bookName varchar(50),
	categoryId tinyint,
	authorId int,
	publicationYear char(4),
	publishingHouse varchar(50),
	pageNumber varchar(4),
	statusBook bit,
	foreign key (categoryId) references Category (categoryId),
	foreign key (authorId) references Author (authorId)
)

create table Members(
	memberId int primary key identity(1,1),
	firstName varchar(20),
	lasName varchar(20),
	eMail varchar(50),
	userName varchar(20),
	userPassword varchar(20),
	photo varchar(250),
	phone varchar(20),
	school varchar(100)
)

create table Employee(
	employeeId tinyint primary key identity(1,1),
	employeeName varchar(100)
)

create table MovementType(
	movementTypeId int primary key identity(1,1),
	bookId int,
	memberId int,
	employeeId tinyint, 
	purchaseDate smallDateTime,
	returnDate smallDateTime,
	foreign key (bookId) references Book (bookId),
	foreign key (memberId) references Members (memberId),
	foreign key (employeeId) references Employee (employeeId)
)

create table Penalties(
	penaltyId int primary key identity(1,1),
	memberId int,
	movementTypeId int,
	penaltyStart smallDateTime,
	penaltyEnd smallDateTime,
	money decimal(18,2),
	foreign key (memberId) references Members (memberId),
	foreign key (movementTypeId) references MovementType (movementTypeId)
)

create table CashRegisters(
	id int primary key identity(1,1),
	monthDate varchar(20),
	totalAmount decimal(18,2)
)