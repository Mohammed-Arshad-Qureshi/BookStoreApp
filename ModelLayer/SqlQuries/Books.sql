USE BookStoreDB

--CREATING THE BOOK TABLE
CREATE TABLE BooksTable(
BookId int identity(1,1) primary key,
BookName varchar(255) unique not null,
Author varchar(255) unique not null,
Description varchar(255) not null,
Quantity int not null,
Price money not null,
DiscountPrice money not null,
TotalRating float,
RatingCount int,
BookImg varchar(255)
)


SELECT * FROM BooksTable


---****************************************************************
-------------STORED PROCEDURES ----------------------------------- 


---------------------------------------------------------------
---SP for the Adding Book -------------------------------------


CREATE PROCEDURE spAddBook(
@BookName varchar(255),
@Author varchar(255),
@Description Nvarchar(255),
@Quantity int,
@Price money,
@DiscountPrice money,
@TotalRating float,
@RatingCount int,
@BookImg varchar(255)
)
As
Begin try
insert into BooksTable(BookName,Author,Description,Quantity,Price,DiscountPrice,TotalRating,RatingCount,BookImg) values(@BookName,@Author,@Description,@Quantity,@Price,@DiscountPrice,@TotalRating,@RatingCount,@BookImg)
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH


EXEC spAddBook 'Quresiton are the Answers','Robert t','Every soluton raises another question',20,250,200,4.6,200,'http://image.jpg'

---------------------------------------------------------------
--SP FOR GETIING ALL BOOKS ------------------------------------

create procedure spGetAllBooks
As
Begin try
select * from BooksTable
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH





------------------------------------------------------------
--SP FOR GETIING ALL BOOKS BY BOOKID -----------------------

create procedure spGetAllBookSById
@BookId int
As
Begin try
select * from BooksTable where BookId =@BookId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH





----------------------------------------------------------
--SP FOR UPDATE BOOK--------------------------------------

CREATE PROCEDURE spUpdateBook(
@BookId int,
@BookName varchar(255),
@Author varchar(255),
@Description Nvarchar(255),
@Quantity int,
@Price money,
@DiscountPrice money,
@TotalRating float,
@RatingCount int,
@BookImg varchar(255)
)
As
Begin try
update BooksTable set BookName=@BookName,Author=@Author,Description=@Description,Quantity=@Quantity,Price=@Price,DiscountPrice=@DiscountPrice,TotalRating=@TotalRating,RatingCount=@RatingCount,BookImg=@BookImg where BookId=@BookId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH


----------------------------------------------------------
--SP FOR DELETE BOOK--------------------------------------

CREATE PROCEDURE spDeleteBook(
@BookId int
)
As
Begin try
delete from BooksTable where BookId=@BookId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH



