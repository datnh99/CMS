import axios from 'axios';
import config from '../config/index';
const API_COMMENT = `${config.apiUrl}/comment`
const pageSize = 10;

function addComment(form) {
    return axios.post(`${API_COMMENT}/addComment/{add-comment}`,form)
}
function getCommentByArticleId(id,current) {
    return axios.get(`${API_COMMENT}/getCommentByArticleID/{get-comment-by-article-id}?id=${id}&pageIndex=${current}&pageSize=${pageSize}`)
}
function deleteCommentByCommentID(id) {
    return axios.post(`${API_COMMENT}/deleteCommentByCommentID/{delete-comment-by-commentid}?id=${id}`)
}
function updateCommentByCommentID(form) {
    return axios.post(`${API_COMMENT}/updateCommentByCommentID/{update-comment-by-commentid}`,form)
} 
export default{
    addComment,
    getCommentByArticleId,
    deleteCommentByCommentID,
    updateCommentByCommentID
}
