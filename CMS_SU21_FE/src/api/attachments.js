import axios from 'axios';
import config from '../config/index';
const API_ATTACHMENTS = `${config.apiUrl}/attachments`


function deleteByIds(ids) {
    return axios.post(`${API_ATTACHMENTS}/deleteByIds/{delete-by-ids}`,ids)
}

export default{
    deleteByIds
}
