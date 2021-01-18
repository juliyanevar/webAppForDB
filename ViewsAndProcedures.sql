use Movie_theaters;



create view Cinema as select m.Id, m.Name, m.Address, c.Number [Number of hall], c.Count_places, c.Count_rows, c.Count_places/c.Count_rows [Count places on rows] 
	from Movie_theater m join Cinema_hall c 
	on m.id=c.Movie_theater_id;

	select * from Cinema






create procedure AddMovieGenre @id_movie int, @name_genre nvarchar(30)
as
begin
	declare @id_genre int;
	if EXISTS(select * from Movie where Id=@id_movie)
	begin
		if EXISTS(select * from genre where [name]=@name_genre)
		begin
			set @id_genre=(select id from genre where [name]=@name_genre);
			insert into Movies_genres(Movie_id,Genre_id) values(@id_movie, @id_genre);
			return 1;
		end
		else
		begin
			return 0;
		end
	end
	else 
	begin
		return 0;
	end
end;

select * from Movie;
select * from Genre;

select * from Movies_genres 

create procedure Movies
as
begin
declare cursorMovie cursor local static
	for select id, title, Age_bracket, Duration, First_day_of_rental, Last_day_of_rental from Movie order by title;
declare cursorGenre cursor local static 
	for select g.Id, g.Name, mg.Movie_id from Genre g join Movies_genres mg on g.id=mg.Genre_id;
declare @id_movie int, @title nvarchar(200), @age nvarchar(10), @duration int, @first_day date, @last_day date,
@fetchst1 int, @fetchst2 int, @id_genre int, @name nvarchar(30),@id_movie1 int,@genre nvarchar(350);

open cursorMovie;
fetch cursorMovie into @id_movie, @title, @age, @duration, @first_day, @last_day;
	set @fetchst1=@@FETCH_STATUS;
	while @fetchst1=0
	begin
		print 'Movie: '+@title;
		print '		Age bracket: '+@age;
		print '		Duration: '+cast(@duration as varchar(10));
		print '		First day of rental: '+cast(@first_day as varchar(30));
		print '		Last day of rental: '+cast(@last_day as varchar(30));
		open cursorGenre;
		fetch cursorGenre into @id_genre, @name, @id_movie1;
			set @fetchst2=@@FETCH_STATUS;
			set @genre='		Genre: ';
			while @fetchst2=0
			begin
				if(@id_movie=@id_movie1)
				begin
					set @genre=@genre+' '+@name;
				end;
				fetch cursorGenre into @id_genre, @name, @id_movie1;
				set @fetchst2 = @@FETCH_STATUS;
			end;
			print @genre;
		close cursorGenre;
		fetch cursorMovie into @id_movie, @title, @age, @duration, @first_day, @last_day;
		set @fetchst1 = @@FETCH_STATUS;
	end;
close cursorMovie;
end;

exec Movies;



create procedure AddSeance @id_cinema_hall int, @id_movie int, @date date, @time time
as
begin
	declare @id_movie_th int = (select Movie_theater_id from Cinema_hall where Id=@id_cinema_hall);
	declare @first_day date = (select First_day_of_rental from Movie where Id=@id_movie);
	declare @last_day date = (select Last_day_of_rental from Movie where Id=@id_movie);
	declare @seance_id int;
	if(not exists(select * from Seance where date=@date and time=@time))
	begin
		if((@date>@first_day and @date<@last_day) or @date=@first_day or @date=@last_day)
		begin
			insert into Seance(Movie_theater_id, Cinema_hall_id, Movie_id, [Date], [Time]) 
				values(@id_movie_th, @id_cinema_hall, @id_movie, @date, @time);
			set @seance_id=(select Id from Seance 
				where Movie_theater_id = @id_movie_th and Cinema_hall_id = @id_cinema_hall and Movie_id = @id_movie and [Date] = @date and [Time] = @time);
			return @seance_id;
		end;
		else
		begin
			return 0;
		end;
	end;
	else
	begin
		return 0;
	end;
end;

select * from Cinema_hall



select * from Movie;
select * from Cinema_hall;

declare @time time = '10:00';
declare @timenow time =GetDate();
print cast(@time as varchar(200)) + ' '+cast(@timenow as varchar(200))


delete Seance
select * from Seance

select * from Cinema_hall

