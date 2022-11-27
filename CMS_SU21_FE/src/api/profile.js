import axios from 'axios';
import config from '../config/index';
const API_PROFILE = `${config.apiUrl}/profile`
const pageSize = 10;

function countProfileArticle(status) {
    return axios.get(`${API_PROFILE}/countProfileArticle/{count-profile-article}?status=${status}`)
}
function countProfileComments(id) {
    return axios.get(`${API_PROFILE}/countProfileComments/{count-profile-comments}`)
}
export default{
    countProfileArticle,
    countProfileComments
}
