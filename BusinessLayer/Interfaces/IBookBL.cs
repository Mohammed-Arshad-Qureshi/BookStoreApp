﻿using ModelLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBookBL
    {
        public BookPostModel AddBook(BookPostModel bookPostModel);
    }
}