import axios from 'axios';
import config from '../config/index';
const API_USER_ROLE = `${config.apiUrl}/user-role`
const pageSize = 10;

function addUserRole (formUserRole) {
    return axios.post(`${API_USER_ROLE}/addUserRole/{add-user-role}`,formUserRole)
}

function updateRoleOfUser (updateForm) {
    return axios.post(`${API_USER_ROLE}/updateRoles/{update-roles}`, updateForm)
}

function searchUser (formSearch, pageIndex) {
    return axios.post(`${API_USER_ROLE}/search/{search}?pageSize=${pageSize}&pageIndex=${pageIndex}`,formSearch)
}

function deleteUserRole (formDelete) {
    return axios.post(`${API_USER_ROLE}/deleteUserRole/{delete-user-role}`, formDelete)
}

export default{
    addUserRole,
    updateRoleOfUser,
    searchUser,
    deleteUserRole
}
