Use BookStoreDB

CREATE TABLE OrderTable(
OrderId int identity(1,1) primary key,
UserId int  not null FOREIGN KEY (UserId) REFERENCES Users(Id),
BookId int  not null FOREIGN KEY (BookId) REFERENCES BooksTable(BookId),
)