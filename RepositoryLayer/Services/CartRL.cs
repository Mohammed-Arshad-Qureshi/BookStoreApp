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
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
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
    }
}
