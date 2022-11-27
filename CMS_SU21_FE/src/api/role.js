import axios from 'axios';
import config from '../config/index';
const API_ROLE = `${config.apiUrl}/role`


function getAllRole () {
    return axios.get(`${API_ROLE}/getRoles/{get-roles}`)
}
function saveLoggedInUserRole (role) {
    return axios.post(`${API_ROLE}/saveLoggedInUserRole/{save-logged-in-user-role}?role=${role}`)
}

export default{
    getAllRole,
    saveLoggedInUserRole
}
