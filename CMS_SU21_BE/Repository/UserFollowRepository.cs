using CMS_SU21_BE.Models.Requests;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CMS_SU21_BE.Repository
{
    public class UserFollowRepository
    {
        public int add(UserFollowRequest request)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("user_follow");
            sql.Append("(");
            sql.Append("    account,");
            sql.Append("    follower,");
            sql.Append("    created_by,");
            sql.Append("    created_time,");
            sql.Append("    modified_by,");
            sql.Append("    modified_time");
            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @account,");
            sql.Append("    @follower,");
            sql.Append("    @createdBy,");
            sql.Append("    @createdTime,");
            sql.Append("    @modifiedBy,");
            sql.Append("    @modifiedTime");
            sql.Append(");");
            sql.Append("    SELECT LAST_INSERT_ID();");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("account", request.account);
                    cmd.Parameters.AddWithValue("follower", request.follower);
                    cmd.Parameters.AddWithValue("createdBy", request.createdBy);
                    cmd.Parameters.AddWithValue("createdTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", request.modifiedBy);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public bool deleteByAccount(string account,string follower)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE");
            sql.Append("    FROM user_follow");
            sql.Append("    WHERE account = @account AND follower = @follower");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("account", account);
                    cmd.Parameters.AddWithValue("follower", follower);

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

        public Boolean getFollowedUser(string account,string loggedUser)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id");
            sql.Append(" FROM user_follow ");
            sql.Append(" WHERE  ");
            sql.Append("    account = '" + @account + "'");
            sql.Append("   AND follower = '" + @loggedUser + "'");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@account", account);
                    cmd.Parameters.AddWithValue("@loggedUser", @loggedUser);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public List<int> getAllArticleByAccount(string account)
        {
            List<int> listResult = new List<int>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" art.id");
            sql.Append(" FROM article art  ");
            sql.Append(" JOIN master_data tmd ON art.status = tmd.id  ");

            sql.Append(" WHERE  ");
            sql.Append("    art.author = '" + @account + "' AND art.deleted = 0 AND art.clone = 0 AND tmd.lower_code = 'posted' ");

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
                                    listResult.Add(reader.GetInt32(0));
                                }
                            }
                        }
                    }
                }
            }
            return listResult;
        }
        public bool update(UserFollowRequest request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append("user_follow");
            sql.Append("    SET ");
            sql.Append(" account=");
            sql.Append(" @account,");
            sql.Append(" follower=");
            sql.Append(" @follower,");
            sql.Append(" created_by=");
            sql.Append(" @createdBy,");
            sql.Append(" created_time=");
            sql.Append(" @createdTime,");
            sql.Append(" modified_by=");
            sql.Append(" @modifiedBy,");
            sql.Append(" modified_time=");
            sql.Append(" @modifiedTime,");
            sql.Append(" act_flg=");
            sql.Append(" @actFlg");
            sql.Append(" WHERE id = @id");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("account", request.account);
                    cmd.Parameters.AddWithValue("follower", request.follower);
                    cmd.Parameters.AddWithValue("createdBy", request.createdBy);
                    cmd.Parameters.AddWithValue("createdTime", request.createdTime);
                    cmd.Parameters.AddWithValue("modifiedBy", request.modifiedBy);
                    cmd.Parameters.AddWithValue("modifiedTime", request.modifiedTime);
                    cmd.Parameters.AddWithValue("id", request.id);
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