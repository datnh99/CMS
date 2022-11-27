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
    public class DenounceArticleRepository
    {
        public int add(DenounceArticleRequest request)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("denounce_article");
            sql.Append("(");
            sql.Append("    created_by,");
            sql.Append("    created_time,");
            sql.Append("    modified_by,");
            sql.Append("    modified_time,");
            sql.Append("    deleted,");
            sql.Append("    article_id, ");
            sql.Append("    reason ");
            if(!string.IsNullOrEmpty(request.reasonOther)) { 
                sql.Append("    ,reason_other ");
            }
            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @created_by,");
            sql.Append("    @created_time,");
            sql.Append("    @modified_by,");
            sql.Append("    @modified_time,");
            sql.Append("    @deleted,");
            sql.Append("    @article_id,");
            sql.Append("    @reason ");

            if (!string.IsNullOrEmpty(request.reasonOther))
            {
                sql.Append("   , @reason_other ");
            }
            sql.Append(");");
            sql.Append("    SELECT LAST_INSERT_ID();");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("created_by", request.createdBy);
                    cmd.Parameters.AddWithValue("created_time", DateTime.Now);
                    cmd.Parameters.AddWithValue("modified_by", request.createdBy);
                    cmd.Parameters.AddWithValue("modified_time", DateTime.Now);
                    cmd.Parameters.AddWithValue("deleted", 0);
                    cmd.Parameters.AddWithValue("article_id", request.articleID);
                    cmd.Parameters.AddWithValue("reason", string.Join(",", request.reason));
                    cmd.Parameters.AddWithValue("reason_other", request.reasonOther);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public DenounceArticleResponse getByReporterAndArticleID(string reporter, int articleID)
        {
            DenounceArticleResponse denounceArticleResponse = new DenounceArticleResponse();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MAX(da.id), da.created_by, da.created_time, da.modified_by, da.modified_time, da.deleted, da.article_id, da.reason ");
            sql.Append(" FROM denounce_article da ");
            sql.Append(" WHERE 1 = 1 AND da.deleted = 0 ");
            if (articleID > 0)
            {
                sql.Append("    AND da.article_id = " + @articleID + " ");
            }
            if (!string.IsNullOrEmpty(reporter))
            {
                sql.Append("    AND da.created_by = '" + @reporter + "'");
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();

                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@articleID", articleID);
                    cmd.Parameters.AddWithValue("@reporter", reporter);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                denounceArticleResponse = new DenounceArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    denounceArticleResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    denounceArticleResponse.createdBy = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    denounceArticleResponse.createdTime = reader.GetDateTime(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    denounceArticleResponse.modifiedBy = reader.GetString(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    denounceArticleResponse.modifiedTime = reader.GetDateTime(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    denounceArticleResponse.deleted = reader.GetInt32(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    denounceArticleResponse.articleID = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    denounceArticleResponse.reason = reader.GetString(7);
                                }
                            }
                        }
                    }
                }
                con.Close();
            }
            return denounceArticleResponse;
        }

        public List<DenounceArticleResponse> getByArticleId(int articleID, int pageIndex, int pageSize)
        {
            var startIndex = (pageIndex - 1) * pageSize;
            List<DenounceArticleResponse> denounceArticleResponseList = new List<DenounceArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MAX(da.id), da.created_by, da.created_time, da.modified_by, da.modified_time, da.deleted, da.article_id, da.reason, da.reason_other ");
            sql.Append(" FROM denounce_article da ");
            sql.Append(" WHERE 1 = 1 AND da.deleted = 0 ");
            if (articleID <= 0)
            {
                throw new ArgumentException(String.Format("Don't have any report for article with id = {0}!", articleID));
            }
            sql.Append("    AND da.article_id = " + @articleID + " ");
            sql.Append("    GROUP BY da.created_by ");
            sql.Append("    ORDER BY da.modified_time DESC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();

                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@articleID", articleID);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DenounceArticleResponse denounceArticleResponse = new DenounceArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    denounceArticleResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    denounceArticleResponse.createdBy = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    denounceArticleResponse.createdTime = reader.GetDateTime(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    denounceArticleResponse.modifiedBy = reader.GetString(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    denounceArticleResponse.modifiedTime = reader.GetDateTime(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    denounceArticleResponse.deleted = reader.GetInt32(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    denounceArticleResponse.articleID = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    denounceArticleResponse.reason = reader.GetString(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    denounceArticleResponse.reasonOther = reader.GetString(8);
                                }
                                denounceArticleResponseList.Add(denounceArticleResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return denounceArticleResponseList;
        }

        public int countByArticleId(int articleID)
        {
            int total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(DISTINCT da.created_by) ");
            sql.Append(" FROM denounce_article da ");
            sql.Append(" WHERE 1 = 1 AND da.deleted = 0 ");
            if (articleID <= 0)
            {
                throw new ArgumentException(String.Format("Don't have any report for article with id = {0}!", articleID));
            }
            sql.Append("    AND da.article_id = " + @articleID + " ");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@articleID", articleID);

                    total = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            return total;
        }

        public bool deleteByArticleID(int articleId)
        {
            var result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE denounce_article ");
            sql.Append("    SET deleted = true ");
            sql.Append("    WHERE article_id = @articleId");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("articleId", articleId);
                    result = cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            return result == 1;
        }
    }
}