create procedure SeanceProcedure @date date
as
begin
	declare @count_movies int = (select count(*) from Movie);
	declare @count_halls int = ( select count(*) from Cinema_hall);
	declare @time time, @x_hall int =1, @x_time int = 1, @number_movie int,@seance_id int;
	while(@x_hall<22)
	begin
		set @x_time=1;
		while(@x_time<7)
		begin
			if(@x_time=1)
			begin
				set @time='10:00';
			end;
			else if(@x_time=2)
			begin
				set @time='12:00';
			end;
			else if(@x_time=3)
			begin
				set @time='14:00';
			end;
			else if(@x_time=4)
			begin
				set @time='16:00';
			end;
			else if(@x_time=5)
			begin
				set @time='18:00';
			end;
			else if(@x_time=6)
			begin
				set @time='20:00';
			end;
			set @number_movie=ABS(CHECKSUM(NewId()) % @count_movies)+1; 
			exec @seance_id= AddSeance @x_hall, @number_movie, @date, @time;
			exec TicketProcedure @seance_id;
			set @x_time = @x_time + 1;
		end;
		set @x_hall = @x_hall + 1;
	end;
end;



select top 1 id from seance



create procedure AddTicket @id_seance int, @cost money, @row int, @place int
as
begin
	if(not exists(select * from Ticket where Seance_id=@id_seance and [Row]=@row and Place=@place))
	begin
		insert into Ticket(Seance_id, Cost, [Row], Place)
			values(@id_seance, @cost, @row, @place);
			return 1;
	end;
	else
	begin
		return 0;
	end;
end;




create procedure TicketProcedure @seance_id int
as
begin
	declare @count_seance int = (select count(*) from seance);
	declare @row int = 1, @place int = 1, @cinema_hall_id int, @count_rows int, @count_places int, @cost money = 7.5;
	set @row = 1;
	set @cinema_hall_id = (select cinema_hall_id from Seance where id = @seance_id);
	set @count_rows = (select [Count_rows] from Cinema_hall where id = @cinema_hall_id);
	set @count_places = (select Count_places from Cinema_hall where id = @cinema_hall_id)/@count_rows;
	while(@row<(@count_rows+1))
	begin
		set @place = 1;
		while(@place<(@count_places+1))
		begin
			exec AddTicket @seance_id, @cost, @row, @place;
			set @place = @place + 1;
		end;
		set @row = @row + 1;
	end;
	set @seance_id = @seance_id + 1;
end;

select * from Ticket;

select * from Seance;


/*set @password = '12345';
set @hashed_p = hashbytes('SHA2_512', CAST(@password as VARBINARY(max)));*/



create PROCEDURE AddUser @login nvarchar(30), @password nvarchar(30), @email nvarchar(320)
AS
BEGIN
	DECLARE @hashed_password varbinary(max);
	set @hashed_password = hashbytes('SHA2_512', CAST(@password as VARBINARY(max)));
	begin try
		insert into Customer([Login], [Password], Email) values(@login, @hashed_password, @email)
		return 1;
	end try
	begin catch
		RETURN 0;
	end catch;
end;


create PROCEDURE AddAdmin @login nvarchar(30) = 'admin', @password nvarchar(30) = 'admin', @email nvarchar(320) = 'juliyanevar@gmail.com', @role nvarchar(15) = 'admin'
AS
BEGIN
	DECLARE @hashed_password varbinary(max);
	set @hashed_password = hashbytes('SHA2_512', CAST(@password as VARBINARY(max)));
	if(NOT EXISTS (SELECT * FROM Customer WHERE [role] = @role))
	BEGIN
		INSERT INTO Customer([Login], [Password], Email, [Role]) values(@login, @hashed_password, @email, @role);
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	end;
END;


delete Customer;
DBCC CHECKIDENT ('Customer', RESEED, 0);
SELECT * FROM dbo.Customer;

CREATE PROCEDURE AddBooking @id_user int, @id_ticket int
AS
BEGIN
	IF(NOT EXISTS(SELECT * FROM Booking WHERE Ticket_id = @id_ticket))
	BEGIN
		IF((SELECT [Role] FROM Customer WHERE Id=@id_user) = 'user')
		BEGIN
			INSERT INTO Booking(Customer_id, Ticket_id) values(@id_user, @id_ticket);
			RETURN 1;
		END;
		ELSE
		BEGIN	
			RETURN 0;
		END;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;




