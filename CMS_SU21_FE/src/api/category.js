import axios from 'axios';
import config from '../config/index';
const API_CATEGORY = `${config.apiUrl}/category`
const pageSize = 10;

function searchCategory (categoryName, pageIndex) {
    return axios.get(`${API_CATEGORY}/Search/{search}?categoryName=`+categoryName+`&pageSize=`+pageSize+`&pageIndex=`+pageIndex)
}
function getAllCategory () {
    return axios.get(`${API_CATEGORY}/getAllCategory/{get-all-category}`)
}
function totalCategory () {
    return axios.get(`${API_CATEGORY}/totalCategory/{total-category}`)
}
function searchCategoryAndManager (categoryName, account, pageIndex) {
    return axios.get(`${API_CATEGORY}/SearchByNameAndManager/{search-by-name-and-manager}?categoryName=`+categoryName+`&account=`+account+`&pageSize=`+pageSize+`&pageIndex=`+pageIndex)
}
function addCategory (formValue) {
    return axios.post(`${API_CATEGORY}/Add/{add}`,formValue)
}
function editCategory (editForm) {
    return axios.post(`${API_CATEGORY}/updateCategory/{update-category}`, editForm)
}
function deleteCategory (categoryID) {
    return axios.post(`${API_CATEGORY}/Delete/{delete}?categoryID=`+categoryID)
}

function getCategoryById (categoryID) {
    return axios.get(`${API_CATEGORY}/getCategoryById/{get-category-by-id}?id=`+categoryID)
}

function searchCategoryToAddManager (categoryName, pageIndex) {
    return axios.get(`${API_CATEGORY}/searchCategoryToAddManager/{search-to-add-manager}?categoryName=`+categoryName+`&pageSize=`+pageSize+`&pageIndex=`+pageIndex)
}

function getByManager (manager) {
    return axios.get(`${API_CATEGORY}/getByManager/{get-by-manager}?manager=`+manager)
}

export default{
    searchCategoryAndManager,
    searchCategory,
    addCategory,
    editCategory,
    getAllCategory,
    totalCategory,
    deleteCategory,
    getCategoryById,
    searchCategoryToAddManager,
    getByManager
}
