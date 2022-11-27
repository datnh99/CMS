using CMS_SU21_BE.Common.Helper;
using CMS_SU21_BE.Common.Constant;
using CMS_SU21_BE.Models.Requests;
using CMS_SU21_BE.Models.Responses;
using CMS_SU21_BE.Services;
using CMS_SU21_BE.Services.Implements;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Linq;

namespace CMS_SU21_BE.Controllers
{
    [RoutePrefix("api/comment")]
    public class CommentController : ApiController
    {
        private CommentService commentService = new CommentServiceImpl();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpPost]
        [Route("addComment/{add-comment}")]
        public ResponseData addComment([FromBody] CommentRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = commentService.insertComment(request);
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newID"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpGet]
        [Route("getCommentByArticleID/{get-comment-by-article-id}")]
        public ResponseData getCommentByArticleID(int? id,int pageIndex, int pageSize)
        {
            ResponseData responseData = new ResponseData();
            var mapResult = new Dictionary<string, Object>();
            try
            {
                List<CommentResponse> commentResponse = commentService.getCommentByArticleID(id,pageIndex,pageSize);
                int totals = commentService.countCommentByArticleID(id);
                if(totals > 0)
                {
                    List<int> lstId = commentResponse.Select(cm => cm.id).ToList();
                    List<CommentResponse> listChilds = commentService.getCommentInlistId(lstId);
                    mapResult.Add("children", listChilds);
                }
                mapResult.Add("items", commentResponse);
                mapResult.Add("totals", totals);
                responseData.data = mapResult;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;

            }
        }

        [DisableCors]
        [HttpPost]
        [Route("updateCommentByCommentID/{update-comment-by-commentid}")]
        public ResponseData updateCommentByCommentID([FromBody] CommentRequest request)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = commentService.updateCommentByCommentID(request);
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentID"></param>
        /// <returns></returns>
        [DisableCors]
        [HttpPost]
        [Route("deleteCommentByCommentID/{delete-comment-by-commentid}")]
        public ResponseData deleteCommentByCommentID(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                responseData.data = commentService.deleteCommentByCommentID(id);
                return responseData;
            }
            catch (Exception e)
            {
                responseData.success = false;
                responseData.message = e.StackTrace;
                return responseData;
            }
        }

    }
}
