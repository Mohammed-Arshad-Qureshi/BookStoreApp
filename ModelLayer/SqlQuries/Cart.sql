Use BookStoreDB

CREATE TABLE CartTable(
CartId int identity(1,1) primary key,
UserId int  not null FOREIGN KEY (UserId) REFERENCES Users(Id),
BookId int  not null FOREIGN KEY (BookId) REFERENCES BooksTable(BookId),
BookQuantity int not null
)

select * from CartTable

SELECT * FROM USERS
SELECT * FROM BooksTable
SELECT * FROM CartTable



---****************************************************************
-------------STORED PROCEDURES ----------------------------------- 




--stored procedure for AddBook TO Cart
CREATE PROCEDURE spAddBookToCart(
@UserId int,
@BookId int,
@BookQuantity int
)
As
Begin try
DECLARE @count int;
SET @count=(select count(CartId) from CartTable where UserId IN (@UserId) AND BookId IN (@BookId))
IF(@count = 0)
insert into CartTable(UserId,BookId,BookQuantity) values(@UserId,@BookId,@BookQuantity)
ELSE
print'Check if Book is available or Its already in Cart!!'
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

 EXEC spAddBookToCart 1,2,10





 --------------------------------------------------------
 --stored procedure for GetALlBookInCart-----------------

CREATE PROCEDURE spGetAllBooksInCart(
@UserId int
)
As
Begin try
select 
c.CartId,b.BookId,b.BookName,b.Author,b.Description,c.BookQuantity,b.Price,b.DiscountPrice,b.BookImg
from CartTable c INNER JOIN BooksTable b ON c.BookId = b.BookId where UserId = @UserId
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
--stored procedure for UpdateCartbyCartId-----------------

CREATE PROCEDURE spUdateCartItems(
@UserId int,
@CartId int,
@BookId int,
@BookQuantity int
)
As
Begin try
update CartTable set BookQuantity=@BookQuantity where CartId = @CartId and UserId = @UserId and BookId = @BookId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH




-------------------------------------------------------
--stored procedure for deleteCartItem -----------------

CREATE PROCEDURE spDeleteCartItems(
@CartId int,
@UserId int
)
As
Begin try
delete from CartTable where CartId=@CartId AND UserId=@UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH





---------------------------------------------------------
--stored procedure for GetCartItemByCartId --------------

CREATE PROCEDURE spGetCartItemByCartId(
@UserId int,
@CartId int
)
As
Begin try
select 
c.CartId,b.BookId,b.BookName,b.Author,b.Description,c.BookQuantity,b.Price,b.DiscountPrice,b.BookImg
from CartTable c INNER JOIN BooksTable b ON c.BookId = b.BookId where UserId = @UserId and CartId = @CartId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

