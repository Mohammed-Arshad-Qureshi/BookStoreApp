--Using database
use BookStoreDB


---Creating Admin table
create table AdminTable(
AdminId int identity(1,1) primary key,
AdminName varchar(255) not null,
AdminEmail varchar(255) unique not null,
AdminPassword varchar(255) not null,
AdminAddress varchar(255) not null,
AdminMobile bigint unique not null
)


--Displaying all the records in the table
SELECT * FROM AdminTable



----=========================================================
----================SP CALLS ================================


--SP Call For Adding Admin
CREATE PROCEDURE spAddingAdmin(
@Name varchar(255),
@Email varchar(255),
@Password varchar(255),
@Address varchar(255),
@Mobile bigint
)
As
Begin try
insert into AdminTable(AdminName,AdminEmail,AdminPassword,AdminAddress,AdminMobile)values(@Name,@Email,@Password,@Address,@Mobile);
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

--executeing the above sp
execute spAddingAdmin 'Rehan Qureshi','rehan@gmail.com','Rehan@123','Aaron Larson 123 center ln.XYZ Area,55441,Unitited States',8788998897



--SP Call For Admin Login
CREATE PROCEDURE spAdminLogin(
@AdminEmail varchar(255),
@AdminPassword varchar(255)
)
As
Begin try
select * from AdminTable where AdminEmail=@AdminEmail and AdminPassword=@AdminPassword
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

