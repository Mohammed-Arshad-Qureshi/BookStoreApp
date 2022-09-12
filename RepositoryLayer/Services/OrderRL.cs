using Microsoft.Extensions.Configuration;
using ModelLayer.OrderModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        private readonly string connectionString;
        public OrderRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore");
        }

        public bool AddOrder(OrderPostModel orderModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spAddOrder", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", orderModel.CartId);
                    cmd.Parameters.AddWithValue("@AddressId", orderModel.AddressId);
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
