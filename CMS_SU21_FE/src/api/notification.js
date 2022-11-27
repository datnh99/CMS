import axios from 'axios';


function showNotificationToUser(message, self) {
  self.$notification.open({
    message: 'Notification',
    description: message,
    style: {
      marginTop: '40px',
    },
    duration: 2.5
  });
}

function showNotification(typeMessage, message, self) {
    if (typeMessage === "success") {
    self.$notification.success({
      message: "Notification",
      description:message,
      style: {
        marginTop: '40px'
      },
      duration: 2.5
    });
    } else {
    self.$notification.error({
      message: "Notification",
      description:message,
      style: {
        marginTop: '40px'
      },
      duration: 2.5
    });
    }
  }
function showErrorNotification(error, self) {
  let message = "Interal Server Error!"
  if (error.response &&
  error.response.status &&
  error.response.status == '400' &&
  error.response.data.message &&
  error.response.data.message[0]) {
    message = error.response.data.message[0].msg 
  }
  this.showNotification("error", message, self)
}

export default {
  showNotificationToUser,
  showNotification,
  showErrorNotification
}