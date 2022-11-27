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
    public class CategoryRepository
    {
        public List<CategoryResponse> Search(string categoryName, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<CategoryResponse> categoryResponses = new List<CategoryResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" cat.id ");
            sql.Append(", cat.categoryName ");
            sql.Append(", cat.createdTime ");
            sql.Append(", cat.modifiedTime ");
            sql.Append(", cat.deleted ");
            sql.Append(", cat.modifiedBy ");
            sql.Append(", cat.createdBy ");
            sql.Append(", cat.introductionImage ");
            //sql.Append(", cat.categoryCode ");
            sql.Append(", COUNT(art.id) ");
            sql.Append(" FROM category cat ");
            sql.Append(" LEFT JOIN article art ON art.categoryID = cat.id  ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(categoryName))
            {
                sql.Append("    AND cat.categoryName LIKE '%" + @categoryName + "%'");
            }
            sql.Append(" GROUP BY cat.id ");
            sql.Append(" ORDER BY cat.modifiedTime DESC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@categoryName", categoryName);
                    cmd.Parameters.AddWithValue("@startIndex", startIndex);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                CategoryResponse categoryResponse = new CategoryResponse
                                {
                                    id = reader.GetInt32(0),
                                    categoryName = reader.GetString(1),
                                    createdTime = reader.GetDateTime(2),
                                    modifiedTime = reader.GetDateTime(3),
                                    deleted = reader.GetInt32(4),
                                    modifiedBy = reader.GetString(5),
                                    createdBy = reader.GetString(6),
                                    introductionImage = reader.GetInt32(7),
                                    totalArticle = reader.GetInt32(8)
                                };

                                categoryResponses.Add(categoryResponse);
                            }
                        }
                    }
                    con.Close();
                }
            }
            return categoryResponses;
        }
        public int totalCategory()
        {
            int result;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" COUNT(*)");
            sql.Append(" FROM category ");
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

        public List<CategoryResponse> SearchByNameAndManager(string categoryName, string username, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<CategoryResponse> categoryResponses = new List<CategoryResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" category.id ");
            sql.Append(", category.categoryName ");
            sql.Append(", category.createdTime ");
            sql.Append(", category.modifiedTime ");
            sql.Append(", category.deleted ");
            sql.Append(", category.modifiedBy ");
            sql.Append(", category.createdBy ");
            sql.Append(", category.manager ");
            //sql.Append(", category.categoryCode ");
            sql.Append(" FROM category ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(categoryName))
            {
                sql.Append("    AND category.categoryName LIKE '%" + @categoryName + "%'");

            }
            if (!string.IsNullOrEmpty(username))
            {
                sql.Append("    AND category.manager = '" + @username + "'");

            }
            sql.Append("    ORDER BY category.categoryName ASC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();

                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@categoryName", categoryName);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@startIndex", startIndex);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                CategoryResponse categoryResponse = new CategoryResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    categoryResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    categoryResponse.categoryName = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    categoryResponse.createdTime = reader.GetDateTime(2);
                                } 
                                if (!reader.IsDBNull(3))
                                {
                                    categoryResponse.modifiedTime = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    categoryResponse.deleted = reader.GetInt32(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    categoryResponse.modifiedBy = reader.GetString(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    categoryResponse.createdBy = reader.GetString(6);
                                }
                                if (!reader.IsDBNull(7))
                                {
                                    categoryResponse.manager = reader.GetString(7);
                                }
                                //if (!reader.IsDBNull(8))
                                //{
                                //    categoryResponse.categoryCode = reader.GetString(8);
                                //}

                                categoryResponses.Add(categoryResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return categoryResponses;
        }

        public int countByNameAndManager(string categoryName, string username)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(DISTINCT category.id) ");
            sql.Append(" FROM category ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(categoryName))
            {
                sql.Append("    AND category.categoryName LIKE '%" + @categoryName + "%'");
            }
            if (!string.IsNullOrEmpty(username))
            {
                sql.Append("    AND category.manager = '" + @username + "'");

            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();

                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@categoryName", categoryName);
                    cmd.Parameters.AddWithValue("@username", username);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public int Add(CategoryRequest request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("category");
            sql.Append("(");
            sql.Append("    categoryName,");
            sql.Append("    createdTime,");
            sql.Append("    deleted,");
            sql.Append("    modifiedBy,");
            sql.Append("    modifiedTime,");
            sql.Append("    createdBy,");
            //sql.Append("    categoryCode,");
            sql.Append("    introductionImage");
            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @categoryName,");
            sql.Append("    @createdTime,");
            sql.Append("    @deleted,");
            sql.Append("    @modifiedBy,");
            sql.Append("    @modifiedTime,");
            sql.Append("    @createdBy,");
            //sql.Append("    @categoryCode,");
            sql.Append("    @introductionImage");
            sql.Append(");");
            sql.Append("    SELECT LAST_INSERT_ID();");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("createdBy", request.createdBy);
                    cmd.Parameters.AddWithValue("createdTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", request.createdBy);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("deleted", 0);
                    cmd.Parameters.AddWithValue("categoryName", request.categoryName);
                    //cmd.Parameters.AddWithValue("categoryCode", request.categoryCode);
                    cmd.Parameters.AddWithValue("introductionImage", request.introductionImage);

                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return 1;
        }

        public bool updateCategory(CategoryRequest request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append("category");
            sql.Append("    SET ");
            if(!string.IsNullOrEmpty(request.categoryName)) { 
                sql.Append(" categoryName =");
                sql.Append(" @categoryName,");
            }
            if (!string.IsNullOrEmpty(request.manager))
            {
                sql.Append(" manager =");
                sql.Append(" @manager,");
            }
            if (request.introductionImage > 0)
            {
                sql.Append(" introductionImage =");
                sql.Append(" @introductionImage,");
            }
            sql.Append(" modifiedTime =");
            sql.Append(" @modifiedTime,");
            sql.Append(" deleted =");
            sql.Append(" @deleted,");
            sql.Append(" modifiedBy =");
            sql.Append(" @modifiedBy ");
            sql.Append(" WHERE id = @id ;");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("categoryName", request.categoryName);
                    cmd.Parameters.AddWithValue("introductionImage", request.introductionImage);
                    cmd.Parameters.AddWithValue("manager", request.manager);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("deleted", request.deleted);
                    cmd.Parameters.AddWithValue("modifiedBy", request.modifiedBy);
                    cmd.Parameters.AddWithValue("id", request.id);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }

            return true;
        }

        public bool updateManageForCategory(List<int> categoryIDs, string modifiedBy, string manager)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append("category");
            sql.Append("    SET ");
            if(manager.Equals("null")) { 
                sql.Append(" manager = NULL ,");
            } else
            {
                sql.Append(" manager =");
                sql.Append(" @manager,");
            }
            sql.Append(" modifiedTime =");
            sql.Append(" @modifiedTime,");
            sql.Append(" modifiedBy =");
            sql.Append(" @modifiedBy ");
            sql.Append(" WHERE id IN (" + String.Join(",", categoryIDs) + ")");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", modifiedBy);
                    cmd.Parameters.AddWithValue("manager", manager);
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

        public bool DeleteById(int categoryID)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE");
            sql.Append("    FROM category");
            sql.Append("    WHERE id = @id");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("id", categoryID);
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

        public List<CategoryResponse> getAll()
        {
            List<CategoryResponse> categoryResponses = new List<CategoryResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" category.id ");
            sql.Append(", category.categoryName ");
            sql.Append(", category.createdTime ");
            sql.Append(", category.modifiedTime ");
            sql.Append(", category.deleted ");
            sql.Append(", category.modifiedBy ");
            sql.Append(", category.createdBy ");
            sql.Append(", COUNT(article.id) as totalArticles ");

            //sql.Append(", category.categoryCode ");
            sql.Append(" FROM category LEFT JOIN article ON category.id = article.categoryID");
            sql.Append(" WHERE  category.deleted = FALSE ");
            sql.Append(" GROUP BY category.id ORDER BY CHAR_LENGTH(category.categoryName) ASC");

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
                                CategoryResponse categoryResponse = new CategoryResponse
                                {
                                    id = reader.GetInt32(0),
                                    categoryName = reader.GetString(1),
                                    createdTime = reader.GetDateTime(2),
                                    modifiedTime = reader.GetDateTime(3),
                                    deleted = reader.GetInt32(4),
                                    modifiedBy = reader.GetString(5),
                                    createdBy = reader.GetString(6),
                                    //categoryCode = reader.GetString(7),
                                };

                                categoryResponses.Add(categoryResponse);
                            }
                        }
                    }
                    con.Close();
                }
            }
            return categoryResponses;
        }
        public bool checkExistCategoryName(string categoryName)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" category.id ");
            sql.Append(" FROM category ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(categoryName))
            {
                sql.Append("    AND category.categoryName = '" + @categoryName + "'");

            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();

                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@categoryName", categoryName);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return true;
                            }
                        }
                    }
                }
                con.Close();
            }
            return false;
        }

        public int countBySearch(string categoryName)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(DISTINCT category.id) ");
            sql.Append(" FROM category ");
            sql.Append(" WHERE 1 = 1 ");
            if (!string.IsNullOrEmpty(categoryName))
            {
                sql.Append("    AND category.categoryName LIKE '%" + @categoryName + "%'");
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();

                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@categoryName", categoryName);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }
        public CategoryResponse getCategoryById(int id)
        {
            CategoryResponse categoryResponse = null;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" category.id ");
            sql.Append(", category.categoryName ");
            sql.Append(", category.createdTime ");
            sql.Append(", category.modifiedTime ");
            sql.Append(", category.deleted ");
            sql.Append(", category.modifiedBy ");
            sql.Append(", category.createdBy ");
            //sql.Append(", category.categoryCode ");
            sql.Append(",category.introductionImage ");
            sql.Append(" FROM category ");
            sql.Append(" WHERE category.id = @id");


            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();

                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("id", id);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                categoryResponse = new CategoryResponse { };
                                if (!reader.IsDBNull(0))
                                {
                                    categoryResponse.id = reader.GetInt32(0);
                                }
                                if (!reader.IsDBNull(1))
                                {
                                    categoryResponse.categoryName = reader.GetString(1);
                                }
                                if (!reader.IsDBNull(2))
                                {
                                    categoryResponse.createdTime = reader.GetDateTime(2);
                                }
                                if (!reader.IsDBNull(3))
                                {
                                    categoryResponse.modifiedTime = reader.GetDateTime(3);
                                }
                                if (!reader.IsDBNull(4))
                                {
                                    categoryResponse.deleted = reader.GetInt32(4);
                                }
                                if (!reader.IsDBNull(5))
                                {
                                    categoryResponse.modifiedBy = reader.GetString(5);
                                }
                                if (!reader.IsDBNull(6))
                                {
                                    categoryResponse.createdBy = reader.GetString(6);
                                }
                         
                                //if (!reader.IsDBNull(7))
                                //{
                                //    categoryResponse.categoryCode = reader.GetString(7);
                                //}
                                if (!reader.IsDBNull(7))
                                {
                                    categoryResponse.introductionImage = reader.GetInt32(7);
                                }

                            }
                        }
                    }
                }
                con.Close();
            }
            return categoryResponse;
        }

        public List<CategoryResponse> searchCategoryToAddManager(string categoryName, int pageSize, int pageIndex)
        {
            var startIndex = (pageIndex - 1) * pageSize;
            List<CategoryResponse> categoryResponses = new List<CategoryResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" cat.id ");
            sql.Append(", cat.categoryName ");
            sql.Append(", cat.createdTime ");
            sql.Append(", cat.modifiedTime ");
            sql.Append(", cat.deleted ");
            sql.Append(", cat.modifiedBy ");
            sql.Append(", cat.createdBy ");
            //sql.Append(", cat.categoryCode ");
            sql.Append(", COUNT(art.id) ");
            sql.Append(" FROM category cat ");
            sql.Append(" LEFT JOIN article art ON art.categoryID = cat.id  ");
            sql.Append(" WHERE 1 = 1 ");
            sql.Append("    AND cat.deleted = false ");
            if (!string.IsNullOrEmpty(categoryName))
            {
                sql.Append("    AND cat.categoryName LIKE '%" + @categoryName + "%'");

            }
            sql.Append("    AND (cat.manager = '' OR cat.manager IS NULL)");
            sql.Append(" GROUP BY cat.id ");
            sql.Append(" ORDER BY cat.modifiedTime DESC ");
            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@categoryName", categoryName);
                    cmd.Parameters.AddWithValue("@startIndex", startIndex);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                CategoryResponse categoryResponse = new CategoryResponse
                                {
                                    id = reader.GetInt32(0),
                                    categoryName = reader.GetString(1),
                                    createdTime = reader.GetDateTime(2),
                                    modifiedTime = reader.GetDateTime(3),
                                    deleted = reader.GetInt32(4),
                                    modifiedBy = reader.GetString(5),
                                    createdBy = reader.GetString(6),
                                    //categoryCode = reader.GetString(7),
                                    totalArticle = reader.GetInt32(7)
                                };

                                categoryResponses.Add(categoryResponse);
                            }
                        }
                    }
                    con.Close();
                }
            }
            return categoryResponses;
        }

        public int countBySearchCategoryToAddManager(string categoryName)
        {
            int total;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(*) ");
            sql.Append(" FROM category cat ");
            sql.Append(" WHERE 1 = 1 ");
            sql.Append("    AND cat.deleted = false ");
            if (!string.IsNullOrEmpty(categoryName))
            {
                sql.Append("    AND cat.categoryName LIKE '%" + @categoryName + "%'");

            }
            sql.Append("    AND (cat.manager = '' OR cat.manager IS NULL)");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@categoryName", categoryName);

                    total = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            return total;
        }

        public List<CategoryResponse> getByManager(string manager)
        {
            List<CategoryResponse> categoryResponses = new List<CategoryResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" cat.id ");
            sql.Append(", cat.categoryName ");
            sql.Append(", cat.createdTime ");
            sql.Append(", cat.modifiedTime ");
            sql.Append(", cat.deleted ");
            sql.Append(", cat.modifiedBy ");
            sql.Append(", cat.createdBy ");
            //sql.Append(", cat.categoryCode ");
            sql.Append(", COUNT(art.id) ");
            sql.Append(" FROM category cat ");
            sql.Append(" LEFT JOIN article art ON art.categoryID = cat.id  ");
            sql.Append(" WHERE 1 = 1 ");
            sql.Append("    AND cat.deleted = false ");
            sql.Append("    AND cat.manager = @manager");
            sql.Append(" GROUP BY cat.id ");
            sql.Append(" ORDER BY cat.modifiedTime DESC ");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@manager", manager);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                CategoryResponse categoryResponse = new CategoryResponse
                                {
                                    id = reader.GetInt32(0),
                                    categoryName = reader.GetString(1),
                                    createdTime = reader.GetDateTime(2),
                                    modifiedTime = reader.GetDateTime(3),
                                    deleted = reader.GetInt32(4),
                                    modifiedBy = reader.GetString(5),
                                    createdBy = reader.GetString(6),
                                    //categoryCode = reader.GetString(7),
                                    totalArticle = reader.GetInt32(7)
                                };

                                categoryResponses.Add(categoryResponse);
                            }
                        }
                    }
                    con.Close();
                }
            }
            return categoryResponses;
        }
    }
}