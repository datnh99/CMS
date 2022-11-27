import axios from 'axios';
import config from '../config/index';
const API_HASH_TAG = `${config.apiUrl}/hashtag`


function addHastag (formHashtag) {
    return axios.post(`${API_HASH_TAG}/Add/{add}`,formHashtag)
}

function getByNewId (newID) {
    return axios.get(`${API_HASH_TAG}/getByNewId/{get-by-new-id}?newID=${newID}`)
}
function getPopularTags () {
    return axios.get(`${API_HASH_TAG}/getPopularTags/{get-popular-tags}`)
}
export default{
    addHastag,
    getByNewId,
    getPopularTags
}