CREATE PROCEDURE AddMovie @title nvarchar(200), @age_bracket nvarchar(10), @duration int, @first_day date, @last_day date
AS
BEGIN
	IF(NOT exists(SELECT * FROM Movie WHERE title=@title AND First_day_of_rental=@first_day))
	BEGIN
		INSERT INTO Movie(title, Age_bracket, Duration, First_day_of_rental, Last_day_of_rental)
				values(@title, @age_bracket, @duration, @first_day, @last_day);
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;


CREATE PROCEDURE GetAllMovies
AS
BEGIN
	SELECT * FROM Movie;
END;



CREATE PROCEDURE UpdateMovie @id int, @title nvarchar(200), @age_bracket nvarchar(10), @duration int, @first_day date, @last_day date
AS
BEGIN
	IF(exists(SELECT * FROM Movie WHERE id=@id))
	BEGIN
		UPDATE Movie SET title=@title WHERE id=@id;
		UPDATE Movie SET Age_bracket=@age_bracket WHERE id=@id;
		UPDATE Movie SET Duration=@duration WHERE id=@id;
		UPDATE Movie SET First_day_of_rental=@first_day WHERE id=@id;
		UPDATE Movie SET Last_day_of_rental=@last_day WHERE id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;


SELECT * FROM seance

USE Movie_theaters;


CREATE PROCEDURE AddMovieTheater @name nvarchar(50), @address nvarchar(100), @count_hall int
AS
BEGIN
	IF(NOT exists(SELECT * FROM Movie_theater WHERE [name]=@name))
	BEGIN
		INSERT INTO Movie_theater([Name], [Address], Count_cinema_hall) values(@name, @address, @count_hall);
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;


CREATE PROCEDURE AddGenre @name nvarchar(30)
AS
BEGIN
	if(NOT EXISTS(SELECT * FROM Genre WHERE [name]=@name))
	BEGIN
		INSERT INTO Genre([Name]) VALUES(@name);
		RETURN 1;
	END;
	ELSE
	BEGIN	
		RETURN 0;
	END;
END;


CREATE PROCEDURE AddCinemaHall @id_movie_theater int, @number int, @count_places int, @count_rows int
AS
BEGIN
	IF(NOT EXISTS(SELECT * FROM Cinema_hall WHERE Movie_theater_id=@id_movie_theater AND [Number]=@number))
	BEGIN
		INSERT INTO Cinema_hall(Movie_theater_id, [Number], Count_places, [Count_rows]) values(@id_movie_theater, @number, @count_places, @count_rows);
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;


CREATE PROCEDURE GetAllMovieTheater 
AS
BEGIN
	SELECT * FROM Movie_theater;
END;

CREATE PROCEDURE GetAllCustomer 
AS
BEGIN
	SELECT * FROM Customer;
END;


CREATE PROCEDURE GetAllGenre
AS
BEGIN
	SELECT * FROM Genre;
END;


CREATE PROCEDURE GetAllMoviesGenres
AS
BEGIN
	SELECT * FROM Movies_genres;
END;


CREATE PROCEDURE GetAllCinemaHall
AS
BEGIN
	SELECT * FROM Cinema_hall;
END;


CREATE PROCEDURE GetAllSeance
AS
BEGIN
	SELECT * FROM Seance;
END;


CREATE PROCEDURE GetAllTicket
AS
BEGIN	
	SELECT * FROM Ticket;
END;


CREATE PROCEDURE GetAllBooking
AS
BEGIN
	SELECT * FROM Booking;
END;



USE Movie_theaters;


CREATE PROCEDURE UpdateMovieTheater @id int, @name nvarchar(50), @address nvarchar(100), @countHall int
AS
BEGIN
	if(EXISTS(SELECT * FROM Movie_theater WHERE Id=@id))
	BEGIN
		UPDATE Movie_theater SET [Name]=@name WHERE Id=@id;
		UPDATE Movie_theater SET [Address]=@address WHERE Id=@id;
		UPDATE Movie_theater SET Count_cinema_hall=@countHall WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



