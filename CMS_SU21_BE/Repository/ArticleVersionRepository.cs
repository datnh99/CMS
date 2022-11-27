using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace CMS_SU21_BE.Repository
{
    public class ArticleVersionRepository
    {
        public int AddNew(int oldArticleID, int articleID, string account)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("article_version");
            sql.Append("(");
            sql.Append("    createdTime,");
            sql.Append("    createdBy,");
            sql.Append("    modifiedTime,");
            sql.Append("    modifiedBy,");
            sql.Append("    articleID,");
            sql.Append("    oldArticleID ");

            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @createdTime,");
            sql.Append("    @createdBy,");
            sql.Append("    @createdTime,");
            sql.Append("    @createdBy,");
            sql.Append("    @articleID,");
            sql.Append("    @oldArticleID");
            sql.Append(");");
            sql.Append("    SELECT LAST_INSERT_ID();");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("createdTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("createdBy", account);
                    cmd.Parameters.AddWithValue("articleID", articleID);
                    cmd.Parameters.AddWithValue("oldArticleID", oldArticleID);
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }
    }
}