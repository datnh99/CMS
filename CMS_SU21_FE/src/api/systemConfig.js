import axios from "axios";
import config from "../config/index";
const API_SYSTEM_CONFIG = `${config.apiUrl}/system-config`;
const pageSize = 20;
function setEmailConfig(userInfor) {
  return axios.post(
    `${API_SYSTEM_CONFIG}/setEmailConfig/{set-email-config}?userInfor=${userInfor}`
  );
}
function getEmailConfig() {
  return axios.get(`${API_SYSTEM_CONFIG}/getEmailConfig/{get-email-config}`);
}
function addBlacklistWords(content) {
  return axios.post(
    `${API_SYSTEM_CONFIG}/addBlacklistWords/{add-blacklist-words}`,
    content
  );
}
function getAllBlacklistWords() {
  return axios.get(
    `${API_SYSTEM_CONFIG}/getAllBlacklistWords/{get-all-blacklist-words}`
  );
}
function searchBlacklistWords(content, pageIndex) {
  return axios.get(
    `${API_SYSTEM_CONFIG}/searchBlacklistWords/{search-blacklist-words}?content=${content}&pageSize=${pageSize}&pageIndex=${pageIndex}`
  );
}
function deleteBlackListWord(id) {
  return axios.post(`${API_SYSTEM_CONFIG}/delete/{delete}?id=${id}`);
}
export default {
  setEmailConfig,
  getEmailConfig,
  addBlacklistWords,
  getAllBlacklistWords,
  searchBlacklistWords,
  deleteBlackListWord,
};
