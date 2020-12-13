use Movie_theaters;



create procedure ImportFromXMLTheaters
as
begin
	insert into Movie_theater([Name], [Address], Count_cinema_hall)
	select 
		my_xml.rec.query('Name').value('.', 'nvarchar(50)'),
		my_xml.rec.query('Address').value('.', 'nvarchar(100)'),
		my_xml.rec.query('Count_cinema_hall').value('.', 'int')
		from (select cast(my_xml as xml)
				from openrowset(bulk 'C:\temp.xml', single_blob) as t(my_xml)) as t(my_xml)
				cross apply my_xml.nodes('Theaters/Movie_theaters/Movie_theater') as my_xml (rec);
end;


create procedure ImportFromXMLCustomer
as
begin
	insert into Customer([Login], [Password], Email, [Role])
	select 
		my_xml.rec.query('Login').value('.', 'nvarchar(30)'),
		my_xml.rec.query('Password').value('.', 'varbinary(max)'),
		my_xml.rec.query('Email').value('.', 'nvarchar(320)'),
		my_xml.rec.query('Role').value('.', 'nvarchar(15)')
		from (select cast(my_xml as xml)
				from openrowset(bulk 'C:\temp.xml', single_blob) as t(my_xml)) as t(my_xml)
				cross apply my_xml.nodes('Theaters/Customers/Customer') as my_xml (rec);
end;


create procedure ImportFromXMLMovie
as
begin
	insert into Movie(Title, Age_bracket, Duration, First_day_of_rental, Last_day_of_rental)
	select 
		my_xml.rec.query('Title').value('.', 'nvarchar(200)'),
		my_xml.rec.query('Age_bracket').value('.', 'nvarchar(10)'),
		my_xml.rec.query('Duration').value('.', 'int'),
		my_xml.rec.query('First_day_of_rental').value('.', 'date'),
		my_xml.rec.query('Last_day_of_rental').value('.', 'date')
		from (select cast(my_xml as xml)
				from openrowset(bulk 'C:\temp.xml', single_blob) as t(my_xml)) as t(my_xml)
				cross apply my_xml.nodes('Theaters/Movies/Movie') as my_xml (rec);
end;

create procedure ImportFromXMLGenre
as
begin
	insert into Genre([Name])
	select 
		my_xml.rec.query('Name').value('.', 'nvarchar(30)')
		from (select cast(my_xml as xml)
				from openrowset(bulk 'C:\temp.xml', single_blob) as t(my_xml)) as t(my_xml)
				cross apply my_xml.nodes('Theaters/Genres/Genre') as my_xml (rec);
end;



create procedure ImportFromXMLMoviesGenres
as
begin
	insert into Movies_genres(Movie_id, Genre_id)
	select 
		my_xml.rec.query('Movie_id').value('.', 'int'),
		my_xml.rec.query('Genre_id').value('.', 'int')
		from (select cast(my_xml as xml)
				from openrowset(bulk 'C:\temp.xml', single_blob) as t(my_xml)) as t(my_xml)
				cross apply my_xml.nodes('Theaters/Movies_genres/Movie_tgenre') as my_xml (rec);
end;


create procedure ImportFromXMLCinemaHall
as
begin
	insert into Cinema_hall(Movie_theater_id, [Number], Count_places, [Count_rows])
	select 
		my_xml.rec.query('Movie_theater_id').value('.', 'int'),
		my_xml.rec.query('Number').value('.', 'int'),
		my_xml.rec.query('Count_places').value('.', 'int'),
		my_xml.rec.query('Count_rows').value('.', 'int')
		from (select cast(my_xml as xml)
				from openrowset(bulk 'C:\temp.xml', single_blob) as t(my_xml)) as t(my_xml)
				cross apply my_xml.nodes('Theaters/Cinema_halls/Cinema_hall') as my_xml (rec);
end;


create procedure ImportFromXMLSeance
as
begin
	insert into Seance(Movie_theater_id, Cinema_hall_id, Movie_id, [Date], [Time])
	select 
		my_xml.rec.query('Movie_theater_id').value('.', 'int'),
		my_xml.rec.query('Cinema_hall_id').value('.', 'int'),
		my_xml.rec.query('Movie_id').value('.', 'int'),
		my_xml.rec.query('Date').value('.', 'date'),
		my_xml.rec.query('Time').value('.', 'time')
		from (select cast(my_xml as xml)
				from openrowset(bulk 'C:\temp.xml', single_blob) as t(my_xml)) as t(my_xml)
				cross apply my_xml.nodes('Theaters/Seances/Seance') as my_xml (rec);
end;


create procedure ImportFromXMLTicket
as
begin
	insert into Ticket(Seance_id, Cost, [Row], Place)
	select 
		my_xml.rec.query('Seance_id').value('.', 'int'),
		my_xml.rec.query('Cost').value('.', 'smallmoney'),
		my_xml.rec.query('Row').value('.', 'int'),
		my_xml.rec.query('Place').value('.', 'int')
		from (select cast(my_xml as xml)
				from openrowset(bulk 'C:\temp.xml', single_blob) as t(my_xml)) as t(my_xml)
				cross apply my_xml.nodes('Theaters/Tickets/Ticket') as my_xml (rec);
end;


create procedure ImportFromXMLBooking
as
begin
	insert into Booking(Customer_id, Ticket_id)
	select 
		my_xml.rec.query('Customer_id').value('.', 'int'),
		my_xml.rec.query('Ticket_id').value('.', 'int')
		from (select cast(my_xml as xml)
				from openrowset(bulk 'C:\temp.xml', single_blob) as t(my_xml)) as t(my_xml)
				cross apply my_xml.nodes('Theaters/Booking/Booking') as my_xml (rec);
end;



