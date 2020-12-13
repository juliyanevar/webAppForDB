create database Movie_theaters;

use Movie_theaters;


create table Movie_theater
(
	Id int identity(1,1) primary key,
	[Name] nvarchar(50) unique,
	[Address] nvarchar(100) unique,
	Count_cinema_hall int
)


create table Customer
(
	Id int identity(1,1) primary key,
	[Login] nvarchar(30) unique,
	[Password] varbinary(max),
	Email nvarchar(320) unique,
	[Role] nvarchar(15) default 'user' check ([Role] in ('admin', 'user'))
)

create table Movie 
(
	Id int identity(1,1) primary key,
	Title nvarchar(200),
	Age_bracket nvarchar(10),
	Duration int,
	First_day_of_rental date,
	Last_day_of_rental date
)

create table Genre
(
	Id int identity(1,1) primary key,
	[Name] nvarchar(30) unique
)

create table Movies_genres
(
	Movie_id int foreign key references Movie(Id),
	Genre_id int foreign key references Genre(Id)
)

create table Cinema_hall
(
	Id int identity(1,1) primary key,
	Movie_theater_id int foreign key references Movie_theater(Id),
	[Number] int,
	Count_places int,
	[Count_rows] int
)

create table Seance
(
	Id int identity(1,1) primary key,
	Movie_theater_id int foreign key references Movie_theater(Id),
	Cinema_hall_id int foreign key references Cinema_hall(Id),
	Movie_id int foreign key references Movie(Id),
	[Date] date,
	[Time] time
)

create table Ticket
(
	Id int identity(1,1) primary key,
	Seance_id int foreign key references Seance(Id),
	Cost smallmoney,
	[Row] int,
	Place int
)

create table Booking 
(
	Id int identity(1,1) primary key,
	Customer_id int foreign key references Customer(Id),
	Ticket_id int foreign key references Ticket(Id)
)