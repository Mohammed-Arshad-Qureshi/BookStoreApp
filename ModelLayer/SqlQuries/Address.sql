Use BookStoreDB


--Created the Address Table
CREATE Table AddressTable(
AddressId int identity(1,1) primary key,
UserId int  not null FOREIGN KEY (UserId) REFERENCES Users(Id),
AddressType int not null FOREIGN KEY (AddressType) REFERENCES AddressTypeTable(AddressTypeId),
FullAddress varchar(255) not null,
City varchar(255) not null,
State varchar(255) not null,
)


--CREATED ThE AddressTypeTable
CREATE TABLE AddressTypeTable(
AddressTypeId int identity(1,1) primary key,
AddressType varchar(20) not null
)


--Insert data in Addresstypetable
Insert INTO AddressTypeTable values ('Home'),('Office'),('Other')

--Displaying the table
select * from AddressTypeTable
select * from AddressTable


-------------------------------------------------------------
-------------------------------------------------------------
--------------Stored Procedures -----------------------------
--------------------------------------------------------------






--------------------------------------------------------------
------------SP Call for Add Address --------------------------

--stored procedure for AddAddress
CREATE PROCEDURE spAddAddress(
@UserId int,
@AddressType int, 
@FullAddress varchar(255),
@City varchar(255),
@State varchar(255)
)
As
Begin try
   insert into AddressTable(UserId,AddressType,FullAddress,City,State) values(@UserId,@AddressType,@FullAddress,@City,@State)
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

Exec spAddAddress 1,1,'Gandhi pur , Srinivas colony','Vizag','Andhra Pradesh'
Select * from AddressTable






--------------------------------------------------------------
------------SP Call for Get ALL Address ----------------------


CREATE PROCEDURE spForGetAllAddress(
@UserId int
)
As
Begin try
select 
a.AddressId,a.AddressType,a.FullAddress,a.City,a.State,u.Id,u.FullName,u.Phone
from AddressTable a INNER JOIN Users u ON a.UserId = u.Id
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

EXEC spForGetAllAddress 1




--------------------------------------------------------------
------------SP Call for Delete Address -----------------------

CREATE PROCEDURE spForDeleteAddressById(
@AddressId int,
@UserId int
)
As
Begin try
delete from AddressTable where AddressId = @AddressId AND UserId = @UserId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH




----------------------------------------------------------------
------------SP Call for Update Address --------------------------
CREATE PROCEDURE SpUpdateAddress(
@AddressId int,
@UserId int,
@AddressType int, 
@FullAddress varchar(255),
@City varchar(255),
@State varchar(255)
)
As
Begin try
update AddressTable set AddressType=@AddressType,FullAddress=@FullAddress,City=@City,State=@State where UserId=@UserId
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
------------SP Call for GE Address By Id--------------------------


CREATE PROCEDURE spForGetAddressById(
@AddressId int,
@UserId int
)
As
Begin try
select 
a.AddressId,a.AddressType,a.FullAddress,a.City,a.State,u.Id,u.FullName,u.Phone
from AddressTable a INNER JOIN Users u ON a.UserId = u.Id where AddressId=@AddressId
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH
