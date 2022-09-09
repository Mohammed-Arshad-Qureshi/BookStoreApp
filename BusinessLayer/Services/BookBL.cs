﻿using BusinessLayer.Interfaces;
using ModelLayer.BookModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL _bookRL;
        public BookBL(IBookRL bookRL)
        {
            _bookRL = bookRL;
        }

        public BookPostModel AddBook(BookPostModel bookModel)
        {
            try
            {
                return this._bookRL.AddBook(bookModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<BookResponseModel> GetAllBooks()
        {
            try
            {
                return this._bookRL.GetAllBooks();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
