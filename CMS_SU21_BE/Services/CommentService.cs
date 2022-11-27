using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web;

namespace CMS_SU21_BE.Services
{
    public interface CommentService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        int insertComment(CommentRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newID"></param>
        /// <returns></returns>
        List<CommentResponse> getCommentByArticleID(int? newID,int pageIndex,int pageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        bool updateCommentByCommentID(CommentRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        bool deleteCommentByCommentID(int? commentID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleID"></param>
        /// <returns></returns>
        int countCommentByArticleID(int? articleID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstId"></param>
        /// <returns></returns>
        List<CommentResponse> getCommentInlistId(List<int> lstId);

    }
}