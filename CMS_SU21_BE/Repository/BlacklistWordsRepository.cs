using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Text;
using CMS_SU21_BE.Models.Entities;

namespace CMS_SU21_BE.Repository
{
    public class BlacklistWordsRepository
    {
        public List<BlackListWords> searchBlacklistWords(string content, int? pageSize, int? pageIndex)
        {
            List<BlackListWords> listResult = new List<BlackListWords>();
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" b.id, ");
            sql.Append(" b.content, ");
            sql.Append(" b.created_by, ");
            sql.Append(" b.created_time ");
            sql.Append(" FROM blacklist_words b ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(content))
            {
                sql.Append("    AND LOWER(b.content) LIKE '" + content.ToLower().Trim() + "'");

            }
            sql.Append("    ORDER BY b.created_time DESC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();

                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@content", content);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int i = 0;
                            while (reader.Read())
                            {
                                i++;
                                BlackListWords words = new BlackListWords();
                                if (!reader.IsDBNull(0))
                                {
                                    words.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    words.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    words.createdBy = reader.GetString(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    words.createdTime = reader.GetDateTime(3);
                                }
                                words.index = i;
                                listResult.Add(words);
                            }
                        }
                    }
                }
                con.Close();
            }
            return listResult;
        }
        public int countBlacklistWords(string content)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" COUNT(b.id) ");
            sql.Append(" FROM blacklist_words b ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(content))
            {
                sql.Append("    AND LOWER(b.content) LIKE '" + content.ToLower().Trim() + "'");

            }
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@content", content);
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            return result;
        }
        public List<string> getAllBlacklistWords()
        {
            List<string> listResult = new List<string>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" b.content ");
            sql.Append(" FROM blacklist_words b ");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    listResult.Add(reader.GetString(0));
                                }
                            }
                        }
                    }
                }
                con.Close();
            }
            return listResult;
        }
        public int add(string content, string username)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("blacklist_words");
            sql.Append("(");
            sql.Append("    content,");
            sql.Append("    created_by,");
            sql.Append("    created_time");

            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @content,");
            sql.Append("    @createdBy,");
            sql.Append("    @createdTime");

            sql.Append(");");
            sql.Append("    SELECT LAST_INSERT_ID();");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("content", content);
                    cmd.Parameters.AddWithValue("createdBy", username);
                    cmd.Parameters.AddWithValue("createdTime", DateTime.Now);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return 1;
        }

        public bool delete(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE");
            sql.Append("    FROM blacklist_words");
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
    }
}