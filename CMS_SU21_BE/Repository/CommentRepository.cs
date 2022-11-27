using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;

namespace CMS_SU21_BE.Repository
{
    public class CommentRepository
    {
        public int insertComment(CommentRequest request, string username)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("comment");
            sql.Append("(");
            sql.Append("    articleID,");
            sql.Append("    content,");
            sql.Append("    parentID,");
            sql.Append("    createdTime,");
            sql.Append("    modifiedTime,");
            sql.Append("    deleted,");
            sql.Append("    createdBy,modifiedBy");
            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @articleID,");
            sql.Append("    @content,");
            sql.Append("    @parentID,");
            sql.Append("    @createdTime,");
            sql.Append("    @modifiedTime,");
            sql.Append("    @deleted,");
            sql.Append("    @createdBy,@modifiedBy");
            sql.Append(");");
            sql.Append("    SELECT LAST_INSERT_ID();");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("articleID", request.articleID);
                    cmd.Parameters.AddWithValue("content", request.content);
                    cmd.Parameters.AddWithValue("parentID", request.parentID);
                    cmd.Parameters.AddWithValue("createdTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("deleted", 0);
                    cmd.Parameters.AddWithValue("createdBy", username);
                    cmd.Parameters.AddWithValue("modifiedBy", username);

                    result = Convert.ToInt32(cmd.ExecuteScalar());

                }
                con.Close();
            }
            return result;
        }

        public List<CommentResponse> getCommentByArticleID(int? articleID, int pageIndex, int pageSize)
        {
            var startIndex = (pageIndex - 1) * pageSize;
            List<CommentResponse> listResult = new List<CommentResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" comment.id");
            sql.Append(", comment.articleID");
            sql.Append(", comment.content");
            sql.Append(", comment.parentID");
            sql.Append(", comment.createdTime");
            sql.Append(", comment.modifiedTime");
            sql.Append(", comment.deleted");
            sql.Append(", comment.createdBy");
            sql.Append(", comment.modifiedBy");
            sql.Append(", CONCAT(u.first_name, ' ',u.last_name)");
            sql.Append(" FROM comment ");
            sql.Append(" JOIN user u  ON comment.createdBy = u.account ");
            sql.Append(" WHERE 1=1 AND deleted = 0 AND comment.parentID = -1 ");
            if (articleID != -1)
            {
                sql.Append("   AND comment.articleID =  @articleID ");
                sql.Append("   GROUP BY comment.id   ");
                sql.Append("   ORDER BY comment.createdTime DESC  ");
                sql.Append("    LIMIT " + startIndex + "," + @pageSize + "");

                using (MySqlConnection con = WebApiConfig.conn())
                {
                    con.Open();
                    string sqlCommand = sql.ToString();
                    using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("articleID", articleID);

                        using (DbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {

                                while (reader.Read())
                                {
                                    CommentResponse commentResponse = new CommentResponse { };
                                    if (!reader.IsDBNull(0))
                                    {
                                        commentResponse.id = reader.GetInt32(0);
                                    }
                                    if (!reader.IsDBNull(1))
                                    {
                                        commentResponse.articleID = reader.GetInt32(1);
                                    }
                                    if (!reader.IsDBNull(2))
                                    {
                                        commentResponse.content = reader.GetString(2);
                                    }
                                    if (!reader.IsDBNull(3))
                                    {
                                        commentResponse.parentID = reader.GetInt32(3);
                                    }
                                    if (!reader.IsDBNull(4))
                                    {
                                        commentResponse.createdTime = reader.GetDateTime(4);
                                    }
                                    if (!reader.IsDBNull(5))
                                    {
                                        commentResponse.modifiedTime = reader.GetDateTime(5);
                                    }
                                    if (!reader.IsDBNull(6))
                                    {
                                        commentResponse.deleted = reader.GetBoolean(6);
                                    }
                                    if (!reader.IsDBNull(7))
                                    {
                                        commentResponse.createdBy = reader.GetString(7);
                                    }
                                    if (!reader.IsDBNull(8))
                                    {
                                        commentResponse.modifiedBy = reader.GetString(8);
                                    }
                                    if (!reader.IsDBNull(9))
                                    {
                                        commentResponse.displayName = reader.GetString(9);
                                    }
                                    listResult.Add(commentResponse);
                                }
                            }
                        }
                    }
                    con.Close();
                }
            }
            return listResult;
        }
        public List<CommentResponse> getCommentInlistId(List<int> lstId)
        {
            List<CommentResponse> listResult = new List<CommentResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" comment.id");
            sql.Append(", comment.articleID");
            sql.Append(", comment.content");
            sql.Append(", comment.parentID");
            sql.Append(", comment.createdTime");
            sql.Append(", comment.modifiedTime");
            sql.Append(", comment.deleted");
            sql.Append(", comment.createdBy");
            sql.Append(", comment.modifiedBy");
            sql.Append(", CONCAT(u.first_name, ' ',u.last_name)");
            sql.Append(" FROM comment ");
            sql.Append(" JOIN user u  ON comment.createdBy = u.account ");
            sql.Append(" WHERE 1=1 AND deleted = 0 ");
            sql.Append("   AND comment.parentID IN (" + string.Join(",", lstId) + ")");
            sql.Append("   GROUP BY comment.id   ");
            sql.Append("   ORDER BY comment.createdTime DESC  ");

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
                                CommentResponse commentResponse = new CommentResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    commentResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    commentResponse.articleID = reader.GetInt32(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    commentResponse.content = reader.GetString(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    commentResponse.parentID = reader.GetInt32(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    commentResponse.createdTime = reader.GetDateTime(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    commentResponse.modifiedTime = reader.GetDateTime(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    commentResponse.deleted = reader.GetBoolean(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    commentResponse.createdBy = reader.GetString(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    commentResponse.modifiedBy = reader.GetString(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    commentResponse.displayName = reader.GetString(9);
                                }
                                listResult.Add(commentResponse);
                            }
                        }
                    }
                    con.Close();
                }
                return listResult;
            }
        }
        public int countCommentByArticleID(int? articleID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" COUNT(comment.id)");
            sql.Append(" FROM comment ");
            sql.Append(" WHERE 1=1 AND deleted = 0 AND comment.parentID = -1 ");
            if (articleID != -1)
            {
                sql.Append("   AND articleID =  @articleID ");

                using (MySqlConnection con = WebApiConfig.conn())
                {
                    con.Open();
                    string sqlCommand = sql.ToString();
                    using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("articleID", articleID);
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    con.Close();
                }
            }
            return 0;
        }

        public bool updateCommentByCommentID(CommentRequest request, string username)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append("comment");
            sql.Append("    SET ");
            sql.Append(" content=");
            sql.Append(" @content,");
            sql.Append(" modifiedTime=");
            sql.Append(" @modifiedTime,");
            sql.Append(" modifiedBy=");
            sql.Append(" @modifiedBy");
            sql.Append(" WHERE id = @id");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("content", request.content);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", username);
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

        public bool deleteCommentByCommentID(int? id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE comment ");
            sql.Append("    SET deleted = 1 ");
            sql.Append("    WHERE id = @id OR parentID = @id ");
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