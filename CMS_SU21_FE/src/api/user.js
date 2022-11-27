import axios from 'axios';
import config from '../config/index';
const API_USER = `${config.apiUrl}/user`
const pageSize = 10;

function addUser (form) {
    return axios.post(`${API_USER}/addUser/{addUser}?`,form)
}

function searchUserByAccount (account) {
    return axios.get(`${API_USER}/searchUserByAccount/{search-user-by-account}?account=${account}`)
}

function getUserByAccount (account) {
    return axios.get(`${API_USER}/getUserByAccount/{get-user-by-account}?account=${account}`)
}

function editUserInfor (form) {
    return axios.post(`${API_USER}/editUserInfor/{edit-user-infor}?`,form)
}

export default{
    addUser,
    searchUserByAccount,
    getUserByAccount,
    editUserInfor
}
