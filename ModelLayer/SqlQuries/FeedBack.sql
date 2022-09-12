Use BookStoreDB

CREATE TABLE FeedbackTable(
FeedbackId int identity(1,1) primary key,
Rating float not null,
Comment varchar(max) not null,
BookId int not null foreign key (BookId) references BooksTable(BookId),
UserId int not null foreign key (UserId) references Users(Id)
)

-------------------------------------------------
------------------------------------------------
-----------STORED PROCEDURE ---------------------
------------------------------------------------
------------------------------------------------


-----------------------------------------
--------SP FOR ADD FEEDBACK -------------

CREATE PROCEDURE spAddFeedBack(
@Rating float,
@Comment varchar(max),
@BookId int,
@UserId int
)
as
DECLARE @TotalRating float;
BEGIN TRY
	if(not exists(select * from FeedbackTable where BookId=@BookId and UserId=@UserId))
	BEGIN
		insert into FeedbackTable(Rating,Comment,BookId,UserId) values(@Rating,@Comment,@BookId,@UserId)
		set @TotalRating = (select AVG(TotalRating) from BooksTable where BookId = @BookId);
		Update BooksTable set TotalRating = @TotalRating, RatingCount = (RatingCount+1) where BookId = @BookId;
	END
END TRY
BEGIN CATCH
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

Exec spAddFeedBack 4.5,'Book is good for reading',1,1
select * from FeedbackTable
select * from BooksTable



-------------------------------------------------
------------SP FOR GET FEEDBACK BY ID -----------


CREATE PROCEDURE spAddFeedBackById(
@BookId int
)
As
BEGIN TRY
	select f.FeedbackId,f.Comment,f.BookId,f.Rating,f.UserId,u.FullName
	from FeedbackTable f inner join Users u on f.UserId = u.Id where BookId = @BookId
END TRY
BEGIN CATCH
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH




-------------------------------------------------
------------SP FOR DELETE FEEDBACK BY ID -----------

CREATE PROCEDURE spDeleteFeedBackById(
@FeedbackId int
)
As
Begin try
delete from FeedbackTable where FeedbackId = @FeedbackId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH