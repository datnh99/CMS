import axios from 'axios';
import config from '../config/index';
const API_MASTER_DATA = `${config.apiUrl}/master-data`
const pageSize = 10;

function search(masterDataName, pageIndex) {
    return axios.get(`${API_MASTER_DATA}/search/{search}?masterDataName=${masterDataName}&pageSize=`+pageSize+`&pageIndex=`+pageIndex)
}

function searchByCodeType(lowerCode, type, pageIndex) {
    return axios.get(`${API_MASTER_DATA}/getByTypeAndCode/{get-by-type-and-code}?lowerCode=${lowerCode}&type=${type}`)
}

function searchByType(type, pageIndex) {
    return axios.get(`${API_MASTER_DATA}/getByType/{get-by-type}?type=${type}&pageSize=`+pageSize+`&pageIndex=`+pageIndex)
}

function searchAllByType(type) {
    return axios.get(`${API_MASTER_DATA}/getAllByType/{get-all-by-type}?type=${type}`)
}

function add(request) {
    return axios.post(`${API_MASTER_DATA}/add/{add}`,request)
}

function deleteMasterData(id) { 
    return axios.post(`${API_MASTER_DATA}/delete/{delete}?id=`+id)
}

export default{
    search,
    searchByCodeType,
    searchByType,
    add,
    deleteMasterData,
    searchAllByType
}
