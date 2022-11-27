using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Repository;
using CMS_SU21_BE.Repository.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_SU21_BE.Services.Implements
{
    public class UserRoleServiceImpl : BaseService, UserRoleService
    {
        private UserRoleRepository userRoleRepository = new UserRoleRepository();

        private CategoryService categoryService = new CategoryServiceImpl();

        public bool Add(UserRoleRequest request)
        {
            string account = getLoggedInUsername();
            request.createdBy = account;

            // Check exist role of user
            if (userRoleRepository.checkExistRoleOfUser(request.Account, request.RoleCode))
            {
                throw new ArgumentException(String.Format("Account '{0}' already has {1}'s role!", request.Account, request.RoleCode));
            }

            // Add manager for category
            bool checkUpdateCategory = true;
            if (request.RoleCode.Equals("moderator") && request.categoryID != null) {
                int count = 0;
                CategoryRequest categoryRequest = new CategoryRequest();
                categoryRequest.manager = request.Account;
                categoryRequest.deleted = 0;
                for (int i = 0; i < request.categoryID.Count; i++) { 
                    categoryRequest.id = request.categoryID[i]; 
                    if(categoryService.updateCategory(categoryRequest))
                    {
                        count++;
                    }
                }
                if(count != request.categoryID.Count)
                {
                    checkUpdateCategory = false;
                }
            }

            bool checkAddUserRole = userRoleRepository.Add(request);

            return checkUpdateCategory && checkAddUserRole;
        }

        public bool deleteById(int id)
        {
            return userRoleRepository.deleteById(id);
        }

        public List<UserRoleResponse> search(UserRoleRequest request, int pageSize, int pageIndex)
        {
            List<UserRoleResponse> userRoleResponses = userRoleRepository.search(request, pageSize, pageIndex);
            if(userRoleResponses.Count > 0)
            {
                for (int i = 0; i < userRoleResponses.Count; i++)
                {
                    userRoleResponses[i].index = pageSize * (pageIndex - 1) + i + 1;
                }
            }
            return userRoleResponses;
        }

        public int totalSearchUserRole(UserRoleRequest request)
        {
            return userRoleRepository.totalSearchUserRole(request);
        }

        public List<int> findNumberInListAButNotInListB(List<int> a, List<int> b)
        {
            List<int> result = new List<int>();
            for(int i = 0; i < a.Count; i++)
            {
                if (b.IndexOf(a[i]) < 0)
                {
                    result.Add(a[i]);
                }
            }
            return result;
        } 

        public bool updateUserRoles(UserRoleRequest request)
        {
            /*if (userRoleRepository.checkExistRoleOfUser(request.Account, request.RoleCode))
            {
                throw new ArgumentException(String.Format("Account '{0}' already has '{1} role'!", request.Account, request.RoleCode));
            }*/

            request.modifiedBy = getLoggedInUsername();
            bool checkUpdateRole = userRoleRepository.updateUserRoles(request);

            if(request.RoleCode.Equals("moderator")) {
                    // find new element to add
                List<int> addNew = findNumberInListAButNotInListB(request.categoryID, request.oldCategoryID);

                //find element in old but not in new to delete
                List<int> deleteManager = findNumberInListAButNotInListB(request.oldCategoryID, request.categoryID);

                if (addNew.Count == 0 && deleteManager.Count == 0)
                {
                    return true;
                }
                bool checkDelete = false;
                bool checkAddNew = false;
                // SQL delete
                CategoryRequest categoryRequest = new CategoryRequest();
                if(deleteManager.Count > 0) { 
                    checkDelete = categoryService.updateManageForCategory(deleteManager, "null");
                } else
                {
                    checkDelete = true;
                }

                // SQL add new
                if (addNew.Count > 0) { 
                    checkAddNew = categoryService.updateManageForCategory(addNew, request.Account);
                } else
                {
                    checkAddNew = true;
                }

                return checkAddNew && checkDelete;
            }
            return false;
        }

        public bool checkExistRoleOfUser(string account, string roleCode)
        {
            return userRoleRepository.checkExistRoleOfUser(account, roleCode);
        }

        public List<UserRoleResponse> getUserRoleByAccount(string account)
        {
            return userRoleRepository.getUserRoleByAccount(account);
        }
    }
}