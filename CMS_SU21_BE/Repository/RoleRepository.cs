using CMS_SU21_BE.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CMS_SU21_BE.Repository
{
    public class RoleRepository
    {  
        /*public IEnumerable<Role> GetAllRole()
        {
            var roles = new List<Role>();
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();
            query.CommandText = "SELECT * from role";
            conn.Open();
            MySqlDataReader fetch_query = query.ExecuteReader();
            while (fetch_query.Read())
            {
                var role = new Role();
                for (int i = 0; i < fetch_query.FieldCount; i++)
                {
                    var colName = fetch_query.GetName(i);
                    var value = fetch_query.GetValue(i);
                    var property = role.GetType().GetProperty(colName);
                    if (property != null && value != DBNull.Value)
                    {
                        property.SetValue(role, value);
                    }
                }
                roles.Add(role);
            }
            return roles;
        }*/

        public List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT roleCode, roleName FROM role ");
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
                                Role role = new Role
                                {
                                    roleCode = reader.GetString(0),
                                    roleName = reader.GetString(1),

                                };

                                roles.Add(role);
                            }
                        }
                    }
                }
                con.Close();
            }
            return roles;
        }
    }
}