CREATE PROCEDURE UpdateCustomer @id int,@login nvarchar(30), @password nvarchar(30), @email nvarchar(320), @role nvarchar(15)
AS
BEGIN
	DECLARE @hashed_password varbinary(max);
	set @hashed_password = hashbytes('SHA2_512', CAST(@password as VARBINARY(max)));
	IF(exists(SELECT * FROM Customer WHERE Id=@id))
	BEGIN
		UPDATE Customer SET [Login]=@login WHERE Id=@id;
		UPDATE Customer SET [Password]=@hashed_password WHERE Id=@id;
		UPDATE Customer SET Email=@email WHERE Id=@id;
		UPDATE Customer SET [Role]=@role WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



CREATE PROCEDURE UpdateGenre @id int,@name nvarchar(30)
AS
BEGIN
	if(exists(SELECT * FROM Genre WHERE Id=@id))
	BEGIN
		UPDATE Genre SET [Name]=@name WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;




CREATE PROCEDURE UpdateMoviesGenres @id_movie int, @id_genre_old int, @id_genre_new int
AS
BEGIN
	if(exists(SELECT * FROM Movies_genres WHERE Movie_id=@id_movie AND Genre_id=@id_genre_old))
	BEGIN
		UPDATE Movies_genres SET Genre_id=@id_genre_new WHERE Movie_id=@id_movie AND Genre_id=@id_genre_old;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;


CREATE PROCEDURE UpdateCinemaHall @id int, @id_theater int, @number int, @places int, @rows int
AS
BEGIN 
	IF(EXISTS(SELECT * FROM Cinema_hall WHERE Id=@id))
	BEGIN
		UPDATE Cinema_hall SET Movie_theater_id=@id_theater WHERE Id=@id;
		UPDATE Cinema_hall SET [Number]=@number WHERE Id=@id;
		UPDATE Cinema_hall SET Count_places=@places WHERE Id=@id;
		UPDATE Cinema_hall SET [Count_rows]=@rows WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



CREATE PROCEDURE UpdateSeance @id int, @id_theater int, @id_hall int, @id_movie int, @date date, @time time
AS
BEGIN
	if(exists(SELECT * FROM Seance WHERE Id=@id))
	BEGIN
		UPDATE Seance SET Movie_theater_id=@id_theater WHERE Id=@id;
		UPDATE Seance SET Cinema_hall_id=@id_hall WHERE Id=@id;
		UPDATE Seance SET Movie_id=@id_movie WHERE Id=@id;
		UPDATE Seance SET [Date]=@date WHERE Id=@id;
		UPDATE Seance SET [Time]=@time WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



CREATE PROCEDURE UpdateTicket @id int, @id_seance int, @cost smallmoney, @row int, @place int
AS
BEGIN
	if(exists(SELECT * FROM Ticket WHERE Id=@id))
	BEGIN
		UPDATE Ticket SET Seance_id=@id_seance WHERE Id=@id;
		UPDATE Ticket SET Cost=@cost WHERE Id=@id;
		UPDATE Ticket SET [Row]=@row WHERE Id=@id;
		UPDATE Ticket SET Place=@place WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



CREATE PROCEDURE UpdateBooking @id int, @id_customer int, @id_ticket int
AS
BEGIN
	if(exists(SELECT * FROM Booking WHERE Id=@id))
	BEGIN
		UPDATE Booking SET Customer_id=@id_customer WHERE Id=@id;
		UPDATE Booking SET Ticket_id=@id_ticket WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



USE Movie_theaters;

CREATE PROCEDURE DeleteBooking @id int
AS
BEGIN
	if(exists(SELECT * FROM Booking WHERE Id=@id))
	BEGIN
		DELETE Booking WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;




CREATE PROCEDURE DeleteMoviesGenres @id_movie int, @id_genre int
AS
BEGIN
	if(exists(SELECT * FROM Movies_genres WHERE Movie_id=@id_movie AND Genre_id=@id_genre))
	BEGIN
		DELETE Movies_genres WHERE Movie_id=@id_movie AND Genre_id=@id_genre;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;


CREATE PROCEDURE DeleteMoviesGenresByMovie @id_movie int
AS
BEGIN
	if(exists(SELECT * FROM Movies_genres WHERE Movie_id=@id_movie))
	BEGIN
		DELETE Movies_genres WHERE Movie_id=@id_movie;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;


CREATE PROCEDURE DeleteMoviesGenresByGenre @id_genre int
AS
BEGIN
	if(exists(SELECT * FROM Movies_genres WHERE Genre_id=@id_genre))
	BEGIN
		DELETE Movies_genres WHERE Genre_id=@id_genre;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



