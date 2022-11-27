using CMS_SU21_BE.Models.Responses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CMS_SU21_BE.Repository
{
    public class DataReportRepository
    {
        public List<ArticleResponse> getTopArticles(DateTime? fromDate, DateTime? toDate,Boolean export)
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
            sql.Append(", COUNT(comment.id)");
            sql.Append(", user.avatar");
            sql.Append(" FROM article ");
            sql.Append("    INNER JOIN category ");
            sql.Append("        ON category.id = article.categoryID ");
            sql.Append("    INNER JOIN master_data ");
            sql.Append("        ON master_data.id = article.status ");
            sql.Append("    LEFT JOIN comment ");
            sql.Append("        ON article.id = comment.articleID ");
            sql.Append("    LEFT JOIN user  ");
            sql.Append("        ON user.account = article.author ");
            sql.Append(" WHERE 1=1 AND article.clone = 0 AND  article.deleted = 0 AND master_data.lower_code = 'posted' ");
            sql.Append("  AND  category.deleted = 0 ");
            if (fromDate.HasValue && fromDate != DateTime.MinValue)
            {
                sql.Append(" AND article.approveDate >=  @fromDate");
            }
            if (toDate.HasValue && toDate != DateTime.MinValue)
            {
                sql.Append(" AND article.approveDate <=  @toDate");
            }
            sql.Append(" GROUP BY article.id  ");
            sql.Append(" ORDER BY article.totalViews DESC ");
            if (!export)
            {
                sql.Append(" LIMIT 0,9 ");
            }
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("fromDate", fromDate);
                    cmd.Parameters.AddWithValue("toDate", toDate);
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
                                    newsResponse.totalComment = reader.GetInt32(15);
                                }
                                if (!reader.IsDBNull(16))
                                {
                                    newsResponse.authorAvatar = reader.GetInt32(16);
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
        public List<CategoryReportResponse> dataReportCategory(DateTime? fromDate, DateTime? toDate)
        {
            List<CategoryReportResponse> listResult = new List<CategoryReportResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT cat.`categoryName`,COUNT(DISTINCT art.`id`) AS totalArticles FROM `article` art ");
            sql.Append(" JOIN `category` cat ON art.`categoryID` = `cat`.`id` ");
            sql.Append(" JOIN `master_data` tmd ON art.`status` AND tmd.id ");
            sql.Append(" WHERE art.`clone` = 0 AND tmd.`lower_code` NOT IN ('reported','deleted','draft') AND art.`deleted` = 0 ");
            if (fromDate.HasValue && fromDate != DateTime.MinValue)
            {
                sql.Append(" AND art.createdTime >=  @fromDate");

            }
            if (toDate.HasValue && toDate != DateTime.MinValue)
            {
                sql.Append(" AND art.createdTime <=  @toDate");

            }
            sql.Append(" GROUP BY cat.`id` ORDER BY totalArticles DESC");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("fromDate", fromDate);
                    cmd.Parameters.AddWithValue("toDate", toDate);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                CategoryReportResponse categoryReportResponse = new CategoryReportResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    categoryReportResponse.categoryName = reader.GetString(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    categoryReportResponse.totalArticles = reader.GetInt32(1);
                                }

                                listResult.Add(categoryReportResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return listResult;
        }
        public List<TopWriterResponse> getTopWriter(DateTime? fromDate, DateTime? toDate)
        {
            List<TopWriterResponse> listResult = new List<TopWriterResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(DISTINCT art.id) AS totalArticle,");
            sql.Append("COUNT(DISTINCT urf.`follower`) AS totalFollower,");
            sql.Append("SUM(art.`totalViews`) AS totalViews,");
            sql.Append("u.`account`,");
            sql.Append("u.`first_name`,");
            sql.Append("u.`last_name`,");
            sql.Append("u.`avatar`");
            sql.Append("FROM `user` u JOIN `article` art ON u.`account` = art.`author` ");
            sql.Append("LEFT JOIN `user_follow` urf ON u.`account` = urf.`account` ");
            sql.Append("JOIN `master_data` tmd ON tmd.`id` = art.`status`");
            sql.Append("WHERE `art`.`deleted` = 0 AND tmd.`lower_code` = 'posted'");

            if (fromDate.HasValue && fromDate != DateTime.MinValue)
            {
                sql.Append(" AND art.createdTime >=  @fromDate");
            }
            if (toDate.HasValue && toDate != DateTime.MinValue)
            {
                sql.Append(" AND art.createdTime <=  @toDate");

            }
            sql.Append(" GROUP BY u.`id` ORDER BY totalArticle DESC");
            sql.Append(" LIMIT 0,5 ");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("fromDate", fromDate);
                    cmd.Parameters.AddWithValue("toDate", toDate);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                TopWriterResponse topWriterResponse = new TopWriterResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    topWriterResponse.totalArticle = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    topWriterResponse.totalFollower = reader.GetInt32(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    topWriterResponse.totalViews = reader.GetInt32(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    topWriterResponse.account = reader.GetString(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    topWriterResponse.firstName = reader.GetString(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    topWriterResponse.lastName = reader.GetString(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    topWriterResponse.avatar = reader.GetInt32(6);
                                }
                                listResult.Add(topWriterResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return listResult;
        }
        public List<TimelineResponse> dataTimelineReport(DateTime? fromDate, DateTime? toDate,string viewType)
        {
            List<TimelineResponse> listResult = new List<TimelineResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append(" SELECT DATE_FORMAT(art.`createdTime`,'"+ viewType+"') AS times,COUNT(DISTINCT art.id) FROM `article` art ");
            sql.Append(" JOIN `master_data` tmd ON art.`status` = tmd.`id`");
            sql.Append(" AND tmd.`lower_code` NOT IN ('rejected','draft')");
            sql.Append("WHERE art.`deleted` = 0 ");

            if (fromDate.HasValue && fromDate != DateTime.MinValue)
            {
                sql.Append(" AND art.createdTime >=  @fromDate");

            }
            if (toDate.HasValue && toDate != DateTime.MinValue)
            {
                sql.Append(" AND art.createdTime <=  @toDate");

            }
            sql.Append(" GROUP BY times ORDER BY times ASC");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("fromDate", fromDate);
                    cmd.Parameters.AddWithValue("toDate", toDate);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                TimelineResponse timelineResponse = new TimelineResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    timelineResponse.time = reader.GetString(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    timelineResponse.totalArticles = reader.GetInt32(1);
                                }

                                listResult.Add(timelineResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return listResult;
        }
    }
}