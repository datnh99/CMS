import moment from "moment"
export function generateTime(dateTime){
    let date = moment(dateTime)
    let currentDate = moment()
    if(currentDate.subtract(2, 'days') > date){
        return date.format("lll")
    }
    return capitalize(moment(dateTime).fromNow()) 
}
function capitalize(s)
{
    return s[0].toUpperCase() + s.slice(1);
}