CREATE PROCEDURE DeleteGenre @id int
AS
BEGIN
	if(exists(SELECT * FROM Genre WHERE Id=@id))
	BEGIN
		EXEC DeleteMoviesGenresByGenre @id;
		DELETE Genre WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



CREATE PROCEDURE DeleteBookingByTicket @id_ticket int
AS
BEGIN
	if(exists(SELECT * FROM Booking WHERE Ticket_id=@id_ticket))
	BEGIN
		DELETE Booking WHERE Ticket_id=@id_ticket;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



CREATE PROCEDURE DeleteTicket @id int
AS
BEGIN
	if(exists(SELECT * FROM Ticket WHERE Id=@id))
	BEGIN
		EXEC DeleteBookingByTicket @id;
		DELETE Ticket WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;

USE Movie_theaters;

CREATE PROCEDURE DeleteTicketBySeance @id_seance int
AS
BEGIN
	DECLARE @id_ticket int;
	if(exists(SELECT * FROM Ticket WHERE Seance_id=@id_seance))
	BEGIN
		DECLARE Tickets CURSOR LOCAL for SELECT Id FROM Ticket WHERE Seance_id=@id_seance;
		OPEN Tickets;
		FETCH Tickets INTO @id_ticket;
		WHILE @@FETCH_STATUS=0
		BEGIN
			EXEC DeleteBookingByTicket @id_ticket;
			FETCH Tickets INTO @id_ticket;
		END;
		CLOSE Tickets;
		DELETE Ticket WHERE Seance_id=@id_seance;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



CREATE PROCEDURE DeleteSeance @id int
AS
BEGIN
	if(exists(SELECT * FROM Seance WHERE Id=@id))
	BEGIN
		EXEC DeleteTicketBySeance @id;
		DELETE Seance WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;


USE Movie_theaters;

CREATE PROCEDURE DeleteBookingByCustomer @id_customer int
AS
BEGIN
	if(exists(SELECT * FROM Booking WHERE Customer_id=@id_customer))
	BEGIN
		DELETE Booking WHERE Customer_id=@id_customer;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;

CREATE PROCEDURE DeleteCustomer @id int
AS
BEGIN
	if(exists(SELECT * FROM Customer WHERE Id=@id))
	BEGIN
		EXEC DeleteBookingByCustomer @id;
		DELETE Customer WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;




create PROCEDURE DeleteSeanceByCinemaHall @id_hall int
AS
BEGIN
	DECLARE @id int;
	if(exists(SELECT * FROM Seance WHERE Cinema_hall_id=@id_hall))
	BEGIN
		declare Seances cursor local for SELECT Id FROM Seance WHERE Cinema_hall_id=@id_hall;
		open Seances;
		fetch Seances into @id;
		while @@FETCH_STATUS=0
		begin
			EXEC DeleteTicketBySeance @id;
			fetch Seances into @id;
		end;
		close Seances;
		DELETE Seance WHERE Cinema_hall_id=@id_hall;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



CREATE PROCEDURE DeleteCinemaHall @id int
AS
BEGIN
	if(EXISTS(SELECT * FROM Cinema_hall WHERE Id=@id))
	BEGIN
		EXEC DeleteSeanceByCinemaHall @id;
		DELETE Cinema_hall WHERE Id=@id;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



create PROCEDURE DeleteSeanceByMovie @id_movie int
AS
BEGIN
	DECLARE @id int;
	if(exists(SELECT * FROM Seance WHERE Movie_id=@id_movie))
	BEGIN
		declare Seances cursor local for SELECT Id FROM Seance WHERE Movie_id=@id_movie;
		open Seances;
		fetch Seances into @id;
		while @@FETCH_STATUS=0
		begin
			EXEC DeleteTicketBySeance @id;
			fetch Seances into @id;
		end;
		close Seances;
		DELETE Seance WHERE Movie_id=@id_movie;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;


create procedure DeleteMovie @id int
as
begin
	if(exists(select * from Movie where Id=@id))
	begin
		exec DeleteSeanceByMovie @id;
		exec DeleteMoviesGenresByMovie @id;
		delete Movie where Id=@id;
		return 1;
	end;
	else
	begin
		return 0;
	end;
