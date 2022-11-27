using CMS_SU21_BE.Models.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CMS_SU21_BE.Repository
{
    public class FileRepository
    {
        public List<FileResponse> getByArticleId(int? articleId)
        {
            List<FileResponse> listResult = new List<FileResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id ");
            sql.Append(", name ");
            sql.Append(", size ");
            sql.Append(", link ");
            sql.Append(", created_by ");
            sql.Append(", created_time ");
            sql.Append(", modified_by ");
            sql.Append(", modified_time ");
            sql.Append(", article_id ");
            sql.Append(", original_name ");
            sql.Append(" FROM file ");
            sql.Append(" WHERE   article_id = '" + @articleId + "' ");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@articleId", @articleId);


                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FileResponse fileResponse = new FileResponse();
                                if (!reader.IsDBNull(0))
                                {
                                    fileResponse.id = reader.GetInt32(0);

                                }
                                if (!reader.IsDBNull(1))
                                {
                                    fileResponse.name = reader.GetString(1);

                                }
                                if (!reader.IsDBNull(2))
                                {
                                    fileResponse.size = reader.GetInt64(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    fileResponse.link = reader.GetString(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    fileResponse.createdBy = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    fileResponse.createdTime = reader.GetDateTime(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    fileResponse.modifiedBy = reader.GetString(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    fileResponse.modifiedTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    fileResponse.articleId = reader.GetInt32(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    fileResponse.originalName = reader.GetString(9);
                                }
                                listResult.Add(fileResponse);
                            }
                        }
                    }
                }
                con.Close();
            }

            return listResult;
        }
        public List<FileResponse> getByListIds(List<int> ids)
        {
            List<FileResponse> listResult = new List<FileResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id ");
            sql.Append(", name ");
            sql.Append(", size ");
            sql.Append(", link ");
            sql.Append(", created_by ");
            sql.Append(", created_time ");
            sql.Append(", modified_by ");
            sql.Append(", modified_time ");
            sql.Append(", article_id ");
            sql.Append(", original_name ");

            sql.Append(" FROM file ");
            sql.Append("    WHERE id IN (" + string.Join(",", ids) + ")");

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
                                FileResponse fileResponse = new FileResponse();
                                if (!reader.IsDBNull(0))
                                {
                                    fileResponse.id = reader.GetInt32(0);

                                }
                                if (!reader.IsDBNull(1))
                                {
                                    fileResponse.name = reader.GetString(1);

                                }
                                if (!reader.IsDBNull(2))
                                {
                                    fileResponse.size = reader.GetInt64(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    fileResponse.link = reader.GetString(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    fileResponse.createdBy = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    fileResponse.createdTime = reader.GetDateTime(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    fileResponse.modifiedBy = reader.GetString(6);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    fileResponse.modifiedTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    fileResponse.originalName = reader.GetString(7);
                                }
                                listResult.Add(fileResponse);
                            }
                        }
                    }
                }
                con.Close();
            }

            return listResult;
        }
        public FileResponse getById(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id ");
            sql.Append(", name ");
            sql.Append(", size ");
            sql.Append(", link ");
            sql.Append(", created_by ");
            sql.Append(", created_time ");
            sql.Append(", modified_by ");
            sql.Append(", modified_time ");
            sql.Append(", article_id ");
            sql.Append(" FROM file ");

            if (!string.IsNullOrEmpty(id.ToString()))
            {
                sql.Append(" WHERE 1 = 1 ");
                sql.Append("    AND id = '" + id + "'");

            }


            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);


                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FileResponse fileResponse = new FileResponse
                                {
                                    id = reader.GetInt32(0),
                                    name = reader.GetString(1),
                                    size = reader.GetInt64(2),
                                    link = reader.GetString(3),
                                    createdBy = reader.GetString(4),
                                    createdTime = reader.GetDateTime(5),
                                    modifiedBy = reader.GetString(6),
                                    modifiedTime = reader.GetDateTime(7),
                                    articleId = reader.GetInt32(8)
                                };
                                return fileResponse;
                            }
                        }
                    }
                }
                con.Close();
            }

            return null;
        }
        public int Add(FileRequest request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("file");
            sql.Append("(");
            sql.Append("    name,");
            sql.Append("    size,");
            sql.Append("    link,");
            sql.Append("    created_by,");
            sql.Append("    created_time,");
            sql.Append("    modified_by,");
            sql.Append("    modified_time,");
            sql.Append("    article_id,");
            sql.Append("    original_name ");

            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @name,");
            sql.Append("    @size,");
            sql.Append("    @link,");
            sql.Append("    @createdBy,");
            sql.Append("    @createdTime,");
            sql.Append("    @modifiedBy,");
            sql.Append("    @modifiedTime,");
            sql.Append("    @article_id,");
            sql.Append("    @originalName ");
            sql.Append(");");
            sql.Append("    SELECT LAST_INSERT_ID();");


            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", request.name);
                    cmd.Parameters.AddWithValue("@size", request.size);
                    cmd.Parameters.AddWithValue("@link", request.link);
                    cmd.Parameters.AddWithValue("@createdBy", request.createdBy);
                    cmd.Parameters.AddWithValue("@createdTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@modifiedBy", request.modifiedBy);
                    cmd.Parameters.AddWithValue("@modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@article_id", request.articleId);
                    cmd.Parameters.AddWithValue("@originalName", request.originalName);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                    return result;
                }
            }
        }

        public bool deleteByIds(List<int> ids)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE");
            sql.Append("    FROM file");
            sql.Append("    WHERE id IN ("+string.Join(",",ids) +")");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    var result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (result != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}