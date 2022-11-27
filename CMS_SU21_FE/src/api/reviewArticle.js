import axios from 'axios';
import config from '../config/index';
const API_REVIEW_ARTICLE = `${config.apiUrl}/review-article`
const pageSize = 10;

function searchReviewArticle(reviewArticleRequest, pageIndex) {
    return axios.post(`${API_REVIEW_ARTICLE}/searchReviewArticle/{search-review-article}?pageSize=`+pageSize+`&pageIndex=`+pageIndex,reviewArticleRequest)
}

function getArticleReviewed(articleID) {
    return axios.get(`${API_REVIEW_ARTICLE}/getArticleReviewed/{get-article-reviewed}?articleID=${articleID}`)
}

function addReviewArticle(formAdd) {
    return axios.post(`${API_REVIEW_ARTICLE}/addReviewArticle/{add-review-article}`, formAdd)
}

function reviewArticle (formArticle) {
    return axios.post(`${API_REVIEW_ARTICLE}/reviewArticleByReviewer/{review-article-by-reviewer}`,formArticle)
}
export default{
    searchReviewArticle,
    addReviewArticle,
    reviewArticle,
    getArticleReviewed
}
