use Movie_theaters;

delete Movie_theater;
DBCC CHECKIDENT ('Movie_theater', RESEED, 0);

delete Customer;
DBCC CHECKIDENT ('Customer', RESEED, 0);

delete Movie;
DBCC CHECKIDENT ('Movie', RESEED, 0);

delete Genre;
DBCC CHECKIDENT ('Genre', RESEED, 0);

delete Movies_genres;
DBCC CHECKIDENT ('Movies_genres', RESEED, 0);

delete Cinema_hall;
DBCC CHECKIDENT ('Cinema_hall', RESEED, 0);

delete Seance;
DBCC CHECKIDENT ('Seance', RESEED, 0);

delete Ticket;
DBCC CHECKIDENT ('Ticket', RESEED, 0);

delete Booking;
DBCC CHECKIDENT ('Booking', RESEED, 0);

insert into Genre([name]) values('�����'),
								('��������������'),
								('������'),
								('�������'),
								('�������'),
								('��������'),
								('��������������'),
								('�����'),
								('������������'),
								('����������'),
								('�������'),
								('����������������'),
								('��������'),
								('���������'),
								('�������'),
								('����������'),
								('������'),
								('�������'),
								('�����������'),
								('��������'),
								('�������'),
								('�����'),
								('����������'),
								('�������');

select * from genre;

insert into Movie_theater([name],[Address],Count_cinema_hall) values('������', '�. �����, ��. ����������, 23', 3),
																	('��������','�. �����, ��. ����������� �������, 28', 5),
																	('��� ����','�. �����, ��. ���������, 18', 1),
																	('���','�. �����, ��. �������, 4�', 2),
																	('������','�. �����, ��-� �����������, 13', 1),
																	('�������','�. �����, ��-� �������������, 73', 4),
																	('������','�. �����, ��. ��������, 20', 2),
																	('������','�. �����, ��. �����������������, 20', 2),
																	('�����������','�. �����, ��-� �������������, 13', 1)

use Movie_theaters
select * from Movie_theater
insert into Cinema_hall(Movie_theater_id, [number], Count_places, [Count_rows]) values(1, 1, 195, 13),
																					  (1, 2, 240, 15),
																					  (1, 3, 210, 14),
																					  (2, 1, 255, 15),
																					  (2, 2, 272, 16),
																					  (2, 3, 240, 15),
																					  (2, 4, 240, 15),
																					  (2, 5, 256, 16),
																					  (3, 1, 324, 18),
																					  (4, 1, 288, 16),
																					  (4, 2, 306, 17),
																					  (5, 1, 324, 18),
																					  (6, 1, 210, 14),
																					  (6, 2, 225, 15),
																					  (6, 3, 240, 15),
																					  (6, 4, 256, 16),
																					  (7, 1, 272, 17),
																					  (7, 2, 289, 17),
																					  (8, 1, 306, 18),
																					  (8, 2, 289, 17),
																					  (9, 1, 306, 17)



select * from Cinema;

select * from Movie;

insert into Movie(Title, Age_bracket, Duration, First_day_of_rental, Last_day_of_rental) values('���', '16+', 97, '2020-11-1', '2020-11-29')


insert into Movie(Title, Age_bracket, Duration, First_day_of_rental, Last_day_of_rental) values('������', '12+', 115, '2020-10-29', '2020-11-25'),
																							   ('�������� ��������', '6+', 80, '2020-11-12', '2020-11-25'),
																							   ('���', '16+', 98, '2020-11-5', '2020-12-1'),
																							   ('����������� �������', '12+', 97, '2020-10-22', '2020-11-29'),
																							   ('��������', '18+', 90, '2020-11-20', '2020-11-25'),
																							   ('������� ����', '16+', 90, '2020-11-15', '2020-11-25'),
																							   ('��� �� �����', '16+', 117, '2020-11-6', '2020-11-25')




exec Movie_Genre_Proc 1, '�������'
exec Movie_Genre_Proc 2, '�������'
exec Movie_Genre_Proc 2, '�����������'
exec Movie_Genre_Proc 2, '�������'
exec Movie_Genre_Proc 2, '��������'
exec Movie_Genre_Proc 2, '��������'
exec Movie_Genre_Proc 3, '��������'
exec Movie_Genre_Proc 3, '����������'
exec Movie_Genre_Proc 4, '�����'
exec Movie_Genre_Proc 4, '��������������'
exec Movie_Genre_Proc 5, '�������'
exec Movie_Genre_Proc 6, '�������'
exec Movie_Genre_Proc 6, '�����'
exec Movie_Genre_Proc 6, '��������'
exec Movie_Genre_Proc 7, '�����'
exec Movie_Genre_Proc 8, '�������'
exec Movie_Genre_Proc 8, '�����'





exec SeanceProcedure '2020/11/30'


exec AddAdmin

EXEC AddUser 'julia', '12345', 'y.nevar2001@gmail.com'


EXEC AddBooking 2, 1

SELECT * FROM Booking
