using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CMS_SU21_BE.Repository
{
    public class UserRepository
    {
        /// <summary>
        /// Search user
        /// </summary>
        /// <param name="account"></param>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public List<UserResponse> searchUserByAccount(string account)
        {
            List<UserResponse> userResponses = new List<UserResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" user.id");
            sql.Append(", user.date_of_birth");
            sql.Append(", user.account");
            sql.Append(", user.first_name");
            sql.Append(", user.last_name");
            sql.Append(", IFNULL(user_role.roleCode,'')");
            sql.Append(", user.created_by");
            sql.Append(", user.created_time");
            sql.Append(", user.modified_by");
            sql.Append(", user.modified_time");
            sql.Append(", user.address");
            sql.Append(", user.gender");
            sql.Append(", user.phone_number");
            sql.Append(" FROM user ");
            sql.Append("    LEFT JOIN user_role");
            sql.Append("        ON user.account = user_role.account");
            sql.Append("    LEFT JOIN role");
            sql.Append("        ON user_role.roleCode = role.roleCode ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(account))
            {
                sql.Append("    AND user.account = '" + @account + "'");
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@account", account);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                UserResponse userResponse = new UserResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    userResponse.userId = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    userResponse.dateOfBirth = reader.GetDateTime(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    userResponse.account = reader.GetString(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    userResponse.firstName = reader.GetString(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    userResponse.lastName = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    userResponse.roleCode = reader.GetString(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    userResponse.createdBy = reader.GetString(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    userResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    userResponse.modifiedBy = reader.GetString(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    userResponse.modifiedTime = reader.GetDateTime(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    userResponse.address = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    userResponse.gender = reader.GetBoolean(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    userResponse.phoneNumber = reader.GetString(12);
                                }


                                userResponses.Add(userResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return userResponses;
        }


        public int Add(UserRequest request)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("user");
            sql.Append("(");
            sql.Append("    created_by,");
            sql.Append("    created_time,");
            sql.Append("    modified_by,");
            sql.Append("    modified_time,");
            sql.Append("    account,");
            sql.Append("    first_name,");
            sql.Append("    last_name,");
            sql.Append("    address,");
            sql.Append("    gender,");
            sql.Append("    phone_number,");
            if (request.avatar > 0)
            {
                sql.Append("    avatar,");
            }
            sql.Append("    date_of_birth");

            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @createdBy,");
            sql.Append("    @createdTime,");
            sql.Append("    @modifiedBy,");
            sql.Append("    @modifiedTime,");
            sql.Append("    @account,");
            sql.Append("    @firstName,");
            sql.Append("    @lastName,");
            sql.Append("    @address,");
            sql.Append("    @gender,");
            sql.Append("    @phoneNumber,");
            if (request.avatar > 0)
            {
                sql.Append("    @avatar,");
            }
            sql.Append("    @dateOfBirth");
            sql.Append(");");
            sql.Append("    SELECT LAST_INSERT_ID();");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("createdBy", request.createdBy);
                    cmd.Parameters.AddWithValue("createdTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", request.createdBy);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("account", request.account);
                    cmd.Parameters.AddWithValue("firstName", request.firstName);
                    cmd.Parameters.AddWithValue("lastName", request.lastName);
                    cmd.Parameters.AddWithValue("address", request.address);
                    cmd.Parameters.AddWithValue("gender", request.gender);
                    cmd.Parameters.AddWithValue("phoneNumber", request.phoneNumber);
                    cmd.Parameters.AddWithValue("dateOfBirth", request.dateOfBirth);
                    cmd.Parameters.AddWithValue("avatar", request.avatar);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }

            return result;
        }

        public UserResponse getUserDetailByAccount(String account)
        {
            StringBuilder sql = new StringBuilder();
            UserResponse userResponse = new UserResponse();
            sql.Append("SELECT ");
            sql.Append(" user.id");
            sql.Append(", user.date_of_birth");
            sql.Append(", user.account");
            sql.Append(", user.first_name");
            sql.Append(", user.last_name");
            sql.Append(", IFNULL(GROUP_CONCAT(IFNULL(user_role.roleCode,'')),'')");
            sql.Append(", user.created_by");
            sql.Append(", user.created_time");
            sql.Append(", user.modified_by");
            sql.Append(", user.modified_time");
            sql.Append(", user.address");
            sql.Append(", user.gender");
            sql.Append(", user.phone_number");
            sql.Append(", user.avatar");

            sql.Append(" FROM user ");
            sql.Append("    LEFT JOIN user_role");
            sql.Append("        ON user.account = user_role.account");
            sql.Append("    LEFT JOIN role");
            sql.Append("        ON user_role.roleCode = role.roleCode ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(account))
            {
                sql.Append("    AND user.account = '" + @account + "'");
            }
            sql.Append(" GROUP BY user.account ");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@account", account);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    userResponse.userId = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    userResponse.dateOfBirth = reader.GetDateTime(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    userResponse.account = reader.GetString(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    userResponse.firstName = reader.GetString(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    userResponse.lastName = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    userResponse.roleCode = reader.GetString(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    userResponse.createdBy = reader.GetString(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    userResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    userResponse.modifiedBy = reader.GetString(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    userResponse.modifiedTime = reader.GetDateTime(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    userResponse.address = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    userResponse.gender = reader.GetBoolean(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    userResponse.phoneNumber = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    userResponse.avatar = reader.GetInt32(13);
                                }
                            }
                        }
                    }
                }
                con.Close();
            }
            return userResponse;
        }

        public UserResponse getUserByAccount(string account)
        {
            UserResponse userResponse = new UserResponse { };
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" user.id");
            sql.Append(", user.date_of_birth");
            sql.Append(", user.account");
            sql.Append(", user.first_name");
            sql.Append(", user.last_name");
            sql.Append(", IFNULL(user_role.roleCode,'')");
            sql.Append(", user.created_by");
            sql.Append(", user.created_time");
            sql.Append(", user.modified_by");
            sql.Append(", user.modified_time");
            sql.Append(", user.address");
            sql.Append(", user.gender");
            sql.Append(", user.phone_number");
            sql.Append(", user.avatar");
            sql.Append(" FROM user ");
            sql.Append("    LEFT JOIN user_role");
            sql.Append("        ON user.account = user_role.account");
            sql.Append("    LEFT JOIN role");
            sql.Append("        ON user_role.roleCode = role.roleCode ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(account))
            {
                sql.Append("    AND user.account = '" + @account + "'");
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@account", account);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    userResponse.userId = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    userResponse.dateOfBirth = reader.GetDateTime(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    userResponse.account = reader.GetString(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    userResponse.firstName = reader.GetString(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    userResponse.lastName = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    userResponse.roleCode = reader.GetString(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    userResponse.createdBy = reader.GetString(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    userResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    userResponse.modifiedBy = reader.GetString(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    userResponse.modifiedTime = reader.GetDateTime(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    userResponse.address = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    userResponse.gender = reader.GetBoolean(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    userResponse.phoneNumber = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    userResponse.avatar = reader.GetInt32(13);
                                }
                            }
                        }
                    }
                }
                con.Close();
            }
            return userResponse;
        }

        public bool editUserInfor(UserRequest request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append(" user ");
            sql.Append("    SET ");
            sql.Append(" first_name =");
            sql.Append(" @firstName,");
            sql.Append(" last_name =");
            sql.Append(" @lastName,");
            sql.Append(" modified_time =");
            sql.Append(" @modifiedTime,");
            sql.Append(" modified_by =");
            sql.Append(" @modifiedBy,");
            sql.Append(" gender =");
            sql.Append(" @gender,");
            sql.Append(" date_of_birth =");
            sql.Append(" @dateOfBirth,");
            sql.Append(" address =");
            sql.Append(" @address,");
            sql.Append(" avatar =");
            sql.Append(" @avatar,");
            sql.Append(" phone_number =");
            sql.Append(" @phoneNumber");
            sql.Append(" WHERE account = @account");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("account", request.account);
                    cmd.Parameters.AddWithValue("firstName", request.firstName);
                    cmd.Parameters.AddWithValue("lastName", request.lastName);
                    cmd.Parameters.AddWithValue("gender", request.gender == true ? 1 : 0);
                    cmd.Parameters.AddWithValue("dateOfBirth", request.dateOfBirth);
                    cmd.Parameters.AddWithValue("address", request.address);
                    cmd.Parameters.AddWithValue("avatar", request.avatar);
                    cmd.Parameters.AddWithValue("phoneNumber", request.phoneNumber);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", request.modifiedBy);

                    var result = cmd.ExecuteNonQuery();
                    if (result != 1)
                    {
                        return false;
                    }
                }
                con.Close();
            }
            return true;
        }
    }
}