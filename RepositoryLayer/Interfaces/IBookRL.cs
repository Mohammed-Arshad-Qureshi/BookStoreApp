using ModelLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        public BookPostModel AddBook(BookPostModel bookModel);
        public List<BookResponseModel> GetAllBooks();
        public BookResponseModel GetBook(int BookId);
        public string UpdateBook(int BookId, BookPostModel bookModel);
    }
}
