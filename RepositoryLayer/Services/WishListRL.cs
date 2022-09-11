using Microsoft.Extensions.Configuration;
using ModelLayer.WishListModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishListRL:IWishListRL
    {
        private readonly string connectionString;
        public WishListRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore");
        }

        public bool AddToWishList(int UserId, int BookId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("spAddBookToWishList", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@UserId",UserId );
                    com.Parameters.AddWithValue("@BookId", BookId);
                    var result = com.ExecuteNonQuery();
                    if (result > 0)
                        return true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WishListResponseModel> GetAllWishList(int UserId)
        {
            List<WishListResponseModel> Books = new List<WishListResponseModel>();
            SqlConnection Connection = new SqlConnection(connectionString);
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("spGetAllWishList", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WishListResponseModel book = new WishListResponseModel();
                            book.WishListId = reader["WishListId"] == DBNull.Value ? default : reader.GetInt32("WishListId");
                            book.UserId = UserId;
                            book.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                            book.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                            book.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                            book.Price = reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price");
                            book.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountPrice");
                            book.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                            Books.Add(book);
                        }
                        return Books;
                    }
                    return null;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
