﻿using Microsoft.Extensions.Configuration;
using ModelLayer.UserModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly string connectionString;
        public UserRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore");
        }

        public string AddUser(UserPostModel user)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            var encryptPassword = EncryptPassword(user.Password);

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("spAddUser", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@FullName", user.FullName);
                    com.Parameters.AddWithValue("@Email", user.Email);
                    com.Parameters.AddWithValue("@Password", encryptPassword);
                    com.Parameters.AddWithValue("@Phone", user.Mobile);
                    var result = com.ExecuteNonQuery();
                    return result.ToString();

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string EncryptPassword(string Password)
        {
            try
            {
                if (Password == null)
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(Password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DecryptedPassword(string encryptedPassword)
        {
            byte[] b;
            string decrypted;
            try
            {
                if (encryptedPassword == null)
                {
                    return null;
                }
                else
                {
                    b = Convert.FromBase64String(encryptedPassword);
                    decrypted = Encoding.ASCII.GetString(b);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string Login(UserLoginModel loginUser)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            var Password = EncryptPassword(loginUser.Password);

            try
            {
                using (connection)
                {
                    connection.Open();
                    int UserId = 0;
                    SqlCommand com = new SqlCommand("spLoginUser", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Email", loginUser.Email);
                    com.Parameters.AddWithValue("@password", Password);
                    SqlDataReader rd = com.ExecuteReader();
                    UserPostModel response = new UserPostModel();

                    if(rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            UserId = rd["Id"] == DBNull.Value ? default : rd.GetInt32("Id");
                            response.Email = rd["Email"] == DBNull.Value ? default : rd.GetString("Email");
                            response.Password = rd["Password"] == DBNull.Value ? default : rd.GetString("Password");

                        }
                        return GenerateJWTToken(response.Email, UserId);
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

        private string GenerateJWTToken(string email, int userId)
        {
            try
            {
                // generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),
                    new Claim("UserId",userId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
