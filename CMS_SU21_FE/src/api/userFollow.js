import axios from 'axios';
import config from '../config/index';
const API_USER_FOLLOW = `${config.apiUrl}/user-follow`

function getFollowedUser(account) {
    return axios.get(`${API_USER_FOLLOW}/getFollowedUser/{get-followed-user}?account=${account}`)
}
function follow(account) {
    return axios.post(`${API_USER_FOLLOW}/add/{add}?account=${account}`)
}
function unfollow(account) {
    return axios.post(`${API_USER_FOLLOW}/delete/{delete-by-account}?account=${account}`)
}
export default{
    getFollowedUser,
    follow,
    unfollow
}
