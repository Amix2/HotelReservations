create table Rooms (
	RoomNumber int primary key,
	FloorNumber int,
	RoomType varchar(255) not null,
	RoomSize int,
	Capacity int,
	PriceForNight decimal
)

create table Customers (
	Id int primary key,
	Name varchar(255) not null,
	Surname varchar(255) not null,
	Email varchar(255) not null,
	Phone varchar(255) not null
)

create table Reservations (
	Id int primary key,
	CustomerID int foreign key references Customers(Id),
	RoomID int foreign key references Rooms(RoomNumber),
	StartDate Date not null,
	EndDate Date not null,
	VisitorsCount int
)