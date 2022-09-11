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
    }
}
