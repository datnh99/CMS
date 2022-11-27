using CMS_SU21_BE.Models;
using CMS_SU21_BE.Models.Requests;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;

namespace CMS_SU21_BE.Services
{
    public interface RoleService
    {
        List<Role> GetRoles();
        
    }
}