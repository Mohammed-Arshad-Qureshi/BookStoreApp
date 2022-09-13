Use BookStoreDB

CREATE TABLE WishListTable(
WishListId int primary key identity,
UserId int  not null FOREIGN KEY (UserId) REFERENCES Users(Id),
BookId int  not null FOREIGN KEY (BookId) REFERENCES BooksTable(BookId),
)




---****************************************************************
-------------STORED PROCEDURES ------------------------------------




-----------------------------------------------------------------
--------------SP FOR ADD WISHLIST ---------------------------

CREATE PROCEDURE spAddBookToWishList(
@UserId int,
@BookId int
)
As
Begin try
DECLARE @Wcount int,@Ccount int;
SET @Ccount=(select count(CartId) from CartTable where UserId IN (@UserId) AND BookId IN (@BookId))
SET @Wcount=(select count(WishListId) from WishListTable where UserId IN (@UserId) AND BookId IN (@BookId))
IF(@Ccount = 0)
  IF(@Wcount = 0)
   insert into WishListTable(UserId,BookId) values(@UserId,@BookId)
  ELSE
   print'Check if Book is available or Its already in WishList!!'
ElSE
  print'Your Book is already in cart!!'
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH




-----------------------------------------------------------------
--------------SP FOR GET ALL WISHLIST ---------------------------

create procedure spGetAllWishList(
@UserId int
)
As
Begin try
select 
w.WishListId,b.BookId,b.BookName,b.Author,b.Description,b.Price,b.DiscountPrice,b.BookImg
from WishListTable w INNER JOIN BooksTable b ON w.BookId = b.BookId where UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH





-----------------------------------------------------------------
--------------SP FOR DELETE WISHLIST ---------------------------

create procedure spDeleteBookFromWishList(
@WishListId int,
@UserId int
)
As
Begin try
delete from WishListTable where WishListId = @WishListId AND UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH


select * from WishListTable