﻿using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.BookModel;
using System.Data;
using System;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL _bookBL;

        public BookController(IBookBL bookBL)
        {
            _bookBL = bookBL;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("AddBook")]
        public IActionResult AddBook(BookPostModel bookPostModel)
        {
            try
            {
                var result = _bookBL.AddBook(bookPostModel);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = "Book Add Failed!!" });
                }

                return this.Ok(new { success = true, Message = "Book Added Sucessfullly..", data = "Book Added : " + result.BookName });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpGet("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = _bookBL.GetAllBooks();
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = "No Books Available!!" });
                }

                return this.Ok(new { success = true, Message = "Books fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllBooks/BookId")]
        public IActionResult GetAllBooks(int BookId)
        {
            try
            {
                var result = _bookBL.GetBook(BookId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = "No Books Available!!" });
                }

                return this.Ok(new { success = true, Message = "Books fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