end;


create PROCEDURE DeleteCinemaHallByTheater @id_theater int
AS
BEGIN
	declare @id int;
	if(EXISTS(SELECT * FROM Cinema_hall WHERE Movie_theater_id=@id_theater))
	BEGIN
		declare Halls cursor local for SELECT Id FROM Cinema_hall WHERE Movie_theater_id=@id_theater;
		open Halls;
		fetch Halls into @id;
		while @@FETCH_STATUS=0
		begin
			EXEC DeleteSeanceByCinemaHall @id;
			fetch Halls into @id;
		end;
		DELETE Cinema_hall WHERE Movie_theater_id=@id_theater;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;



create PROCEDURE DeleteSeanceByTheater @id_theater int
AS
BEGIN
	DECLARE @id int;
	if(exists(SELECT * FROM Seance WHERE Movie_theater_id=@id_theater))
	BEGIN
		declare Seances cursor local for SELECT Id FROM Seance WHERE Movie_theater_id=@id_theater;
		open Seances;
		fetch Seances into @id;
		while @@FETCH_STATUS=0
		begin
			EXEC DeleteTicketBySeance @id;
			fetch Seances into @id;
		end;
		close Seances;
		DELETE Seance WHERE Movie_theater_id=@id_theater;
		RETURN 1;
	END;
	ELSE
	BEGIN
		RETURN 0;
	END;
END;


create procedure DeleteMovieTheater @id int
as
begin
	if(exists(select * from Movie_theater where Id=@id))
	begin
		exec DeleteSeanceByTheater @id;
		exec DeleteCinemaHallByTheater @id;
		delete Movie_theater where Id=@id;
		return 1;
	end;
	else
	begin
		return 0;
	end;
end;

use Movie_theaters;




create procedure DeleteAllBooking
as
begin
	delete Booking;
	DBCC CHECKIDENT ('Booking', RESEED, 0);
end;
	


create procedure DeleteAllCustomers
as
begin
	exec DeleteAllBooking;
	delete Customer;
	DBCC CHECKIDENT ('Customer', RESEED, 0);
end;

create procedure DeleteAllTickets
as
begin
	exec DeleteAllBooking;
	delete Ticket;
	DBCC CHECKIDENT ('Ticket', RESEED, 0);
end;

create procedure DeleteAllMoviesGenres
as
begin
	delete Movies_genres;
	DBCC CHECKIDENT ('Movies_genres', RESEED, 0);
end;


create procedure DeleteAllGenres
as
begin
	exec DeleteAllMoviesGenres;
	delete Genre;
	DBCC CHECKIDENT ('Genre', RESEED, 0);
end;


create procedure DeleteAllSeances
as
begin
	exec DeleteAllTickets;
	delete Seance;
	DBCC CHECKIDENT ('Seance', RESEED, 0);
end;



create procedure DeleteAllMovies
as
begin
	exec DeleteAllMoviesGenres;
	exec DeleteAllSeances;
	delete Movie;
	DBCC CHECKIDENT ('Movie', RESEED, 0);
end;



create procedure DeleteAllCinemaHalls
as
begin
	exec DeleteAllSeances;
	delete Cinema_hall;
	DBCC CHECKIDENT ('Cinema_hall', RESEED, 0);
end;



create procedure DeleteAllMovieTheaters
as
begin
	exec DeleteAllCinemaHalls;
	delete Movie_theater;
	DBCC CHECKIDENT ('Movie_theater', RESEED, 0);
end;


create procedure MoviePoster @id_theater int, @date date
as
begin
	select mt.[Name] as Theater , ch.Number as [Number of hall], m.Title, s.[Date], s.[Time]
	from Seance s join Movie_theater mt on s.Movie_theater_id=mt.Id
	join Cinema_hall ch on s.Cinema_hall_id=ch.Id
	join Movie m on s.Movie_id=m.Id
	where s.Movie_theater_id=@id_theater and s.[Date]=@date order by [Time];
end;

use Movie_theaters;

create procedure GetTheaterById @id_movie int
as
begin
	if(exists(select * from Movie_theater where Id=@id_movie))
	begin
		select *  from Movie_theater where Id=@id_movie;
		return 1;
	end;
	else
	begin
		return 0;
	end;
end;

