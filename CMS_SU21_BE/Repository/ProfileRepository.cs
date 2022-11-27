using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;

namespace CMS_SU21_BE.Repository
{
    public class ProfileRepository
    {
        public int countProfileArticle(string status, string username)
        {
            int result;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" COUNT(article.id)");
            sql.Append(" FROM article ");
            sql.Append(" JOIN master_data ");
            sql.Append(" ON master_data.id = article.status  ");
            sql.Append(" WHERE article.deleted = 0 AND  article.clone = 0 ");
            sql.Append(" AND article.author = @username ");

            if (!string.IsNullOrEmpty(status))
            {
                sql.Append("AND master_data.lower_code = '"+@status+"'");

            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("@status", status);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }
        public int countProfileComments( string username)
        {
            int result;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" COUNT( DISTINCT comment.id)");
            sql.Append(" FROM article ");
            sql.Append(" JOIN master_data ");
            sql.Append(" ON master_data.id = article.status  ");
            sql.Append(" JOIN comment ON  comment.articleID = article.id ");
            sql.Append(" WHERE article.deleted = 0 AND  article.clone = 0  AND master_data.lower_code = 'posted' ");
            sql.Append(" AND article.author = @username ");

  
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("username", username);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

    }
}