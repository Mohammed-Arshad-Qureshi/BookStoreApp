using ModelLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBookBL
    {
        public BookPostModel AddBook(BookPostModel bookPostModel);
        public List<BookResponseModel> GetAllBooks();
        public BookResponseModel GetBook(int BookId);
        public string UpdateBook(int BookId, BookPostModel bookModel);
        public bool DeleteBook(int BookId);

    }
}
