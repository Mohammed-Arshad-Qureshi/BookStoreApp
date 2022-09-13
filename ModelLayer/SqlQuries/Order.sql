Use BookStoreDB

--CREATE TABLE ORDER
CREATE TABLE OrdersTable(
OrderId int identity(1,1) primary key,
UserId int not null FOREIGN KEY (UserId) REFERENCES Users(Id),
BookId int not null FOREIGN KEY (BookId) REFERENCES BooksTable(BookId),
AddressId int not null FOREIGN KEY (AddressId) REFERENCES AddressTable(AddressId),
OrderQuantity int not null,
TotalOrderPrice money not null,
OrderDate datetime Default getdate()
)





 -------------------------------------------------------------
-------------------------------------------------------------
--------------STORED PROCEDURE -----------------------------
--------------------------------------------------------------



--------------------------------------------------------------
------------SP Call for Add Address --------------------------

CREATE PROCEDURE spAddOrder(
@CartId int,
@AddressId int
)
As
Begin try
Select c.CartId,c.UserId,c.BookId,b.Quantity,b.DiscountPrice,c.BookQuantity into tempdetails from CartTable c Inner join BooksTable b on c.BookId = b.BookId where CartId = @CartId
Declare @UserId int = (select UserId from tempdetails)
Declare @BookId int = (select BookId from tempdetails)
DECLARE @BookPrice float =(select DiscountPrice from tempdetails)
DECLARE @QuantityAvail int =(select Quantity from tempdetails)
Declare @OrderQuantity int = (select BookQuantity from tempdetails)

IF ((@UserId != 0) AND (@QuantityAvail > @OrderQuantity))
BEGIN
    Declare @TotalOrderPrice money = (@BookPrice * @OrderQuantity)
	DECLARE @BookPresentInWishList int =(select Count(WishListId) from WishListTable where BookId= @BookId)	
	IF(@BookPresentInWishList>0)
	BEGIN
	 delete from wishListTable where BookId = @BookId and UserId=@UserId
    END
	 insert into OrdersTable(UserId,BookId,AddressId,OrderQuantity,TotalOrderPrice) values(@UserId,@BookId,1,@OrderQuantity,@TotalOrderPrice)
	 IF(@@ROWCOUNT > 0)
	 BEGIN
	   delete from CartTable where CartId = @CartId
	   DECLARE @Booksleft int = @QuantityAvail - @OrderQuantity
	   update BooksTable SET Quantity = @Booksleft where BookId = @BookId
	 END
	 drop table tempdetails
END
ELSE
	drop table tempdetails
    print 'Books available in stock are only'+CAST(@BookId AS VARCHAR(255))
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

--------------------------------------------------------------
------------SP Call for get Orders-------------------------

CREATE PROCEDURE spGetAllOrders(
@UserId int
)
As
Begin try
select 
o.OrderId,o.UserId,o.BookId,o.AddressId,o.OrderQuantity,o.TotalOrderPrice,o.OrderDate,b.BookName,b.Author,b.BookImg
from OrdersTable o INNER JOIN BooksTable b ON o.BookId = b.BookId where UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH


---------------------------------------
spGetAllOrders 1
select * from Carttable


--------------------------------------------------------------
------------SP Call for get Orders----------------------------

CREATE PROCEDURE spDeleteOrder(
@UserId int,
@OrderId int
)
As
Begin try
delete from OrdersTable where OrderId=@OrderId and UserId =@UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

Exec spDeleteOrder 3,2



select * from OrdersTable