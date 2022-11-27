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
    public class CategoryServiceImpl : BaseService,CategoryService
    {


        private CategoryRepository categoryRepository = new CategoryRepository();

        private ArticleService articleService = new ArticleServiceImpl();


        public int Add(CategoryRequest request)
        {
            request.createdBy = getLoggedInUsername();
            Boolean checkExistName = categoryRepository.checkExistCategoryName(request.categoryName);
            if (checkExistName == true) {
                throw new ArgumentException(String.Format("Category Name '{0}' is already exist!", request.categoryName));
            }
            //Boolean checkExistCode = categoryRepository.checkExistCategoryCode(request.categoryCode);
            //if (checkExistCode == true)
            //{
            //    throw new ArgumentException(String.Format("Category Code '{0}' is already exist!", request.categoryCode));
            //}
            return categoryRepository.Add(request);
        }

        public int countBySearch(string categoryName)
        {
            return categoryRepository.countBySearch(categoryName);
        }

        public bool DeleteById(int categoryID)
        {
            List<ArticleResponse> articleResponses = articleService.Search(null, null, categoryID, null, 10, 1);
            if (articleResponses == null || articleResponses.Count == 0)
            {
                return categoryRepository.DeleteById(categoryID);
            }
            CategoryRequest request = new CategoryRequest();
            request.deleted = 1;
            request.modifiedBy = getLoggedInUsername();
            return categoryRepository.updateCategory(request);
        }

        public List<CategoryResponse> getAll()
        {
            return categoryRepository.getAll();
        }

        public List<CategoryResponse> Search(string categoryName, int? pageSize, int? pageIndex)
        {
            List<CategoryResponse> responses = categoryRepository.Search(categoryName, pageSize, pageIndex);
            if (responses != null && responses.Count > 0)
            {
                for (int i = 0; i < responses.Count; i++)
                {
                    responses[i].index = (pageIndex.Value - 1) * pageSize.Value + i + 1;
                }
            }
            return responses;
        }

        public List<CategoryResponse> SearchByNameAndManager(string categoryName, string account, int? pageSize, int? pageIndex)
        { 
            return categoryRepository.SearchByNameAndManager(categoryName, account, pageSize, pageIndex);
        }

        public int countByNameAndManager(string categoryName, string account)
        { 
            return categoryRepository.countByNameAndManager(categoryName, account);
        }

        public int totalCategory()
        {
            return categoryRepository.totalCategory();
        }

        public bool updateCategory(CategoryRequest request)
        {
            String username = getLoggedInUsername();
            request.modifiedBy = username;
            return categoryRepository.updateCategory(request);
        }

        public CategoryResponse getCategoryById(int id)
        {
            return categoryRepository.getCategoryById(id);
        }

        public List<CategoryResponse> searchCategoryToAddManager(string categoryName, int pageSize, int pageIndex)
        {
            return categoryRepository.searchCategoryToAddManager(categoryName, pageSize, pageIndex);
        }

        public int countBySearchCategoryToAddManager(string categoryName)
        {
            return categoryRepository.countBySearchCategoryToAddManager(categoryName);
        }

        public List<CategoryResponse> getByManager(string manager)
        {
            return categoryRepository.getByManager(manager);
        }

        public bool updateManageForCategory(List<int> categoryIDs, string manager)
        {
            string username = getLoggedInUsername();
            return categoryRepository.updateManageForCategory(categoryIDs, username, manager);
        }
    }
}