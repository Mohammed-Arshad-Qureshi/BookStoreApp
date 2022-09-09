using Microsoft.Extensions.Configuration;
using ModelLayer.AdminModel;
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
    public class AdminRL:IAdminRL
    {
        private readonly string connectionString;
        public AdminRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore");
        }

        public string AdminLogin(AdminLoginModel loginModel)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spAdminLogin", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminEmail", loginModel.AdminEmail);
                    cmd.Parameters.AddWithValue("@AdminPassword", loginModel.AdminPassword);
                    SqlDataReader rd = cmd.ExecuteReader();
                    AdminResponseModel response = new AdminResponseModel();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            response.AdminId = rd["AdminId"] == DBNull.Value ? default : rd.GetInt32("AdminId");
                            response.AdminEmail = rd["AdminEmail"] == DBNull.Value ? default : rd.GetString("AdminEmail");
                            response.AdminPassword = rd["AdminPassword"] == DBNull.Value ? default : rd.GetString("AdminPassword");

                        }
                        return GenerateJWTToken_Admin(response.AdminEmail, response.AdminId);
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
            finally
            {
                sqlConnection.Close();
            }
        }

        private string GenerateJWTToken_Admin(string adminEmail, int adminId)
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
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim("AdminEmail", adminEmail),
                        new Claim("AdminId",adminId.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
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
