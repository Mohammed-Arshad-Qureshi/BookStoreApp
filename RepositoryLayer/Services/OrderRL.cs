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

        public List<OrderResponseModel> GetAllOrders(int UserId)
        {
            List<OrderResponseModel> OrdersList = new List<OrderResponseModel>();
            SqlConnection Connection = new SqlConnection(connectionString);
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("spGetAllOrders", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            OrderResponseModel order = new OrderResponseModel();
                            order.OrderId = reader["OrderId"] == DBNull.Value ? default : reader.GetInt32("OrderId");
                            order.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                            order.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                            order.AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId");
                            order.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                            order.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                            order.OrderQuantity = reader["OrderQuantity"] == DBNull.Value ? default : reader.GetInt32("OrderQuantity");
                            order.TotalOrderPrice = reader["TotalOrderPrice"] == DBNull.Value ? default : reader.GetDecimal("TotalOrderPrice");
                            order.OrderDate = reader["OrderDate"] == DBNull.Value ? default : reader.GetDateTime("OrderDate");
                            order.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                            OrdersList.Add(order);
                        }

                        return OrdersList;
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
