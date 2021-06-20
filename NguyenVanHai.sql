CREATE DATABASE NguyenVanHaiDB

USE NguyenVanHaiDB

CREATE TABLE UserAccount
(
	ID int IDENTITY(1,1) primary key,
	UserName  char(50) NOT NULL,
	PassWord char(100) NOT NULL,
	Status char(10)
)

CREATE TABLE Category
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(200) NOT NULL,
	Description nvarchar(200)
)

CREATE TABLE Product
(
	ID int IDENTITY(1,1) PRIMARY KEY,
	Name nvarchar(200) NOT NULL,
	UnitCost decimal(18, 0),
	Quantity BIGINT,
	Image varchar(200),
	Description nvarchar(200),
	Status nvarchar(50),
	IDCategory int NOT NULL foreign key references Category(ID)
)


 --ALTER TABLE Product ALTER COLUMN IDCategory  int NOT NULL;