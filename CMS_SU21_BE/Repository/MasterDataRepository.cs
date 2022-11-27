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
    public class MasterDataRepository
    {
        public List<MasterDataResponse> Search(string masterDataName, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<MasterDataResponse> masterDataResponses = new List<MasterDataResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id ");
            sql.Append(", code ");
            sql.Append(", name ");
            sql.Append(", lower_code ");
            sql.Append(", lower_name ");
            sql.Append(", created_by ");
            sql.Append(", created_time ");
            sql.Append(", modified_by ");
            sql.Append(", modified_time ");
            sql.Append(", type ");
            sql.Append(", act_flg ");
            sql.Append(" FROM master_data ");

            if (!string.IsNullOrEmpty(masterDataName))
            {
                sql.Append(" WHERE 1 = 1 ");
                sql.Append("    AND name LIKE '%" + @masterDataName + "%'");
                sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");
            }

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@masterDataName", masterDataName);
                    cmd.Parameters.AddWithValue("@startIndex", startIndex);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MasterDataResponse masterDataResponse = new MasterDataResponse
                                {
                                    id = reader.GetInt32(0),
                                    code = reader.GetString(1),
                                    name = reader.GetString(2),
                                    lowerCode = reader.GetString(3),
                                    lowerName = reader.GetString(4),
                                    createdBy = reader.GetString(5),
                                    createdTime = reader.GetDateTime(6),
                                    modifiedBy = reader.GetString(7),
                                    modifiedTime = reader.GetDateTime(8),
                                    type = reader.GetString(9),
                                    actFlg = reader.GetBoolean(10)
                                };
                                masterDataResponses.Add(masterDataResponse);
                            }
                        }
                    }
                }
            }

            return masterDataResponses;
        }

        public MasterDataResponse getByTypeAndCode(string lowerCode, string type)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id ");
            sql.Append(", code ");
            sql.Append(", name ");
            sql.Append(", lower_code ");
            sql.Append(", lower_name ");
            sql.Append(", created_by ");
            sql.Append(", created_time ");
            sql.Append(", modified_by ");
            sql.Append(", modified_time ");
            sql.Append(", type ");
            sql.Append(", act_flg ");
            sql.Append(" FROM master_data ");

            if (!string.IsNullOrEmpty(lowerCode) && !string.IsNullOrEmpty(type))
            {
                sql.Append(" WHERE 1 = 1 ");
                sql.Append("    AND type = '" + @type + "'");
                sql.Append("    AND lower_code = '" + @lowerCode + "'");
            }
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@lowerCode", lowerCode);
                    cmd.Parameters.AddWithValue("@type", type);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MasterDataResponse masterDataResponse = new MasterDataResponse
                                {
                                    id = reader.GetInt32(0),
                                    code = reader.GetString(1),
                                    name = reader.GetString(2),
                                    lowerCode = reader.GetString(3),
                                    lowerName = reader.GetString(4),
                                    createdBy = reader.GetString(5),
                                    createdTime = reader.GetDateTime(6),
                                    modifiedBy = reader.GetString(7),
                                    modifiedTime = reader.GetDateTime(8),
                                    type = reader.GetString(9),
                                    actFlg = reader.GetBoolean(10)
                                };
                                return masterDataResponse;
                            }
                        }
                    }
                }
            }

            return null;
        }
        public List<MasterDataResponse> getByType(string type, int? pageSize, int? pageIndex)
        {
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;
            List<MasterDataResponse> masterDataResponses = new List<MasterDataResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id ");
            sql.Append(", code ");
            sql.Append(", name ");
            sql.Append(", lower_code ");
            sql.Append(", lower_name ");
            sql.Append(", created_by ");
            sql.Append(", created_time ");
            sql.Append(", modified_by ");
            sql.Append(", modified_time ");
            sql.Append(", type ");
            sql.Append(", act_flg ");
            sql.Append(" FROM master_data ");
            sql.Append(" WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(type))
            {
                sql.Append("    AND type = '" + @type + "'");
            }

            sql.Append("    LIMIT " + @startIndex + "," + @pageSize + "");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@startIndex", startIndex);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MasterDataResponse masterDataResponse = new MasterDataResponse
                                {
                                    id = reader.GetInt32(0),
                                    code = reader.GetString(1),
                                    name = reader.GetString(2),
                                    lowerCode = reader.GetString(3),
                                    lowerName = reader.GetString(4),
                                    createdBy = reader.GetString(5),
                                    createdTime = reader.GetDateTime(6),
                                    modifiedBy = reader.GetString(7),
                                    modifiedTime = reader.GetDateTime(8),
                                    type = reader.GetString(9),
                                    actFlg = reader.GetBoolean(10)
                                };
                                masterDataResponses.Add(masterDataResponse);
                            }
                        }
                    }
                }
            }
            return masterDataResponses;
        }

        public bool Add(MasterDataRequest request)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            sql.Append("master_data");
            sql.Append("(");
            sql.Append("    code,");
            sql.Append("    name,");
            sql.Append("    lower_code,");
            sql.Append("    lower_name,");
            sql.Append("    created_by,");
            sql.Append("    created_time,");
            sql.Append("    modified_by,");
            sql.Append("    modified_time,");
            sql.Append("    type,");
            sql.Append("    act_flg");
            sql.Append(")");
            sql.Append("VALUES");
            sql.Append("(");
            sql.Append("    @code,");
            sql.Append("    @name,");
            sql.Append("    @lowerCode,");
            sql.Append("    @lowerName,");
            sql.Append("    @createdBy,");
            sql.Append("    @createdTime,");
            sql.Append("    @modifiedBy,");
            sql.Append("    @modifiedTime,");
            sql.Append("    @type,");
            sql.Append("    @actFlg");
            sql.Append(")");

            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@code", request.code);
                    cmd.Parameters.AddWithValue("@name", request.name);
                    cmd.Parameters.AddWithValue("@lowerCode", request.lowerCode);
                    cmd.Parameters.AddWithValue("@lowerName", request.lowerName);
                    cmd.Parameters.AddWithValue("@createdBy", request.createdBy);
                    cmd.Parameters.AddWithValue("@createdTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@modifiedBy", request.modifiedBy);
                    cmd.Parameters.AddWithValue("@modifiedTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@type", request.type);
                    cmd.Parameters.AddWithValue("@actFlg", request.actFlg);

                    var result = cmd.ExecuteNonQuery();
                    if (result != 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool DeleteById(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE");
            sql.Append("    FROM master_data");
            sql.Append("    WHERE id = @id");
            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);
                    var result = cmd.ExecuteNonQuery();
                    if (result != 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<MasterDataResponse> getByTypeAndListCode(string type, List<string> listCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id ");
            sql.Append(", code ");
            sql.Append(", name ");
            sql.Append(", lower_code ");
            sql.Append(", lower_name ");
            sql.Append(", created_by ");
            sql.Append(", created_time ");
            sql.Append(", modified_by ");
            sql.Append(", modified_time ");
            sql.Append(", type ");
            sql.Append(", act_flg ");
            sql.Append(" FROM master_data ");

            if (listCode != null && listCode.Count > 0 && !string.IsNullOrEmpty(type))
            {
                sql.Append(" WHERE 1 = 1 ");
                sql.Append("    AND type = '" + @type + "'");
                sql.Append("    AND lower_code IN ('" + string.Join("','", listCode) + "')");
            }
            using (MySqlConnection con = WebApiConfig.conn())
            {
                List<MasterDataResponse> result = new List<MasterDataResponse>();
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@type", type);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MasterDataResponse masterDataResponse = new MasterDataResponse
                                {
                                    id = reader.GetInt32(0),
                                    code = reader.GetString(1),
                                    name = reader.GetString(2),
                                    lowerCode = reader.GetString(3),
                                    lowerName = reader.GetString(4),
                                    createdBy = reader.GetString(5),
                                    createdTime = reader.GetDateTime(6),
                                    modifiedBy = reader.GetString(7),
                                    modifiedTime = reader.GetDateTime(8),
                                    type = reader.GetString(9),
                                    actFlg = reader.GetBoolean(10)
                                };
                                result.Add(masterDataResponse);
                            }
                        }
                        con.Close();
                    }
                }
                return result;
            }
        }

        public List<MasterDataResponse> getAllByType(string type)
        {
            List<MasterDataResponse> masterDataResponses = new List<MasterDataResponse>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");
            sql.Append(" id ");
            sql.Append(", code ");
            sql.Append(", name ");
            sql.Append(", lower_code ");
            sql.Append(", lower_name ");
            sql.Append(", created_by ");
            sql.Append(", created_time ");
            sql.Append(", modified_by ");
            sql.Append(", modified_time ");
            sql.Append(", type ");
            sql.Append(", act_flg ");
            sql.Append(" FROM master_data ");
            sql.Append(" WHERE 1 = 1 AND act_flg = true ");

            if (!string.IsNullOrEmpty(type))
            {
                sql.Append("    AND type = '" + @type + "'");
            }


            using (MySqlConnection con = WebApiConfig.conn())
            {
                con.Open();
                string sqlCommand = sql.ToString();
                using (MySqlCommand cmd = new MySqlCommand(sqlCommand, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@type", type);

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                MasterDataResponse masterDataResponse = new MasterDataResponse
                                {
                                    id = reader.GetInt32(0),
                                    code = reader.GetString(1),
                                    name = reader.GetString(2),
                                    lowerCode = reader.GetString(3),
                                    lowerName = reader.GetString(4),
                                    createdBy = reader.GetString(5),
                                    createdTime = reader.GetDateTime(6),
                                    modifiedBy = reader.GetString(7),
                                    modifiedTime = reader.GetDateTime(8),
                                    type = reader.GetString(9),
                                    actFlg = reader.GetBoolean(10)
                                };
                                masterDataResponses.Add(masterDataResponse);
                            }
                        }
                    }
                }
            }
            return masterDataResponses;
        }
    }
}