/*!

=========================================================
* Vue Argon Design System - v1.1.0
=========================================================

* Product Page: https://www.creative-tim.com/product/argon-design-system
* Copyright 2019 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/argon-design-system/blob/master/LICENSE.md)

* Coded by www.creative-tim.com

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import VueCookies from 'vue-cookies'

import Argon from "./plugins/argon-kit";
import Antd from 'ant-design-vue';
import './registerServiceWorker'
import GoogleAuth from '@/config/google_oAuth.js'
import 'ant-design-vue/dist/antd.css';
import CKEditor from '@ckeditor/ckeditor5-vue2';
import "./plugins/ckeditor5-custom/ckeditor";
import VueScrollTo from 'vue-scrollto'
import VueNavigationBar from "vue-navigation-bar";
import VueClipboard from 'vue-clipboard2'

const gauthOption = {
  clientId: '985357376148-o3brqu8m5967u34sa9sakir7qegc4o4o.apps.googleusercontent.com',
  scope: 'profile email',
  prompt: 'select_account'
}
Vue.use(VueCookies)
Vue.use(VueClipboard)
Vue.component("vue-navigation-bar", VueNavigationBar);
Vue.$cookies.config('7d')
require('./config/interceptor')
// set global cookie
Vue.$cookies.set('theme','default');
Vue.$cookies.set('hover-time','1s');
Vue.use(GoogleAuth, gauthOption)
Vue.config.productionTip = false;
Vue.use(Argon);
Vue.use(Antd);
Vue.use( CKEditor );
Vue.use(VueScrollTo, {
  container: 'body',
  duration: 500,
  easing: 'ease',
  offset: 0,
  force: true,
  cancelable: true,
  onStart: false,
  onDone: false,
  onCancel: false,
  x: false,
  y: true
})
window.ClassicEditor = ClassicEditor
new Vue({
  router,
  render: h => h(App)
}).$mount("#app");
