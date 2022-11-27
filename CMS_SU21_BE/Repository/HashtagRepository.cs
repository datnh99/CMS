using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CMS_SU21_BE.Repository
{
    public class HashtagRepository
    {
        public bool Add(HashtagRequest request)
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
                for (int i = 0; i < request.tagContent.Count; i++)
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("article_id", request.id);
                        cmd.Parameters.AddWithValue("tag_content", request.tagContent[i]);
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

        public List<HashtagResponse> GetHashtag(int? newID)
        {
            List<HashtagResponse> hashtags = new List<HashtagResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append("article_id");
            sql.Append(",tag_content");
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
                                HashtagResponse hashtag = new HashtagResponse
                                {
                                    articleId = reader.GetInt32(0),
                                    tagContent = reader.GetString(1),

                                };

                                hashtags.Add(hashtag);
                            }
                        }
                    }
                }
                con.Close();
            }
            return hashtags;
        }
        public List<HashtagResponse> getPopularTags()
        {
            List<HashtagResponse> hashtags = new List<HashtagResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append("tag_content,COUNT(DISTINCT article_id) as total");
            sql.Append(" FROM hash_tag ");
            sql.Append(" GROUP BY tag_content ORDER BY total DESC LIMIT 0,6");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                HashtagResponse hashtag = new HashtagResponse();
                                if (!reader.IsDBNull(0))
                                {
                                    hashtag.tagContent = reader.GetString(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    hashtag.totalArticle = reader.GetInt32(1);
                                }
                                hashtags.Add(hashtag);
                            }
                        }
                    }
                }
                con.Close();
            }
            return hashtags;
        }
    }
}