import axios from "axios";
import config from "../config/index";
const API_EMAIL = `${config.apiUrl}/email`;

function sendMail(shareForm) {
  return axios.post(`${API_EMAIL}/sendEmail/{send-email}`, shareForm);
}
function testEmailConnection() {
  return axios.post(`${API_EMAIL}/testEmailConnection/{tes-email-connection}`);
}
export default {
  sendMail,
  testEmailConnection,
};
