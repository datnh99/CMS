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
    public class CommentServiceImpl :BaseService,CommentService
    {
        private CommentRepository commentRepository = new CommentRepository();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int insertComment(CommentRequest request)
        {
            return commentRepository.insertComment(request,getLoggedInUsername());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newID"></param>
        /// <returns></returns>
        public List<CommentResponse> getCommentByArticleID(int? newID,int pageIndex,int pageSize)
        {
            return commentRepository.getCommentByArticleID(newID,pageIndex,pageSize);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool updateCommentByCommentID(CommentRequest request)
        {
            return commentRepository.updateCommentByCommentID(request,getLoggedInUsername());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        public bool deleteCommentByCommentID(int? commentID)
        {
            return commentRepository.deleteCommentByCommentID(commentID);
        }

        public int countCommentByArticleID(int? articleID)
        {
            return commentRepository.countCommentByArticleID(articleID);
        }

        public List<CommentResponse> getCommentInlistId(List<int> lstId)
        {
            return commentRepository.getCommentInlistId(lstId);
        }
    }
}