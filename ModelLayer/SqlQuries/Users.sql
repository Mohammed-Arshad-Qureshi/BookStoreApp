--created database
Create Database BookStoreDB
Use BookStoreDB


-- creating users table
Create Table Users
(
	Id int identity(1,1) primary key,
	FullName varchar(255) Not Null,
	Email varchar(255) Not Null unique,
	Password Varchar(50) Not Null,
	Phone varchar(20) Not Null,
	CreatedAt Datetime,
	ModifiedAt Datetime
)

--display users table
select * from Users


------------------------------------------------------------------------
------------------sp calls----------------------------------------------


--Sp call for Registration
CREATE PROCEDURE spAddUser(
@FullName varchar(255), 
@Email varchar(255),
@Password varchar(50),
@Phone varchar(20)
)
As
Begin try
insert into Users(FullName,Email,Password,Phone,CreatedAt,ModifiedAt) values(@FullName,@Email,@Password,@Phone,GetDate(),GetDate())
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH

Execute spAddUser 'Arshad Qureshi','arshad@gmail.com','Arshad@123','8758965847'


--sp call for login
CREATE PROCEDURE spLoginUser(
@Email varchar(255),
@Password varchar(50)
)
As
Begin try
select * from Users where Email=@Email and password = @Password
end try
Begin catch
SELECT 
	ERROR_NUMBER() AS ErrorNumber,
	ERROR_STATE() AS ErrorState,
	ERROR_PROCEDURE() AS ErrorProcedure,
	ERROR_LINE() AS ErrorLine,
	ERROR_MESSAGE() AS ErrorMessage;
END CATCH






