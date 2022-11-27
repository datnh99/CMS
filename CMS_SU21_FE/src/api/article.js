import axios from 'axios';
import config from '../config/index';
const API_ARTICLE = `${config.apiUrl}/article`
const pageSize = 10;
const pageSizeLanding = 6;


function submitArticle (formArticle,draft) {
    return axios.post(`${API_ARTICLE}/add/{add}?isDraft=${draft}`,formArticle)
}

function getById (id,status) {
    return axios.get(`${API_ARTICLE}/getArticleById/{get-article-by-id}?articleId=${id}&status=${status}`,)
}

function getOriginalArticleById (id) {
    return axios.get(`${API_ARTICLE}/getOriginalArticleById/{get-original-article-by-id}?articleId=${id}`,)
}

function searchMyArticle (submissionRequest, pageIndex) {
    return axios.post(`${API_ARTICLE}/searchRequest/{search-request}?pageSize=`+pageSize+`&pageIndex=`+pageIndex,submissionRequest)
}

function deleteMyArticle (id) {
    return axios.post(`${API_ARTICLE}/deleteById/{delete-id}?newID=`+id)
}

function hideArticle (id) {
    return axios.get(`${API_ARTICLE}/hideArticleById/{hide-article-by-id}?articleID=`+id)
}
function updateStatusPinArticle (id, categoryID, isPinned) {
    return axios.get(`${API_ARTICLE}/updateStatusPinArticle/{update-status-pin-article-by-id}?articleID=${id}&categoryID=${categoryID}&isPinned=${isPinned}`)
}
function getPinnedArticle () {
    return axios.get(`${API_ARTICLE}/getPinnedArticle/{get-pinned-article}`)
}
function editArticle (formArticle,draft) {
    return axios.post(`${API_ARTICLE}/updateArticle/{update-article}?isDraft=${draft}`,formArticle)
}
function editArticleByReviewer (formArticle) {
    return axios.post(`${API_ARTICLE}/updateArticleByReviewer/{update-article-by-reviewer}`,formArticle)
}
function fullTextSearchArticle (inputSearch, pageIndex,pageSize) {
    return axios.post(`${API_ARTICLE}/fullTextSearch/{full-text-search}?inputSearch=${inputSearch}&pageSize=`+pageSize+`&pageIndex=`+pageIndex)
}
function searchLandingArticles (submissionRequest, pageIndex,pageSize) {
    return axios.post(`${API_ARTICLE}/searchLandingArticles/{search-landing-article}?pageSize=`+pageSize+`&pageIndex=`+pageIndex,submissionRequest)
}
function getRelatedArticlesByListId (ids) {
    return axios.post(`${API_ARTICLE}/getRelatedArticlesByListId/{get-related-article-by-list-id}`,ids)
}
function getTopArticles (submissionRequest) {
    return axios.post(`${API_ARTICLE}/getTopArticles/{get-top-articles}`,submissionRequest)
}

function getByIdForViewer (id) {
    return axios.get(`${API_ARTICLE}/getByIdForViewer/{get-by-id-for-viewer}?articleId=${id}`)
}
function updateStatusArticle (id, status, modifiedBy) {
    return axios.get(`${API_ARTICLE}/updateStatusArticle/{update-status-article}?articleId=${id}&status=${status}&modifiedBy=${modifiedBy}`)
}
function updateStatusArticleByStatusID (id, status, modifiedBy) {
    return axios.get(`${API_ARTICLE}/updateStatusArticleByStatusID/{update-status-article-by-status-id}?articleId=${id}&status=${status}&modifiedBy=${modifiedBy}`)
}
function getArticleByPopularTags (hashtag) {
    return axios.post(`${API_ARTICLE}/getArticleByPolularTags/{get-article-by-popular-tags}`,hashtag)
}
function getNewestArticleOfCategory () {
    return axios.get(`${API_ARTICLE}/getNewestArticleOfCategory/{get-newest-article-of-category}`)
}
function getPinnedArticleByCategory (categoryID) {
    return axios.get(`${API_ARTICLE}/getPinnedArticleByCategory/{get-pinned-article-by-category}?categoryID=${categoryID}`)
}
function getAuthorInforByArticleID (articleID) {
    return axios.get(`${API_ARTICLE}/getAuthorInforByArticleID/{get-author-infor-by-article-id}?articleID=${articleID}`)
}
function searchRelatedArticle (title) {
    return axios.get(`${API_ARTICLE}/searchRelatedArticle/{search-related-article}?title=${title}`)
}

function getArticleOptimizeByOldId (id) {
    return axios.get(`${API_ARTICLE}/getArticleOptimizeByOldId/{get-article-optimize-by-old-id}?articleId=${id}`,)
}

function searchArticleManagement (submissionRequest, pageIndex) {
    return axios.post(`${API_ARTICLE}/searchArticleManagement/{search-article-management}?pageSize=`+pageSize+`&pageIndex=`+pageIndex,submissionRequest)
}

function unhideArticle (id) {
    return axios.get(`${API_ARTICLE}/unhideArticleById/{unhide-article-by-id}?articleID=`+id)
}

function updateStatusEditting (id, modifiedBy) {
    return axios.get(`${API_ARTICLE}/updateStatusEditting/{update-status-editting}?articleID=${id}&modifiedBy=${modifiedBy}`)
}

export default{
    submitArticle,
    getById,
    searchMyArticle,
    searchArticleManagement,
    deleteMyArticle,
    editArticle,
    editArticleByReviewer,
    fullTextSearchArticle,
    searchLandingArticles,
    getRelatedArticlesByListId,
    hideArticle,
    getOriginalArticleById,
    getTopArticles,
    getByIdForViewer,
    getArticleByPopularTags,
    updateStatusPinArticle,
    getPinnedArticle,
    updateStatusArticle,
    getNewestArticleOfCategory,
    getPinnedArticleByCategory,
    getAuthorInforByArticleID,
    searchRelatedArticle,
    getArticleOptimizeByOldId,
    unhideArticle,
    updateStatusEditting,
    updateStatusArticleByStatusID
}
