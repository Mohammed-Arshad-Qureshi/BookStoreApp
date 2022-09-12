using Microsoft.Extensions.Configuration;
using ModelLayer.AddressModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {
        private readonly string connectionString;
        public AddressRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore");
        }

        public bool AddAddress(int UserId, AddressPostModel postModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlconnection)
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand("spAddAddress", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressType", postModel.AddressType);
                    cmd.Parameters.AddWithValue("@FullAddress", postModel.FullAddress);
                    cmd.Parameters.AddWithValue("@City", postModel.City);
                    cmd.Parameters.AddWithValue("@State", postModel.State);

                    int result = cmd.ExecuteNonQuery();

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

        public List<AddressResponseModel> GetAllAddresses(int UserId)
        {
            List<AddressResponseModel> Addresses = new List<AddressResponseModel>();
            SqlConnection Connection = new SqlConnection(connectionString);
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("spForGetAllAddress", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AddressResponseModel addressdetails = new AddressResponseModel();
                            addressdetails.AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId");
                            addressdetails.UserId = UserId;
                            addressdetails.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                            string phone = reader["Phone"] == DBNull.Value ? default : reader.GetString("Phone");
                            addressdetails.MobileNo = Convert.ToInt64(phone);
                            addressdetails.AddressType = reader["AddressType"] == DBNull.Value ? default : reader.GetInt32("AddressType");
                            addressdetails.FullAddress = reader["FullAddress"] == DBNull.Value ? default : reader.GetString("FullAddress");
                            addressdetails.City = reader["City"] == DBNull.Value ? default : reader.GetString("City");
                            addressdetails.State = reader["State"] == DBNull.Value ? default : reader.GetString("State");
                            Addresses.Add(addressdetails);
                        }

                        return Addresses;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateAddressbyId(int UserId, AddressUpdateModel postModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                {
                    sqlconnection.Open();

                    SqlCommand cmd = new SqlCommand("SpUpdateAddress", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressId", postModel.AddressId);
                    cmd.Parameters.AddWithValue("@AddressType", postModel.AddressType);
                    cmd.Parameters.AddWithValue("@FullAddress", postModel.FullAddress);
                    cmd.Parameters.AddWithValue("@City", postModel.City);
                    cmd.Parameters.AddWithValue("@State", postModel.State);

                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAddressByAddressId(int AddressId, int UserId)
        {
            SqlConnection Connection = new SqlConnection(connectionString);
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("spForDeleteAddressById", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId ", AddressId);
                    cmd.Parameters.AddWithValue("@UserId ", UserId);
                    var result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddressResponseModel GetAllAddressById(int UserId, int AddressId)
        {
            SqlConnection Connection = new SqlConnection(connectionString);
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("spForGetAddressById", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressId", AddressId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    AddressResponseModel addressdetails = new AddressResponseModel();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            addressdetails.AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId");
                            addressdetails.UserId = UserId;
                            addressdetails.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                            string phone = reader["Phone"] == DBNull.Value ? default : reader.GetString("Phone");
                            addressdetails.MobileNo = Convert.ToInt64(phone);
                            addressdetails.AddressType = reader["AddressType"] == DBNull.Value ? default : reader.GetInt32("AddressType");
                            addressdetails.FullAddress = reader["FullAddress"] == DBNull.Value ? default : reader.GetString("FullAddress");
                            addressdetails.City = reader["City"] == DBNull.Value ? default : reader.GetString("City");
                            addressdetails.State = reader["State"] == DBNull.Value ? default : reader.GetString("State");
                        }
                        return addressdetails;
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

