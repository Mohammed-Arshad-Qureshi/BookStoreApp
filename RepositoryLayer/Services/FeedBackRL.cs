using Microsoft.Extensions.Configuration;
using ModelLayer.FeedBack;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;

namespace RepositoryLayer.Services
{
    public class FeedBackRL : IFeedBackRL
    {
        private readonly string connectionString;
        public FeedBackRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore");
        }

        public bool AddFeedback(int UserId, FeedBackPostModel feedbackModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spAddFeedBack", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BookId", feedbackModel.BookId);
                    cmd.Parameters.AddWithValue("@Rating", feedbackModel.Rating);
                    cmd.Parameters.AddWithValue("@Comment", feedbackModel.Comment);

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

        public List<FeedBackResponseModel> GetAllFeedbacksByBookId(int BookId)
        {
            List<FeedBackResponseModel> Feedbacks = new List<FeedBackResponseModel>();
            SqlConnection Connection = new SqlConnection(connectionString);
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("spGetFeedBackById", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {


                        while (reader.Read())
                        {
                            FeedBackResponseModel Feedbackdetails = new FeedBackResponseModel();
                            Feedbackdetails.FeedbackId = reader["FeedbackId"] == DBNull.Value ? default : reader.GetInt32("FeedbackId");
                            Feedbackdetails.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                            Feedbackdetails.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                            Feedbackdetails.Rating = reader["Rating"] == DBNull.Value ? default : reader.GetDouble("Rating");
                            Feedbackdetails.Comment = reader["Comment"] == DBNull.Value ? default : reader.GetString("Comment");
                            Feedbackdetails.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                            Feedbacks.Add(Feedbackdetails);
                        }

                        return Feedbacks;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteFeedbackById(int FeedBackId)
        {

            SqlConnection Connection = new SqlConnection(connectionString);
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    SqlCommand cmd = new SqlCommand("spDeleteFeedBackById", Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FeedbackId ", FeedBackId);

                    var result = cmd.ExecuteNonQuery();
                    if (result < 0)
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

    }
}
