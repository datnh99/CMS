using CMS_SU21_BE.Common.Base;
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
    public class StatisticRepository : BaseRepository
    {
        public int add(StatisticRequest request)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("statistic");
            sql.Append("(");
            sql.Append("    action,");
            sql.Append("    object_id,");
            sql.Append("    account,");
            sql.Append("    created_by,");
            sql.Append("    created_time,");
            sql.Append("    modified_by,");
            sql.Append("    modified_time,");
            sql.Append("    deleted");
            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @action,");
            sql.Append("    @objectId,");
            sql.Append("    @account,");
            sql.Append("    @createdBy,");
            sql.Append("    @createdTime,");
            sql.Append("    @modifiedBy,");
            sql.Append("    @modifiedTime,");
            sql.Append("    @deleted");
            sql.Append(");");
            sql.Append("    SELECT LAST_INSERT_ID();");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("action", request.action);
                    cmd.Parameters.AddWithValue("objectId", request.objectId);
                    cmd.Parameters.AddWithValue("account", request.account);
                    cmd.Parameters.AddWithValue("createdBy", request.createdBy);
                    cmd.Parameters.AddWithValue("createdTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("modifiedBy", request.modifiedBy);
                    cmd.Parameters.AddWithValue("modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("deleted", request.deleted);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                con.Close();
            }
            return result;
        }

        public bool deleteById(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE");
            sql.Append("    FROM statistic");
            sql.Append("    WHERE id = @id");
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

        public List<StatisticResponse> search(string account, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<StatisticResponse> statisticResponses = new List<StatisticResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id");
            sql.Append(", action");
            sql.Append(", object_id");
            sql.Append(", account");
            sql.Append(", created_by");
            sql.Append(", created_time");
            sql.Append(", modified_by");
            sql.Append(", modified_time");
            sql.Append(", deleted");
            sql.Append(" FROM statistic ");
            sql.Append(" WHERE 1=1 AND deleted = 0 ");

            if (!string.IsNullOrEmpty(account))
            {
                sql.Append("   AND account = '" + @account + "'");
                sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");
            }


            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@account", account);
                    cmd.Parameters.AddWithValue("@startIndex", startIndex);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                StatisticResponse statisticResponse = new StatisticResponse
                                {
                                    id = reader.GetInt32(0),
                                    action = reader.GetString(1),
                                    objectId = reader.GetInt32(2),
                                    account = reader.GetString(3),
                                    createdBy = reader.GetString(4),
                                    createdTime = reader.GetDateTime(5),
                                    modifiedBy = reader.GetString(6),
                                    modifiedTime = reader.GetDateTime(7),
                                    deleted = reader.GetBoolean(8)
                                };

                                statisticResponses.Add(statisticResponse);
                            }
                        }
                    }
                }
            }
            return statisticResponses;
        }
        public List<int> getListNearestArticlesId(string lstNotIn,int limit)
        {
            string username = getLoggedInUsername();
            List<int> statisticResponses = new List<int>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.* FROM ( ");
            sql.Append("SELECT ");
            sql.Append("DISTINCT(st.object_id) as object_id,GROUP_CONCAT(st.account) as account1,SUM(article.totalViews) as score");
            sql.Append(" FROM statistic st ");
            sql.Append(" JOIN article ON st.object_id = article.id ");
            sql.Append(" JOIN master_data ON article.status = master_data.id ");
            sql.Append(" JOIN category ON category.id = article.categoryID ");
            sql.Append(" WHERE  st.action = 'view_article' ");
            sql.Append(" AND st.created_time  >= now() - interval 3 day ");
            sql.Append(" AND article.clone = 0 AND  article.deleted = 0 AND master_data.lower_code = 'posted' AND  category.deleted = 0 GROUP BY article.id) a WHERE 1=1");
            if (!string.IsNullOrEmpty(lstNotIn))
            {
                sql.Append("  AND a.object_id NOT IN  (" + lstNotIn + ")");
            }
            if (!string.IsNullOrEmpty(username))
            {
                sql.Append("  AND a.account1 NOT LIKE'%" + username + "%'");
            }
            sql.Append(" ORDER BY a.score DESC  ");
            sql.Append(" LIMIT 0,@limit ");
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
                                if (!reader.IsDBNull(0))
                                {
                                    statisticResponses.Add(reader.GetInt32(0));
                                }
                            }
                        }
                    }
                }
            }
            return statisticResponses;
        }

        public bool update(StatisticRequest request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            sql.Append("statistic");
            sql.Append("    SET ");
            sql.Append(" action=");
            sql.Append(" @action,");
            sql.Append(" object_id=");
            sql.Append(" @objectId,");
            sql.Append(" account=");
            sql.Append(" @account,");
            sql.Append(" created_by=");
            sql.Append(" @createdBy,");
            sql.Append(" created_time=");
            sql.Append(" @createdTime,");
            sql.Append(" modified_by=");
            sql.Append(" @modifiedBy,");
            sql.Append(" modified_time=");
            sql.Append(" @modifiedTime,");
            sql.Append(" deleted=");
            sql.Append(" @deleted");
            sql.Append(" WHERE id = @id");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("action", request.action);
                    cmd.Parameters.AddWithValue("objectId", request.objectId);
                    cmd.Parameters.AddWithValue("account", request.account);
                    cmd.Parameters.AddWithValue("createdBy", request.createdBy);
                    cmd.Parameters.AddWithValue("createdTime", request.createdTime);
                    cmd.Parameters.AddWithValue("modifiedBy", request.modifiedBy);
                    cmd.Parameters.AddWithValue("modifiedTime", request.modifiedTime);
                    cmd.Parameters.AddWithValue("deleted", request.deleted);
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

        public List<StatisticResponse> getByAccountAndObjectID(string account, int objectID)
        {
            List<StatisticResponse> statisticResponses = new List<StatisticResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id");
            sql.Append(", action");
            sql.Append(", object_id");
            sql.Append(", account");
            sql.Append(", created_by");
            sql.Append(", created_time");
            sql.Append(", modified_by");
            sql.Append(", modified_time");
            sql.Append(", deleted");
            sql.Append(" FROM statistic ");
            sql.Append(" WHERE 1=1 AND deleted = 0 ");
            sql.Append("   AND account = '" + @account + "'");
            sql.Append("   AND object_id = " + @objectID + "");


            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                StatisticResponse statisticResponse = new StatisticResponse
                                {
                                    id = reader.GetInt32(0),
                                    action = reader.GetString(1),
                                    objectId = reader.GetInt32(2),
                                    account = reader.GetString(3),
                                    createdBy = reader.GetString(4),
                                    createdTime = reader.GetDateTime(5),
                                    modifiedBy = reader.GetString(6),
                                    modifiedTime = reader.GetDateTime(7),
                                    deleted = reader.GetBoolean(8)
                                };
                                statisticResponses.Add(statisticResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return statisticResponses;
        }

        public List<StatisticResponse> getByAccountAndObjectIDAndAction(string account, int objectID, string action)
        {
            List<StatisticResponse> statisticResponses = new List<StatisticResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id");
            sql.Append(", action");
            sql.Append(", object_id");
            sql.Append(", account");
            sql.Append(", created_by");
            sql.Append(", created_time");
            sql.Append(", modified_by");
            sql.Append(", modified_time");
            sql.Append(", deleted");
            sql.Append(" FROM statistic ");
            sql.Append(" WHERE 1=1 AND deleted = 0 ");
            sql.Append("   AND account = '" + @account + "'");
            sql.Append("   AND object_id = " + @objectID + "");
            sql.Append("   AND action = '" + @action + "'");


            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                StatisticResponse statisticResponse = new StatisticResponse
                                {
                                    id = reader.GetInt32(0),
                                    action = reader.GetString(1),
                                    objectId = reader.GetInt32(2),
                                    account = reader.GetString(3),
                                    createdBy = reader.GetString(4),
                                    createdTime = reader.GetDateTime(5),
                                    modifiedBy = reader.GetString(6),
                                    modifiedTime = reader.GetDateTime(7),
                                    deleted = reader.GetBoolean(8)
                                };
                                statisticResponses.Add(statisticResponse);
                            }
                        }
                    }
                }
                con.Close();
            }
            return statisticResponses;
        }
    }
}