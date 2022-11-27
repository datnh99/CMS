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
    public class ReviewArticleRepository
    {
        public bool addReviewArticle(AddReviewArticleRequest request)
        {
            var result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("review_article");
            sql.Append("(");
            sql.Append("    createdBy,");
            sql.Append("    createdTime,");
            sql.Append("    modifiedTime,");
            sql.Append("    modifiedBy,");
            sql.Append("    articleId,");
            sql.Append("    account,");
            sql.Append("    approve,");
            sql.Append("    note");
            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @createdBy,");
            sql.Append("    @createdTime,");
            sql.Append("    @modifiedTime,");
            sql.Append("    @modifiedBy,");
            sql.Append("    @articleId,");
            sql.Append("    @account,");
            sql.Append("    @approve,");
            sql.Append("    @note");
            sql.Append(")");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@createdBy", request.account);
                    cmd.Parameters.AddWithValue("@createdTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@modifiedBy", request.account);
                    cmd.Parameters.AddWithValue("@articleId", request.articleId);
                    cmd.Parameters.AddWithValue("@account", request.account);
                    cmd.Parameters.AddWithValue("@approve", request.confirm);
                    cmd.Parameters.AddWithValue("@note", request.note);

                    result = cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            return result == 1;
        }

        public ReviewArticleResponse existRequestReviewArticle(int newID, string account)
        {
            ReviewArticleResponse newsResponse = new ReviewArticleResponse();
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT ra.articleId, ra.account  FROM review_article ra  WHERE ra.articleId = @newID");
            sql.Append(" AND ra.account = @account ");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@newID", newID);
                    cmd.Parameters.AddWithValue("@account", account);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.newID = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.account = reader.GetString(1);
                                }
                            }
                        }
                    }
                }
                con.Close();
            }
            return newsResponse;
        }

        public int countRequestReviewArticle(ReviewArticleRequest reviewArticleRequest, List<int> listStatus)
        {
            int total = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT( DISTINCT n.id) ");
            sql.Append(" FROM article n ");
            sql.Append(" 	JOIN master_data md on n.status = md.id ");
            sql.Append("		JOIN category cat ON cat.id = n.categoryID ");
            sql.Append("		    LEFT JOIN article_version av ON av.oldArticleID = n.id ");
            sql.Append(" WHERE 1 = 1  ");
            sql.Append(" AND md.act_flg = 1 ");
            if (!string.IsNullOrEmpty(reviewArticleRequest.roleCode) && !reviewArticleRequest.roleCode.ToLower().Equals("editor"))
            {
                sql.Append(" AND cat.manager =  '" + @reviewArticleRequest.reviewer + "'");
            }
            sql.Append(" AND cat.deleted = 0 ");
            sql.Append(" AND md.id IN (" + string.Join(",", listStatus) + ") ");
            sql.Append(" AND n.clone != 1 ");
            sql.Append(" AND n.deleted = 0 ");
            if (reviewArticleRequest.statusID > 0)
            {
                sql.Append("    AND md.id = " + @reviewArticleRequest.statusID);
            }
            if (reviewArticleRequest.newID > 0)
            {
                sql.Append("    AND n.id = " + @reviewArticleRequest.newID);
            }
            if (!string.IsNullOrEmpty(reviewArticleRequest.title))
            {
                sql.Append("    AND n.title LIKE '%" + @reviewArticleRequest.title + "%'");
            }
            if (reviewArticleRequest.categoryID > 0)
            {
                sql.Append("    AND cat.id = '" + @reviewArticleRequest.categoryID + "'");
            }
            if (!string.IsNullOrEmpty(reviewArticleRequest.account))
            {
                sql.Append("    AND n.author LIKE '%" + @reviewArticleRequest.account + "%'");
            }
            if (reviewArticleRequest.statusID > 0)
            {
                sql.Append("    AND md.id = '" + @reviewArticleRequest.statusID + "'");
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;

                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return total;
        }

        public List<ReviewArticleResponse> searchRequestReviewArticle(ReviewArticleRequest reviewArticleRequest, List<int> listStatus, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<ReviewArticleResponse> reviewArticleResponses = new List<ReviewArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT n.id, n.title, n.categoryID, cat.categoryName, n.author, n.status AS statusID,  md.name AS statusName,  n.createdTime, n.modifiedTime, n.deleted, av.articleID, ra.account AS reviewer ");
            sql.Append(" FROM article n ");
            sql.Append(" 	JOIN master_data md on n.status = md.id ");
            sql.Append("		JOIN category cat ON cat.id = n.categoryID ");
            sql.Append("		    LEFT JOIN article_version av ON av.oldArticleID = n.id ");
            sql.Append("		    LEFT JOIN review_article ra ON ra.articleId = n.id ");
            sql.Append(" WHERE 1 = 1  ");
            sql.Append(" AND md.act_flg = 1 ");
            if(!string.IsNullOrEmpty(reviewArticleRequest.roleCode) && !reviewArticleRequest.roleCode.ToLower().Equals("editor")) { 
                sql.Append(" AND cat.manager =  '" + @reviewArticleRequest.reviewer + "'");
            }
            sql.Append(" AND cat.deleted = 0 ");
            sql.Append(" AND md.id IN (" + string.Join(",", listStatus) + ") ");
            sql.Append(" AND n.clone != 1 ");
            sql.Append(" AND n.deleted = 0 "); 
            if (reviewArticleRequest.statusID > 0)
            {
                sql.Append("    AND md.id = " + @reviewArticleRequest.statusID);
            }
            if (reviewArticleRequest.newID > 0)
            {
                sql.Append("    AND n.id = " + @reviewArticleRequest.newID);
            }
            if (!string.IsNullOrEmpty(reviewArticleRequest.title))
            {
                sql.Append("    AND n.title LIKE '%" + @reviewArticleRequest.title + "%'");
            }
            if (reviewArticleRequest.categoryID > 0)
            {
                sql.Append("    AND cat.id = '" + @reviewArticleRequest.categoryID + "'");
            }
            if (!string.IsNullOrEmpty(reviewArticleRequest.account))
            {
                sql.Append("    AND n.author LIKE '%" + @reviewArticleRequest.account + "%'");
            }
            if (reviewArticleRequest.statusID > 0)
            {
                sql.Append("    AND md.id = '" + @reviewArticleRequest.statusID + "'");
            }
            sql.Append(" group by n.id ");
            sql.Append(" ORDER BY n.modifiedTime DESC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    if (reviewArticleRequest.newID > 0)
                    {
                        cmd.Parameters.AddWithValue("newID", reviewArticleRequest.newID);
                    }
                    cmd.Parameters.AddWithValue("title", reviewArticleRequest.title);
                    cmd.Parameters.AddWithValue("categoryName", reviewArticleRequest.categoryName);
                    cmd.Parameters.AddWithValue("account", reviewArticleRequest.account);
                    cmd.Parameters.AddWithValue("statusName", reviewArticleRequest.statusName);
                    cmd.Parameters.AddWithValue("reviewer", reviewArticleRequest.reviewer);
                    cmd.Parameters.AddWithValue("startIndex", startIndex);
                    cmd.Parameters.AddWithValue("pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ReviewArticleResponse newsResponse = new ReviewArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.newID = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.title = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.categoryName = reader.GetString(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.account = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.statusID = reader.GetInt32(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.statusName = reader.GetString(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createDate = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.updateDate = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleteFlag = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.originalArticleID = reader.GetInt32(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.reviewer = reader.GetString(11);
                                }
                                reviewArticleResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return reviewArticleResponses;
        }

        public bool updateReviewArticle(AddReviewArticleRequest addReviewArticleRequest)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append("review_article");
            sql.Append("    SET ");
            sql.Append(" modifiedTime=");
            sql.Append(" @modifiedTime,");
            sql.Append(" modifiedBy=");
            sql.Append(" @modifiedBy,");
            sql.Append(" approve=");
            sql.Append(" @approve,");
            sql.Append(" note=");
            sql.Append(" @note ");
            sql.Append(" WHERE articleId = @articleId ");
            sql.Append(" AND account = @account ");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", addReviewArticleRequest.account);
                    cmd.Parameters.AddWithValue("approve", addReviewArticleRequest.confirm);
                    cmd.Parameters.AddWithValue("note", addReviewArticleRequest.note);
                    cmd.Parameters.AddWithValue("articleId", addReviewArticleRequest.articleId);
                    cmd.Parameters.AddWithValue("account", addReviewArticleRequest.account);
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

        public List<ReviewArticleResponse> getArticleReviewed(int articleID)
        {
            List<ReviewArticleResponse> reviewArticleResponses = new List<ReviewArticleResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT n.id, n.title, n.categoryID, cat.categoryName, n.author, n.status AS statusID,  md.name AS statusName,  n.createdTime, n.modifiedTime, n.deleted, av.articleID, ra.account AS reviewer, ra.note as note");
            sql.Append(" FROM article n ");
            sql.Append(" 	JOIN master_data md on n.status = md.id ");
            sql.Append("		JOIN category cat ON cat.id = n.categoryID ");
            sql.Append("		    LEFT JOIN article_version av ON av.oldArticleID = n.id ");
            sql.Append("		    JOIN review_article ra ON ra.articleId = n.id ");
            sql.Append(" WHERE 1 = 1  ");
            sql.Append(" AND md.act_flg = 1 ");
            sql.Append(" AND cat.deleted = 0 ");
            sql.Append(" AND n.clone != 1 ");
            sql.Append(" AND n.deleted = 0 ");
            sql.Append("    AND ra.articleId = @articleID ");
            sql.Append(" group by ra.id ");
            sql.Append(" ORDER BY n.modifiedTime DESC ");

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
                                ReviewArticleResponse newsResponse = new ReviewArticleResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    newsResponse.newID = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    newsResponse.title = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    newsResponse.categoryID = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    newsResponse.categoryName = reader.GetString(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    newsResponse.account = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    newsResponse.statusID = reader.GetInt32(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    newsResponse.statusName = reader.GetString(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    newsResponse.createDate = reader.GetDateTime(7);
                                }
                                if (!reader.IsDBNull(8))
                                {
                                    newsResponse.updateDate = reader.GetDateTime(8);
                                }
                                if (!reader.IsDBNull(9))
                                {
                                    newsResponse.deleteFlag = reader.GetBoolean(9);
                                }
                                if (!reader.IsDBNull(10))
                                {
                                    newsResponse.originalArticleID = reader.GetInt32(10);
                                }
                                if (!reader.IsDBNull(11))
                                {
                                    newsResponse.reviewer = reader.GetString(11);
                                }
                                if (!reader.IsDBNull(12))
                                {
                                    newsResponse.note = reader.GetString(12);
                                }
                                reviewArticleResponses.Add(newsResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return reviewArticleResponses;
        }
    }
}