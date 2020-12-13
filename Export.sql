use Movie_theaters;


--drop procedure ExportToXML
create procedure ExportToXML
as
begin
	
	--select
	--(select Id, RTRIM([Name])as [Name], RTRIM([Address])as [Address], Count_cinema_hall from Movie_theater for xml path('Movie_theater'),type) as Movie_theaters,
	--(select Id, RTRIM([Login])as [Login], [Password], RTRIM(Email)as Email, RTRIM([Role])as [Role] from Customer for xml path('Customer'), type) as Customers,
	--(select Id, RTRIM(Title)as Title, RTRIM(Age_bracket)as Age_bracket, Duration, First_day_of_rental, Last_day_of_rental from Movie for xml path('Movie'), type) as Movies,
	--(select Id, RTRIM([Name])as [Name] from Genre for xml path('Genre'), type) as Genres,
	--(select Movie_id, Genre_id from Movies_genres for xml path('Movie_genre'), type)as Movies_genres,
	--(select Id, Movie_theater_id, [Number], Count_places, [Count_rows] from Cinema_hall for xml path('Cinema_hall'), type)as Cinema_halls,
	--(select Id, Movie_theater_id, Cinema_hall_id, Movie_id, [Date], [Time] from Seance for xml path('Seance'), type)as Seances,
	--(select Id, Seance_id, Cost, [Row], Place from Ticket for xml path('Ticket'), type)as Tickets,
	--(select Id, Customer_id, Ticket_id from Booking for xml path('Booking'), type)as Booking
	--for xml path('Theaters'), type

	--to use xp_cmdshell
	EXEC master.dbo.sp_configure 'show advanced options', 1
		RECONFIGURE WITH OVERRIDE;
	EXEC master.dbo.sp_configure 'xp_cmdshell', 1
		RECONFIGURE WITH OVERRIDE;


	-- Save XML records to a file
	declare @fileName nvarchar(500);
	declare @sep char(1)=',';
	declare @presqlStr varchar(50)='use Movie_theaters; select ';
	declare @postsqlStr varchar(100)=' for xml path(''''), type, root(''Theaters'')';
	declare @theaterQ varchar(250)='(select Id, RTRIM([Name])as [Name], RTRIM([Address])as [Address], Count_cinema_hall from Movie_theater for xml path(''Movie_theater''),type) as Movie_theaters ';
	declare @customerQ varchar(250)='(select Id, RTRIM([Login])as [Login], [Password], RTRIM(Email)as Email, RTRIM([Role])as [Role] from Customer for xml path(''Customer''), type) as Customers ';
	declare @movieQ varchar(250)='(select Id, RTRIM(Title)as Title, RTRIM(Age_bracket)as Age_bracket, Duration, First_day_of_rental, Last_day_of_rental from Movie for xml path(''Movie''), type) as Movies ';
	declare @genreQ varchar(250)='(select Id, RTRIM([Name])as [Name] from Genre for xml path(''Genre''), type) as Genres ';
	declare @movieGenreQ varchar(250)='(select Movie_id, Genre_id from Movies_genres for xml path(''Movie_genre''), type)as Movies_genres ';
	declare @hallQ varchar(250)='(select Id, Movie_theater_id, [Number], Count_places, [Count_rows] from Cinema_hall for xml path(''Cinema_hall''), type)as Cinema_halls ';
	declare @seanceQ varchar(250)='(select Id, Movie_theater_id, Cinema_hall_id, Movie_id, [Date], [Time] from Seance for xml path(''Seance''), type)as Seances ';
	declare @ticketQ varchar(250)='(select Id, Seance_id, Cost, [Row], Place from Ticket for xml path(''Ticket''), type)as Tickets ';
	declare @bookingQ varchar(250)='(select Id, Customer_id, Ticket_id from Booking for xml path(''Booking''), type)as Booking ';


	declare @sqlStr varchar(3500)=@presqlStr+
								  @theaterQ+@sep+
								  @customerQ+@sep+
								  @movieQ+@sep+
								  @genreQ+@sep+
								  @movieGenreQ+@sep+
								  @hallQ+@sep+
								  @seanceQ+@sep+
								  @ticketQ+@sep+
								  @bookingQ+
								  @postsqlStr;

	declare @sqlCmd varchar(4000);

	set @fileName='C:\temp.xml';
	SET @sqlCmd = 'bcp "' + @sqlStr + '" queryout ' + @fileName + ' -w -T';
	EXEC xp_cmdshell @sqlCmd;
end;


exec ExportToXML;