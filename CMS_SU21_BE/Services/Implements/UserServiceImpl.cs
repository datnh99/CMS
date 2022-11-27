using CMS_SU21_BE.Common.Base;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Repository;
using CMS_SU21_BE.Repository.Implements;
using System;
using System.Collections.Generic;

namespace CMS_SU21_BE.Services.Implements
{
    public class UserServiceImpl : BaseService, UserService
    {
        private UserRepository userRepository = new UserRepository();

        private UserRoleService userRoleService = new UserRoleServiceImpl();

        private CategoryService categoryService = new CategoryServiceImpl();

        public int Add(UserRequest request)
        {

            string account = getLoggedInUsername();
            if (string.IsNullOrEmpty(account))
            {
                account = request.account;
            }
            request.createdBy = account;

            UserResponse checkExistUser = userRepository.getUserByAccount(request.account);
            int userID = 0;
            if (checkExistUser.account == null)
            {
                userID = userRepository.Add(request);
            } else
            {
                throw new ArgumentException(String.Format("Account {0} already exist!", request.account));
            }

            if(!string.IsNullOrEmpty(request.roleCode)) { 
                UserRoleRequest userRoleRequest = new UserRoleRequest();
                userRoleRequest.Account = request.account;
                userRoleRequest.categoryID = request.categoryID;
                userRoleRequest.createdBy = account;
                userRoleRequest.RoleCode = request.roleCode;

                // Check exist role of user
                if (userRoleService.checkExistRoleOfUser(userRoleRequest.Account, userRoleRequest.RoleCode))
                {
                    throw new ArgumentException(String.Format("Account '{0}' already has {1}'s role!", userRoleRequest.Account, userRoleRequest.RoleCode));
                }

                // Add manager for category
                bool checkUpdateCategory = true;
                if (userRoleRequest.RoleCode.ToLower().Equals("moderator") && userRoleRequest.categoryID != null)
                {
                    int count = 0;
                    CategoryRequest categoryRequest = new CategoryRequest();
                    categoryRequest.manager = userRoleRequest.Account;
                    categoryRequest.deleted = 0;
                    for (int i = 0; i < userRoleRequest.categoryID.Count; i++)
                    {
                        categoryRequest.id = userRoleRequest.categoryID[i];
                        if (categoryService.updateCategory(categoryRequest))
                        {
                            count++;
                        }
                    }
                    if (count != userRoleRequest.categoryID.Count)
                    {
                        checkUpdateCategory = false;
                    }
                }
                bool checkAddUserRole = userRoleService.Add(userRoleRequest);

                if(userID > 0 && checkUpdateCategory && checkAddUserRole)
                {
                    return userID;
                }

                throw new ArgumentException("Fail to add role!");
            }
            return userID;
        }

        public bool editUserInfor(UserRequest request)
        {
            request.modifiedBy = getLoggedInUsername();
            return userRepository.editUserInfor(request);
        }

        public UserResponse getUserByAccount(string account)
        {
            return userRepository.getUserByAccount(account);
        }

        public UserResponse getUserDetailByAccount(string account)
        {
            return userRepository.getUserDetailByAccount(account);
        }

        public List<UserResponse> searchUserByAccount(string account)
        {
            return userRepository.searchUserByAccount(account);
        }

    }
}