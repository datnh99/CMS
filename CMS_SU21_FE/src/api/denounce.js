import axios from 'axios';
import config from '../config/index';
const API_DENOUNCE = `${config.apiUrl}/denounce`
const pageSize = 10;

function addDenounceArticle (formReport) {
    return axios.post(`${API_DENOUNCE}/Add/{add}`,formReport)
}

function getByArticleId (id, pageIndex) {
    return axios.get(`${API_DENOUNCE}/getByArticleId/{get-by-article-id}?articleID=${id}&pageIndex=${pageIndex}&pageSize=${pageSize}`)
}

function checkReportByArticleID (id) {
    return axios.get(`${API_DENOUNCE}/checkReportByArticleID/{check-report-by-article-id}?articleID=${id}`)
}

export default{
    addDenounceArticle,
    getByArticleId,
    checkReportByArticleID
}
