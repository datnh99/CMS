using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Models;
using CMS_SU21_BE.Repository;
using CMS_SU21_BE.Repository.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Services.Implements
{
    public class RoleServiceImpl : BaseService,RoleService
    {
        private RoleRepository roleRepository = new RoleRepository();
        public List<Role> GetRoles()
        {
            return roleRepository.GetRoles();
        }
    }
}