create procedure GetCustomerById @id int
as
begin
	if(exists(select * from Customer where Id=@id))
	begin
		select * from Customer where Id=@id;
		return 1;
	end;
	else
	begin
		return 0;
	end;
end;


create procedure LoginExists @login nvarchar(30)
as
begin
	if(exists(select id from Customer where [Login]=@login))
	begin 
		select * from Customer where [Login]=@login;
	end;
end;



create procedure EmailExists @email nvarchar(30)
as
begin
	if(exists(select id from Customer where Email=@email))
	begin 
		select * from Customer where Email=@email;
	end;
end;


create procedure [Authorization] @login nvarchar(30), @password nvarchar(30)
as
begin
	DECLARE @hashed_password varbinary(max);
	set @hashed_password = hashbytes('SHA2_512', CAST(@password as VARBINARY(max)));
	if(exists(select * from Customer where [Login]=@login and [Password]=@hashed_password))
	begin
		select * from Customer where [Login]=@login and [Password]=@hashed_password;
	end;
end;


create procedure GetRoleByLogin @login nvarchar(30)
as
begin
	select [Role] from Customer where [Login]=@login;
end;


create procedure GetMovieById @id int
as
begin
	select * from Movie where Id=@id;
end;


use Movie_theaters;


create procedure GetGenreById @id int
as
begin
	select * from Genre where Id=@id;
end;

create procedure ListGenreByMovieId @id_movie int
as
begin
	select g.Id, g.[Name]
	from Movies_genres mg join Genre g
	on  mg.Genre_id=g.Id
	where Movie_id=@id_movie;
end;


select  * from Genre;


select m.Title, g.[Name]
	from Movies_genres mg join Genre g 
	on mg.Genre_id=g.Id
	join Movie m
	on mg.Movie_id=m.Id



create procedure GetTheaterByCinemaHall @id_hall int
as
begin
	select mt.Id, mt.Name, mt.Address, mt.Count_cinema_hall from Movie_theater mt join Cinema_hall ch
	on mt.Id = ch.Movie_theater_id 
	where ch.Id=@id_hall;
end;


select * from Cinema_hall


create procedure GetCinemaHallById @id int
as
begin
	select * from Cinema_hall where Id=@id;
end;

select *  from Movie_theater
select * from Seance

exec SeanceProcedure '2020/12/16', 3


create procedure GetTheaterBySeance @id int
as
begin
	select mt.Id, mt.Name, mt.Address, mt.Count_cinema_hall 
	from Movie_theater mt join Seance s
	on
	mt.Id=s.Movie_theater_id
	where s.Id=@id;
end;

create procedure GetCinemaHallBySeance @id int
as
begin
	select ch.Id, ch.Movie_theater_id, ch.Number, ch.Count_places, ch.Count_rows 
	from Cinema_hall ch join Seance s
	on
	ch.Id=s.Cinema_hall_id
	where s.Id=@id;
end;

exec GetCinemaHallBySeance 39


create procedure GetMovieBySeance @id int
as
begin
	select m.Id, m.Title, m.Age_bracket, m.Duration, m.First_day_of_rental, m.Last_day_of_rental
	from Movie m join Seance s
	on
	m.Id=s.Movie_id
	where s.Id=@id;
end;


create procedure GetSeanceById @id int
as
begin
	select * from Seance where Id=@id;
end;



create procedure GetSeanceForTheater @id int
as
begin
	select * from Seance where Movie_theater_id=@id order by date,time;
end;


create procedure IsAvailableTicket @id_seance int, @row int, @place int
as
begin
	declare @id int =(select id from Ticket where Seance_id=@id_seance and [Row]=@row and Place=@place) 
	if(exists(select * from Booking where Ticket_id=@id))
	begin
		select * from Ticket where Id=@id;
	end;
end;

create procedure GetTicketForSeance @id_seance int
as
begin
	select * from Ticket where Seance_id=@id_seance and IsAvailable=0;
end;

create procedure UpdateStatusTicket @id_seance int, @row int, @place int
as
begin
	update Ticket set IsAvailable=1 where Seance_id=@id_seance and [Row]=@row and Place=@place;
end;

create procedure GetTicketById @id int
as
begin
	select * from Ticket where Id=@id;
end;

select * from Booking


create procedure GetIdByLogin @login nvarchar(30)
as
begin
	select * from Customer where [login]=@login;
end;



