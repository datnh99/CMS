using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Common.Utils;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace CMS_SU21_BE.Repository
{
    public class ArticleRepository : BaseRepository
    {
        public string CheckTitle(string title)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" COUNT(title)");
            sql.Append(" FROM article ");

            if (!string.IsNullOrEmpty(title))
            {
                sql.Append(" WHERE 1 = 1 ");
                sql.Append("    AND title = @title");
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("title", title);
                    var result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result > 1)
                    {
                        return "The title is not exit, try again.";
                    }
                }
            }
            return null;
        }

        public int Add(ArticleRequest request)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("article");
            sql.Append("(");
            sql.Append("    content,");
            sql.Append("    categoryID,");
            sql.Append("    author,");
            sql.Append("    status,");
            sql.Append("    totalViews,");
            sql.Append("    createdBy,");
            sql.Append("    createdTime,");
            sql.Append("    deleted,");
            sql.Append("    clone,");
            sql.Append("    sapo,");
            sql.Append("    title,");
            sql.Append("    introductionImage,");
            sql.Append("    modifiedTime,");
            sql.Append("    modifiedBy,");
            sql.Append("    relatedArticle,");
            sql.Append("    fullTextContent,");
            sql.Append("    pinned");

            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @content,");
            sql.Append("    @categoryID,");
            sql.Append("    @author,");
            sql.Append("    @status,");
            sql.Append("    @totalViews,");
            sql.Append("    @createdBy,");
            sql.Append("    @createdTime,");
            sql.Append("    @deleted,");
            sql.Append("    @clone,");
            sql.Append("    @sapo,");
            sql.Append("    @title,");
            sql.Append("    @introductionImage,");
            sql.Append("    @modifiedTime,");
            sql.Append("    @modifiedBy,");
            sql.Append("    @relatedArticle,");
            sql.Append("    @fullTextContent,");
            sql.Append("    @pinned");
            sql.Append(");");
            sql.Append("    SELECT LAST_INSERT_ID();");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("content", request.content);
                    cmd.Parameters.AddWithValue("categoryID", request.categoryID);
                    cmd.Parameters.AddWithValue("author", request.author);
                    cmd.Parameters.AddWithValue("status", request.status);
                    cmd.Parameters.AddWithValue("totalViews", 1);
                    cmd.Parameters.AddWithValue("createdBy", request.author);
                    cmd.Parameters.AddWithValue("createdTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", request.author);
                    cmd.Parameters.AddWithValue("deleted", false);
                    cmd.Parameters.AddWithValue("clone", request.clone);
                    cmd.Parameters.AddWithValue("sapo", request.sapo);
                    cmd.Parameters.AddWithValue("title", request.title);
                    cmd.Parameters.AddWithValue("introductionImage", request.introductionImage);
                    cmd.Parameters.AddWithValue("relatedArticle", request.relatedArticle);
                    cmd.Parameters.AddWithValue("pinned", false);

                    cmd.Parameters.AddWithValue("fullTextContent", (request.title + request.sapo + request.content).Trim().ToLower());

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public bool DeleteById(int? newID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE");
            sql.Append("    FROM hash_tag");
            sql.Append("    WHERE article_id = @article_id");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("article_id", newID);
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

        public bool updateTotalViews(int? articleId)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE article");
            sql.Append("    SET totalViews = totalViews + 1 ");
            sql.Append("    WHERE id = @article_id");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("article_id", articleId);
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

        public bool DeleteArticleById(int? articleId, int? statusID, String modifiedBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE article ");
            sql.Append("    SET deleted = true, "); 
            sql.Append("    status = @statusID, ");
            sql.Append("    modifiedBy = @modifiedBy, ");
            sql.Append("    modifiedTime = @modifiedTime, ");
            sql.Append("    pinned = false ");
            sql.Append("    WHERE id = @articleId");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("articleId", articleId);
                    cmd.Parameters.AddWithValue("statusID", statusID);
                    cmd.Parameters.AddWithValue("modifiedBy", modifiedBy);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
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

        public bool UpdateNews(ArticleRequest request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append(" article ");
            sql.Append("    SET ");
            sql.Append(" content=");
            sql.Append(" @content,");
            sql.Append(" categoryID=");
            sql.Append(" @categoryID,");
            if (!string.IsNullOrEmpty(request.approveDate.ToString()) && request.confirm)
            {
                sql.Append(" approveDate=");
                sql.Append(" @approveDate,");
            }
            if (request.status > 0)
            {
                sql.Append(" status=");
                sql.Append(" @status,");
            }
            sql.Append(" modifiedTime=");
            sql.Append(" @modifiedTime,");
            sql.Append(" modifiedBy=");
            sql.Append(" @modifiedBy,");
            sql.Append(" deleted=");
            sql.Append(" @deleted,");
            sql.Append(" sapo=");
            sql.Append(" @sapo,");
            sql.Append(" title=");
            sql.Append(" @title,");
            sql.Append(" introductionImage=");
            sql.Append(" @introductionImage,");
            sql.Append(" relatedArticle=");
            sql.Append(" @relatedArticle");
            sql.Append(" WHERE id = @id");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                DeleteById(request.id);
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("content", request.content);
                    cmd.Parameters.AddWithValue("categoryID", request.categoryID);
                    cmd.Parameters.AddWithValue("id", request.id);

                    if (!string.IsNullOrEmpty(request.approveDate.ToString()) && request.confirm)
                    {
                        cmd.Parameters.AddWithValue("approveDate", request.approveDate);
                    }
                    if (request.status > 0)
                    {
                        cmd.Parameters.AddWithValue("status", request.status);
                    }
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", request.modifiedBy);
                    cmd.Parameters.AddWithValue("deleted", request.deleted);
                    cmd.Parameters.AddWithValue("sapo", request.sapo);
                    cmd.Parameters.AddWithValue("title", request.title);
                    cmd.Parameters.AddWithValue("introductionImage", request.introductionImage);
                    cmd.Parameters.AddWithValue("relatedArticle", request.relatedArticle);

                    var result = cmd.ExecuteNonQuery();
                    if (result != 1)
                    {
                        return false;
                    }
                    if (request.tagContent != null)
                    {
                        if (request.tagContent.Count > 0)
                        {
                            addHashTag(request.tagContent, request.id);
                        }
                    }
                }
                con.Close();
            }
            return true;
        }
        public bool addHashTag(List<string> tagContent, int? newID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("hash_tag");
            sql.Append("(");
            sql.Append("    article_id,");
            sql.Append("    tag_content");
            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @article_id,");
            sql.Append("    @tag_content");
            sql.Append(")");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                for (int i = 0; i < tagContent.Count; i++)
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("article_id", newID);
                        cmd.Parameters.AddWithValue("tag_content", tagContent[i]);
                        var result = cmd.ExecuteNonQuery();
                        if (result != 1)
                        {
                            return false;
                        }

                    }
                }
                con.Close();
            }

            return true;
        }


        /// <summary>
        /// Search news
        /// </summary>
        /// <param name="newID"></param>
        /// <returns></returns>
        public List<ArticleResponse> Search(int? newID, string title, int? categoryID, int? status, int? pageSize, int? pageIndex)
        {
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            /*var startIndex = (pageIndex.Value - 1) * pageSize.Value;
           
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" newID");
            sql.Append(", content");
            sql.Append(", categoryID");
            sql.Append(", approveDate");
            sql.Append(", account");
            sql.Append(", status");
            sql.Append(", draftFlag");
            sql.Append(", totalViews");
            sql.Append(", createDate");
            sql.Append(", updateDate");
            sql.Append(", deleteFlag");
            sql.Append(", sapo");
            sql.Append(", title");
            sql.Append(", introductionImage");
            sql.Append(" FROM news ");
            sql.Append(" WHERE 1=1 ");
            if (newID != -1)
            {
                sql.Append("   AND newID = " + @newID + "");
            }
            if (!string.IsNullOrEmpty(title))
            {
                sql.Append("   AND title = " + @title + "");
            }
            if (categoryID != -1)
            {
                sql.Append("   AND categoryID = " + @categoryID + "");
            }
            if (status != -1)
            {
                sql.Append("   AND status = " + @status + "");
            }
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@newID", newID);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@categoryID", categoryID);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@startIndex", startIndex);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ArticleResponse newsResponse = new ArticleResponse
                                {
                                    newID = reader.GetInt32(0),
                                    content = reader.GetString(1),
                                    categoryID = reader.GetInt32(2),
                                    approveDate = reader.GetDateTime(3),
                                    account = reader.GetString(4),
                                    status = reader.GetInt32(5),
                                    draftFlag = reader.GetBoolean(6),
                                    totalViews = reader.GetInt32(7),
                                    createDate = reader.GetDateTime(8),
                                    updateDate = reader.GetDateTime(9),
                                    deleteFlag = reader.GetBoolean(10),
                                };

                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < newsResponses.Count; i++)
            {
                newsResponses[i].tagContent = GetTagContentByNewID(newsResponses[i].newID);
            }
*/
            return newsResponses;
        }

        public List<string> GetTagContentByNewID(int? newID)
        {
            List<string> tagContent = new List<string>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" tag_content");
            sql.Append(" FROM hash_tag ");
            sql.Append("    WHERE article_id = @article_id");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@article_id", newID);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                tagContent.Add(reader.GetString(0));
                            }
                        }
                    }
                }
                con.Close();
            }

            return tagContent;
        }

        public List<ArticleResponse> searchArticleToManagement(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage");
            sql.Append(", article_version.articleID");
            sql.Append(", (Select COUNT(da.article_id) FROM denounce_article da WHERE da.article_id = article.id AND da.deleted = false )");
            sql.Append(", article.pinned, article.modifiedBy");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN article_version ");
            sql.Append("        ON article_version.oldArticleID = article.id ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 ");
            if (submissionRequest != null)
            {
                if (!string.IsNullOrEmpty(submissionRequest.title))
                {
                    sql.Append("    AND LOWER(article.title) LIKE '%" + @submissionRequest.title.ToLower() + "%'");
                }
                if (submissionRequest.categoryID != -1)
                {
                    sql.Append("    AND article.categoryID = " + @submissionRequest.categoryID + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.account))
                {
                    sql.Append("    AND LOWER(article.author) LIKE '%" + @submissionRequest.account.ToLower() + "%'");
                }
                if (submissionRequest.status != -1)
                {
                    sql.Append("    AND article.status = " + @submissionRequest.status + "");
                }
            }
            sql.Append(" GROUP BY article.id ");
            sql.Append(" ORDER BY article.modifiedTime DESC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("title", submissionRequest.title);
                    cmd.Parameters.AddWithValue("categoryID", submissionRequest.categoryID);
                    cmd.Parameters.AddWithValue("author", submissionRequest.account);
                    cmd.Parameters.AddWithValue("status", submissionRequest.status);
                    cmd.Parameters.AddWithValue("startIndex", startIndex);
                    cmd.Parameters.AddWithValue("pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.originalArticleID = reader.GetInt32(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.numberReport = reader.GetInt64(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.pinned = reader.GetBoolean(17);
                                }
                                if (!reader.IsDBNull(18))
                                {
                                    newsResponse.modifiedBy = reader.GetString(18);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }

        public List<ArticleResponse> searchAllArticleToManagement(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage");
            sql.Append(", article_version.articleID");
            sql.Append(", (Select COUNT(da.article_id) FROM denounce_article da WHERE da.article_id = article.id AND da.deleted = false )");
            sql.Append(", article.pinned, article.modifiedBy");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN article_version ");
            sql.Append("        ON article_version.oldArticleID = article.id ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 ");
            sql.Append(" AND master_data.code <> 'DRAFT' ");
            if (submissionRequest != null)
            {
                if (!string.IsNullOrEmpty(submissionRequest.title))
                {
                    sql.Append("    AND LOWER(article.title) LIKE '%" + @submissionRequest.title.ToLower() + "%'");
                }
                if (submissionRequest.categoryID != -1)
                {
                    sql.Append("    AND article.categoryID = " + @submissionRequest.categoryID + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.account))
                {
                    sql.Append("    AND LOWER(article.author) LIKE '%" + @submissionRequest.account.ToLower() + "%'");
                }
                if (submissionRequest.status != -1)
                {
                    sql.Append("    AND article.status = " + @submissionRequest.status + "");
                }
            }
            sql.Append(" GROUP BY article.id ");
            sql.Append(" ORDER BY article.modifiedTime DESC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("title", submissionRequest.title);
                    cmd.Parameters.AddWithValue("categoryID", submissionRequest.categoryID);
                    cmd.Parameters.AddWithValue("author", submissionRequest.account);
                    cmd.Parameters.AddWithValue("status", submissionRequest.status);
                    cmd.Parameters.AddWithValue("startIndex", startIndex);
                    cmd.Parameters.AddWithValue("pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.originalArticleID = reader.GetInt32(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.numberReport = reader.GetInt64(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.pinned = reader.GetBoolean(17);
                                }
                                if (!reader.IsDBNull(18))
                                {
                                    newsResponse.modifiedBy = reader.GetString(18);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }
        public List<ArticleResponse> getTopArticles( string username)
        {
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage");
            sql.Append(", CASE WHEN uf.follower IS NOT NULL THEN TRUE ELSE FALSE END AS cared ");
            sql.Append(", COUNT(comment.id)");
            sql.Append(", article.pinned");
            sql.Append(", u.first_name");
            sql.Append(", u.last_name");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN comment ");
            sql.Append("        ON article.id = comment.articleID ");
            sql.Append("     JOIN user_follow uf ON article.author = uf.account ");
            sql.Append("    JOIN user u ON article.author = u.account ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 AND  article.deleted = 0 AND master_data.lower_code = 'posted' ");
            sql.Append("  AND  category.deleted = 0 AND uf.follower = '" + username + "' ");
            
            sql.Append("  AND  category.deleted = 0");
            sql.Append(" GROUP BY article.id ORDER BY article.approveDate DESC ");
            sql.Append("    LIMIT 0,8 ");

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
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.followedUser = true;
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.totalComment = reader.GetInt32(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.pinned = reader.GetBoolean(17);
                                }
                                if (!reader.IsDBNull(18))
                                {
                                    newsResponse.firstName = reader.GetString(18);
                                }
                                if (!reader.IsDBNull(19))
                                {
                                    newsResponse.lastName = reader.GetString(19);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }
        public List<ArticleResponse> getRecommendedArticles(String lstArticlesId, String lstArticlesIdNotIn,int limit)
        {
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage");
            sql.Append(", CASE WHEN uf.follower IS NOT NULL THEN TRUE ELSE FALSE END AS cared ");
            sql.Append(", COUNT(comment.id)");
            sql.Append(", article.pinned");
            sql.Append(", u.first_name");
            sql.Append(", u.last_name");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN comment ");
            sql.Append("        ON article.id = comment.articleID ");
            sql.Append("    LEFT JOIN user_follow uf ON article.author = uf.account ");
            sql.Append("    JOIN user u ON article.author = u.account ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 AND  article.deleted = 0 AND master_data.lower_code = 'posted' ");
            /*            sql.Append("  AND  category.deleted = 0 OR uf.follower = '" + username + "' ");
             *            
            */
            sql.Append("  AND  category.deleted = 0");
            if (!string.IsNullOrEmpty(lstArticlesId))
            {
                sql.Append("  AND article.id IN  (" + lstArticlesId + ")");
            }
            if (!string.IsNullOrEmpty(lstArticlesIdNotIn))
            {
                sql.Append("  AND article.id NOT IN  (" + lstArticlesIdNotIn + ")");
            }
            sql.Append(" GROUP BY article.id ");
            sql.Append("    LIMIT 0,@limit ");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("limit", limit);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.followedUser = reader.GetBoolean(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.totalComment = reader.GetInt32(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.pinned = reader.GetBoolean(17);
                                }
                                if (!reader.IsDBNull(18))
                                {
                                    newsResponse.firstName = reader.GetString(18);
                                }
                                if (!reader.IsDBNull(19))
                                {
                                    newsResponse.lastName = reader.GetString(19);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }
        public List<ArticleResponse> getNewestPinnedArticleOfCategory()
        {
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage");
            sql.Append(", CASE WHEN uf.follower IS NOT NULL THEN TRUE ELSE FALSE END AS cared ");
            sql.Append(", COUNT(comment.id)");
            sql.Append(", article.pinned");
            sql.Append(", u.first_name");
            sql.Append(", u.last_name");
            sql.Append("");

            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN comment ");
            sql.Append("        ON article.id = comment.articleID ");
            sql.Append("    LEFT JOIN user_follow uf ON article.author = uf.account ");
            sql.Append("    JOIN user u ON article.author = u.account ");
            sql.Append(" WHERE  " +
                "   article.clone = 0 AND  article.deleted = 0 AND master_data.lower_code = 'posted'  ");
            sql.Append("  AND  category.deleted = 0 AND article.`pinned` = TRUE ");
            sql.Append(" GROUP BY article.categoryID ORDER BY article.approveDate DESC ");

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
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.followedUser = reader.GetBoolean(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.totalComment = reader.GetInt32(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.pinned = reader.GetBoolean(17);
                                }
                                if (!reader.IsDBNull(18))
                                {
                                    newsResponse.firstName = reader.GetString(18);
                                }
                                if (!reader.IsDBNull(19))
                                {
                                    newsResponse.lastName = reader.GetString(19);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }
        public List<ArticleResponse> getNewestArticleOfCategory()
        {
            List<ArticleResponse> listPinned = getNewestPinnedArticleOfCategory();
            List<int> listNotInIds = new List<int>();

            if (!ObjectUtils.isNullOrEmpty(listPinned) && listPinned.Count > 0)
            {
                listNotInIds = listPinned.Select(res => res.categoryID).ToList();
            }
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage");
            sql.Append(", CASE WHEN uf.follower IS NOT NULL THEN TRUE ELSE FALSE END AS cared ");
            sql.Append(", COUNT(comment.id)");
            sql.Append(", article.pinned");
            sql.Append(", u.first_name");
            sql.Append(", u.last_name");
            sql.Append(", MAX(article.`approveDate`) AS dates");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN comment ");
            sql.Append("        ON article.id = comment.articleID ");
            sql.Append("    LEFT JOIN user_follow uf ON article.author = uf.account ");
            sql.Append("    JOIN user u ON article.author = u.account ");
            sql.Append(" WHERE article.`approveDate` IN (SELECT MAX(art.`approveDate`) FROM `article` art GROUP BY art.`categoryID`)" +
                "  AND article.clone = 0 AND  article.deleted = 0 AND master_data.lower_code = 'posted'  ");
            sql.Append("  AND  category.deleted = 0 AND article.`pinned` = FALSE ");
            if (!ObjectUtils.isNullOrEmpty(listNotInIds) && listNotInIds.Count > 0)
            {
                sql.Append("  AND  category.id NOT IN (" + string.Join(",", listNotInIds) + ") ");
            }

            sql.Append(" GROUP BY article.categoryID ORDER BY dates DESC ");

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
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.followedUser = reader.GetBoolean(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.totalComment = reader.GetInt32(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.pinned = reader.GetBoolean(17);
                                }
                                if (!reader.IsDBNull(18))
                                {
                                    newsResponse.firstName = reader.GetString(18);
                                }
                                if (!reader.IsDBNull(19))
                                {
                                    newsResponse.lastName = reader.GetString(19);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            if (!ObjectUtils.isNullOrEmpty(newsResponses) && newsResponses.Count > 0)
            {
                listPinned.AddRange(newsResponses);
            }
            return listPinned;
        }
        public List<ArticleResponse> searchLandingArticles(SubmissionRequest submissionRequest, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage ");
            sql.Append(", CASE WHEN uf.follower IS NOT NULL THEN TRUE ELSE FALSE END AS cared ");
            sql.Append(", COUNT(comment.id) ");
            sql.Append(", article.pinned");
            sql.Append(", u.first_name");
            sql.Append(", u.last_name");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN user_follow uf ON article.author = uf.account ");
            sql.Append("    LEFT JOIN comment ");
            sql.Append("        ON article.id = comment.articleID ");
            sql.Append("    JOIN user u ON article.author = u.account ");

            sql.Append(" WHERE 1=1 AND article.clone = 0 AND  article.deleted = 0 AND master_data.lower_code = 'posted' ");
            sql.Append("  AND  category.deleted = 0 ");
            if (submissionRequest != null)
            {
                if (!string.IsNullOrEmpty(submissionRequest.title))
                {
                    sql.Append("    AND article.title LIKE '%" + @submissionRequest.title + "%'");
                }
                if (submissionRequest.categoryID != -1)
                {
                    sql.Append("    AND article.categoryID = " + @submissionRequest.categoryID + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.account))
                {
                    sql.Append("    AND article.account LIKE '%" + @submissionRequest.account + "%'");
                }
                if (submissionRequest.status != -1)
                {
                    sql.Append("    AND article.status = " + @submissionRequest.status + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.lstArticlesId))
                {
                    sql.Append("    AND article.id  NOT IN (" + submissionRequest.lstArticlesId + ")");
                }
            }
            sql.Append(" GROUP BY article.id ORDER BY article.approveDate DESC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("title", submissionRequest.title);
                    cmd.Parameters.AddWithValue("categoryID", submissionRequest.categoryID);
                    cmd.Parameters.AddWithValue("author", submissionRequest.account);
                    cmd.Parameters.AddWithValue("status", submissionRequest.status);
                    cmd.Parameters.AddWithValue("startIndex", startIndex);
                    cmd.Parameters.AddWithValue("pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.followedUser = reader.GetBoolean(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.totalComment = reader.GetInt32(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.pinned = reader.GetBoolean(17);
                                }
                                if (!reader.IsDBNull(18))
                                {
                                    newsResponse.firstName = reader.GetString(18);
                                }
                                if (!reader.IsDBNull(19))
                                {
                                    newsResponse.lastName = reader.GetString(19);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }
        public List<ArticleResponse> getArticleByPolularTags(List<string> hashtag)
        {
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage ");
            sql.Append(", CASE WHEN uf.follower IS NOT NULL THEN TRUE ELSE FALSE END AS cared ");
            sql.Append(", COUNT(comment.id) ");

            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN user_follow uf ON article.author = uf.account ");
            sql.Append("    LEFT JOIN comment ");
            sql.Append("        ON article.id = comment.articleID ");
            sql.Append("    JOIN hash_tag ");
            sql.Append("        ON hash_tag.article_id = article.id ");

            sql.Append(" WHERE 1=1 AND article.clone = 0 AND  article.deleted = 0 AND master_data.lower_code = 'posted' ");
            sql.Append("  AND  category.deleted = 0 ");
            if (hashtag != null)
            {
                if (hashtag.Count > 0)
                {
                    sql.Append("    AND  ( ");
                    for (int i = 0; i < hashtag.Count; i++)
                    {
                        if (i != 0)
                        {
                            sql.Append(" OR ");
                        }
                        sql.Append(" hash_tag.tag_content = '" + hashtag[i] + "'");

                    }
                    sql.Append(")");

                }
            }
            sql.Append("  GROUP BY article.id ORDER BY article.totalViews DESC ");

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
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.followedUser = reader.GetBoolean(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.totalComment = reader.GetInt32(16);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }
        public int totalSearchRequest()
        {
            int result;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" COUNT(*)");
            sql.Append(" FROM article ");
            sql.Append("     JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("     JOIN master_data ");
            sql.Append("        ON master_data.id = article.status  ");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public int totalSearchLandingArticle(SubmissionRequest submissionRequest)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(article.id) ");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 AND article.deleted = 0 AND master_data.lower_code = 'posted' ");
            sql.Append(" AND category.deleted = 0 ");
            if (submissionRequest != null)
            {
                if (!string.IsNullOrEmpty(submissionRequest.title))
                {
                    sql.Append("    AND article.title LIKE '%" + @submissionRequest.title + "%'");
                }
                if (submissionRequest.categoryID != -1)
                {
                    sql.Append("    AND article.categoryID = " + @submissionRequest.categoryID + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.account))
                {
                    sql.Append("    AND article.author LIKE '%" + @submissionRequest.account + "%'");
                }
                if (submissionRequest.status != -1)
                {
                    sql.Append("    AND article.status = " + @submissionRequest.status + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.lstArticlesId))
                {
                    sql.Append("    AND article.id  NOT IN (" + submissionRequest.lstArticlesId + ")");
                }
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("title", submissionRequest.title);
                    cmd.Parameters.AddWithValue("categoryID", submissionRequest.categoryID);
                    cmd.Parameters.AddWithValue("author", submissionRequest.account);
                    cmd.Parameters.AddWithValue("status", submissionRequest.status);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }
        public ArticleResponse getArticleById(int? articleId, string status)
        {
            string userRole = getLoggedInUserRole();
            bool isAuthor = isAuthorOfArticle(articleId);
            ArticleResponse newsResponse = null;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", article.introductionImage, article.relatedArticle,article.modifiedBy");
            sql.Append(", article.pinned");
            sql.Append(", user.first_name");
            sql.Append(", user.last_name");
            sql.Append(", master_data.name");
            sql.Append(" FROM article JOIN master_data ON article.status = master_data.id ");
            sql.Append(" JOIN user ON article.author = user.account ");
            sql.Append(" WHERE 1=1 AND clone = 0 ");
            if (articleId != null)
            {
                sql.Append("   AND article.id = " + @articleId + "");
            }
            if (!string.IsNullOrEmpty(status) && (!isAuthor && (userRole != "moderator" && userRole != "editor")))
            {
                sql.Append("    AND master_data.lower_code = '" + @status + "'");
            }
            if (userRole != "moderator" && userRole != "editor" )
            {
                sql.Append(" AND deleted = 0 ");
            }
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@articleId", articleId);
                    cmd.Parameters.AddWithValue("@status", status);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                newsResponse = new ArticleResponse();

                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.relatedArticle = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.modifiedBy = reader.GetString(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.pinned = reader.GetBoolean(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.firstName = reader.GetString(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.lastName = reader.GetString(17);
                                }
                                if (!reader.IsDBNull(18))
                                {
                                    newsResponse.statusName = reader.GetString(18);
                                }
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponse;
        }
        public Boolean isAuthorOfArticle(int? articleId)
        {
            string username = getLoggedInUsername();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id FROM article ");
            sql.Append(" WHERE 1=1 AND clone = 0 AND article.author = @username AND article.id = @articleId");
       
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("articleId", articleId);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                    }
                }
                con.Close();
            }
            return false;
        }

        public List<ArticleResponse> getRelatedArticlesByListId(List<int> articleIds)
        {
            List<ArticleResponse> listResult = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" a.id");
            sql.Append(", a.content");
            sql.Append(", a.categoryID");
            sql.Append(", a.approveDate");
            sql.Append(", a.author");
            sql.Append(", a.status");
            sql.Append(", a.totalViews");
            sql.Append(", a.createdTime");
            sql.Append(", a.modifiedTime");
            sql.Append(", a.deleted");
            sql.Append(", a.sapo");
            sql.Append(", a.title");
            sql.Append(", a.introductionImage, a.relatedArticle,a.modifiedBy,c.categoryName");
            sql.Append(", a.pinned");
            sql.Append(" FROM article a JOIN category c ON a.categoryID = c.id  " +
                " JOIN master_data ON a.status = master_data.id ");
            sql.Append(" WHERE 1=1 AND a.clone = 0  AND a.deleted = 0 AND master_data.lower_code = 'posted' ");
            sql.Append(" AND c.deleted = 0 ");
            if (articleIds != null)
            {
                sql.Append("   AND a.id IN  (" + String.Join(",", articleIds) + ")");


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
                                    ArticleResponse newsResponse = new ArticleResponse();

                                    if (!reader.IsDBNull(0))
                                    {
                                        newsResponse.id = reader.GetInt32(0);
                                    }
                                    if (!reader.IsDBNull(1))
                                    {
                                        newsResponse.content = reader.GetString(1);
                                    }
                                    if (!reader.IsDBNull(2))
                                    {
                                        newsResponse.categoryID = reader.GetInt32(2);
                                    }
                                    if (!reader.IsDBNull(3))
                                    {
                                        newsResponse.approveDate = reader.GetDateTime(3);
                                    }
                                    if (!reader.IsDBNull(4))
                                    {
                                        newsResponse.author = reader.GetString(4);
                                    }
                                    if (!reader.IsDBNull(5))
                                    {
                                        newsResponse.status = reader.GetInt32(5);
                                    }

                                    if (!reader.IsDBNull(6))
                                    {
                                        newsResponse.totalViews = reader.GetInt32(6);
                                    }
                                    if (!reader.IsDBNull(7))
                                    {
                                        newsResponse.createdTime = reader.GetDateTime(7);
                                    }
                                    if (!reader.IsDBNull(8))
                                    {
                                        newsResponse.modifiedTime = reader.GetDateTime(8);
                                    }
                                    if (!reader.IsDBNull(9))
                                    {
                                        newsResponse.deleted = reader.GetBoolean(9);
                                    }
                                    if (!reader.IsDBNull(10))
                                    {
                                        newsResponse.sapo = reader.GetString(10);
                                    }
                                    if (!reader.IsDBNull(11))
                                    {
                                        newsResponse.title = reader.GetString(11);
                                    }
                                    if (!reader.IsDBNull(12))
                                    {
                                        newsResponse.introductionImage = reader.GetInt32(12);
                                    }
                                    if (!reader.IsDBNull(13))
                                    {
                                        newsResponse.relatedArticle = reader.GetString(13);
                                    }
                                    if (!reader.IsDBNull(14))
                                    {
                                        newsResponse.modifiedBy = reader.GetString(14);
                                    }
                                    if (!reader.IsDBNull(15))
                                    {
                                        newsResponse.categoryName = reader.GetString(15);
                                    }
                                    if (!reader.IsDBNull(16))
                                    {
                                        newsResponse.pinned = reader.GetBoolean(16);
                                    }
                                    listResult.Add(newsResponse);
                                }
                            }
                        }
                    }
                    con.Close();
                }
            }
            return listResult;
        }

        public int totalsSearchRequest(SubmissionRequest submissionRequest)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT( DISTINCT article.id) ");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status  ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 ");
            if (submissionRequest != null)
            {
                if (!string.IsNullOrEmpty(submissionRequest.title))
                {
                    sql.Append("    AND LOWER(article.title) LIKE '%" + @submissionRequest.title.ToLower() + "%'");
                }
                if (submissionRequest.categoryID != -1)
                {
                    sql.Append("    AND article.categoryID = " + @submissionRequest.categoryID + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.account))
                {
                    sql.Append("    AND LOWER(article.author) LIKE '%" + @submissionRequest.account.ToLower() + "%'");
                }
                if (submissionRequest.status != -1)
                {
                    sql.Append("    AND article.status = " + @submissionRequest.status + "");
                }
            }
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("title", submissionRequest.title);
                    cmd.Parameters.AddWithValue("categoryID", submissionRequest.categoryID);
                    cmd.Parameters.AddWithValue("author", submissionRequest.account);
                    cmd.Parameters.AddWithValue("status", submissionRequest.status);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public int totalsSearchArticleManagement(SubmissionRequest submissionRequest)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT( DISTINCT article.id) ");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status  ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 ");
            sql.Append(" AND master_data.code <> 'DRAFT' ");
            if (submissionRequest != null)
            {
                if (!string.IsNullOrEmpty(submissionRequest.title))
                {
                    sql.Append("    AND LOWER(article.title) LIKE '%" + @submissionRequest.title.ToLower() + "%'");
                }
                if (submissionRequest.categoryID != -1)
                {
                    sql.Append("    AND article.categoryID = " + @submissionRequest.categoryID + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.account))
                {
                    sql.Append("    AND LOWER(article.author) LIKE '%" + @submissionRequest.account.ToLower() + "%'");
                }
                if (submissionRequest.status != -1)
                {
                    sql.Append("    AND article.status = " + @submissionRequest.status + "");
                }
            }
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("title", submissionRequest.title);
                    cmd.Parameters.AddWithValue("categoryID", submissionRequest.categoryID);
                    cmd.Parameters.AddWithValue("author", submissionRequest.account);
                    cmd.Parameters.AddWithValue("status", submissionRequest.status);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public int totalsSearch(int? newID, string title, int? categoryID, int? status)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(article.id) ");
            sql.Append(" FROM article ");
            sql.Append(" WHERE 1=1 ");
            if (newID != -1)
            {
                sql.Append("   AND id = " + @newID + "");
            }
            if (!string.IsNullOrEmpty(title))
            {
                sql.Append("   AND title = " + @title + "");
            }
            if (categoryID != -1)
            {
                sql.Append("   AND categoryID = " + @categoryID + "");
            }
            if (status != -1)
            {
                sql.Append("   AND status = " + @status + "");
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@newID", newID);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@categoryID", categoryID);
                    cmd.Parameters.AddWithValue("@status", status);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }
        public List<ArticleResponse> fullTextSearchArticle(string inputSearch, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage");
            sql.Append(", article.pinned");
            if (!string.IsNullOrEmpty(inputSearch))
            {
                sql.Append(", MATCH (fullTextContent) AGAINST ('" + @inputSearch.Trim().ToLower() + "*' IN BOOLEAN MODE) as score");
            }
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 ");
            sql.Append(" AND article.deleted = 0 ");
            sql.Append(" AND category.deleted = 0 ");
            sql.Append(" AND master_data.lower_code = 'posted'  ");

            if (!string.IsNullOrEmpty(inputSearch))
            {
                sql.Append(" AND MATCH (fullTextContent) AGAINST ('" + @inputSearch.Trim().ToLower() + "*' IN BOOLEAN MODE)");
                sql.Append(" ORDER BY score DESC ");
            }
            else{
                sql.Append(" ORDER BY article.approveDate DESC ");
            }

            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (!string.IsNullOrEmpty(inputSearch))
                    {
                        cmd.Parameters.AddWithValue("@inputSearch", inputSearch.Trim().ToLower());
                    }
                    cmd.Parameters.AddWithValue("startIndex", startIndex);
                    cmd.Parameters.AddWithValue("pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.pinned = reader.GetBoolean(15);
                                }

                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }
        public int totalsFullTextSearch(string inputSearch)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(*) ");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 AND  article.deleted = 0  AND master_data.lower_code = 'posted' ");
            sql.Append(" AND category.deleted = 0 ");
            if (!string.IsNullOrEmpty(inputSearch))
            {
                sql.Append(" AND MATCH (article.`fullTextContent`) AGAINST ('" + @inputSearch.Trim().ToLower() + "*' IN BOOLEAN MODE)");
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (!string.IsNullOrEmpty(inputSearch))
                    {
                        cmd.Parameters.AddWithValue("@inputSearch", inputSearch.Trim().ToLower());
                    }

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public ArticleResponse getOriginalArticleById(int? articleId)
        {
            ArticleResponse newsResponse = null;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", article.introductionImage, article.relatedArticle,article.modifiedBy");
            sql.Append(", article.pinned");
            sql.Append(" FROM article JOIN master_data ON article.status = master_data.id ");
            sql.Append(" WHERE clone = 1 ");
            if (articleId != null)
            {
                sql.Append("   AND article.id = " + @articleId + "");
            }
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@articleId", articleId);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                newsResponse = new ArticleResponse();

                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.relatedArticle = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.modifiedBy = reader.GetString(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.pinned = reader.GetBoolean(15);
                                }
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponse;
        }

        public bool UpdateStatusArticle(int articleID, int statusId, string modifiedBy)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append(" article ");
            sql.Append("    SET ");
            sql.Append(" status=");
            sql.Append(" @status,");
            sql.Append(" pinned = false,");
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
                    cmd.Parameters.AddWithValue("id", articleID);
                    cmd.Parameters.AddWithValue("status", statusId);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", modifiedBy);

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

        public List<ArticleResponse> searchArticleReviewedByAccount(SubmissionRequest submissionRequest, List<int> statusIDs, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage");
            sql.Append(", article_version.articleID");
            sql.Append(", (Select COUNT(da.article_id) FROM denounce_article da WHERE da.article_id = article.id AND da.deleted = false )");
            sql.Append(", article.pinned");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN article_version ");
            sql.Append("        ON article_version.oldArticleID = article.id ");
            sql.Append("    INNER JOIN review_article ra ON ra.articleId = article.id ");

            sql.Append(" WHERE 1=1 AND article.clone = 0 AND article.deleted = 0 ");
            sql.Append(" AND article.status IN (" + string.Join(",", @statusIDs) + ")");
            sql.Append(" AND ra.account = '" + @submissionRequest.reviewer + "'");
            if (submissionRequest != null)
            {
                if (!string.IsNullOrEmpty(submissionRequest.title))
                {
                    sql.Append("    AND LOWER(article.title) LIKE '%" + @submissionRequest.title.ToLower() + "%'");
                }
                if (submissionRequest.categoryID != -1)
                {
                    sql.Append("    AND article.categoryID = " + @submissionRequest.categoryID + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.account))
                {
                    sql.Append("    AND LOWER(article.author) LIKE '%" + @submissionRequest.account.ToLower() + "%'");
                }
                if (submissionRequest.status != -1)
                {
                    sql.Append("    AND article.status = " + @submissionRequest.status + "");
                }
            }
            sql.Append(" GROUP BY article.id ");
            sql.Append(" ORDER BY article.modifiedTime DESC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("title", submissionRequest.title);
                    cmd.Parameters.AddWithValue("categoryID", submissionRequest.categoryID);
                    cmd.Parameters.AddWithValue("author", submissionRequest.account);
                    cmd.Parameters.AddWithValue("status", submissionRequest.status);
                    cmd.Parameters.AddWithValue("startIndex", startIndex);
                    cmd.Parameters.AddWithValue("pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.originalArticleID = reader.GetInt32(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.numberReport = reader.GetInt64(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.pinned = reader.GetBoolean(17);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }


        public List<ArticleResponse> searchArticleManagementByAccountManager(SubmissionRequest submissionRequest, List<int> statusIDs, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage");
            sql.Append(", article_version.articleID");
            sql.Append(", (Select COUNT(da.article_id) FROM denounce_article da WHERE da.article_id = article.id AND da.deleted = false )");
            sql.Append(", article.pinned, article.modifiedBy");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN article_version ");
            sql.Append("        ON article_version.oldArticleID = article.id ");
            sql.Append("    INNER JOIN category cat ON cat.id = article.categoryID ");

            sql.Append(" WHERE 1=1 AND article.clone = 0 AND article.deleted = 0 ");
            sql.Append(" AND article.status IN (" + string.Join(",", @statusIDs) + ")");
            sql.Append(" AND cat.manager = '" + @submissionRequest.reviewer + "'");
            if (submissionRequest != null)
            {
                if (!string.IsNullOrEmpty(submissionRequest.title))
                {
                    sql.Append("    AND LOWER(article.title) LIKE '%" + @submissionRequest.title.ToLower() + "%'");
                }
                if (submissionRequest.categoryID != -1)
                {
                    sql.Append("    AND article.categoryID = " + @submissionRequest.categoryID + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.account))
                {
                    sql.Append("    AND LOWER(article.author) LIKE '%" + @submissionRequest.account.ToLower() + "%'");
                }
                if (submissionRequest.status != -1)
                {
                    sql.Append("    AND article.status = " + @submissionRequest.status + "");
                }
            }
            sql.Append(" GROUP BY article.id ");
            sql.Append(" ORDER BY article.modifiedTime DESC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("title", submissionRequest.title);
                    cmd.Parameters.AddWithValue("categoryID", submissionRequest.categoryID);
                    cmd.Parameters.AddWithValue("author", submissionRequest.account);
                    cmd.Parameters.AddWithValue("status", submissionRequest.status);
                    cmd.Parameters.AddWithValue("startIndex", startIndex);
                    cmd.Parameters.AddWithValue("pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.originalArticleID = reader.GetInt32(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.numberReport = reader.GetInt64(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.pinned = reader.GetBoolean(17);
                                }
                                if (!reader.IsDBNull(18))
                                {
                                    newsResponse.modifiedBy = reader.GetString(18);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }

        public int totalsArticleReviewedByAccount(SubmissionRequest submissionRequest, List<int> statusIDs)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(DISTINCT article.id) ");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status  ");
            sql.Append("    INNER JOIN review_article ra ON ra.articleId = article.id ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 AND article.deleted = 0  ");
            sql.Append(" AND article.status IN (" + string.Join(",", @statusIDs) + ")");
            sql.Append(" AND ra.account = '" + @submissionRequest.reviewer + "'");
            if (submissionRequest != null)
            {
                if (!string.IsNullOrEmpty(submissionRequest.title))
                {
                    sql.Append("    AND LOWER(article.title) LIKE '%" + @submissionRequest.title.ToLower() + "%'");
                }
                if (submissionRequest.categoryID != -1)
                {
                    sql.Append("    AND article.categoryID = " + @submissionRequest.categoryID + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.account))
                {
                    sql.Append("    AND LOWER(article.author) LIKE '%" + @submissionRequest.account.ToLower() + "%'");
                }
                if (submissionRequest.status != -1)
                {
                    sql.Append("    AND article.status = " + @submissionRequest.status + "");
                }
            }
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("title", submissionRequest.title);
                    cmd.Parameters.AddWithValue("categoryID", submissionRequest.categoryID);
                    cmd.Parameters.AddWithValue("author", submissionRequest.account);
                    cmd.Parameters.AddWithValue("status", submissionRequest.status);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public int totalsArticleManagementByAccountManager(SubmissionRequest submissionRequest, List<int> statusIDs)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(DISTINCT article.id) ");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status  ");
            sql.Append("    INNER JOIN category cat ON cat.id = article.categoryID ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 AND article.deleted = 0  ");
            sql.Append(" AND article.status IN (" + string.Join(",", @statusIDs) + ")");
            sql.Append(" AND cat.manager = '" + @submissionRequest.reviewer + "'");
            if (submissionRequest != null)
            {
                if (!string.IsNullOrEmpty(submissionRequest.title))
                {
                    sql.Append("    AND LOWER(article.title) LIKE '%" + @submissionRequest.title.ToLower() + "%'");
                }
                if (submissionRequest.categoryID != -1)
                {
                    sql.Append("    AND article.categoryID = " + @submissionRequest.categoryID + "");
                }
                if (!string.IsNullOrEmpty(submissionRequest.account))
                {
                    sql.Append("    AND LOWER(article.author) LIKE '%" + @submissionRequest.account.ToLower() + "%'");
                }
                if (submissionRequest.status != -1)
                {
                    sql.Append("    AND article.status = " + @submissionRequest.status + "");
                }
            }
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("title", submissionRequest.title);
                    cmd.Parameters.AddWithValue("categoryID", submissionRequest.categoryID);
                    cmd.Parameters.AddWithValue("author", submissionRequest.account);
                    cmd.Parameters.AddWithValue("status", submissionRequest.status);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public bool updateStatusPinArticle(int articleID, string modifiedBy, bool isPinned)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append(" article ");
            sql.Append("    SET ");
            sql.Append(" pinned = @isPinned,");
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
                    cmd.Parameters.AddWithValue("id", articleID);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", modifiedBy);
                    cmd.Parameters.AddWithValue("isPinned", isPinned);

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

        public bool unpinAllArticleByCategoryID(string modifiedBy, int categoryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append(" article ");
            sql.Append("    SET ");
            sql.Append(" pinned = false,");
            sql.Append(" modifiedTime=");
            sql.Append(" @modifiedTime,");
            sql.Append(" modifiedBy=");
            sql.Append(" @modifiedBy");
            sql.Append(" WHERE pinned = true ");
            sql.Append(" AND categoryID = @categoryID ");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", modifiedBy);
                    cmd.Parameters.AddWithValue("categoryID", categoryID);

                    var result = cmd.ExecuteNonQuery();
                    if (result < 1)
                    {
                        return false;
                    }
                }
                con.Close();
            }
            return true;
        }

        public List<ArticleResponse> getAllPinnedArticle(string statusCode)
        {
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage");
            sql.Append(", article_version.articleID");
            sql.Append(", (Select COUNT(da.article_id) FROM denounce_article da WHERE da.article_id = article.id AND da.deleted = false )");
            sql.Append(", article.pinned");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN article_version ");
            sql.Append("        ON article_version.oldArticleID = article.id ");
            sql.Append("    INNER JOIN review_article ra ON ra.articleId = article.id ");

            sql.Append(" WHERE 1=1 AND article.clone = 0 AND article.deleted = 0 AND article.pinned = true ");
            sql.Append(" AND LOWER(master_data.lower_code) = @status ");

            sql.Append(" GROUP BY article.id ");
            sql.Append(" ORDER BY article.modifiedTime DESC ");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("status", statusCode.ToLower());

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.originalArticleID = reader.GetInt32(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.numberReport = reader.GetInt64(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.pinned = reader.GetBoolean(17);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }

        public bool updateStatusArticle(int id, string username, int statusID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append(" article ");
            sql.Append("    SET ");
            if (statusID > 0)
            {
                sql.Append(" status=");
                sql.Append(" @status,");
            }
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
                    cmd.Parameters.AddWithValue("status", statusID);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", username);

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

        public List<ArticleResponse> getPinnedArticleByCategory(string statusCode, int categoryID)
        {
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", category.categoryName as categoryName ");
            sql.Append(", master_data.name as statusName ");
            sql.Append(", article.introductionImage");
            sql.Append(", article_version.articleID");
            sql.Append(", (Select COUNT(da.article_id) FROM denounce_article da WHERE da.article_id = article.id AND da.deleted = false )");
            sql.Append(", article.pinned");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN article_version ");
            sql.Append("        ON article_version.oldArticleID = article.id ");
            sql.Append("    INNER JOIN review_article ra ON ra.articleId = article.id ");

            sql.Append(" WHERE 1=1 AND article.clone = 0 AND article.deleted = 0 AND article.pinned = true ");
            sql.Append(" AND LOWER(master_data.lower_code) = @status ");
            sql.Append(" AND article.categoryID = @categoryID ");

            sql.Append(" GROUP BY article.id ");
            sql.Append(" ORDER BY article.modifiedTime DESC ");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("status", statusCode.ToLower());
                    cmd.Parameters.AddWithValue("categoryID", categoryID);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.categoryName = reader.GetString(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.statusName = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.originalArticleID = reader.GetInt32(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.numberReport = reader.GetInt64(16);
                                }
                                if (!reader.IsDBNull(17))
                                {
                                    newsResponse.pinned = reader.GetBoolean(17);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }
        public List<ArticleResponse> searchRelatedArticle(string title)
        {
            List<ArticleResponse> newsResponses = new List<ArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.title");
            sql.Append(", article.introductionImage ");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append(" WHERE article.clone = 0 AND  article.deleted = 0 AND master_data.lower_code = 'posted' ");
            if (!string.IsNullOrEmpty(title))
            {
                sql.Append("    AND LOWER(article.title) LIKE '%" + @title + "%'");
            }
            sql.Append(" GROUP BY article.id ORDER BY article.approveDate DESC ");
            sql.Append("    LIMIT 0,10 ");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (!string.IsNullOrEmpty(title))
                    {
                        cmd.Parameters.AddWithValue("title", title.ToLower().Trim());
                    }
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ArticleResponse newsResponse = new ArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.title = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(2);
                                }
                                newsResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponses;
        }

        public ArticleResponse getArticleOptimizeByOldId(int articleId)
        {
            ArticleResponse newsResponse = null;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" article.id");
            sql.Append(", article.content");
            sql.Append(", article.categoryID");
            sql.Append(", article.approveDate");
            sql.Append(", article.author");
            sql.Append(", article.status");
            sql.Append(", article.totalViews");
            sql.Append(", article.createdTime");
            sql.Append(", article.modifiedTime");
            sql.Append(", article.deleted");
            sql.Append(", article.sapo");
            sql.Append(", article.title");
            sql.Append(", article.introductionImage, article.relatedArticle,article.modifiedBy");
            sql.Append(", article.pinned");
            sql.Append(" FROM article JOIN master_data ON article.status = master_data.id ");
            sql.Append(" JOIN article_version ON article_version.oldArticleID = article.id ");
            sql.Append(" WHERE clone = 0 ");
            if (articleId > 0)
            {
                sql.Append("   AND article_version.articleID = " + @articleId + "");
            }
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@articleId", articleId);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                newsResponse = new ArticleResponse();

                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.content = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.approveDate = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.author = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.status = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.totalViews = reader.GetInt32(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createdTime = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.modifiedTime = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleted = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.sapo = reader.GetString(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.title = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.introductionImage = reader.GetInt32(12);
                                }
                                if (!reader.IsDBNull(13))
                                {
                                    newsResponse.relatedArticle = reader.GetString(13);
                                }
                                if (!reader.IsDBNull(14))
                                {
                                    newsResponse.modifiedBy = reader.GetString(14);
                                }
                                if (!reader.IsDBNull(15))
                                {
                                    newsResponse.pinned = reader.GetBoolean(15);
                                }
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponse;
        }
    }
}