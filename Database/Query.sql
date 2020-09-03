----------------------------------------------------
select * from 
	(select Rooms.RoomNumber, FloorNumber, count(RoomNumber) as FutureReservations from Rooms 
	left outer join Reservations on Reservations.RoomID=Rooms.RoomNumber 
	where Reservations.StartDate > GETDATE() 
	group by RoomNumber, FloorNumber ) t1
left outer join
(select RoomNumber, Name, Surname, Email, Phone from Rooms 
	left outer join Reservations on Reservations.RoomID=Rooms.RoomNumber 
	inner join Customers on Customers.Id=Reservations.CustomerID
	where Reservations.StartDate < GETDATE() and Reservations.EndDate > GETDATE()
	) t2
on t1.RoomNumber = t2.RoomNumber

----------------------------------------------------

select * from 
	(select  Customers.Id, Name, Surname, COUNT(Customers.Id) AS ReservationsCount, AVG(DATEDIFF(DAY, StartDate, EndDate)) AS AvgTime from Customers 
	left outer join Reservations on Reservations.CustomerID=Customers.Id 
	group by Customers.Id, Name, Surname
	order by ReservationsCount offset 0 rows) t1
inner join
	(select Customers.Id, Reservations.RoomID from Customers 
	left outer join Reservations on Reservations.CustomerID=Customers.Id 
	group by Customers.Id, Reservations.RoomID
	order by count(Reservations.RoomID) desc offset 0 rows) t2
on t1.Id = t2.Id

