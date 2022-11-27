import axios from 'axios';
import config from '../config/index';
const API_TEST_URL = `${config.apiUrl}/test`
const API_LOGIN_URL = `${config.apiUrl}/authenticate/GoogleLogin`


function testApi () {
    return axios.get(`${API_TEST_URL}`)
}
function testLogin () {
    return axios.get(`${API_LOGIN_URL}`)
}
export default{
    testApi,
    testLogin
}
