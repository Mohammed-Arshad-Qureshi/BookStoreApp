using Microsoft.Extensions.Configuration;
using ModelLayer.BookModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private readonly string connectionString;
        public BookRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore");
        }

        public BookPostModel AddBook(BookPostModel bookModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spAddBook", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookName ", bookModel.BookName);
                    cmd.Parameters.AddWithValue("@Author", bookModel.Author);
                    cmd.Parameters.AddWithValue("@Description ", bookModel.Description);
                    cmd.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    cmd.Parameters.AddWithValue("@Price", bookModel.Price);
                    cmd.Parameters.AddWithValue("@DiscountPrice ", bookModel.DiscountPrice);
                    cmd.Parameters.AddWithValue("@TotalRating ", bookModel.TotalRating);
                    cmd.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                    cmd.Parameters.AddWithValue("@BookImg", bookModel.BookImg);
                    var result = cmd.ExecuteNonQuery();
                    if (result >= 0)
                    {
                        return bookModel;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
