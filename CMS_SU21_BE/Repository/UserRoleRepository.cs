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
    public class UserRoleRepository
    {
        public bool Add(UserRoleRequest request)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO user_role(createdBy, createdTime, modifiedBy, modifiedTime, account, roleCode) " +
                " VALUES(@createdBy, @createdTime, @modifiedBy, @modifiedTime, @account, @roleCode)");
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
                    cmd.Parameters.AddWithValue("account", request.Account);
                    cmd.Parameters.AddWithValue("roleCode", request.RoleCode);
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

        public bool checkExistRoleOfUser(string account, string roleCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" user_role.account ");
            sql.Append(" FROM user_role ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(account))
            {
                sql.Append("    AND user_role.account = '" + @account + "'");
            }
            else
            {
                return false;
            }
            if (!string.IsNullOrEmpty(roleCode))
            {
                sql.Append("    AND user_role.roleCode = '" + @roleCode + "'");

            }
            else
            {
                return false;
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();

                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@account", @account);
                    cmd.Parameters.AddWithValue("@roleCode", @roleCode);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return true;
                            }
                        }
                    }
                }
                con.Close();
            }
            return false;
        }

        public bool deleteById(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE");
            sql.Append("    FROM user_role");
            sql.Append("    WHERE id = @id");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("id", id);
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

        public List<UserRoleResponse> search(UserRoleRequest request, int pageSize, int pageIndex)
        {
            var startIndex = (pageIndex - 1) * pageSize;
            List<UserRoleResponse> userResponses = new List<UserRoleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" user_role.id,");
            sql.Append(" user_role.account,");
            sql.Append(" user_role.roleCode,");
            sql.Append(" role.roleName,");
            sql.Append(" user_role.createdBy,");
            sql.Append(" user_role.createdTime,");
            sql.Append(" user_role.modifiedBy,");
            sql.Append(" user_role.modifiedTime");
            sql.Append(" From user_role ");
            sql.Append(" Join role on role.roleCode = user_role.roleCode ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(request.Account))
            {
                sql.Append("    AND user_role.account LIKE '%" + @request.Account + "%'");
            }

            if (request.RoleCodeOfUser != null)
            {
                sql.Append("    AND user_role.roleCode IN ('" + String.Join("','", request.RoleCodeOfUser) + "')");
            }
            sql.Append(" ORDER BY user_role.account ASC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@account", request.Account);
                    cmd.Parameters.AddWithValue("@startIndex", startIndex);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                UserRoleResponse userResponse = new UserRoleResponse
                                {
                                    id = reader.GetInt32(0),
                                    account = reader.GetString(1),
                                    roleCode = reader.GetString(2),
                                    roleName = reader.GetString(3),
                                    createdBy = reader.GetString(4),
                                    createdTime = reader.GetDateTime(5),
                                    modifiedBy = reader.GetString(6),
                                    modifiedTime = reader.GetDateTime(7),

                                };

                                userResponses.Add(userResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return userResponses;
        }
        public List<UserRoleResponse> getUserRoleByAccount(string account)
        {
            List<UserRoleResponse> userResponses = new List<UserRoleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" user_role.roleCode,");
            sql.Append(" role.roleName");
            sql.Append(" From user_role ");
            sql.Append(" Join role on role.roleCode = user_role.roleCode ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(account))
            {
                sql.Append("    AND user_role.account = '" +@account + "'");
            }
            sql.Append(" ORDER BY user_role.createdTime DESC ");

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
                                UserRoleResponse userResponse = new UserRoleResponse();

                                if (!reader.IsDBNull(0))
                                {
                                    userResponse.roleCode = reader.GetString(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    userResponse.roleName = reader.GetString(1);
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
        public int totalSearchUserRole(UserRoleRequest request)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(*) ");
            sql.Append(" From user_role ");
            sql.Append(" Join role on role.roleCode = user_role.roleCode ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(request.Account))
            {
                sql.Append("    AND user_role.account LIKE '%" + @request.Account + "%'");
            }

            if (request.RoleCodeOfUser != null)
            {
                sql.Append("    AND user_role.roleCode IN ('" + String.Join("','", request.RoleCodeOfUser) + "')");
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@account", request.Account);
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public bool updateUserRoles(UserRoleRequest request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append("user_role");
            sql.Append("    SET ");
            sql.Append(" roleCode =");
            sql.Append(" @roleCode, ");
            sql.Append(" modifiedBy =");
            sql.Append(" @modifiedBy, ");
            sql.Append(" modifiedTime =");
            sql.Append(" @modifiedTime ");
            sql.Append(" WHERE id = @id ");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("id", request.Id);
                    cmd.Parameters.AddWithValue("account", request.Account);
                    cmd.Parameters.AddWithValue("roleCode", request.RoleCode);
                    cmd.Parameters.AddWithValue("modifiedBy", request.modifiedBy);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }

            return true;
        }
    }
}