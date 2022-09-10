using Microsoft.Extensions.Configuration;
using ModelLayer.CartModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL : ICartRL
    {
        private readonly string connectionString;
        public CartRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore");
        }
        public bool AddBookTOCart(int UserId, CartPostModel postModel)
        {
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            try
            {
                using (sqlconnection)
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand("spAddBookToCart", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BookId", postModel.BookId);
                    cmd.Parameters.AddWithValue("@BookQuantity", postModel.BookQuantity);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CartResponseModel> GetAllBooksInCart(int UserId)
        {
            List<CartResponseModel> Books = new List<CartResponseModel>();
            SqlConnection Connection = new SqlConnection(connectionString);
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("spGetAllBooksInCart", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CartResponseModel book = new CartResponseModel();
                            book.CartId = reader["CartId"] == DBNull.Value ? default : reader.GetInt32("CartId");
                            book.UserId = UserId;
                            book.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                            book.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                            book.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                            book.BookQuantity = reader["BookQuantity"] == DBNull.Value ? default : reader.GetInt32("BookQuantity");
                            book.Price = reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price");
                            book.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountPrice");
                            book.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                            Books.Add(book);
                        }
                        return Books;
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
