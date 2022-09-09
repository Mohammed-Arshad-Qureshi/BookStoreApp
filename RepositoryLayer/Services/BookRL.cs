using Microsoft.Extensions.Configuration;
using ModelLayer.BookModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

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

        public List<BookResponseModel> GetAllBooks()
        {
            List<BookResponseModel> Books = new List<BookResponseModel>();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spGetAllBooks", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BookResponseModel book = new BookResponseModel();
                            book.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                            book.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                            book.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                            book.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                            book.Quantity = reader["Quantity"] == DBNull.Value ? default : reader.GetInt32("Quantity");
                            book.Price = reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price");
                            book.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountPrice");
                            book.TotalRating = reader["TotalRating"] == DBNull.Value ? default : reader.GetDouble("TotalRating");
                            book.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                            book.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                            Books.Add(book);
                        }
                    }
                    return Books;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookResponseModel GetBook(int BookId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            BookResponseModel book = new BookResponseModel();

            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spGetAllBookSById", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            book.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                            book.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                            book.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                            book.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                            book.Quantity = reader["Quantity"] == DBNull.Value ? default : reader.GetInt32("Quantity");
                            book.Price = reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price");
                            book.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountPrice");
                            book.TotalRating = reader["TotalRating"] == DBNull.Value ? default : reader.GetDouble("TotalRating");
                            book.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                            book.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                        }
                        return book;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdateBook(int BookId, BookPostModel bookModel)
        {
            SqlConnection Connection = new SqlConnection(connectionString);
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("spUpdateBook", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
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
                        return result.ToString();
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
