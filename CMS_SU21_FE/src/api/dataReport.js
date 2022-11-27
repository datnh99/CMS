import axios from 'axios';
import config from '../config/index';
const API_DATA_REPORT = `${config.apiUrl}/data-report`

function dataReportCategory(form) {
    return axios.post(`${API_DATA_REPORT}/dataReportCategory/{data-report-category}`,form)
}
function getTopArticles(form) {
    return axios.post(`${API_DATA_REPORT}/getTopArticles/{get-top-articles}`,form)
}
function dataTimelineReport(form,viewType) {
    return axios.post(`${API_DATA_REPORT}/dataTimelineReport/{data-timeline-report}?viewType=${viewType}`,form)
}
function getTopWriter(form) {
    return axios.post(`${API_DATA_REPORT}/getTopWriter/{get-top-writer}`,form)
}
function exportData(form) {
    return axios.post(`${API_DATA_REPORT}/exportData/{export-data}`,form,{ responseType: "blob" })
}
export default{
    dataReportCategory,
    getTopArticles,
    dataTimelineReport,
    getTopWriter,
    exportData
}
