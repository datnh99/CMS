import config from '../config/index';
import Vue  from 'vue';
export function menuAccessPemission(objectTypeCode){
    if(objectTypeCode){
        const accessRole = config.DISPLAY_SCREEN_MAP[objectTypeCode]
        const userRole = Vue.$cookies.get('roleCode')
        if(accessRole.includes(userRole)){
           return true
        }
    }
    return false
} 
